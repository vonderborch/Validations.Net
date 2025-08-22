using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a date/time value falls on a weekday (Monday through Friday).
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
    /// Ensures that the specified DateTime value falls on a weekday, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value does not fall on a weekday.</exception>
    public static void EnsureIsWeekday(this DateTime value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsWeekday(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("DayOfWeek", value.DayOfWeek)
            };
            throw ValidationException.Create(ValidatorName, "Value must fall on a weekday (Monday through Friday).",
                parameterName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable DateTime value falls on a weekday, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or does not fall on a weekday.</exception>
    public static void EnsureIsWeekday(this DateTime? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", parameterName, blackboard,
                contextList);
        }

        EnsureIsWeekday(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Ensures that the specified DateTimeOffset value falls on a weekday, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value does not fall on a weekday.</exception>
    public static void EnsureIsWeekday(this DateTimeOffset value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsWeekday(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("DayOfWeek", value.DayOfWeek)
            };
            throw ValidationException.Create(ValidatorName, "Value must fall on a weekday (Monday through Friday).",
                parameterName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable DateTimeOffset value falls on a weekday, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or does not fall on a weekday.</exception>
    public static void EnsureIsWeekday(this DateTimeOffset? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", parameterName, blackboard,
                contextList);
        }

        EnsureIsWeekday(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Validates that the specified DateTime value falls on a weekday.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value falls on a weekday.</returns>
    public static ValidationResult ValidateIsWeekday(this DateTime value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsWeekday(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("DayOfWeek", value.DayOfWeek),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "Value must fall on a weekday (Monday through Friday).",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified nullable DateTime value falls on a weekday.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value falls on a weekday.</returns>
    public static ValidationResult ValidateIsWeekday(this DateTime? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", null),
                ("ParameterName", parameterName)
            };
            return ValidationResult.CreateFromValidationFailure(
                ValidatorName,
                "Value cannot be null.",
                parameterName,
                blackboard,
                contextList);
        }

        return ValidateIsWeekday(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Validates that the specified DateTimeOffset value falls on a weekday.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value falls on a weekday.</returns>
    public static ValidationResult ValidateIsWeekday(this DateTimeOffset value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsWeekday(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("DayOfWeek", value.DayOfWeek),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "Value must fall on a weekday (Monday through Friday).",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified nullable DateTimeOffset value falls on a weekday.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value falls on a weekday.</returns>
    public static ValidationResult ValidateIsWeekday(this DateTimeOffset? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", null),
                ("ParameterName", parameterName)
            };
            return ValidationResult.CreateFromValidationFailure(
                ValidatorName,
                "Value cannot be null.",
                parameterName,
                blackboard,
                contextList);
        }

        return ValidateIsWeekday(value.Value, blackboard, parameterName);
    }
}
