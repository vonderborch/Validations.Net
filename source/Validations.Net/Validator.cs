using System.Diagnostics;
using System.Reflection;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net;

/// <summary>
///     Provides static methods for validating objects and their members using validation attributes.
///     Supports caching of validation metadata for performance.
/// </summary>
public static class Validator
{
    private static readonly Dictionary<Type, CachedValidations> _cachedValidations = new();

    private static int cacheSize = 32;

    private static readonly List<Type> cacheKeys = new();

    /// <summary>
    ///     Checks if an instance is valid by evaluating all validation attributes on the instance, its fields, and its
    ///     properties.
    /// </summary>
    /// <typeparam name="T">The type of the instance.</typeparam>
    /// <param name="instance">The instance to validate.</param>
    /// <returns>True if all validations pass; otherwise, false.</returns>
    private static bool CheckIsValid<T>(T? instance)
    {
        CachedValidations typeValidations = GetCachedValidations(instance);

        // Validate the instance against the cached validations
        foreach (ValidationAttribute attribute in typeValidations.InstanceValidations)
        {
            // Validate the value using each validation attribute
            var result = attribute.Check(instance);
            if (!result)
            {
                return false;
            }
        }

        // Validate the fields
        foreach ((FieldInfo field, List<ValidationAttribute> validations) in typeValidations.FieldValidations)
        {
            foreach (ValidationAttribute attribute in validations)
            {
                // Validate the value using each validation attribute
                var result = attribute.Check(field.GetValue(instance));
                if (!result)
                {
                    return false;
                }
            }
        }

        // Validate the properties
        foreach ((PropertyInfo property, List<ValidationAttribute> validations) in typeValidations.PropertyValidations)
        {
            foreach (ValidationAttribute attribute in validations)
            {
                // Validate the value using each validation attribute
                var value = property.GetValue(instance);
                var result = attribute.Check(value);
                if (!result)
                {
                    return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if a class instance is valid by evaluating all validation attributes on the instance, its fields, and its
    ///     properties.
    /// </summary>
    /// <typeparam name="T">The type of the class.</typeparam>
    /// <param name="instance">The class instance to validate.</param>
    /// <returns>True if all validations pass; otherwise, false.</returns>
    [DebuggerStepThrough]
    public static bool CheckIsValidClass<T>(this T? instance) where T : class
    {
        var isValid = CheckIsValid(instance);
        return isValid;
    }

    /// <summary>
    ///     Checks if a struct instance is valid by evaluating all validation attributes on the instance, its fields, and its
    ///     properties.
    /// </summary>
    /// <typeparam name="T">The type of the struct.</typeparam>
    /// <param name="instance">The struct instance to validate.</param>
    /// <returns>True if all validations pass; otherwise, false.</returns>
    public static bool CheckIsValidStruct<T>(this T? instance) where T : struct
    {
        var isValid = CheckIsValid(instance);
        return isValid;
    }

    /// <summary>
    ///     Clears the validation metadata cache.
    /// </summary>
    public static void ClearCache()
    {
        cacheKeys.Clear();
        _cachedValidations.Clear();
    }

    /// <summary>
    ///     Gets the cached validations for the type of the given instance, creating and caching them if necessary.
    /// </summary>
    /// <typeparam name="T">The type of the instance.</typeparam>
    /// <param name="instance">The instance whose type's validations to retrieve.</param>
    /// <returns>A <see cref="CachedValidations" /> object containing validation metadata for the type.</returns>
    private static CachedValidations GetCachedValidations<T>(T? instance)
    {
        // Get the cached validations for the type of the instance, or create a new one if it doesn't exist
        CachedValidations typeValidations;
        if (!_cachedValidations.TryGetValue(instance.GetType(), out typeValidations))
        {
            // If the type is not cached, create a new CachedValidations instance
            typeValidations = new CachedValidations(instance.GetType());
            _cachedValidations[instance.GetType()] = typeValidations;
            cacheKeys.Add(instance.GetType());
        }
        else
        {
            // Update the cache keys to maintain the order of access
            cacheKeys.Remove(instance.GetType());
            cacheKeys.Add(instance.GetType());
        }

        // If the cache size exceeds the limit, remove the oldest entry
        if (cacheKeys.Count > cacheSize)
        {
            Type oldestKey = cacheKeys[0];
            _cachedValidations.Remove(oldestKey);
            cacheKeys.RemoveAt(0);
        }

        return typeValidations;
    }

    /// <summary>
    ///     Sets the maximum number of types to cache validation metadata for.
    /// </summary>
    /// <param name="size">The maximum cache size. Must be at least 1.</param>
    public static void SetCacheSize(int size)
    {
        size.ValidateIsInRange(1, int.MaxValue, nameof(size), true, true);

        cacheSize = size;
    }

    /// <summary>
    ///     Validates a class instance and throws a <see cref="ValidationException" /> if any validation fails.
    /// </summary>
    /// <typeparam name="T">The type of the class.</typeparam>
    /// <param name="instance">The class instance to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated instance if all validations pass.</returns>
    /// <exception cref="ValidationException">Thrown if any validation fails.</exception>
    [DebuggerStepThrough]
    public static T ValidateIsValidClass<T>(this T instance, Blackboard? blackboard = null) where T : class
    {
        Dictionary<string, object?> exceptions = ValidateObject(instance, nameof(instance));
        if (exceptions.Count > 0)
        {
            throw new ValidationException("IsValidClass", nameof(instance), $"Validation failed for {typeof(T).Name}",
                blackboard, exceptions);
        }

        return instance;
    }

    /// <summary>
    ///     Validates a struct instance and throws a <see cref="ValidationException" /> if any validation fails.
    /// </summary>
    /// <typeparam name="T">The type of the struct.</typeparam>
    /// <param name="instance">The struct instance to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated instance if all validations pass.</returns>
    /// <exception cref="ValidationException">Thrown if any validation fails.</exception>
    [DebuggerStepThrough]
    public static T ValidateIsValidStruct<T>(this T instance, Blackboard? blackboard = null) where T : struct
    {
        Dictionary<string, object?> exceptions = ValidateObject(instance, nameof(instance));
        if (exceptions.Count > 0)
        {
            throw new ValidationException("IsValidStruct", nameof(instance), $"Validation failed for {typeof(T).Name}",
                blackboard, exceptions);
        }

        return instance;
    }

    /// <summary>
    ///     Validates an object and collects all validation exceptions for the instance, its fields, and its properties.
    /// </summary>
    /// <param name="instance">The object to validate.</param>
    /// <param name="propertyName">The name of the property or variable being validated.</param>
    /// <returns>A dictionary mapping property paths to validation exceptions.</returns>
    [DebuggerStepThrough]
    private static Dictionary<string, object?> ValidateObject(object? instance, string propertyName)
    {
        Dictionary<string, object?> exceptions = new();
        CachedValidations typeValidations = GetCachedValidations(instance);

        // Validate the instance against the cached validations
        foreach (ValidationAttribute attribute in typeValidations.InstanceValidations)
        {
            // Validate the value using each validation attribute
            Exception? exception = attribute.SafeValidate(instance, propertyName);
            if (exception != null)
            {
                exceptions.Add($"{propertyName}->{attribute.GetType().Name}", exception);
            }
        }

        // Validate the fields
        foreach ((FieldInfo field, List<ValidationAttribute> validations) in typeValidations.FieldValidations)
        {
            foreach (ValidationAttribute attribute in validations)
            {
                // Validate the value using each validation attribute
                Exception? exception = attribute.SafeValidate(field.GetValue(instance), $"{propertyName}.{field.Name}");
                if (exception != null)
                {
                    exceptions.Add($"{propertyName}.{field.Name}->{attribute.GetType().Name}", exception);
                }
            }
        }

        // Validate the properties
        foreach ((PropertyInfo property, List<ValidationAttribute> validations) in typeValidations.PropertyValidations)
        {
            foreach (ValidationAttribute attribute in validations)
            {
                // Validate the value using each validation attribute
                Exception? exception =
                    attribute.SafeValidate(property.GetValue(instance), $"{propertyName}.{property.Name}");
                if (exception != null)
                {
                    exceptions.Add($"{propertyName}.{property.Name}->{attribute.GetType().Name}", exception);
                }
            }
        }

        return exceptions;
    }

    /// <summary>
    ///     Caches validation attributes for a specific type, including instance, field, and property validations.
    /// </summary>
    private class CachedValidations
    {
        private List<(FieldInfo Field, List<ValidationAttribute> Validations)>? fieldValidations;
        private List<ValidationAttribute>? instanceValidations;

        private List<(PropertyInfo Property, List<ValidationAttribute> Validations)>? propertyValidations;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CachedValidations" /> class for the specified type.
        /// </summary>
        /// <param name="type">The type to cache validations for.</param>
        public CachedValidations(Type type)
        {
            this.Type = type;
        }

        /// <summary>
        ///     Gets the list of fields and their associated validation attributes.
        /// </summary>
        public List<(FieldInfo Field, List<ValidationAttribute> Validations)> FieldValidations
        {
            get
            {
                if (this.fieldValidations == null)
                {
                    this.fieldValidations = new List<(FieldInfo Field, List<ValidationAttribute> Validations)>();
                    FieldInfo[] fields = this.Type.GetFields(BindingFlags.Public | BindingFlags.NonPublic |
                                                             BindingFlags.Instance | BindingFlags.Static);

                    foreach (FieldInfo field in fields)
                    {
                        // Get the validation attributes for the field
                        List<ValidationAttribute> validations = field
                            .GetCustomAttributes(typeof(ValidationAttribute), true)
                            .Cast<ValidationAttribute>()
                            .ToList();

                        // If there are no validations, skip this field
                        if (validations.Count == 0)
                        {
                            continue;
                        }

                        // Add the field and its validations to the list
                        this.fieldValidations.Add((field, validations));
                    }
                }

                return this.fieldValidations;
            }
        }

        /// <summary>
        ///     Gets the list of validation attributes applied to the type itself.
        /// </summary>
        public List<ValidationAttribute> InstanceValidations
        {
            get
            {
                if (this.instanceValidations == null)
                {
                    this.instanceValidations = this.Type.GetCustomAttributes(typeof(ValidationAttribute), true)
                        .Cast<ValidationAttribute>()
                        .ToList();
                }

                return this.instanceValidations;
            }
        }

        /// <summary>
        ///     Gets the list of properties and their associated validation attributes.
        /// </summary>
        public List<(PropertyInfo Property, List<ValidationAttribute> Validations)> PropertyValidations
        {
            get
            {
                if (this.propertyValidations == null)
                {
                    this.propertyValidations =
                        new List<(PropertyInfo Property, List<ValidationAttribute> Validations)>();
                    PropertyInfo[] properties = this.Type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic |
                                                                        BindingFlags.Instance | BindingFlags.Static);

                    foreach (PropertyInfo property in properties)
                    {
                        // Get the validation attributes for the property
                        List<ValidationAttribute> validations = property
                            .GetCustomAttributes(typeof(ValidationAttribute), true)
                            .Cast<ValidationAttribute>()
                            .ToList();

                        // If there are no validations, skip this property
                        if (validations.Count == 0)
                        {
                            continue;
                        }

                        // Add the property and its validations to the list
                        this.propertyValidations.Add((property, validations));
                    }
                }

                return this.propertyValidations;
            }
        }

        /// <summary>
        ///     Gets the type associated with this cache.
        /// </summary>
        public Type Type { get; }
    }
}
