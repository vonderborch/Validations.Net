using SimpleBlackboard.Net;
using Validations.Net.OLD.Validators;

namespace Validations.Net.OLD.ValidationAttributes;

/// <summary>
///     Attribute that validates if a string does not consist only of white-space characters.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateIsNotWhiteSpaceAttribute() : ValidationAttribute("IsNotWhiteSpace")
{
    /// <summary>
    ///     Checks if the provided value does not consist only of white-space characters.
    /// </summary>
    /// <param name="value">The value to check. Must be a string.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <returns>True if the value does not consist only of white-space characters, false otherwise.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a string.</exception>
    public override bool Check(object? value, object? instance)
    {
        return value switch
        {
            null => ((string?)value).CheckIsNotWhiteSpace(),
            string str => str.CheckIsNotWhiteSpace(),
            _ => throw ValidationException.CreateFromTypeMisMatch<object>("IsNotWhiteSpace", nameof(value), value)
        };
    }

    /// <summary>
    ///     Validates if the provided value does not consist only of white-space characters and throws a ValidationException if
    ///     it does.
    /// </summary>
    /// <param name="value">The value to validate. Must be a string.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context.</param>
    /// <exception cref="ValidationException">Thrown when the value consists only of white-space characters or is not a string.</exception>
    public override void Validate(object? value, object? instance, string propertyName, IBlackboard? blackboard = null)
    {
        switch (value)
        {
            case string str:
                str.ValidateIsNotWhiteSpace(propertyName, blackboard);
                break;
            default:
                throw ValidationException.CreateFromTypeMisMatch<object>("IsNotWhiteSpace", propertyName, value,
                    blackboard);
        }
    }
}
