using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Reflection;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.ValidationAttributes.Helpers;

/// <summary>
///     Contains validation metadata for a specific type, including validators for the type itself
///     and its members (fields and properties).
/// </summary>
/// <remarks>
///     This struct caches validation information for a type to avoid repeated reflection operations
///     when validating multiple instances of the same type.
/// </remarks>
public readonly struct TypeValidationInfo
{
    /// <summary>
    ///     Static cache of type validation info to avoid reflection overhead for commonly validated types
    /// </summary>
    private static readonly ConcurrentDictionary<Type, TypeValidationInfo> _typeValidationCache = new();

    /// <summary>
    ///     Gets the type for which validation information is stored.
    /// </summary>
    public Type Type { get; }

    /// <summary>
    ///     Gets the collection of validation attributes applied to the type itself.
    /// </summary>
    public ReadOnlyCollection<ValidationAttribute> InstanceValidations { get; }

    /// <summary>
    ///     Gets the collection of validation attributes applied to fields in this type.
    /// </summary>
    public ReadOnlyCollection<FieldValidationInfo> FieldValidations { get; }

    /// <summary>
    ///     Gets the collection of validation attributes applied to properties in this type.
    /// </summary>
    public ReadOnlyCollection<PropertyValidationInfo> PropertyValidations { get; }

    /// <summary>
    ///     Gets pre-filtered collections of public-only fields and properties to avoid filtering during validation
    /// </summary>
    private readonly ReadOnlyCollection<FieldValidationInfo> _publicFieldValidations;

    private readonly ReadOnlyCollection<PropertyValidationInfo> _publicPropertyValidations;

    /// <summary>
    ///     Creates or retrieves a cached TypeValidationInfo for the specified type.
    /// </summary>
    /// <param name="type">The type to get validation info for</param>
    /// <returns>A TypeValidationInfo instance for the specified type</returns>
    public static TypeValidationInfo For(Type type)
    {
        return _typeValidationCache.GetOrAdd(type, t => new TypeValidationInfo(t));
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="TypeValidationInfo" /> struct with validation information for the
    ///     specified type.
    /// </summary>
    /// <param name="type">The type to extract validation information from.</param>
    public TypeValidationInfo(Type type)
    {
        this.Type = type;

        // Extract validations using reflection and make them read-only for thread safety
        this.InstanceValidations = ExtractInstanceValidators(type).AsReadOnly();
        this.FieldValidations = ExtractFieldValidators(type).AsReadOnly();
        this.PropertyValidations = ExtractPropertyValidators(type).AsReadOnly();

        // Pre-filter public fields and properties for performance
        this._publicFieldValidations = this.FieldValidations
            .Where(x => x.IsPublic)
            .ToList()
            .AsReadOnly();

        this._publicPropertyValidations = this.PropertyValidations
            .Where(x => x.IsPublic)
            .ToList()
            .AsReadOnly();
    }

    /// <summary>
    ///     Retrieves all validation attributes applied directly to the type itself.
    /// </summary>
    private static List<ValidationAttribute> ExtractInstanceValidators(Type type)
    {
        return type.GetCustomAttributes(typeof(ValidationAttribute), true)
            .Cast<ValidationAttribute>()
            .ToList();
    }

    /// <summary>
    ///     Retrieves validation information for all fields in the specified type that have validation attributes.
    /// </summary>
    private static List<FieldValidationInfo> ExtractFieldValidators(Type type)
    {
        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic |
                                            BindingFlags.Instance | BindingFlags.Static);

        List<FieldValidationInfo> result = new();
        foreach (FieldInfo field in fields)
        {
            List<ValidationAttribute> validations = field
                .GetCustomAttributes(typeof(ValidationAttribute), true)
                .Cast<ValidationAttribute>()
                .ToList();

            if (validations.Count > 0)
            {
                result.Add(new FieldValidationInfo(field, field.IsPublic, validations));
            }
        }

        return result;
    }

    /// <summary>
    ///     Retrieves validation information for all properties in the specified type that have validation attributes.
    /// </summary>
    private static List<PropertyValidationInfo> ExtractPropertyValidators(Type type)
    {
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic |
                                                       BindingFlags.Instance | BindingFlags.Static);

        List<PropertyValidationInfo> result = new();
        foreach (PropertyInfo property in properties)
        {
            // Skip properties without a getter
            if (property.GetMethod is null)
            {
                continue;
            }

            List<ValidationAttribute> validations = property
                .GetCustomAttributes(typeof(ValidationAttribute), true)
                .Cast<ValidationAttribute>()
                .ToList();

            if (validations.Count > 0)
            {
                result.Add(new PropertyValidationInfo(property, property.GetMethod.IsPublic, validations));
            }
        }

        return result;
    }

    /// <summary>
    ///     Validates the instance of the specified type against its validation attributes, including fields and properties.
    /// </summary>
    /// <typeparam name="T">The type of the instance to validate.</typeparam>
    /// <param name="instance">The instance of the type to validate. Can be null.</param>
    /// <param name="includePrivateFields">Indicates whether private fields should be included in the validation process.</param>
    /// <param name="includePrivateProperties">
    ///     Indicates whether private properties should be included in the validation
    ///     process.
    /// </param>
    /// <returns>
    ///     true if the instance satisfies all validation requirements; otherwise, false.
    /// </returns>
    public bool CheckInstance<T>(T? instance, bool includePrivateFields, bool includePrivateProperties)
    {
        // Check if instance attributes pass validation
        if (!CheckInstanceAttributes(instance))
        {
            return false;
        }

        // Get the appropriate collections based on visibility settings
        ReadOnlyCollection<FieldValidationInfo>? fieldsToValidate =
            includePrivateFields ? this.FieldValidations : this._publicFieldValidations;
        ReadOnlyCollection<PropertyValidationInfo>? propertiesToValidate =
            includePrivateProperties ? this.PropertyValidations : this._publicPropertyValidations;

        // Early return if instance is null but we have field/property validators
        if (instance is null)
        {
            return fieldsToValidate.Count == 0 && propertiesToValidate.Count == 0;
        }

        // Check fields and properties
        return CheckFields(instance, fieldsToValidate) &&
               CheckProperties(instance, propertiesToValidate);
    }

    /// <summary>
    ///     Validates an instance against type-level validation attributes.
    /// </summary>
    /// <param name="instance">The instance to validate.</param>
    /// <returns>true if all type-level validators pass; otherwise, false.</returns>
    private bool CheckInstanceAttributes<T>(T? instance)
    {
        foreach (ValidationAttribute attribute in this.InstanceValidations)
        {
            if (!attribute.Check(instance, instance))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Validates all fields of an instance against their validation attributes.
    /// </summary>
    /// <param name="instance">The instance whose fields to validate.</param>
    /// <param name="fieldsToValidate">Collection of field validation info to use.</param>
    /// <returns>true if all field validators pass; otherwise, false.</returns>
    private bool CheckFields<T>(T instance, ReadOnlyCollection<FieldValidationInfo> fieldsToValidate)
    {
        foreach (FieldValidationInfo field in fieldsToValidate)
        {
            var fieldValue = field.Field.GetValue(instance);
            foreach (ValidationAttribute attribute in field.Validators)
            {
                if (!attribute.Check(fieldValue, instance))
                {
                    return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    ///     Validates all properties of an instance against their validation attributes.
    /// </summary>
    /// <param name="instance">The instance whose properties to validate.</param>
    /// <param name="propertiesToValidate">Collection of property validation info to use.</param>
    /// <returns>true if all property validators pass; otherwise, false.</returns>
    private bool CheckProperties<T>(T instance, ReadOnlyCollection<PropertyValidationInfo> propertiesToValidate)
    {
        foreach (PropertyValidationInfo property in propertiesToValidate)
        {
            var propertyValue = property.Property.GetValue(instance);
            foreach (ValidationAttribute attribute in property.Validators)
            {
                if (!attribute.Check(propertyValue, instance))
                {
                    return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// Validates the specified instance against the type's defined validation rules.
    /// </summary>
    /// <param name="instance">The instance of the type to validate.</param>
    /// <param name="includePrivateFields">Indicates whether private fields should be included in the validation.</param>
    /// <param name="includePrivateProperties">Indicates whether private properties should be included in the validation.</param>
    /// <param name="blackboard">An optional Blackboard instance for supplemental validation context or data.</param>
    /// <typeparam name="T">The type of the instance being validated.</typeparam>
    /// <returns>A dictionary containing field/property names as keys and exceptions as values for all validation errors.</returns>
    public Dictionary<string, Exception> ValidateInstance<T>(T? instance, bool includePrivateFields,
        bool includePrivateProperties, IBlackboard? blackboard)
    {
        Dictionary<string, Exception> exceptions = new();
        var instanceName = nameof(instance);

        // Get pre-filtered collections for better performance
        ReadOnlyCollection<FieldValidationInfo>? fieldsToValidate =
            includePrivateFields ? this.FieldValidations : this._publicFieldValidations;
        ReadOnlyCollection<PropertyValidationInfo>? propertiesToValidate =
            includePrivateProperties ? this.PropertyValidations : this._publicPropertyValidations;

        // Validate type instance attributes
        ValidateTypeAttributes(instance, instanceName, exceptions, blackboard);

        // Handle null instance specially
        if (instance is null)
        {
            AddNullInstanceExceptions(instanceName, fieldsToValidate, propertiesToValidate, exceptions);
            return exceptions;
        }

        // Validate fields and properties
        ValidateFields(instance, instanceName, fieldsToValidate, exceptions, blackboard);
        ValidateProperties(instance, instanceName, propertiesToValidate, exceptions, blackboard);

        return exceptions;
    }

    /// <summary>
    ///     Validates an instance against type-level validation attributes and adds any exceptions to the collection.
    /// </summary>
    private void ValidateTypeAttributes<T>(T? instance, string instanceName, Dictionary<string, Exception> exceptions, IBlackboard? blackboard)
    {
        foreach (ValidationAttribute attribute in this.InstanceValidations)
        {
            ValidationResult result = attribute.Validate(instance, instance, instanceName, blackboard);
            if (!result.IsValid)
            {
                exceptions[$"{instanceName}->{attribute.ValidatorName}"] = result.ValidationException!;
            }
        }
    }

    /// <summary>
    ///     Adds null reference exceptions for all field and property validators when the instance is null.
    /// </summary>
    private void AddNullInstanceExceptions(
        string instanceName,
        ReadOnlyCollection<FieldValidationInfo> fieldsToValidate,
        ReadOnlyCollection<PropertyValidationInfo> propertiesToValidate,
        Dictionary<string, Exception> exceptions)
    {
        NullReferenceException nullRefException = new("The instance being validated is null");

        // Add exceptions for fields
        foreach (FieldValidationInfo field in fieldsToValidate)
        {
            foreach (ValidationAttribute attribute in field.Validators)
            {
                var fieldName = $"{instanceName}.{field.Field.Name}";
                exceptions[$"{fieldName}->{attribute.ValidatorName}"] = nullRefException;
            }
        }

        // Add exceptions for properties
        foreach (PropertyValidationInfo property in propertiesToValidate)
        {
            foreach (ValidationAttribute attribute in property.Validators)
            {
                var propertyName = $"{instanceName}.{property.Property.Name}";
                exceptions[$"{propertyName}->{attribute.ValidatorName}"] = nullRefException;
            }
        }
    }

    /// <summary>
    ///     Validates all fields of an instance and adds any validation exceptions to the collection.
    /// </summary>
    private void ValidateFields<T>(
        T instance,
        string instanceName,
        ReadOnlyCollection<FieldValidationInfo> fieldsToValidate,
        Dictionary<string, Exception> exceptions, IBlackboard? blackboard)
    {
        foreach (FieldValidationInfo field in fieldsToValidate)
        {
            var fieldValue = field.Field.GetValue(instance);
            var fieldName = $"{instanceName}.{field.Field.Name}";

            foreach (ValidationAttribute attribute in field.Validators)
            {
                ValidationResult result = attribute.Validate(fieldValue, instance, fieldName, blackboard);
                if (!result.IsValid)
                {
                    exceptions[$"{fieldName}->{attribute.ValidatorName}"] = result.ValidationException!;
                }
            }
        }
    }

    /// <summary>
    ///     Validates all properties of an instance and adds any validation exceptions to the collection.
    /// </summary>
    private void ValidateProperties<T>(
        T instance,
        string instanceName,
        ReadOnlyCollection<PropertyValidationInfo> propertiesToValidate,
        Dictionary<string, Exception> exceptions, IBlackboard? blackboard)
    {
        foreach (PropertyValidationInfo property in propertiesToValidate)
        {
            var propertyValue = property.Property.GetValue(instance);
            var propertyName = $"{instanceName}.{property.Property.Name}";

            foreach (ValidationAttribute attribute in property.Validators)
            {
                ValidationResult result = attribute.Validate(propertyValue, instance, propertyName, blackboard);
                if (!result.IsValid)
                {
                    exceptions[$"{propertyName}->{attribute.ValidatorName}"] = result.ValidationException!;
                }
            }
        }
    }
}
