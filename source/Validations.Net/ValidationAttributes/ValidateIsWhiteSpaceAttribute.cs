using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is whitespace.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsWhiteSpaceAttribute() : ValidationAttribute(IsWhiteSpace.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string str && str.CheckIsWhiteSpace())
            return ValidationResult.CreateFromValidationSuccess();
        if (value is not null && value is not string)
            return ValidationResult.CreateFromValidationFailure(Name, "Value is not a string", memberName, blackboard,
                new List<(string key, object? value)> { ("value", value) });
        var message = Message ?? IsWhiteSpace.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
