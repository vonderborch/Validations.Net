using System.Diagnostics;
using System.Reflection;
using SimpleBlackboard.Net;
using Validations.Net.ValidationAttributes;

namespace Validations.Net;

public static class Validator
{
    private class CachedValidations
    {
        private List<ValidationAttribute>? instanceValidations;
        
        private List<(FieldInfo Field, List<ValidationAttribute> Validations)>? fieldValidations;
        
        private List<(PropertyInfo Property, List<ValidationAttribute> Validations)>? propertyValidations;
        
        public CachedValidations(Type type)
        {
            Type = type;
        }
        
        public Type Type { get; }
        
        public List<ValidationAttribute> InstanceValidations
        {
            get
            {
                if (instanceValidations == null)
                {
                    instanceValidations = Type.GetCustomAttributes(typeof(ValidationAttribute), true)
                        .Cast<ValidationAttribute>()
                        .ToList();
                }
                return instanceValidations;
            }
        }
        
        public List<(FieldInfo Field, List<ValidationAttribute> Validations)> FieldValidations
        {
            get
            {
                if (fieldValidations == null)
                {
                    fieldValidations = new();
                    var fields = Type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                    
                    foreach (var field in fields)
                    {
                        // Get the validation attributes for the field
                        var validations = field.GetCustomAttributes(typeof(ValidationAttribute), true)
                            .Cast<ValidationAttribute>()
                            .ToList();
                        
                        // If there are no validations, skip this field
                        if (validations.Count == 0)
                        {
                            continue;
                        }
                        
                        // Add the field and its validations to the list
                        fieldValidations.Add((field, validations));
                    }
                }
                return fieldValidations;
            }
        }
        
        public List<(PropertyInfo Property, List<ValidationAttribute> Validations)> PropertyValidations
        {
            get
            {
                if (propertyValidations == null)
                {
                    propertyValidations = new();
                    var properties = Type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                    
                    foreach (var property in properties)
                    {
                        // Get the validation attributes for the property
                        var validations = property.GetCustomAttributes(typeof(ValidationAttribute), true)
                            .Cast<ValidationAttribute>()
                            .ToList();
                        
                        // If there are no validations, skip this property
                        if (validations.Count == 0)
                        {
                            continue;
                        }
                        
                        // Add the property and its validations to the list
                        propertyValidations.Add((property, validations));
                    }
                }
                return propertyValidations;
            }
        }
    }
    
    private static Dictionary<Type, CachedValidations> _cachedValidations = new();

    private static int cacheSize = 32;
    
    private static List<Type> cacheKeys = new();
    
    public static void SetCacheSize(int size)
    {
        size.ValidateIsInRange(1, int.MaxValue, nameof(size), true, true);
        
        cacheSize = size;
    }

    public static void ClearCache()
    {
        cacheKeys.Clear();
        _cachedValidations.Clear();
    }
    
    [DebuggerStepThrough]
    public static bool CheckIsValidClass<T>(this T? instance) where T : class
    {
        var isValid = CheckIsValid(instance);
        return isValid;
    }
    
    public static bool CheckIsValidStruct<T>(this T? instance) where T : struct
    {
        var isValid = CheckIsValid(instance);
        return isValid;
    }
    
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
            var oldestKey = cacheKeys[0];
            _cachedValidations.Remove(oldestKey);
            cacheKeys.RemoveAt(0);
        }
        
        return typeValidations;
    }

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
        foreach (var (field, validations) in typeValidations.FieldValidations)
        {
            foreach (var attribute in validations)
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
        foreach (var (property, validations) in typeValidations.PropertyValidations)
        {
            foreach (var attribute in validations)
            {
                // Validate the value using each validation attribute
                var result = attribute.Check(property.GetValue(instance));
                if (!result)
                {
                    return false;
                }
            }
        }

        return true;
    }
    
    [DebuggerStepThrough]
    public static T ValidateIsValidClass<T>(this T instance, Blackboard? blackboard = null) where T : class
    {
        var exceptions = ValidateObject(instance, nameof(instance));
        if (exceptions.Count > 0)
        {
            throw new ValidationException("IsValidClass", nameof(instance), $"Validation failed for {typeof(T).Name}", blackboard, exceptions);
        }
        
        return instance;
    }
    
    [DebuggerStepThrough]
    public static T ValidateIsValidStruct<T>(this T instance, Blackboard? blackboard = null) where T : struct
    {
        var exceptions = ValidateObject(instance, nameof(instance));
        if (exceptions.Count > 0)
        {
            throw new ValidationException("IsValidStruct", nameof(instance), $"Validation failed for {typeof(T).Name}", blackboard, exceptions);
        }
        
        return instance;
    }
    
    [DebuggerStepThrough]
    private static Dictionary<string, object?> ValidateObject(object? instance, string propertyName)
    {
        Dictionary<string, object?> exceptions = new();
        CachedValidations typeValidations = GetCachedValidations(instance);
        
        // Validate the instance against the cached validations
        foreach (ValidationAttribute attribute in typeValidations.InstanceValidations)
        {
            // Validate the value using each validation attribute
            var exception = attribute.SafeValidate(instance, propertyName);
            if (exception != null)
            {
                exceptions.Add($"{propertyName}->{attribute.GetType().Name}", exception);
            }
        }
        
        // Validate the fields
        foreach (var (field, validations) in typeValidations.FieldValidations)
        {
            foreach (var attribute in validations)
            {
                // Validate the value using each validation attribute
                var exception = attribute.SafeValidate(field.GetValue(instance), $"{propertyName}.{field.Name}");
                if (exception != null)
                {
                    exceptions.Add($"{propertyName}.{field.Name}->{attribute.GetType().Name}", exception);
                }
            }
        }
        
        // Validate the properties
        foreach (var (property, validations) in typeValidations.PropertyValidations)
        {
            foreach (var attribute in validations)
            {
                // Validate the value using each validation attribute
                var exception = attribute.SafeValidate(property.GetValue(instance), $"{propertyName}.{property.Name}");
                if (exception != null)
                {
                    exceptions.Add($"{propertyName}.{property.Name}->{attribute.GetType().Name}", exception);
                }
            }
        }

        return exceptions;
    }
}
