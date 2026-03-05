using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators.Network;

/// <summary>
/// The IsValidHostname class provides methods for validation to ensure that
/// a string represents a valid hostname.
/// </summary>
public static class IsValidHostname
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsValidHostname";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Must be a valid hostname";

    /// <summary>
    /// Checks if the given string is a valid hostname.
    /// Hostname rules: length 1-253, each label between dots is 1-63 chars,
    /// alphanumeric and hyphens only (no leading/trailing hyphens per label).
    /// </summary>
    public static bool CheckIsValidHostname(this string? value)
    {
        if (value is null || value.Length == 0 || value.Length > 253)
            return false;

        int labelStart = 0;
        for (int i = 0; i <= value.Length; i++)
        {
            bool atEnd = i == value.Length;
            bool atDot = !atEnd && value[i] == '.';

            if (atEnd || atDot)
            {
                int labelLen = i - labelStart;
                if (labelLen == 0 || labelLen > 63)
                    return false;

                for (int j = labelStart; j < i; j++)
                {
                    char c = value[j];
                    bool isLetter = (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
                    bool isDigit = c >= '0' && c <= '9';
                    bool isHyphen = c == '-';
                    if (!isLetter && !isDigit && !isHyphen)
                        return false;
                }

                if (value[labelStart] == '-' || value[i - 1] == '-')
                    return false;

                if (atDot)
                    labelStart = i + 1;
            }
        }

        return true;
    }

    /// <summary>
    /// Validates whether the given string is a valid hostname.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidHostname(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsValidHostname())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given string is a valid hostname, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidHostname(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsValidHostname(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class ValidHostnameValidator : IValidator
{
    public static readonly ValidHostnameValidator Instance = new();
    public string Name => IsValidHostname.ValidatorName;
    public string DefaultFailureMessage => IsValidHostname.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
            return s.ValidateIsValidHostname(blackboard, this.DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(this.Name, "Value is not a string", memberName, blackboard,
            [("value", value)]);
    }
}

/// <summary>
/// Validates that the decorated member's value is a valid hostname string.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsValidHostnameAttribute() : ValidationAttribute(IsValidHostname.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
            return s.CheckIsValidHostname() ? ValidationResult.CreateFromValidationSuccess() : Fail(value, memberName, blackboard);
        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = this.Message ?? IsValidHostname.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(this.Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
