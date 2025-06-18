using System.Reflection;

namespace Validations.Net;

/// <summary>
///     A static utility class for registering, managing, and retrieving predicate methods at runtime.
///     This class works in conjunction with the <see cref="PredicateRegistrationAttribute" /> to provide
///     a mechanism for dynamically accessing predicate methods across the application.
/// </summary>
public static class PredicateRegistrar
{
    private record CachedPredicate(string name, string group, MethodInfo methodInfo, string type, Type declaringType)
    {
        public string Name { get; } = name;

        public string Group { get; } = group;

        public string Type { get; } = type;

        public string Key { get; } = GetKey(name, group);

        public MethodInfo MethodInfo { get; } = methodInfo;

        public Type DeclaringType { get; } = declaringType;

        public static string GetKey(string name, string group) => $"{group}-{name}";
    }
    
    private sealed class PredicateCache<T>
    {
        private static readonly Lazy<PredicateCache<T>> Instance = new Lazy<PredicateCache<T>>(() => new PredicateCache<T>());

        private PredicateCache()
        {
            this.CachedPredicates = new();
        }

        public static PredicateCache<T> Cache => Instance.Value;

        public Dictionary<string, Func<T, bool>> CachedPredicates;
    }

    private static Dictionary<string, CachedPredicate> _predicates;

    static PredicateRegistrar()
    {
        _predicates = new();
        GetCachedPredicates(true);
    }

    public static void Clear()
    {
        _predicates.Clear();
    }

    private static Dictionary<string, CachedPredicate> GetCachedPredicates(bool refresh)
    {
        if (!refresh && _predicates.Count > 0)
        {
            return _predicates;
        }
        
        IEnumerable<Type> assemblyTypes =
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());
        Dictionary<string, Dictionary<string, MethodInfo>> result = new();

        _predicates.Clear();
        foreach (Type type in assemblyTypes)
        {
            var methodsToAdd = GetValidMethodsInType(type);
            var propertiesToAdd = GetValidPropertiesInType(type);

            foreach (var methodInfo in methodsToAdd)
            {
                CachedPredicate predicateInfo = new CachedPredicate(methodInfo.attribute.Name,
                    methodInfo.attribute.Group, methodInfo.method, "method", type);
                _predicates[predicateInfo.Key] = predicateInfo;
            }

            foreach (var propertyInfo in propertiesToAdd)
            {
                CachedPredicate predicateInfo = new CachedPredicate(propertyInfo.attribute.Name,
                    propertyInfo.attribute.Group, propertyInfo.method, "property", type);
                _predicates[predicateInfo.Key] = predicateInfo;
            }
        }

        return _predicates;
    }

    public static Func<T, bool>? GetPredicate<T>(string name, string group = "default",
        bool refreshPredicates = false)
    {
        var predicates = GetCachedPredicates(refreshPredicates);

        var key = CachedPredicate.GetKey(name, group);
        if (PredicateCache<T>.Cache.CachedPredicates.TryGetValue(key, out var cachedMethod))
        {
            return cachedMethod;
        }

        if (!predicates.TryGetValue(key, out var cachedPredicate))
        {
            return null;
        }

        try
        {
            switch (cachedPredicate.Type)
            {
                case "method":
                    PredicateCache<T>.Cache.CachedPredicates[key] =
                        cachedPredicate.MethodInfo.CreateDelegate<Func<T, bool>>();
                    break;
                case "property":
                    // For properties, we need to get the property value from the declaring type
                    var property = cachedPredicate.DeclaringType.GetProperty(cachedPredicate.MethodInfo.Name.Replace("get_", ""));
                    if (property == null)
                    {
                        throw new PredicateRegistrationException(name, group, typeof(T), cachedPredicate.MethodInfo, 
                            new InvalidOperationException($"Property {cachedPredicate.MethodInfo.Name} not found"));
                    }

                    // Get the property value (which should be a Func<T, bool>)
                    var propertyValue = property.GetValue(null);
                    if (propertyValue is not Func<T, bool> predicate)
                    {
                        throw new PredicateRegistrationException(name, group, typeof(T), cachedPredicate.MethodInfo,
                            new InvalidOperationException($"Property {property.Name} does not return Func<{typeof(T).Name}, bool>"));
                    }

                    PredicateCache<T>.Cache.CachedPredicates[key] = predicate;
                    break;
            }

            return PredicateCache<T>.Cache.CachedPredicates[key];
        }
        catch (Exception ex)
        {
            throw new PredicateRegistrationException(name, group, typeof(T), cachedPredicate.MethodInfo, ex);
        }
    }

    private static IEnumerable<(MethodInfo method, PredicateRegistrationAttribute attribute)> GetValidMethodsInType(Type type)
    {
        IEnumerable<(MethodInfo, PredicateRegistrationAttribute)> output = type
            .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
            .Where(method => method.GetCustomAttributes(typeof(PredicateRegistrationAttribute)).Any() &&
                             IsValidPredicateMethod(method))
            .Select(method => (method, method.GetCustomAttribute<PredicateRegistrationAttribute>()!));
        return output;
    }

    private static bool IsValidPredicateMethod(MethodInfo method)
    {
        if (method.ReturnType != typeof(bool))
        {
            return false;
        }

        return method.GetParameters().Length == 1;
    }
    

    private static IEnumerable<(MethodInfo method, PredicateRegistrationAttribute attribute)> GetValidPropertiesInType(Type type)
    {
        IEnumerable<(MethodInfo, PredicateRegistrationAttribute)> output = type
            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
            .Where(property => property.GetCustomAttributes(typeof(PredicateRegistrationAttribute)).Any() &&
                               IsValidPredicateProperty(property))
            .Select(field => (field.GetMethod!, field.GetCustomAttribute<PredicateRegistrationAttribute>()!));
        return output;
    }

    private static bool IsValidPredicateProperty(PropertyInfo property)
    {
        if (!property.PropertyType.IsGenericType)
        {
            return false;
        }

        Type genericType = property.PropertyType.GetGenericTypeDefinition();
        if (genericType != typeof(Func<,>))
        {
            return false;
        }

        Type[] genericArgs = property.PropertyType.GetGenericArguments();
        return genericArgs.Length == 2 && genericArgs[1] == typeof(bool);
    }
}
