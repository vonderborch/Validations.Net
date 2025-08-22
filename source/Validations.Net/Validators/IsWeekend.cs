using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a date/time value falls on a weekend.
/// </summary>
public static class IsWeekend
{
    private const string ValidatorName = nameof(IsWeekend);

    /// <summary>
    ///     Checks if the specified DateTime value falls on a weekend (Saturday or Sunday).
    /// </summary>
    /// <param name="value">The DateTime value to check.</param>
    /// <returns>True if the value falls on a weekend; otherwise, false.</returns>
    public static bool CheckIsWeekend(this DateTime value)
    {
        return value.DayOfWeek == DayOfWeek.Saturday || value.DayOfWeek == DayOfWeek.Sunday;
    }

    /// <summary>
    ///     Checks if the specified nullable DateTime value falls on a weekend (Saturday or Sunday).
    /// </summary>
    /// <param name="value">The nullable DateTime value to check.</param>
    /// <returns>True if the value falls on a weekend; otherwise, false.</returns>
    public static bool CheckIsWeekend(this DateTime? value)
    {
        return value.HasValue && CheckIsWeekend(value.Value);
    }

    /// <summary>
    ///     Checks if the specified DateTimeOffset value falls on a weekend (Saturday or Sunday).
    /// </summary>
    /// <param name="value">The DateTimeOffset value to check.</param>
    /// <returns>True if the value falls on a weekend; otherwise, false.</returns>
    public static bool CheckIsWeekend(this DateTimeOffset value)
    {
        return value.DayOfWeek == DayOfWeek.Saturday || value.DayOfWeek == DayOfWeek.Sunday;
    }

    /// <summary>
    ///     Checks if the specified nullable DateTimeOffset value falls on a weekend (Saturday or Sunday).
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to check.</param>
    /// <returns>True if the value falls on a weekend; otherwise, false.</returns>
    public static bool CheckIsWeekend(this DateTimeOffset? value)
    {
        return value.HasValue && CheckIsWeekend(value.Value);
    }

    /// <summary>
    ///     Ensures that the specified DateTime value falls on a weekend, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value does not fall on a weekend.</exception>
    public static void EnsureIsWeekend(this DateTime value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsWeekend(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("DayOfWeek", value.DayOfWeek)
            };
            throw ValidationException.Create(ValidatorName, "Value must fall on a weekend (Saturday or Sunday).",
                fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified nullable DateTime value falls on a weekend, throwing a ValidationException if it does
    ///     not.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or does not fall on a weekend.</exception>
    public static void EnsureIsWeekend(this DateTime? value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard,
                contextList);
        }

        EnsureIsWeekend(value.Value, fieldName, blackboard);
    }

    /// <summary>
    ///     Ensures that the specified DateTimeOffset value falls on a weekend, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value does not fall on a weekend.</exception>
    public static void EnsureIsWeekend(this DateTimeOffset value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsWeekend(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("DayOfWeek", value.DayOfWeek)
            };
            throw ValidationException.Create(ValidatorName, "Value must fall on a weekend (Saturday or Sunday).",
                fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified nullable DateTimeOffset value falls on a weekend, throwing a ValidationException if it
    ///     does not.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or does not fall on a weekend.</exception>
    public static void EnsureIsWeekend(this DateTimeOffset? value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard,
                contextList);
        }

        EnsureIsWeekend(value.Value, fieldName, blackboard);
    }

    /// <summary>
    ///     Validates if the specified DateTime value falls on a weekend and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value falls on a weekend.</returns>
    public static ValidationResult ValidateIsWeekend(this DateTime value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsWeekend(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("DayOfWeek", value.DayOfWeek)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "Value must fall on a weekend (Saturday or Sunday).", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates if the specified nullable DateTime value falls on a weekend and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value falls on a weekend.</returns>
    public static ValidationResult ValidateIsWeekend(this DateTime? value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName,
                blackboard, contextList);
        }

        return ValidateIsWeekend(value.Value, fieldName, blackboard);
    }

    /// <summary>
    ///     Validates if the specified DateTimeOffset value falls on a weekend and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value falls on a weekend.</returns>
    public static ValidationResult ValidateIsWeekend(this DateTimeOffset value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsWeekend(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("DayOfWeek", value.DayOfWeek)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "Value must fall on a weekend (Saturday or Sunday).", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates if the specified nullable DateTimeOffset value falls on a weekend and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value falls on a weekend.</returns>
    public static ValidationResult ValidateIsWeekend(this DateTimeOffset? value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName,
                blackboard, contextList);
        }

        return ValidateIsWeekend(value.Value, fieldName, blackboard);
    }
}
