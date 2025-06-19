using System.Reflection;

namespace Validations.Net.ValidationAttributes.Helpers;

/// <summary>
/// Contains validation metadata for a specific type, including validators for the type itself
/// and its members (fields and properties).
/// </summary>
/// <remarks>
/// This struct caches validation information for a type to avoid repeated reflection operations
/// when validating multiple instances of the same type.
/// </remarks>
public readonly struct TypeValidationInfo
{
    /// <summary>
    /// The collection of field validators for this type.
    /// </summary>
    private readonly List<FieldValidationInfo> _fieldValidators;

    /// <summary>
    /// The collection of property validators for this type.
    /// </summary>
    private readonly List<PropertyValidationInfo> _propertyValidators;

    /// <summary>
    /// The collection of validation attributes applied to the type itself.
    /// </summary>
    private readonly List<ValidationAttribute> _instanceValidators;

    /// <summary>
    /// Gets the type for which validation information is stored.
    /// </summary>
    /// <value>The <see cref="System.Type"/> that this validation info describes.</value>
    public Type Type { get; }

    /// <summary>
    /// Gets the collection of validation attributes applied to fields in this type.
    /// </summary>
    public IReadOnlyList<FieldValidationInfo> FieldValidations => _fieldValidators;

    /// <summary>
    /// Gets the collection of validation attributes applied to properties in this type.
    /// </summary>
    public IReadOnlyList<PropertyValidationInfo> PropertyValidations => _propertyValidators;

    /// <summary>
    /// Gets the collection of validation attributes applied to the type itself.
    /// </summary>
    public IReadOnlyList<ValidationAttribute> InstanceValidations => _instanceValidators;

    /// <summary>
    /// Initializes a new instance of the <see cref="TypeValidationInfo"/> struct with validation information for the specified type.
    /// </summary>
    /// <param name="type">The type to extract validation information from.</param>
    /// <remarks>
    /// The constructor collects all validation attributes applied to the type itself and its members (fields and properties).
    /// </remarks>
    public TypeValidationInfo(Type type)
    {
        Type = type;
        this._instanceValidators = GetInstanceValidators(type);
        this._fieldValidators = GetFieldValidators(type);
        this._propertyValidators = GetPropertyValidators(type);
    }

    /// <summary>
    /// Retrieves all validation attributes applied directly to the type itself.
    /// </summary>
    /// <param name="type">The type to inspect for validation attributes.</param>
    /// <returns>A list of validation attributes applied to the type.</returns>
    private List<ValidationAttribute> GetInstanceValidators(Type type)
    {
        List<ValidationAttribute> output = type.GetCustomAttributes(typeof(ValidationAttribute), true)
            .Cast<ValidationAttribute>()
            .ToList();
        return output;
    }

    /// <summary>
    /// Retrieves validation information for all fields in the specified type that have validation attributes.
    /// </summary>
    /// <param name="type">The type to inspect for fields with validation attributes.</param>
    /// <returns>A list of <see cref="FieldValidationInfo"/> objects containing field validation metadata.</returns>
    private List<FieldValidationInfo> GetFieldValidators(Type type)
    {
        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                            BindingFlags.Static);

        List<FieldValidationInfo> output = new();
        foreach (FieldInfo field in fields)
        {
            List<ValidationAttribute> validations = field
                .GetCustomAttributes(typeof(ValidationAttribute), true)
                .Cast<ValidationAttribute>()
                .ToList();

            if (validations.Count == 0)
            {
                continue;
            }

            output.Add(new FieldValidationInfo(field, field.IsPublic, validations));
        }

        return output;
    }

    /// <summary>
    /// Retrieves validation information for all properties in the specified type that have validation attributes.
    /// </summary>
    /// <param name="type">The type to inspect for properties with validation attributes.</param>
    /// <returns>A list of <see cref="PropertyValidationInfo"/> objects containing property validation metadata.</returns>
    private List<PropertyValidationInfo> GetPropertyValidators(Type type)
    {
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                            BindingFlags.Static);

        List<PropertyValidationInfo> output = new();
        foreach (PropertyInfo property in properties)
        {
            MethodInfo? getMethod = property.GetMethod;
            if (getMethod is null)
            {
                continue;
            }
            
            List<ValidationAttribute> validations = property
                .GetCustomAttributes(typeof(ValidationAttribute), true)
                .Cast<ValidationAttribute>()
                .ToList();

            if (validations.Count == 0)
            {
                continue;
            }

            output.Add(new(property, getMethod.IsPublic, validations));
        }

        return output;
    }

    /// <summary>
    /// Validates the instance of the specified type against its validation attributes, including fields and properties.
    /// </summary>
    /// <typeparam name="T">The type of the instance to validate.</typeparam>
    /// <param name="instance">The instance of the type to validate. Can be null.</param>
    /// <param name="includePrivateFields">Indicates whether private fields should be included in the validation process.</param>
    /// <param name="includePrivateProperties">Indicates whether private properties should be included in the validation process.</param>
    /// <returns>
    /// true if the instance satisfies all validation requirements; otherwise, false.
    /// </returns>
    public bool CheckInstance<T>(T? instance, bool includePrivateFields, bool includePrivateProperties)
    {
        // Validate the instance attributes
        foreach (ValidationAttribute attribute in this._instanceValidators)
        {
            if (!attribute.Check(instance, instance))
            {
                return false;
            }
        }

        var relevantFields = includePrivateFields
            ? this._fieldValidators
            : this._fieldValidators.Where(x => x.IsPublic).ToList();
        var relevantProperties = includePrivateProperties
            ? this._propertyValidators
            : this._propertyValidators.Where(x => x.IsPublic).ToList();
        
        // return early if the instance is null...we can't validate anything else!
        if (instance is null)
        {
            return relevantFields.Count == 0 && relevantProperties.Count == 0;
        }
        
        // Validate fields
        foreach (FieldValidationInfo field in relevantFields)
        {
            object? fieldValue = field.Field.GetValue(instance);
            foreach (ValidationAttribute attribute in field.Validators)
            {
                if (!attribute.Check(fieldValue, instance))
                {
                    return false;
                }
            }
        }
        
        // Validate properties
        foreach (PropertyValidationInfo property in relevantProperties)
        {
            object? propertyValue = property.Property.GetValue(instance);
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
    /// Validates the provided instance using the associated validation attributes, including fields and properties,
    /// and returns any validation errors encountered.
    /// </summary>
    /// <typeparam name="T">The type of the instance to validate.</typeparam>
    /// <param name="instance">The instance to validate. Can be null.</param>
    /// <param name="includePrivateFields">Specifies whether to include private fields in the validation process.</param>
    /// <param name="includePrivateProperties">Specifies whether to include private properties in the validation process.</param>
    /// <returns>A dictionary where the keys are the names of the members that failed validation, and the values are the corresponding validation exceptions.</returns>
    public Dictionary<string, Exception> ValidateInstance<T>(T? instance, bool includePrivateFields,
        bool includePrivateProperties)
    {
        Dictionary<string, Exception> exceptions = new();
        var instanceName = nameof(instance);

        // Validate the instance attributes
        foreach (ValidationAttribute attribute in this._instanceValidators)
        {
            Exception? exception = attribute.SafeValidate(instance, instance, nameof(instance));
            if (exception is not null)
            {
                exceptions[$"{instanceName}->{attribute.ValidatorName}"] = exception;
            }
        }

        var relevantFields = includePrivateFields
            ? this._fieldValidators
            : this._fieldValidators.Where(x => x.IsPublic).ToList();
        var relevantProperties = includePrivateProperties
            ? this._propertyValidators
            : this._propertyValidators.Where(x => x.IsPublic).ToList();
        
        // Validate fields
        foreach (FieldValidationInfo field in relevantFields)
        {
            object? fieldValue = instance is not null ? field.Field.GetValue(instance) : null;
            var fieldName = $"{instanceName}.{field.Field.Name}";
            foreach (ValidationAttribute attribute in field.Validators)
            {
                Exception? exception;
                if (instance is null)
                {
                    exception = new NullReferenceException("The instance being validated is null");
                }
                else
                {
                    exception = attribute.SafeValidate(fieldValue, instance, fieldName);
                }

                if (exception is not null)
                {
                    exceptions[$"{fieldName}->{attribute.ValidatorName}"] = exception;
                }
            }
        }
        
        // Validate properties
        foreach (PropertyValidationInfo property in relevantProperties)
        {
            object? propertyValue = instance is not null ? property.Property.GetValue(instance) : null;
            var propertyName = $"{instanceName}.{property.Property.Name}";
            foreach (ValidationAttribute attribute in property.Validators)
            {
                Exception? exception;
                if (instance is null)
                {
                    exception = new NullReferenceException("The instance being validated is null");
                }
                else
                {
                    exception = attribute.SafeValidate(propertyValue, instance, propertyName);
                }

                if (exception is not null)
                {
                    exceptions[$"{propertyName}->{attribute.ValidatorName}"] = exception;
                }
            }
        }

        return exceptions;
    }
}
