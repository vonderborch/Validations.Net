using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Attribute that validates if a string or collection has a specific length.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateIsLengthAttribute : ValidationAttribute
{
    /// <summary>
    /// Initializes a new instance of the ValidateIsLengthAttribute class.
    /// </summary>
    /// <param name="length">The expected length to validate against.</param>
    /// <exception cref="ValidationException">Thrown when length is less than 0.</exception>
    public ValidateIsLengthAttribute(int length) : base("IsLength")
    {
        length.ValidateIsGreaterThanOrEquals(0, nameof(length));
        Length = length;
    }
    
    /// <summary>
    /// Gets the expected length to validate against.
    /// </summary>
    public int Length { get; }
    
    /// <summary>
    /// Checks if the provided value has the expected length.
    /// </summary>
    /// <param name="value">The value to check. Must be a string or ICollection<object?>.</param>
    /// <returns>True if the value has the expected length, false otherwise.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a string or ICollection<object?>.</exception>
    public override bool Check(object? value)
    {
        return value switch
        {
            string str => str.CheckIsLength(Length),
            ICollection<object?> collection => collection.CheckIsLength(Length),
            _ => throw ValidationException.CreateFromTypeMisMatch<object>("IsLength", nameof(value), value)
        };
    }
    
    /// <summary>
    /// Validates if the provided value has the expected length and throws a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The value to validate. Must be a string or ICollection<object?>.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context.</param>
    /// <exception cref="ValidationException">Thrown when the value does not have the expected length or is not a string or ICollection<object?>.</exception>
    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        switch (value)
        {
            case string str:
                str.ValidateIsLength(Length, propertyName, blackboard);
                break;
            case ICollection<object?> collection:
                collection.ValidateIsLength(Length, propertyName, blackboard);
                break;
            default:
                throw ValidationException.CreateFromTypeMisMatch<object>("IsLength", propertyName, value, blackboard);
        }
    }
}
