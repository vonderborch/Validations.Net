using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates if a value is null.
/// </summary>
[AttributeUsage(
    AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Interface)]
public class ValidateIsNullAttribute() : ValidationAttribute("IsNull")
{
    /// <summary>
    ///     Checks if the provided value is null.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <returns>True if the value is null, false otherwise.</returns>
    public override bool Check(object? value, object? instance)
    {
        var result = value.CheckIsNull();
        return result;
    }

    /// <summary>
    ///     Validates if the provided value is null and throws a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not null.</exception>
    public override void Validate(object? value, object? instance, string propertyName, IBlackboard? blackboard = null)
    {
        value.ValidateIsNull(propertyName, blackboard);
    }
}
