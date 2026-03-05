using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators.DateTime;

/// <summary>
/// The IsDateOnly class provides methods for validation to ensure that
/// a DateTime value represents a date only (time component is midnight 00:00:00).
/// </summary>
public static class IsDateOnly
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsDateOnly";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Must be a date only (time component must be midnight)";

    /// <summary>
    /// Checks if the given value represents a date only (time component is midnight 00:00:00).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsDateOnly(this System.DateTime value)
    {
        return value.TimeOfDay == TimeSpan.Zero;
    }

    /// <summary>
    /// Checks if the given value represents a date only (time component is midnight 00:00:00).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsDateOnly(this DateTimeOffset value)
    {
        return value.TimeOfDay == TimeSpan.Zero;
    }

    /// <summary>
    /// Validates whether the given value represents a date only.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsDateOnly(this System.DateTime value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsDateOnly())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", (object)value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given value represents a date only.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsDateOnly(this DateTimeOffset value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsDateOnly())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", (object)value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value represents a date only, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static System.DateTime EnsureIsDateOnly(this System.DateTime value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsDateOnly(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures the given value represents a date only, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTimeOffset EnsureIsDateOnly(this DateTimeOffset value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsDateOnly(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class IsDateOnlyValidator : IValidator
{
    public static readonly IsDateOnlyValidator Instance = new();
    public string Name => IsDateOnly.ValidatorName;
    public string DefaultFailureMessage => IsDateOnly.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.DateTime dt)
            return dt.ValidateIsDateOnly(blackboard, this.DefaultFailureMessage, memberName);
        if (value is DateTimeOffset dto)
            return dto.ValidateIsDateOnly(blackboard, this.DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(this.Name, "Value is not a DateTime or DateTimeOffset", memberName, blackboard,
            [("value", value)]);
    }
}

/// <summary>
/// Validates that the decorated member's value represents a date only (time component is midnight).
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsDateOnlyAttribute() : ValidationAttribute(IsDateOnly.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.DateTime dt)
            return dt.CheckIsDateOnly() ? ValidationResult.CreateFromValidationSuccess() : Fail(dt, memberName, blackboard);
        if (value is DateTimeOffset dto)
            return dto.CheckIsDateOnly() ? ValidationResult.CreateFromValidationSuccess() : Fail(dto, memberName, blackboard);

        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = this.Message ?? IsDateOnly.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(this.Name, message, memberName, blackboard, [("value", value)]);
    }
}
