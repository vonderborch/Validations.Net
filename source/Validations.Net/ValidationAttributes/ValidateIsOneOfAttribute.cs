using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Attribute that validates if a value is one of a specified set of options.
/// </summary>
/// <typeparam name="T">The type of the value to validate.</typeparam>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateIsOneOfAttribute<T> : ValidationAttribute
{
    private readonly ICollection<T> _options;

    /// <summary>
    /// Initializes a new instance of the ValidateIsOneOfAttribute class with an array of options.
    /// </summary>
    /// <param name="options">The array of valid options.</param>
    /// <exception cref="ValidationException">Thrown when options is null or empty.</exception>
    public ValidateIsOneOfAttribute(params T[] options) : base("IsOneOf")
    {
        string paramName = nameof(options);
        options.ValidateIsNotNull(paramName).ValidateIsNotEmpty(paramName);
        _options = options;
    }

    /// <summary>
    /// Initializes a new instance of the ValidateIsOneOfAttribute class with a collection of options.
    /// </summary>
    /// <param name="options">The collection of valid options.</param>
    /// <exception cref="ValidationException">Thrown when options is null or empty.</exception>
    public ValidateIsOneOfAttribute(ICollection<T> options) : base("IsOneOf")
    {
        string paramName = nameof(options);
        options.ValidateIsNotNull(paramName).ValidateIsNotEmpty(paramName);
        _options = options;
    }

    /// <summary>
    /// Checks if the provided value is one of the valid options.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is one of the valid options, false otherwise.</returns>
    public override bool Check(object? value)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        return typedValue.CheckIsOneOf(_options);
    }

    /// <summary>
    /// Validates if the provided value is one of the valid options and throws a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not one of the valid options.</exception>
    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        typedValue.ValidateIsOneOf(_options, propertyName, blackboard);
    }
}
