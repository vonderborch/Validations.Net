using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value has at least the specified minimum length.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateMinLengthAttribute(int minLength) : ValidationAttribute(MinLength.ValidatorName)
{
    private readonly int _minLength = minLength;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string str && str.CheckMinLength(_minLength))
            return ValidationResult.CreateFromValidationSuccess();
        if (value is not null && value is not string)
            return ValidationResult.CreateFromValidationFailure(Name, "Value is not a string", memberName, blackboard,
                new List<(string key, object? value)> { ("value", value) });
        var message = Message ?? MinLength.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("minLength", _minLength), ("actualLength", value is string s ? s.Length : null) });
    }
}
