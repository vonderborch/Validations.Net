using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators.DateTime;

/// <summary>
/// The IsUtc class provides methods for validation to ensure that
/// a DateTime value has Kind set to UTC.
/// </summary>
public static class IsUtc
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsUtc";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Must be UTC";

    /// <summary>
    /// Checks if the given value has Kind set to UTC.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsUtc(this System.DateTime value)
    {
        return value.Kind == DateTimeKind.Utc;
    }

    /// <summary>
    /// Checks if the given value has Offset of zero (UTC).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsUtc(this DateTimeOffset value)
    {
        return value.Offset == TimeSpan.Zero;
    }

    /// <summary>
    /// Validates whether the given value is UTC.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsUtc(this System.DateTime value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsUtc())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", (object)value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given value is UTC.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsUtc(this DateTimeOffset value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsUtc())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", (object)value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is UTC, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static System.DateTime EnsureIsUtc(this System.DateTime value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsUtc(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures the given value is UTC, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTimeOffset EnsureIsUtc(this DateTimeOffset value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsUtc(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class IsUtcValidator : IValidator
{
    public static readonly IsUtcValidator Instance = new();
    public string Name => IsUtc.ValidatorName;
    public string DefaultFailureMessage => IsUtc.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.DateTime dt)
            return dt.ValidateIsUtc(blackboard, this.DefaultFailureMessage, memberName);
        if (value is DateTimeOffset dto)
            return dto.ValidateIsUtc(blackboard, this.DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(this.Name, "Value is not a DateTime or DateTimeOffset", memberName, blackboard,
            [("value", value)]);
    }
}

/// <summary>
/// Validates that the decorated member's value is UTC.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsUtcAttribute() : ValidationAttribute(IsUtc.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.DateTime dt)
            return dt.CheckIsUtc() ? ValidationResult.CreateFromValidationSuccess() : Fail(dt, memberName, blackboard);
        if (value is DateTimeOffset dto)
            return dto.CheckIsUtc() ? ValidationResult.CreateFromValidationSuccess() : Fail(dto, memberName, blackboard);

        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = this.Message ?? IsUtc.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(this.Name, message, memberName, blackboard, [("value", value)]);
    }
}
