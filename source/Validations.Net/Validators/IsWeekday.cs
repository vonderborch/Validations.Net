using System;
using System.Collections.Generic;
using System.Globalization;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a date/time value falls on a weekday.
/// </summary>
public static class IsWeekday
{
    private const string ValidatorName = nameof(IsWeekday);

    /// <summary>
    /// Checks if the specified DateTime value falls on a weekday (Monday through Friday).
    /// </summary>
    /// <param name="value">The DateTime value to check.</param>
    /// <returns>True if the value falls on a weekday; otherwise, false.</returns>
    public static bool CheckIsWeekday(this DateTime value)
    {
        return value.DayOfWeek >= DayOfWeek.Monday && value.DayOfWeek <= DayOfWeek.Friday;
    }

    /// <summary>
    /// Checks if the specified nullable DateTime value falls on a weekday (Monday through Friday).
    /// </summary>
    /// <param name="value">The nullable DateTime value to check.</param>
    /// <returns>True if the value falls on a weekday; otherwise, false.</returns>
    public static bool CheckIsWeekday(this DateTime? value)
    {
        return value.HasValue && CheckIsWeekday(value.Value);
    }

    /// <summary>
    /// Checks if the specified DateTimeOffset value falls on a weekday (Monday through Friday).
    /// </summary>
    /// <param name="value">The DateTimeOffset value to check.</param>
    /// <returns>True if the value falls on a weekday; otherwise, false.</returns>
    public static bool CheckIsWeekday(this DateTimeOffset value)
    {
        return value.DayOfWeek >= DayOfWeek.Monday && value.DayOfWeek <= DayOfWeek.Friday;
    }

    /// <summary>
    /// Checks if the specified nullable DateTimeOffset value falls on a weekday (Monday through Friday).
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to check.</param>
    /// <returns>True if the value falls on a weekday; otherwise, false.</returns>
    public static bool CheckIsWeekday(this DateTimeOffset? value)
    {
        return value.HasValue && CheckIsWeekday(value.Value);
    }

    /// <summary>
    /// Validates if the specified DateTime value falls on a weekday and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value falls on a weekday.</returns>
    public static ValidationResult ValidateIsWeekday(this DateTime value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsWeekday(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("DayOfWeek", value.DayOfWeek)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must fall on a weekday (Monday through Friday).", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified nullable DateTime value falls on a weekday and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value falls on a weekday.</returns>
    public static ValidationResult ValidateIsWeekday(this DateTime? value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        return ValidateIsWeekday(value.Value, fieldName, blackboard);
    }

    /// <summary>
    /// Validates if the specified DateTimeOffset value falls on a weekday and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value falls on a weekday.</returns>
    public static ValidationResult ValidateIsWeekday(this DateTimeOffset value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsWeekday(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("DayOfWeek", value.DayOfWeek)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must fall on a weekday (Monday through Friday).", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified nullable DateTimeOffset value falls on a weekday and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value falls on a weekday.</returns>
    public static ValidationResult ValidateIsWeekday(this DateTimeOffset? value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        return ValidateIsWeekday(value.Value, fieldName, blackboard);
    }

    /// <summary>
    /// Ensures that the specified DateTime value falls on a weekday, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value does not fall on a weekday.</exception>
    public static void EnsureIsWeekday(this DateTime value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsWeekday(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("DayOfWeek", value.DayOfWeek)
            };
            throw ValidationException.Create(ValidatorName, "Value must fall on a weekday (Monday through Friday).", fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable DateTime value falls on a weekday, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or does not fall on a weekday.</exception>
    public static void EnsureIsWeekday(this DateTime? value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        EnsureIsWeekday(value.Value, fieldName, blackboard);
    }

    /// <summary>
    /// Ensures that the specified DateTimeOffset value falls on a weekday, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value does not fall on a weekday.</exception>
    public static void EnsureIsWeekday(this DateTimeOffset value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsWeekday(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("DayOfWeek", value.DayOfWeek)
            };
            throw ValidationException.Create(ValidatorName, "Value must fall on a weekday (Monday through Friday).", fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable DateTimeOffset value falls on a weekday, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or does not fall on a weekday.</exception>
    public static void EnsureIsWeekday(this DateTimeOffset? value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        EnsureIsWeekday(value.Value, fieldName, blackboard);
    }
}
