using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value does not exceed the specified maximum length.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateMaxLengthAttribute(int maxLength) : ValidationAttribute(MaxLength.ValidatorName)
{
    private readonly int _maxLength = maxLength;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string str && str.CheckMaxLength(_maxLength))
            return ValidationResult.CreateFromValidationSuccess();
        if (value is not null && value is not string)
            return ValidationResult.CreateFromValidationFailure(Name, "Value is not a string", memberName, blackboard,
                new List<(string key, object? value)> { ("value", value) });
        var message = Message ?? MaxLength.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("maxLength", _maxLength), ("actualLength", value is string s ? s.Length : null) });
    }
}
