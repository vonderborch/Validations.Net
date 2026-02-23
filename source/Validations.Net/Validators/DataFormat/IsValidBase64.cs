using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.DataFormat;

/// <summary>
/// The IsValidBase64 class provides methods for validation to ensure that
/// a string represents valid Base64-encoded data.
/// </summary>
public static class IsValidBase64
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsValidBase64";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Must be valid Base64";

    /// <summary>
    /// Checks if the given value is valid Base64.
    /// </summary>
    public static bool CheckIsValidBase64(this string? value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        Span<byte> buffer = stackalloc byte[((value.Length * 3) + 3) / 4];
        return Convert.TryFromBase64String(value, buffer, out _);
    }

    /// <summary>
    /// Validates whether the given value is valid Base64.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidBase64(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsValidBase64())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is valid Base64, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidBase64(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsValidBase64(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class ValidBase64Validator : IValidator
{
    public static readonly ValidBase64Validator Instance = new();
    public string Name => IsValidBase64.ValidatorName;
    public string DefaultFailureMessage => IsValidBase64.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
            return s.ValidateIsValidBase64(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not a string", memberName, blackboard,
            [("value", value)]);
    }
}

/// <summary>
/// Validates that the decorated member's value is valid Base64.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsValidBase64Attribute() : ValidationAttribute(IsValidBase64.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
            return s.CheckIsValidBase64() ? ValidationResult.CreateFromValidationSuccess() : Fail(s, memberName, blackboard);

        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = Message ?? IsValidBase64.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard, [("value", value)]);
    }
}
