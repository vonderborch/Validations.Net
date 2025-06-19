using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates if a value is not one of a specified set of options.
/// </summary>
/// <typeparam name="T">The type of the value to validate.</typeparam>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateIsNotWithinAttribute<T> : ValidationAttribute
{
    private readonly ICollection<T> _options;

    /// <summary>
    ///     Initializes a new instance of the ValidateIsNotWithinAttribute class with an array of options.
    /// </summary>
    /// <param name="options">The array of invalid options.</param>
    /// <exception cref="ValidationException">Thrown when options is null or empty.</exception>
    public ValidateIsNotWithinAttribute(params T[] options) : base("IsNotWithin")
    {
        var paramName = nameof(options);
        options.ValidateIsNotNull(paramName).ValidateIsNotEmpty(paramName);
        this._options = options;
    }

    /// <summary>
    ///     Initializes a new instance of the ValidateIsNotWithinAttribute class with a collection of options.
    /// </summary>
    /// <param name="options">The collection of invalid options.</param>
    /// <exception cref="ValidationException">Thrown when options is null or empty.</exception>
    public ValidateIsNotWithinAttribute(ICollection<T> options) : base("IsNotWithin")
    {
        var paramName = nameof(options);
        options.ValidateIsNotNull(paramName).ValidateIsNotEmpty(paramName);
        this._options = options;
    }

    /// <summary>
    ///     Checks if the provided value is not one of the invalid options.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <returns>True if the value is not one of the invalid options, false otherwise.</returns>
    public override bool Check(object? value, object? instance)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        return typedValue.CheckIsNotWithin(this._options);
    }

    /// <summary>
    ///     Validates if the provided value is not one of the invalid options and throws a ValidationException if it is.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context.</param>
    /// <exception cref="ValidationException">Thrown when the value is one of the invalid options.</exception>
    public override void Validate(object? value, object? instance, string propertyName, Blackboard? blackboard = null)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        typedValue.ValidateIsNotWithin(this._options, propertyName, blackboard);
    }
}
