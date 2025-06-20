using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates if a string or collection has a specific length.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateIsLengthAttribute<T> : ValidationAttribute where T : ICollection<T>
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
    /// Validates whether the provided value satisfies the required length condition.
    /// </summary>
    /// <param name="value">The value to be validated, expected to be a collection.</param>
    /// <param name="instance">The instance containing the value.</param>
    /// <returns>
    /// <c>true</c> if the value is a collection and its length matches the required length; otherwise, <c>false</c>.
    /// </returns>
    public override bool Check(object? value, object? instance)
    {
        return value switch
        {
            ICollection<T> collection => collection.CheckIsLength(this.Length),
            _ => false
        };
    }

    /// <summary>
    /// Validates the given value based on the attribute's validation logic.
    /// </summary>
    /// <param name="value">The value to validate. This can be null.</param>
    /// <param name="instance">The instance containing the property or field being validated.</param>
    /// <param name="propertyName">The name of the property or field that is being validated.</param>
    /// <param name="blackboard">Optional parameter for passing a Blackboard instance for additional contextual data.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the outcome of the validation process.</returns>
    public override ValidationResult Validate(object? value, object? instance, string propertyName,
        Blackboard? blackboard = null)
    {
        return value switch
        {
            ICollection<T> collection => collection.ValidateIsLength(this.Length, propertyName, blackboard),
            _ => new ValidationResult(ValidationException.CreateFromTypeMisMatch<ICollection<T>>("IsLength", propertyName, value, blackboard))
        };
    }
}
