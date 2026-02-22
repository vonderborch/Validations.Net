using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value does not equal the default value for its type.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateIsNotDefaultAttribute() : ValidationAttribute(IsNotDefault.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value.CheckIsNotDefault())
            return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsNotDefault.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(
            Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
