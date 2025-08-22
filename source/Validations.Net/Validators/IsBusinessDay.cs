using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a date/time value falls on a business day (Monday through Friday, excluding
///     weekends).
/// </summary>
public static class IsBusinessDay
{
    private const string ValidatorName = nameof(IsBusinessDay);

    /// <summary>
    ///     Checks if the specified DateTime value falls on a business day (Monday through Friday).
    /// </summary>
    /// <param name="value">The DateTime value to check.</param>
    /// <returns>True if the value falls on a business day; otherwise, false.</returns>
    public static bool CheckIsBusinessDay(this DateTime value)
    {
        return value.DayOfWeek >= DayOfWeek.Monday && value.DayOfWeek <= DayOfWeek.Friday;
    }

    /// <summary>
    ///     Checks if the specified nullable DateTime value falls on a business day (Monday through Friday).
    /// </summary>
    /// <param name="value">The nullable DateTime value to check.</param>
    /// <returns>True if the value falls on a business day; otherwise, false.</returns>
    public static bool CheckIsBusinessDay(this DateTime? value)
    {
        return value.HasValue && CheckIsBusinessDay(value.Value);
    }

    /// <summary>
    ///     Checks if the specified DateTimeOffset value falls on a business day (Monday through Friday).
    /// </summary>
    /// <param name="value">The DateTimeOffset value to check.</param>
    /// <returns>True if the value falls on a business day; otherwise, false.</returns>
    public static bool CheckIsBusinessDay(this DateTimeOffset value)
    {
        return value.DayOfWeek >= DayOfWeek.Monday && value.DayOfWeek <= DayOfWeek.Friday;
    }

    /// <summary>
    ///     Checks if the specified nullable DateTimeOffset value falls on a business day (Monday through Friday).
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to check.</param>
    /// <returns>True if the value falls on a business day; otherwise, false.</returns>
    public static bool CheckIsBusinessDay(this DateTimeOffset? value)
    {
        return value.HasValue && CheckIsBusinessDay(value.Value);
    }

    /// <summary>
    ///     Checks if the specified DateTime value falls on a business day, excluding specified holidays.
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
    ///     Checks if the specified nullable DateTime value falls on a business day, excluding specified holidays.
    /// </summary>
    /// <param name="value">The nullable DateTime value to check.</param>
    /// <param name="holidays">An array of holiday dates to exclude from business days.</param>
    /// <returns>True if the value falls on a business day and is not a holiday; otherwise, false.</returns>
    public static bool CheckIsBusinessDay(this DateTime? value, params DateTime[] holidays)
    {
        return value.HasValue && CheckIsBusinessDay(value.Value, holidays);
    }

    /// <summary>
    ///     Checks if the specified DateTimeOffset value falls on a business day, excluding specified holidays.
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
    ///     Checks if the specified nullable DateTimeOffset value falls on a business day, excluding specified holidays.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to check.</param>
    /// <param name="holidays">An array of holiday dates to exclude from business days.</param>
    /// <returns>True if the value falls on a business day and is not a holiday; otherwise, false.</returns>
    public static bool CheckIsBusinessDay(this DateTimeOffset? value, params DateTime[] holidays)
    {
        return value.HasValue && CheckIsBusinessDay(value.Value, holidays);
    }

    /// <summary>
    ///     Ensures that the specified DateTime value falls on a business day, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value does not fall on a business day.</exception>
    public static void EnsureIsBusinessDay(this DateTime value, string? fieldName = null,
        IBlackboard? blackboard = null)
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
                "Value must fall on a business day (Monday through Friday).", fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified nullable DateTime value falls on a business day, throwing a ValidationException if it
    ///     does not.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or does not fall on a business day.</exception>
    public static void EnsureIsBusinessDay(this DateTime? value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard,
                contextList);
        }

        EnsureIsBusinessDay(value.Value, fieldName, blackboard);
    }

    /// <summary>
    ///     Ensures that the specified DateTimeOffset value falls on a business day, throwing a ValidationException if it does
    ///     not.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value does not fall on a business day.</exception>
    public static void EnsureIsBusinessDay(this DateTimeOffset value, string? fieldName = null,
        IBlackboard? blackboard = null)
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
                "Value must fall on a business day (Monday through Friday).", fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified nullable DateTimeOffset value falls on a business day, throwing a ValidationException if
    ///     it does not.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or does not fall on a business day.</exception>
    public static void EnsureIsBusinessDay(this DateTimeOffset? value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard,
                contextList);
        }

        EnsureIsBusinessDay(value.Value, fieldName, blackboard);
    }

    /// <summary>
    ///     Ensures that the specified DateTime value falls on a business day, excluding specified holidays, throwing a
    ///     ValidationException if it does not.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="holidays">An array of holiday dates to exclude from business days.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value does not fall on a business day or is a holiday.</exception>
    public static void EnsureIsBusinessDay(this DateTime value, DateTime[] holidays, string? fieldName = null,
        IBlackboard? blackboard = null)
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
                "Value must fall on a business day (Monday through Friday) and not be a holiday.", fieldName,
                blackboard, contextList);
        }
    }

    /// <summary>
    ///     Validates if the specified DateTime value falls on a business day and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value falls on a business day.</returns>
    public static ValidationResult ValidateIsBusinessDay(this DateTime value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsBusinessDay(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("DayOfWeek", value.DayOfWeek)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "Value must fall on a business day (Monday through Friday).", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates if the specified nullable DateTime value falls on a business day and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value falls on a business day.</returns>
    public static ValidationResult ValidateIsBusinessDay(this DateTime? value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName,
                blackboard, contextList);
        }

        return ValidateIsBusinessDay(value.Value, fieldName, blackboard);
    }

    /// <summary>
    ///     Validates if the specified DateTimeOffset value falls on a business day and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value falls on a business day.</returns>
    public static ValidationResult ValidateIsBusinessDay(this DateTimeOffset value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsBusinessDay(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("DayOfWeek", value.DayOfWeek)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "Value must fall on a business day (Monday through Friday).", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates if the specified nullable DateTimeOffset value falls on a business day and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value falls on a business day.</returns>
    public static ValidationResult ValidateIsBusinessDay(this DateTimeOffset? value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName,
                blackboard, contextList);
        }

        return ValidateIsBusinessDay(value.Value, fieldName, blackboard);
    }

    /// <summary>
    ///     Validates if the specified DateTime value falls on a business day, excluding specified holidays, and returns a
    ///     ValidationResult.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="holidays">An array of holiday dates to exclude from business days.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value falls on a business day and is not a holiday.</returns>
    public static ValidationResult ValidateIsBusinessDay(this DateTime value, DateTime[] holidays,
        string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsBusinessDay(value, holidays);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("DayOfWeek", value.DayOfWeek),
            ("Holidays", holidays)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "Value must fall on a business day (Monday through Friday) and not be a holiday.", fieldName,
                blackboard, contextList);
    }
}
