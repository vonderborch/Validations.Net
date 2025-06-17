using System.Reflection;

namespace Validations.Net.ValidationAttributes;

public static class PredicateRegistrar
{
    private static Dictionary<string, Dictionary<string, MethodInfo>> predicates;
    
    static PredicateRegistrar()
    {
        predicates = new();
    }

    public static void Clear()
    {
        predicates.Clear();
    }

    public static bool Clear(string name)
    {
        return predicates.Remove(name);
    }

    public static Func<T, bool>? GetPredicate<T>(string name, string group = "default", bool searchForPredicate = false)
    {
        Dictionary<string, MethodInfo> methodMapping;
        if (!predicates.TryGetValue(group, out methodMapping) && searchForPredicate)
        {
            var groupedMethodMapping = GetMethodMapping();
            if (!groupedMethodMapping.TryGetValue(group, out methodMapping))
            {
                return null;
            }
        }
        
        MethodInfo? methodInfo = null;
        if (!methodMapping.TryGetValue(name, out methodInfo) && searchForPredicate)
        {
            var groupedMethodMapping = GetMethodMapping();
            if (!groupedMethodMapping.TryGetValue(group, out methodMapping))
            {
                return null;
            }

            if (!methodMapping.TryGetValue(name, out methodInfo))
            {
                return null;
            }
        }

        if (methodInfo == null)
        {
            return null;
        }

        Func<T, bool> func = methodInfo!.CreateDelegate<Func<T, bool>>();
        return func;
    }

    public static void LoadAllPredicates()
    {
        predicates = GetMethodMapping();
    }

    private static Dictionary<string, Dictionary<string, MethodInfo>> GetMethodMapping()
    {
        var methods = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .SelectMany(type => type.GetMethods())
            .Where(x => x.GetCustomAttributes(typeof(PredicateRegistrationAttribute)).Any());

        var result = new Dictionary<string, Dictionary<string, MethodInfo>>();
        
        foreach(var method in methods)
        {
            var attribute = method.GetCustomAttribute<PredicateRegistrationAttribute>();
            if (!result.ContainsKey(attribute.Group))
            {
                result[attribute.Group] = new Dictionary<string, MethodInfo>();
            }
            result[attribute.Group][attribute.Name] = method;
        }

        return result;
    }
}
