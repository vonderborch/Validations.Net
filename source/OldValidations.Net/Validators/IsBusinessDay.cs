using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a date/time value falls on a business day (Monday through Friday, excluding weekends).
/// </summary>
public static class IsBusinessDay
{
    private const string ValidatorName = nameof(IsBusinessDay);

    /// <summary>
    /// Checks if the specified DateTime value falls on a business day (Monday through Friday).
    /// </summary>
    /// <param name="value">The DateTime value to check.</param>
    /// <returns>True if the value falls on a business day; otherwise, false.</returns>
    public static bool CheckIsBusinessDay(this DateTime value)
    {
        return value.DayOfWeek >= DayOfWeek.Monday && value.DayOfWeek <= DayOfWeek.Friday;
    }

    /// <summary>
    /// Checks if the specified nullable DateTime value falls on a business day (Monday through Friday).
    /// </summary>
    /// <param name="value">The nullable DateTime value to check.</param>
    /// <returns>True if the value falls on a business day; otherwise, false.</returns>
    public static bool CheckIsBusinessDay(this DateTime? value)
    {
        return value.HasValue && CheckIsBusinessDay(value.Value);
    }

    /// <summary>
    /// Checks if the specified DateTimeOffset value falls on a business day (Monday through Friday).
    /// </summary>
    /// <param name="value">The DateTimeOffset value to check.</param>
    /// <returns>True if the value falls on a business day; otherwise, false.</returns>
    public static bool CheckIsBusinessDay(this DateTimeOffset value)
    {
        return value.DayOfWeek >= DayOfWeek.Monday && value.DayOfWeek <= DayOfWeek.Friday;
    }

    /// <summary>
    /// Checks if the specified nullable DateTimeOffset value falls on a business day (Monday through Friday).
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to check.</param>
    /// <returns>True if the value falls on a business day; otherwise, false.</returns>
    public static bool CheckIsBusinessDay(this DateTimeOffset? value)
    {
        return value.HasValue && CheckIsBusinessDay(value.Value);
    }

    /// <summary>
    /// Checks if the specified DateTime value falls on a business day, excluding specified holidays.
    /// </summary>
    /// <param name="value">The DateTime value to check.</param>
    /// <param name="holidays">An array of holiday dates to exclude from business days.</param>
    /// <returns>True if the value falls on a business day and is not a holiday; otherwise, false.</returns>
    public static bool CheckIsBusinessDay(this DateTime value, params DateTime[] holidays)
    {
        if (!CheckIsBusinessDay(value))
        {
            return false;
        }

        var dateOnly = value.Date;
        foreach (var holiday in holidays)
        {
            if (holiday.Date == dateOnly)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Checks if the specified nullable DateTime value falls on a business day, excluding specified holidays.
    /// </summary>
    /// <param name="value">The nullable DateTime value to check.</param>
    /// <param name="holidays">An array of holiday dates to exclude from business days.</param>
    /// <returns>True if the value falls on a business day and is not a holiday; otherwise, false.</returns>
    public static bool CheckIsBusinessDay(this DateTime? value, params DateTime[] holidays)
    {
        return value.HasValue && CheckIsBusinessDay(value.Value, holidays);
    }

    /// <summary>
    /// Checks if the specified DateTimeOffset value falls on a business day, excluding specified holidays.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to check.</param>
    /// <param name="holidays">An array of holiday dates to exclude from business days.</param>
    /// <returns>True if the value falls on a business day and is not a holiday; otherwise, false.</returns>
    public static bool CheckIsBusinessDay(this DateTimeOffset value, params DateTime[] holidays)
    {
        if (!CheckIsBusinessDay(value))
        {
            return false;
        }

        var dateOnly = value.Date;
        foreach (var holiday in holidays)
        {
            if (holiday.Date == dateOnly)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Checks if the specified nullable DateTimeOffset value falls on a business day, excluding specified holidays.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to check.</param>
    /// <param name="holidays">An array of holiday dates to exclude from business days.</param>
    /// <returns>True if the value falls on a business day and is not a holiday; otherwise, false.</returns>
    public static bool CheckIsBusinessDay(this DateTimeOffset? value, params DateTime[] holidays)
    {
        return value.HasValue && CheckIsBusinessDay(value.Value, holidays);
    }

    /// <summary>
    /// Ensures that the specified DateTime value falls on a business day, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value does not fall on a business day.</exception>
    public static void EnsureIsBusinessDay(this DateTime value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsBusinessDay(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("DayOfWeek", value.DayOfWeek)
            };
            throw ValidationException.Create(ValidatorName,
                "Value must fall on a business day (Monday through Friday).", parameterName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable DateTime value falls on a business day, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or does not fall on a business day.</exception>
    public static void EnsureIsBusinessDay(this DateTime? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", parameterName, blackboard,
                contextList);
        }

        EnsureIsBusinessDay(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Ensures that the specified DateTimeOffset value falls on a business day, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value does not fall on a business day.</exception>
    public static void EnsureIsBusinessDay(this DateTimeOffset value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsBusinessDay(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("DayOfWeek", value.DayOfWeek)
            };
            throw ValidationException.Create(ValidatorName,
                "Value must fall on a business day (Monday through Friday).", parameterName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable DateTimeOffset value falls on a business day, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or does not fall on a business day.</exception>
    public static void EnsureIsBusinessDay(this DateTimeOffset? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", parameterName, blackboard,
                contextList);
        }

        EnsureIsBusinessDay(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Ensures that the specified DateTime value falls on a business day, excluding specified holidays, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="holidays">An array of holiday dates to exclude from business days.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value does not fall on a business day or is a holiday.</exception>
    public static void EnsureIsBusinessDay(this DateTime value, DateTime[] holidays, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsBusinessDay(value, holidays);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("DayOfWeek", value.DayOfWeek),
                ("Holidays", holidays)
            };
            throw ValidationException.Create(ValidatorName,
                "Value must fall on a business day (Monday through Friday) and not be a holiday.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Validates that the specified DateTime value falls on a business day.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value falls on a business day.</returns>
    public static ValidationResult ValidateIsBusinessDay(this DateTime value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsBusinessDay(value);
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

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            "Value must fall on a business day (Monday through Friday).", parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified nullable DateTime value falls on a business day.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value falls on a business day.</returns>
    public static ValidationResult ValidateIsBusinessDay(this DateTime? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", null),
                ("ParameterName", parameterName)
            };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", parameterName,
                blackboard, contextList);
        }

        return ValidateIsBusinessDay(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Validates that the specified DateTimeOffset value falls on a business day.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value falls on a business day.</returns>
    public static ValidationResult ValidateIsBusinessDay(this DateTimeOffset value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsBusinessDay(value);
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

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            "Value must fall on a business day (Monday through Friday).", parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified nullable DateTimeOffset value falls on a business day.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value falls on a business day.</returns>
    public static ValidationResult ValidateIsBusinessDay(this DateTimeOffset? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", null),
                ("ParameterName", parameterName)
            };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", parameterName,
                blackboard, contextList);
        }

        return ValidateIsBusinessDay(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Validates that the specified DateTime value falls on a business day, excluding specified holidays.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="holidays">An array of holiday dates to exclude from business days.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value falls on a business day and is not a holiday.</returns>
    public static ValidationResult ValidateIsBusinessDay(this DateTime value, DateTime[] holidays, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsBusinessDay(value, holidays);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("DayOfWeek", value.DayOfWeek),
            ("Holidays", holidays),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            "Value must fall on a business day (Monday through Friday) and not be a holiday.", parameterName,
            blackboard, contextList);
    }
}