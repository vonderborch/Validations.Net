using System.Collections.Concurrent;
using System.Reflection;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes.Helpers;

/// <summary>
///     Provides a mechanism to register, retrieve, and manage predicates for validation purposes.
/// </summary>
public static class PredicateRegistrar
{
    /// <summary>
    ///     Represents a thread-safe, global collection of predicates used for validation purposes,
    ///     where each predicate is identified by a unique key and can be accessed across all instances.
    /// </summary>
    private static readonly ConcurrentDictionary<string, PredicateInfo> GlobalPredicates = new();

    /// <summary>
    ///     Maintains a thread-safe collection of predicates associated with instances,
    ///     organized by type and key, to support instance-specific validation logic.
    /// </summary>
    private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<string, PredicateInfo>> InstancePredicates =
        new();

    /// <summary>
    ///     Provides a synchronization mechanism to ensure thread-safe operations
    ///     within the scope of predicate registration and initialization processes.
    /// </summary>
    private static readonly Lock SyncLock = new();

    /// <summary>
    ///     Indicates whether the predicates have been initialized.
    ///     Used to ensure that the predicate registrations are performed only once
    ///     unless explicitly refreshed or reset.
    /// </summary>
    private static bool _predicatesInitialized;

    /// <summary>
    ///     Static constructor that initializes the predicate registrar.
    /// </summary>
    static PredicateRegistrar()
    {
    }

    /// <summary>
    ///     Clears all registered predicates.
    /// </summary>
    public static void Clear()
    {
        lock (SyncLock)
        {
            _predicatesInitialized = false;
            GlobalPredicates.Clear();
            InstancePredicates.Clear();
        }
    }

    /// <summary>
    ///     Creates a delegate for a field-based predicate.
    /// </summary>
    /// <typeparam name="T">The type parameter for the delegate's input.</typeparam>
    /// <param name="predicateInfo">Details about the predicate, including its name, type, and associated metadata.</param>
    /// <param name="instance">An instance of the object containing the field, or null if the field is static.</param>
    /// <returns>
    ///     A delegate of type <c>Func&lt;T, bool&gt;</c> representing the field-based predicate, or null if the predicate
    ///     cannot be created.
    /// </returns>
    /// <exception cref="PredicateRegistrationException">
    ///     Thrown when the field cannot be found, when the field is not of type <c>Func&lt;T, bool&gt;</c>,
    ///     or when an instance is required for a non-static field but was not provided.
    /// </exception>
    private static Func<T, bool>? CreateFieldPredicateDelegate<T>(PredicateInfo predicateInfo, object? instance)
    {
        FieldInfo? field = predicateInfo.DeclaringType.GetField(predicateInfo.MethodInfo.Name);
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
                new InvalidOperationException("Instance cannot be null for a non-static field"));
        }

        var fieldValue = field.GetValue(instanceValue);
        if (fieldValue is not Func<T, bool> fieldPredicate)
        {
            throw new PredicateRegistrationException(predicateInfo, typeof(T),
                new InvalidOperationException($"Field {field.Name} does not return Func<{typeof(T).Name}, bool>"));
        }

        return fieldPredicate;
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
    ///     Creates a predicate delegate for a property, which is expected to return a <see cref="Func{T, Boolean}" />.
    /// </summary>
    /// <param name="predicateInfo">Information about the predicate, including the property details and type.</param>
    /// <param name="instance">
    ///     The instance containing the property, if the property is non-static. If the property is static,
    ///     this parameter can be null.
    /// </param>
    /// <typeparam name="T">The type of the input parameter for the delegate.</typeparam>
    /// <returns>
    ///     A delegate of type <see cref="Func{T, Boolean}" /> extracted from the property, or null if unable to create
    ///     the delegate.
    /// </returns>
    /// <exception cref="PredicateRegistrationException">
    ///     Thrown if the property cannot be found, the property value is not of type <see cref="Func{T, Boolean}" />, or if an
    ///     invalid instance is provided for a non-static property.
    /// </exception>
    private static Func<T, bool>? CreatePropertyPredicateDelegate<T>(PredicateInfo predicateInfo, object? instance)
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
    ///     Ensures predicates are initialized, optionally forcing a refresh.
    /// </summary>
    /// <param name="refresh">Whether to force a refresh of predicates.</param>
    private static void EnsurePredicatesInitialized(bool refresh = false)
    {
        if (_predicatesInitialized && !refresh)
        {
            return;
        }

        lock (SyncLock)
        {
            if (_predicatesInitialized && !refresh)
            {
                return;
            }

            Clear();
            InitializePredicates();
            _predicatesInitialized = true;
        }
    }

    /// <summary>
    ///     Gets a predicate function based on the provided name, group, and instance.
    /// </summary>
    /// <typeparam name="T">The type parameter for the predicate function.</typeparam>
    /// <param name="name">The name of the predicate to retrieve.</param>
    /// <param name="group">Optional group name to scope the predicate.</param>
    /// <param name="instance">The instance object that contains the predicate (required for instance predicates).</param>
    /// <param name="refresh">Whether to refresh the predicates cache.</param>
    /// <returns>A predicate function if found; otherwise, null.</returns>
    public static Func<T, bool>? GetPredicate<T>(string name, string? group, object? instance, bool refresh = false)
    {
        instance.ValidateIsNotNull(nameof(instance));
        var key = PredicateInfo.GetKey(name, group);
        Type type = instance!.GetType();

        // First, check if the predicate is already in the typed cache
        Func<T, bool>? cachedPredicate = PredicateCache<T>.Cache.GetPredicate(key, type);
        if (cachedPredicate is not null)
        {
            return cachedPredicate;
        }

        // Initialize predicates if needed
        EnsurePredicatesInitialized(refresh);

        // Find predicate info from instance or global registrations
        if (!TryGetPredicateInfo(key, type, out PredicateInfo predicateInfo))
        {
            return null;
        }

        // Create the predicate delegate based on the predicate type
        Func<T, bool>? predicate = CreatePredicateDelegate<T>(predicateInfo, instance);

        // Cache the predicate if it's a public static one
        if (predicate is not null && predicateInfo.IsGlobalPredicate)
        {
            PredicateCache<T>.Cache.AddPredicate(key, type, predicate);
        }

        return predicate;
    }

    /// <summary>
    ///     Initializes all predicates by scanning assemblies for types with predicate annotations.
    /// </summary>
    private static void InitializePredicates()
    {
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
                catch
                {
                    return Array.Empty<Type>();
                }
            });

        // Process each type in parallel for better performance
        Parallel.ForEach(assemblyTypes, type =>
        {
            try
            {
                ProcessTypePredicates(type);
            }
            catch
            {
                // Log or handle exception if needed
            }
        });
    }

    /// <summary>
    ///     Processes predicates for a specific type.
    /// </summary>
    /// <param name="type">The type to process.</param>
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
}
