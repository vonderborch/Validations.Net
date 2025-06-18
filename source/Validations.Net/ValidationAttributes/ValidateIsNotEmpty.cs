using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates if a value is not empty.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateIsNotEmpty() : ValidationAttribute("IsNotEmpty")
{
    /// <summary>
    ///     Checks if the provided value is not empty.
    /// </summary>
    /// <param name="value">
    ///     The value to check. Must be a string or ICollection<object?>.
    /// </param>
    /// <returns>True if the value is not empty, false otherwise.</returns>
    /// <exception cref="ValidationException">
    ///     Thrown when the value is not a string or ICollection<object?>.
    /// </exception>
    public override bool Check(object? value)
    {
        return value switch
        {
            string str => str.CheckIsNotEmpty(),
            ICollection<object?> collection => collection.CheckIsNotEmpty(),
            _ => throw ValidationException.CreateFromTypeMisMatch<object>("IsNotEmpty", nameof(value), value)
        };
    }

    /// <summary>
    ///     Validates if the provided value is not empty and throws a ValidationException if it is.
    /// </summary>
    /// <param name="value">
    ///     The value to validate. Must be a string or ICollection<object?>.
    /// </param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context.</param>
    /// <exception cref="ValidationException">
    ///     Thrown when the value is empty or is not a string or ICollection<object?>.
    /// </exception>
    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        switch (value)
        {
            case string str:
                str.ValidateIsNotEmpty(propertyName, blackboard);
                break;
            case ICollection<object?> collection:
                collection.ValidateIsNotEmpty(propertyName, blackboard);
                break;
            default:
                throw ValidationException.CreateFromTypeMisMatch<object>("IsNotEmpty", propertyName, value, blackboard);
        }
    }
}
