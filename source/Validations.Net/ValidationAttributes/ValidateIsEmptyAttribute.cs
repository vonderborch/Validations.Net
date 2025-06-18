using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates if a value is empty.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateIsEmptyAttribute() : ValidationAttribute("IsEmpty")
{
    /// <summary>
    ///     Checks if the provided value is empty.
    /// </summary>
    /// <param name="value">
    ///     The value to check. Must be a string or ICollection<object?>.
    /// </param>
    /// <returns>True if the value is empty, false otherwise.</returns>
    /// <exception cref="ValidationException">
    ///     Thrown when the value is not a string or ICollection<object?>.
    /// </exception>
    public override bool Check(object? value)
    {
        return value switch
        {
            string str => str.CheckIsEmpty(),
            ICollection<object?> collection => collection.CheckIsEmpty(),
            _ => throw ValidationException.CreateFromTypeMisMatch<object>("IsEmpty", nameof(value), value)
        };
    }

    /// <summary>
    ///     Validates if the provided value is empty and throws a ValidationException if it is not.
    /// </summary>
    /// <param name="value">
    ///     The value to validate. Must be a string or ICollection<object?>.
    /// </param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context.</param>
    /// <exception cref="ValidationException">
    ///     Thrown when the value is not empty or is not a string or ICollection<object?>.
    /// </exception>
    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        switch (value)
        {
            case string str:
                str.ValidateIsEmpty(propertyName, blackboard);
                break;
            case ICollection<object?> collection:
                collection.ValidateIsEmpty(propertyName, blackboard);
                break;
            default:
                throw ValidationException.CreateFromTypeMisMatch<object>("IsEmpty", propertyName, value, blackboard);
        }
    }
}
