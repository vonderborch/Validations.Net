using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using Validations.Net;
using Validations.Net.Validators;

namespace Validations.Net.Predicates;

/// <summary>
/// The predicate manager.
/// </summary>
public static class PredicateManager
{
    /// <summary>
    /// The global predicates.
    /// </summary>
    private static readonly ConcurrentDictionary<string, PredicateInfo> GlobalPredicates = new();

    /// <summary>
    /// The instance predicates.
    /// </summary>
    private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<string, PredicateInfo>> InstancePredicates =
        new();

    /// <summary>
    /// The lock object to synchronize access to the predicates.
    /// </summary>
    private static readonly Lock SyncLock = new();

    /// <summary>
    /// Whether the predicates are initialized.
    /// </summary>
    private static volatile bool _predicatesInitialized;

    /// <summary>
    /// The number of threads to use for initializing the predicates.
    /// </summary>
    public static int InitializationThreadCount { get; set; } = 4;

    /// <summary>
    /// Initializes the predicate manager.
    /// </summary>
    static PredicateManager() {}

    /// <summary>
    /// Clears all registered predicates.
    /// </summary>
    public static void Clear()
    {
        lock (SyncLock)
        {
            ClearInternal();
        }
    }

    /// <summary>
    /// Clears all registered predicates without acquiring a lock. Caller must hold SyncLock.
    /// </summary>
    private static void ClearInternal()
    {
        _predicatesInitialized = false;
        GlobalPredicates.Clear();
        InstancePredicates.Clear();
    }

    /// <summary>
    /// Gets a predicate delegate for the specified type and key.
    /// </summary>
    /// <typeparam name="T">The type of the object to be validated by the predicate.</typeparam>
    /// <param name="name">The name of the predicate.</param>
    /// <param name="group">The group of the predicate.</param>
    /// <param name="instance">The instance of the object to be validated by the predicate. Can be null for global predicates.</param>
    /// <param name="refresh">Whether to refresh the predicates.</param>
    /// <returns>A delegate that represents the predicate function, or null if the predicate cannot be created.</returns>
    public static Func<T, bool>? GetPredicate<T>(string name, string? group, object? instance = null, bool refresh = false)
    {
        var key = PredicateInfo.GetKey(name, group);
        
        // Next, load the predicates (as needed)
        if (refresh || !_predicatesInitialized) {
            InitializePredicates(refresh);
        }

        // First, try to find a global predicate (no instance required)
        if (GlobalPredicates.TryGetValue(key, out PredicateInfo globalPredicateInfo))
        {
            // Check if the predicate is already in the typed cache
            Func<T, bool>? cachedPredicate = PredicateCache<T>.Instance.GetPredicate(key, globalPredicateInfo.DeclaringType);
            if (cachedPredicate is not null)
            {
                return cachedPredicate;
            }
            
            Func<T, bool>? predicate = CreatePredicateDelegate<T>(globalPredicateInfo, null);
            
            // Cache the predicate if it's a public static one
            if (predicate is not null)
            {
                PredicateCache<T>.Instance.AddPredicate(key, globalPredicateInfo.DeclaringType, predicate);
            }
            
            return predicate;
        }

        // If no global predicate found, instance is required for instance predicates
        if (instance == null)
        {
            return null;
        }

        Type type = instance.GetType();

        // Check if the predicate is already in the typed cache
        Func<T, bool>? cachedInstancePredicate = PredicateCache<T>.Instance.GetPredicate(key, type);
        if (cachedInstancePredicate is not null)
        {
            return cachedInstancePredicate;
        }

        // Find predicate info from instance registrations
        if (!TryGetPredicateInfo(key, type, out PredicateInfo predicateInfo))
        {
            return null;
        }
        
        Func<T, bool>? instancePredicate = CreatePredicateDelegate<T>(predicateInfo, instance);

        // Cache the predicate if it's a public static one
        if (instancePredicate is not null && predicateInfo.IsGlobalPredicate)
        {
            PredicateCache<T>.Instance.AddPredicate(key, type, instancePredicate);
        }

        return instancePredicate;
    }

    /// <summary>
    /// Initializes the predicates.
    /// </summary>
    /// <param name="refresh">Whether to refresh the predicates.</param>
    private static void InitializePredicates(bool refresh = false) {
        lock (SyncLock)
        {
            if (_predicatesInitialized && !refresh)
            {
                return;
            }

            ClearInternal();

            // Get types from all loaded assemblies for scanning
            IEnumerable<Type> assemblyTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => !a.IsDynamic && !a.FullName!.StartsWith("System.") && !a.FullName.StartsWith("Microsoft."))
                .SelectMany(a =>
                {
                    try
                    {
                        return a.GetTypes();
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceWarning($"Failed to get types from assembly {a.FullName}: {ex.Message}");
                        return [];
                    }
                });

            // Process each type in parallel for better performance
            Parallel.ForEach(assemblyTypes, new ParallelOptions { MaxDegreeOfParallelism = InitializationThreadCount }, type =>
            {
                try
                {
                    ProcessTypePredicates(type);
                }
                catch (Exception ex)
                {
                    Trace.TraceWarning($"Failed to process predicates for type {type.FullName}: {ex.Message}");
                }
            });

            _predicatesInitialized = true;
        }
    }

    /// <summary>
    ///     Processes the predicates for the specified type.
    /// </summary>
    /// <param name="type">The type to process predicates for.</param>
    private static void ProcessTypePredicates(Type type)
    {
        TypePredicatesInfo predicatesInfo = new(type);
        if (predicatesInfo.Predicates.Count == 0)
        {
            return;
        }

        foreach (PredicateInfo predicate in predicatesInfo.Predicates)
        {
            if (predicate is { IsGlobalPredicate: true })
            {
                // Register global predicate
                GlobalPredicates[predicate.Key] = predicate;
            }
            else
            {
                // Register instance predicate
                ConcurrentDictionary<string, PredicateInfo> typePredicates =
                    InstancePredicates.GetOrAdd(type, _ => new ConcurrentDictionary<string, PredicateInfo>());
                typePredicates[predicate.Key] = predicate;
            }
        }
    }

    /// <summary>
    ///     Attempts to retrieve predicate information based on the given key and type.
    /// </summary>
    /// <param name="key">The unique key identifying the predicate.</param>
    /// <param name="type">The type associated with the predicate.</param>
    /// <param name="predicateInfo">
    ///     When this method returns, contains the predicate information if found, otherwise, the
    ///     default value.
    /// </param>
    /// <returns>True if the predicate information is found; otherwise, false.</returns>
    private static bool TryGetPredicateInfo(string key, Type type, out PredicateInfo predicateInfo)
    {
        // First check instance predicates
        if (InstancePredicates.TryGetValue(type, out ConcurrentDictionary<string, PredicateInfo>? instancePredicates) &&
            instancePredicates.TryGetValue(key, out predicateInfo))
        {
            return true;
        }

        // Then check global predicates
        return GlobalPredicates.TryGetValue(key, out predicateInfo);
    }

    /// <summary>
    ///     Creates a predicate delegate based on the specified predicate information and instance.
    /// </summary>
    /// <typeparam name="T">The type of the object to be validated by the predicate.</typeparam>
    /// <param name="predicateInfo">
    ///     The <see cref="PredicateInfo" /> that describes the predicate, including its type,
    ///     location, and other metadata.
    /// </param>
    /// <param name="instance">
    ///     The instance object that provides the context for instance-bound predicates or null for static
    ///     predicates.
    /// </param>
    /// <returns>A delegate that represents the predicate function, or null if the predicate cannot be created.</returns>
    private static Func<T, bool>? CreatePredicateDelegate<T>(PredicateInfo predicateInfo, object? instance)
    {
        return predicateInfo.Type switch
        {
            PredicateType.StaticMethod => predicateInfo.MethodInfo.CreateDelegate<Func<T, bool>>(),

            PredicateType.Method => predicateInfo.MethodInfo.CreateDelegate<Func<T, bool>>(instance),

            PredicateType.StaticProperty or PredicateType.Property => CreatePropertyPredicateDelegate<T>(predicateInfo,
                instance),

            PredicateType.StaticField or PredicateType.Field =>
                CreateFieldPredicateDelegate<T>(predicateInfo, instance),

            _ => null
        };
    }

    /// <summary>
    ///     Creates a property predicate delegate based on the specified predicate information and instance.
    /// </summary>
    /// <typeparam name="T">The type of the object to be validated by the predicate.</typeparam>
    /// <param name="predicateInfo">
    ///     The <see cref="PredicateInfo" /> that describes the predicate, including its type,
    ///     location, and other metadata.
    /// </param>
    /// <param name="instance">
    ///     The instance object that provides the context for instance-bound predicates or null for static
    ///     predicates.
    /// </param>
    /// <returns>A delegate that represents the predicate function.</returns>
    /// <exception cref="PredicateRegistrationException">
    ///     Thrown if the property cannot be found, the property value is not of type <see cref="Func{T, Boolean}" />, or if an
    ///     invalid instance is provided for a non-static property.
    /// </exception>
    private static Func<T, bool> CreatePropertyPredicateDelegate<T>(PredicateInfo predicateInfo, object? instance)
    {
        PropertyInfo? property =
            predicateInfo.DeclaringType.GetProperty(predicateInfo.MethodInfo.Name.Replace("get_", ""));
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
                new InvalidOperationException("Instance cannot be null for a non-static property"));
        }

        // Get the property value
        var propertyValue = property.GetValue(instanceValue);
        if (propertyValue is not Func<T, bool> propertyPredicate)
        {
            throw new PredicateRegistrationException(predicateInfo, typeof(T),
                new InvalidOperationException(
                    $"Property {property.Name} does not return Func<{typeof(T).Name}, bool>"));
        }

        return propertyPredicate;
    }


    /// <summary>
    ///     Creates a field predicate delegate based on the specified predicate information and instance.
    /// </summary>
    /// <typeparam name="T">The type of the object to be validated by the predicate.</typeparam>
    /// <param name="predicateInfo">
    ///     The <see cref="PredicateInfo" /> that describes the predicate, including its type,
    ///     location, and other metadata.
    /// </param>
    /// <param name="instance">
    ///     The instance object that provides the context for instance-bound predicates or null for static
    ///     predicates.
    /// </param>
    /// <returns>A delegate that represents the predicate function.</returns>
    /// <exception cref="PredicateRegistrationException">
    ///     Thrown if the field cannot be found, the field value is not of type <see cref="Func{T, Boolean}" />, or if an
    ///     invalid instance is provided for a non-static field.
    /// </exception>
    private static Func<T, bool> CreateFieldPredicateDelegate<T>(PredicateInfo predicateInfo, object? instance)
    {
        // Get the field by searching for fields with the ValidationPredicateAttribute that matches the predicate name
        FieldInfo? field = predicateInfo.DeclaringType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .FirstOrDefault(f => 
            {
                var attr = f.GetCustomAttribute<ValidationPredicateAttribute>();
                return attr != null && attr.Name == predicateInfo.Name && attr.Group == predicateInfo.Group;
            });
        
        if (field == null)
        {
            throw new PredicateRegistrationException(predicateInfo, typeof(T),
                new InvalidOperationException($"Field with predicate name '{predicateInfo.Name}' not found"));
        }

        // Get the field value (which should be a Func<T, bool>)
        var instanceValue = predicateInfo.IsStatic ? null : instance;
        if (instanceValue is null && !predicateInfo.IsStatic)
        {
            throw new PredicateRegistrationException(predicateInfo, typeof(T),
                new InvalidOperationException("Instance cannot be null for a non-static field"));
        }

        // Get the field value
        var fieldValue = field.GetValue(instanceValue);
        if (fieldValue is not Func<T, bool> fieldPredicate)
        {
            throw new PredicateRegistrationException(predicateInfo, typeof(T),
                new InvalidOperationException($"Field {field.Name} does not return Func<{typeof(T).Name}, bool>"));
        }

        return fieldPredicate;
    }
}
