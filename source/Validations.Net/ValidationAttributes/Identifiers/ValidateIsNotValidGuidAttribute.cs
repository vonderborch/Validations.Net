using SimpleBlackboard.Net;
using Validations.Net.Validators.Identifiers;

namespace Validations.Net.ValidationAttributes.Identifiers;

/// <summary>
/// Validates that the decorated member's value is not a valid GUID string.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsNotValidGuidAttribute() : ValidationAttribute(IsNotValidGuid.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
            return s.CheckIsNotValidGuid() ? ValidationResult.CreateFromValidationSuccess() : Fail(value, memberName, blackboard);
        return ValidationResult.CreateFromValidationSuccess();
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = Message ?? IsNotValidGuid.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
