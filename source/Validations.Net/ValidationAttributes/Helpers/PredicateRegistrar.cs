using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes.Helpers;

public static class PredicateRegistrar
{
    private static readonly Dictionary<string, PredicateInfo> GlobalPredicates;

    private static readonly Dictionary<Type, Dictionary<string, PredicateInfo>> InstancePredicates;

    private static bool _fetchedPredicates;
    
    private static readonly Lock Lock = new();
    
    static PredicateRegistrar()
    {
        _fetchedPredicates = false;
        GlobalPredicates = new();
        InstancePredicates = new();
        GetPredicates(true);
    }

    public static void Clear()
    {
        _fetchedPredicates = false;
        GlobalPredicates.Clear();
        InstancePredicates.Clear();
    }
    
    public static Func<T, bool>? GetPredicate<T>(string name, string? group, object? instance, bool refresh = false)
    {
        instance.ValidateIsNotNull(nameof(instance));
        string key = PredicateInfo.GetKey(name, group);
        Type type = instance!.GetType();
        
        // first, check if the predicate is already in the global cache...
        var cachedPredicate = PredicateCache<T>.Cache.GetPredicate(key, type);
        if (cachedPredicate is not null)
        {
            return cachedPredicate;
        }
        
        // next check instance predicates, then global
        (Dictionary<string, PredicateInfo> globalPredicates, Dictionary<Type, Dictionary<string, PredicateInfo>> instancePredicates) = GetPredicates(refresh);
        PredicateInfo predicateInfo = default;
        bool foundPredicate = false;
        if (instancePredicates.TryGetValue(type, out var predicates))
        {
            if (predicates.TryGetValue(key, out var potentialPredicateInfo))
            {
                predicateInfo = potentialPredicateInfo;
                foundPredicate = true;
            }
        }

        if (!foundPredicate)
        {
            if (!globalPredicates.TryGetValue(key, out var potentialPredicateInfo))
            {
                return null;
            }

            predicateInfo = potentialPredicateInfo;
        }
        
        // Now we have a prospective predicate, we need to get the actual method for it based on what type it is
        Func<T, bool>? predicate = null;

        if (predicateInfo.Type == PredicateType.StaticMethod)
        {
            predicate = predicateInfo.MethodInfo.CreateDelegate<Func<T, bool>>();
        }
        else if (predicateInfo.Type == PredicateType.Method)
        {
            predicate = predicateInfo.MethodInfo.CreateDelegate<Func<T, bool>>(instance);
        }
        else if (predicateInfo.Type.CheckIsOneOf(PredicateType.StaticProperty, PredicateType.Property))
        {
            // For properties, we need to get the property value from the declaring type
            var property = predicateInfo.DeclaringType.GetProperty(predicateInfo.MethodInfo.Name.Replace("get_", ""));
            if (property == null)
            {
                throw new PredicateRegistrationException(predicateInfo, typeof(T), 
                    new InvalidOperationException($"Property {predicateInfo.MethodInfo.Name} not found"));
            }

            // Get the property value (which should be a Func<T, bool>)
            var instanceValue = predicateInfo.IsStatic ? null : instance;
            if (instanceValue is null && !predicateInfo.IsStatic)
            {
                throw new PredicateRegistrationException(predicateInfo, typeof(T), 
                    new InvalidOperationException($"Instance cannot be null for a non-static property"));
            }
            var propertyValue = property.GetValue(instanceValue);
            if (propertyValue is not Func<T, bool> propertyPredicate)
            {
                throw new PredicateRegistrationException(predicateInfo, typeof(T),
                    new InvalidOperationException($"Property {property.Name} does not return Func<{typeof(T).Name}, bool>"));
            }

            predicate = propertyPredicate;
        }
        else if (predicateInfo.Type.CheckIsOneOf(PredicateType.StaticField, PredicateType.Field))
        {
            // For fields, we need to get the field value from the declaring type
            var field = predicateInfo.DeclaringType.GetField(predicateInfo.MethodInfo.Name);
            if (field == null)
            {
                throw new PredicateRegistrationException(predicateInfo, typeof(T),
                    new InvalidOperationException($"Field {predicateInfo.MethodInfo.Name} not found"));
            }

            // Get the field value (which should be a Func<T, bool>)
            var instanceValue = predicateInfo.IsStatic ? null : instance;
            if (instanceValue is null && !predicateInfo.IsStatic)
            {
                throw new PredicateRegistrationException(predicateInfo, typeof(T), 
                    new InvalidOperationException($"Instance cannot be null for a non-static field"));
            }
            var fieldValue = field.GetValue(instanceValue);
            if (fieldValue is not Func<T, bool> fieldPredicate)
            {
                throw new PredicateRegistrationException(predicateInfo, typeof(T),
                    new InvalidOperationException($"Field {field.Name} does not return Func<{typeof(T).Name}, bool>"));
            }

            predicate = fieldPredicate;
        }

        if (predicate is not null && predicateInfo.IsPublic && predicateInfo.IsStatic)
        {
            PredicateCache<T>.Cache.AddPredicate(key, type, predicate);
        }
        return predicate;
    }

    public static (Dictionary<string, PredicateInfo> globalPredicates, Dictionary<Type, Dictionary<string, PredicateInfo>>
        instancePredicates) GetPredicates(bool refresh)
    {
        if (!refresh && _fetchedPredicates)
        {
            return (GlobalPredicates, InstancePredicates);
        }

        lock (Lock)
        {
            if (_fetchedPredicates)
            {
                return (GlobalPredicates, InstancePredicates);
            }
            Clear();
            RefreshPredicates();
            _fetchedPredicates = true;
            return (GlobalPredicates, InstancePredicates);
        }
    }

    private static void RefreshPredicates()
    {
        IEnumerable<Type> assemblyTypes =
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());

        foreach (Type type in assemblyTypes)
        {
            TypePredicatesInfo predicatesInfo = new(type);
            if (predicatesInfo.Predicates.Count > 0)
            {
                foreach (var predicate in predicatesInfo.Predicates)
                {
                    bool isGlobalPredicate = predicate is { IsPublic: true, IsStatic: true };

                    if (isGlobalPredicate)
                    {
                        GlobalPredicates[predicate.Key] = predicate;
                    }
                    else
                    {
                        if (!InstancePredicates.ContainsKey(type))
                        {
                            InstancePredicates[type] = new();
                        }
                        InstancePredicates[type][predicate.Key] = predicate;
                    }
                }
            }
        }
    }
}
