using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates if a string or collection has a specific length.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateIsLengthAttribute : ValidationAttribute
{
    /// <summary>
    ///     Initializes a new instance of the ValidateIsLengthAttribute class.
    /// </summary>
    /// <param name="length">The expected length to validate against.</param>
    /// <exception cref="ValidationException">Thrown when length is less than 0.</exception>
    public ValidateIsLengthAttribute(int length) : base("IsLength")
    {
        length.ValidateIsGreaterThanOrEquals(0, nameof(length));
        this.Length = length;
    }

    /// <summary>
    ///     Gets the expected length to validate against.
    /// </summary>
    public int Length { get; }

    /// <summary>
    /// Checks if the specified value satisfies the validation criteria.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="instance">The instance containing the value being validated.</param>
    /// <returns>True if the value meets the validation criteria; otherwise, false.</returns>
    public override bool Check(object? value, object? instance)
    {
        return value switch
        {
            null => ((string?)value).CheckIsLength(this.Length),
            string str => str.CheckIsLength(this.Length),
            _ => false
        };
    }

    /// <summary>
    /// Validates the provided value against the defined length constraints.
    /// </summary>
    /// <param name="value">The value to be validated, which can be null or a string.</param>
    /// <param name="instance">The object instance containing the property being validated.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">An optional blackboard instance for additional context in validation.</param>
    /// <returns>A ValidationResult indicating whether the validation was successful or failed.</returns>
    public override ValidationResult Validate(object? value, object? instance, string propertyName,
        IBlackboard? blackboard = null)
    {
        return value switch
        {
            null => ((string?)value).ValidateIsLength(this.Length, propertyName, blackboard),
            string str => str.ValidateIsLength(this.Length, propertyName, blackboard),
            _ => new ValidationResult(ValidationException.CreateFromTypeMisMatch<string>("IsLength", propertyName, value, blackboard))
        };
    }
}
