using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators.DateTime;

/// <summary>
/// The IsWeekend class provides methods for validation to ensure that
/// a date/time value falls on a weekend (Saturday or Sunday).
/// </summary>
public static class IsWeekend
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsWeekend";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Must be a weekend";

    /// <summary>
    /// Checks if the given value is a weekend (Saturday or Sunday).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsWeekend(this System.DateTime value)
    {
        return value.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
    }

    /// <summary>
    /// Checks if the given value is a weekend (Saturday or Sunday).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsWeekend(this DateTimeOffset value)
    {
        return value.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
    }

    /// <summary>
    /// Validates whether the given value is a weekend.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsWeekend(this System.DateTime value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsWeekend())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", (object)value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given value is a weekend.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsWeekend(this DateTimeOffset value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsWeekend())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", (object)value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is a weekend, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static System.DateTime EnsureIsWeekend(this System.DateTime value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsWeekend(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures the given value is a weekend, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTimeOffset EnsureIsWeekend(this DateTimeOffset value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsWeekend(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class IsWeekendValidator : IValidator
{
    public static readonly IsWeekendValidator Instance = new();
    public string Name => IsWeekend.ValidatorName;
    public string DefaultFailureMessage => IsWeekend.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.DateTime dt)
            return dt.ValidateIsWeekend(blackboard, this.DefaultFailureMessage, memberName);
        if (value is DateTimeOffset dto)
            return dto.ValidateIsWeekend(blackboard, this.DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(this.Name, "Value is not a DateTime or DateTimeOffset", memberName, blackboard,
            [("value", value)]);
    }
}

/// <summary>
/// Validates that the decorated member's value is a weekend.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsWeekendAttribute() : ValidationAttribute(IsWeekend.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.DateTime dt)
            return dt.CheckIsWeekend() ? ValidationResult.CreateFromValidationSuccess() : Fail(dt, memberName, blackboard);
        if (value is DateTimeOffset dto)
            return dto.CheckIsWeekend() ? ValidationResult.CreateFromValidationSuccess() : Fail(dto, memberName, blackboard);

        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = this.Message ?? IsWeekend.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(this.Name, message, memberName, blackboard, [("value", value)]);
    }
}
