using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.DateTime;

/// <summary>
/// The IsBusinessDay class provides methods for validation to ensure that
/// a date falls on a business day (Monday through Friday).
/// </summary>
public static class IsBusinessDay
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsBusinessDay";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Must be a business day";

    /// <summary>
    /// Checks if the given date is a business day (Monday-Friday).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsBusinessDay(this System.DateTime value)
    {
        var dow = value.DayOfWeek;
        return dow != DayOfWeek.Saturday && dow != DayOfWeek.Sunday;
    }

    /// <summary>
    /// Checks if the given date is a business day (Monday-Friday).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsBusinessDay(this DateTimeOffset value)
    {
        return value.DateTime.CheckIsBusinessDay();
    }

    /// <summary>
    /// Validates whether the given date is a business day.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsBusinessDay(this System.DateTime value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsBusinessDay())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", (object)value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given date is a business day.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsBusinessDay(this DateTimeOffset value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsBusinessDay())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", (object)value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given date is a business day, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static System.DateTime EnsureIsBusinessDay(this System.DateTime value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsBusinessDay(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures the given date is a business day, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTimeOffset EnsureIsBusinessDay(this DateTimeOffset value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsBusinessDay(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class IsBusinessDayValidator : IValidator
{
    public static readonly IsBusinessDayValidator Instance = new();
    public string Name => IsBusinessDay.ValidatorName;
    public string DefaultFailureMessage => IsBusinessDay.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.DateTime dt)
            return dt.ValidateIsBusinessDay(blackboard, DefaultFailureMessage, memberName);
        if (value is DateTimeOffset dto)
            return dto.ValidateIsBusinessDay(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not a DateTime or DateTimeOffset", memberName, blackboard,
            [("value", value)]);
    }
}

/// <summary>
/// Validates that the decorated member's value is a business day (Monday through Friday).
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsBusinessDayAttribute() : ValidationAttribute(IsBusinessDay.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.DateTime dt)
            return dt.CheckIsBusinessDay() ? ValidationResult.CreateFromValidationSuccess() : Fail(dt, memberName, blackboard);
        if (value is DateTimeOffset dto)
            return dto.CheckIsBusinessDay() ? ValidationResult.CreateFromValidationSuccess() : Fail(dto, memberName, blackboard);

        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = Message ?? IsBusinessDay.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard, [("value", value)]);
    }
}
