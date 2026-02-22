using SimpleBlackboard.Net;
using Validations.Net.Validators.Identifiers;

namespace Validations.Net.ValidationAttributes.Identifiers;

/// <summary>
/// Validates that the decorated member's value is a valid identifier string.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsValidIdAttribute(int minLength = 1, int maxLength = 255) : ValidationAttribute(IsValidId.ValidatorName)
{
    private readonly int _minLength = minLength;
    private readonly int _maxLength = maxLength;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s && s.CheckIsValidId(_minLength, _maxLength))
            return ValidationResult.CreateFromValidationSuccess();
        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = Message ?? IsValidId.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("minLength", _minLength), ("maxLength", _maxLength) });
    }
}
