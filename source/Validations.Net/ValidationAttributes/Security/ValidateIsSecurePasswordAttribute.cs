using SimpleBlackboard.Net;
using Validations.Net.Validators.Security;

namespace Validations.Net.ValidationAttributes.Security;

/// <summary>
/// Validates that the decorated member's value meets password security requirements.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsSecurePasswordAttribute(
    int minLength = 8,
    bool requireUppercase = true,
    bool requireLowercase = true,
    bool requireDigit = true,
    bool requireSpecialChar = true) : ValidationAttribute(IsSecurePassword.ValidatorName)
{
    private readonly int _minLength = minLength;
    private readonly bool _requireUppercase = requireUppercase;
    private readonly bool _requireLowercase = requireLowercase;
    private readonly bool _requireDigit = requireDigit;
    private readonly bool _requireSpecialChar = requireSpecialChar;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
            return s.CheckIsSecurePassword(_minLength, _requireUppercase, _requireLowercase, _requireDigit, _requireSpecialChar)
                ? ValidationResult.CreateFromValidationSuccess()
                : Fail(value, memberName, blackboard);
        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = Message ?? IsSecurePassword.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
