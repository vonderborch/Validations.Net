using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Attribute that validates if a value is not one of a specified set of options.
/// </summary>
/// <typeparam name="T">The type of the value to validate.</typeparam>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateIsNotOneOfAttribute<T> : ValidationAttribute
{
    private readonly ICollection<T> _options;

    /// <summary>
    /// Initializes a new instance of the ValidateIsNotOneOfAttribute class with an array of options.
    /// </summary>
    /// <param name="options">The array of invalid options.</param>
    /// <exception cref="ValidationException">Thrown when options is null or empty.</exception>
    public ValidateIsNotOneOfAttribute(params T[] options) : base("IsNotOneOf")
    {
        string paramName = nameof(options);
        options.ValidateIsNotNull(paramName).ValidateIsNotEmpty(paramName);
        _options = options;
    }

    /// <summary>
    /// Initializes a new instance of the ValidateIsNotOneOfAttribute class with a collection of options.
    /// </summary>
    /// <param name="options">The collection of invalid options.</param>
    /// <exception cref="ValidationException">Thrown when options is null or empty.</exception>
    public ValidateIsNotOneOfAttribute(ICollection<T> options) : base("IsNotOneOf")
    {
        string paramName = nameof(options);
        options.ValidateIsNotNull(paramName).ValidateIsNotEmpty(paramName);
        _options = options;
    }

    /// <summary>
    /// Checks if the provided value is not one of the invalid options.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is not one of the invalid options, false otherwise.</returns>
    public override bool Check(object? value)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        return typedValue.CheckIsNotOneOf(_options);
    }

    /// <summary>
    /// Validates if the provided value is not one of the invalid options and throws a ValidationException if it is.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context.</param>
    /// <exception cref="ValidationException">Thrown when the value is one of the invalid options.</exception>
    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        typedValue.ValidateIsNotOneOf(_options, propertyName, blackboard);
    }
}
