using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.DataFormat;

/// <summary>
/// The IsValidEmail class provides methods for validation to ensure that
/// a string represents a valid email address (basic check without regex).
/// </summary>
public static class IsValidEmail
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsValidEmail";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Must be a valid email";

    /// <summary>
    /// Checks if the given value is a valid email (basic: not null, exactly one '@', content before/after, domain has '.', no spaces).
    /// </summary>
    public static bool CheckIsValidEmail(this string? value)
    {
        if (value is null || value.Length == 0)
            return false;

        int atIndex = value.IndexOf('@');
        if (atIndex <= 0 || atIndex != value.LastIndexOf('@'))
            return false;

        string localPart = value[..atIndex];
        string domainPart = value[(atIndex + 1)..];

        if (localPart.Length == 0 || domainPart.Length == 0)
            return false;

        if (!domainPart.Contains('.'))
            return false;

        if (domainPart.StartsWith('.') || domainPart.EndsWith('.'))
            return false;

        if (domainPart.Contains(".."))
            return false;

        if (value.Contains(' '))
            return false;

        return true;
    }

    /// <summary>
    /// Validates whether the given value is a valid email.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidEmail(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsValidEmail())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is a valid email, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidEmail(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsValidEmail(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class ValidEmailValidator : IValidator
{
    public static readonly ValidEmailValidator Instance = new();
    public string Name => IsValidEmail.ValidatorName;
    public string DefaultFailureMessage => IsValidEmail.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
            return s.ValidateIsValidEmail(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not a string", memberName, blackboard,
            [("value", value)]);
    }
}

/// <summary>
/// Validates that the decorated member's value is a valid email address.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsValidEmailAttribute() : ValidationAttribute(IsValidEmail.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
            return s.CheckIsValidEmail() ? ValidationResult.CreateFromValidationSuccess() : Fail(s, memberName, blackboard);

        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = Message ?? IsValidEmail.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard, [("value", value)]);
    }
}
