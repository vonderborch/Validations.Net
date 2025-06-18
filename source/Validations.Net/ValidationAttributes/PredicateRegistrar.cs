using System.Reflection;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// A static utility class for registering, managing, and retrieving predicate methods at runtime.
/// This class works in conjunction with the <see cref="PredicateRegistrationAttribute"/> to provide
/// a mechanism for dynamically accessing predicate methods across the application.
/// </summary>
public static class PredicateRegistrar
{
    /// <summary>
    /// A nested dictionary that stores predicate methods organized by group and name.
    /// The outer dictionary uses group names as keys, while the inner dictionary uses predicate names as keys.
    /// </summary>
    private static Dictionary<string, Dictionary<string, MethodInfo>> predicates;
    
    /// <summary>
    /// Static constructor that initializes the predicates dictionary.
    /// </summary>
    static PredicateRegistrar()
    {
        predicates = new();
    }

    /// <summary>
    /// Clears all registered predicates from all groups.
    /// </summary>
    public static void Clear()
    {
        predicates.Clear();
    }

    /// <summary>
    /// Clears all registered predicates from a specific group.
    /// </summary>
    /// <param name="name">The name of the group to clear.</param>
    /// <returns>True if the group was found and removed; otherwise, false.</returns>
    public static bool Clear(string name)
    {
        return predicates.Remove(name);
    }

    /// <summary>
    /// Retrieves a predicate function for a specific type by its name and group.
    /// </summary>
    /// <typeparam name="T">The type that the predicate will evaluate.</typeparam>
    /// <param name="name">The name of the predicate to retrieve.</param>
    /// <param name="group">The group containing the predicate. Defaults to "default".</param>
    /// <param name="searchForPredicate">When true, searches for the predicate in all loaded assemblies if not found in the existing cache.</param>
    /// <returns>A function that takes an instance of type T and returns a boolean result, or null if the predicate cannot be found.</returns>
    public static Func<T, bool>? GetPredicate<T>(string name, string group = "default", bool searchForPredicate = true)
    {
        Dictionary<string, MethodInfo> methodMapping;
        if (!predicates.TryGetValue(group, out methodMapping) && searchForPredicate)
        {
            var groupedMethodMapping = GetMethodMapping();
            if (!groupedMethodMapping.TryGetValue(group, out methodMapping))
            {
                return null;
            }

            predicates = groupedMethodMapping;
        }
        
        MethodInfo? methodInfo = null;
        if (!methodMapping.TryGetValue(name, out methodInfo) && searchForPredicate)
        {
            var groupedMethodMapping = GetMethodMapping();
            if (!groupedMethodMapping.TryGetValue(group, out methodMapping))
            {
                return null;
            }

            predicates = groupedMethodMapping;

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

    /// <summary>
    /// Scans all loaded assemblies for methods decorated with <see cref="PredicateRegistrationAttribute"/>
    /// and registers them in the predicates dictionary.
    /// </summary>
    public static void LoadAllPredicates()
    {
        predicates = GetMethodMapping();
    }

    /// <summary>
    /// Scans all loaded assemblies for methods decorated with <see cref="PredicateRegistrationAttribute"/>
    /// and creates a mapping of these methods organized by group and name.
    /// </summary>
    /// <returns>A nested dictionary where the outer dictionary is keyed by group name and the inner dictionary is keyed by predicate name.</returns>
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
