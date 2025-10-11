using SimpleBlackboard.Net;
using Validations.Net.OLD.Validators;

namespace Validations.Net.OLD.ValidationAttributes;

/// <summary>
///     Attribute that validates if a string or collection does not have a specific length.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateIsNotLengthAttribute<T> : ValidationAttribute
{
    /// <summary>
    ///     Initializes a new instance of the ValidateIsNotLengthAttribute class.
    /// </summary>
    /// <param name="length">The length to validate against.</param>
    /// <exception cref="ValidationException">Thrown when length is less than 0.</exception>
    public ValidateIsNotLengthAttribute(int length) : base("IsNotLength")
    {
        length.ValidateIsGreaterThanOrEquals(0, nameof(length));
        this.Length = length;
    }

    /// <summary>
    ///     Gets the length to validate against.
    /// </summary>
    public int Length { get; }

    /// <summary>
    ///     Checks if the provided value does not have the specified length.
    /// </summary>
    /// <param name="value">
    ///     The value to check. Must be a string or ICollection<object?>.
    /// </param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <returns>True if the value does not have the specified length, false otherwise.</returns>
    /// <exception cref="ValidationException">
    ///     Thrown when the value is not a string or ICollection<object?>.
    /// </exception>
    public override bool Check(object? value, object? instance)
    {
        return value switch
        {
            null => ((string?)value).CheckIsNotLength(this.Length),
            string str => str.CheckIsNotLength(this.Length),
            //<object?> collection => collection.CheckIsNotLength(this.Length),
            _ => throw ValidationException.CreateFromTypeMisMatch<object>("IsNotLength", nameof(value), value)
        };
    }

    /// <summary>
    ///     Validates if the provided value does not have the specified length and throws a ValidationException if it does.
    /// </summary>
    /// <param name="value">
    ///     The value to validate. Must be a string or ICollection<object?>.
    /// </param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context.</param>
    /// <exception cref="ValidationException">
    ///     Thrown when the value has the specified length or is not a string or ICollection<object?>.
    /// </exception>
    public override ValidationResult Validate(object? value, object? instance, string propertyName, IBlackboard? blackboard = null)
    {
        switch (value)
        {
            case null:
                ((string?)value).ValidateIsNotLength(this.Length, propertyName, blackboard);
                break;
            case string str:
                str.ValidateIsNotLength(this.Length, propertyName, blackboard);
                break;
            case ICollection<object?> collection:
                //collection.ValidateIsNotLength(this.Length, propertyName, blackboard);
                break;
            default:
                throw ValidationException.CreateFromTypeMisMatch<object>("IsNotLength", propertyName, value,
                    blackboard);
        }

        return new ValidationResult();
    }
}
