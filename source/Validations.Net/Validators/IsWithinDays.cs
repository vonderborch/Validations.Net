using System;
using System.Collections.Generic;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a date/time value is within a specified number of days from now.
/// </summary>
public static class IsWithinDays
{
    private const string ValidatorName = nameof(IsWithinDays);

    /// <summary>
    /// Checks if the specified DateTime value is within the given number of days from now.
    /// </summary>
    /// <param name="value">The DateTime value to check.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <returns>True if the value is within the specified number of days from now; otherwise, false.</returns>
    public static bool CheckIsWithinDays(this DateTime value, int days)
    {
        var now = DateTime.Now;
        var difference = Math.Abs((value - now).TotalDays);
        return difference <= days;
    }

    /// <summary>
    /// Checks if the specified nullable DateTime value is within the given number of days from now.
    /// </summary>
    /// <param name="value">The nullable DateTime value to check.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <returns>True if the value is within the specified number of days from now; otherwise, false.</returns>
    public static bool CheckIsWithinDays(this DateTime? value, int days)
    {
        return value.HasValue && CheckIsWithinDays(value.Value, days);
    }

    /// <summary>
    /// Checks if the specified DateTimeOffset value is within the given number of days from now.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to check.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <returns>True if the value is within the specified number of days from now; otherwise, false.</returns>
    public static bool CheckIsWithinDays(this DateTimeOffset value, int days)
    {
        var now = DateTimeOffset.Now;
        var difference = Math.Abs((value - now).TotalDays);
        return difference <= days;
    }

    /// <summary>
    /// Checks if the specified nullable DateTimeOffset value is within the given number of days from now.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to check.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <returns>True if the value is within the specified number of days from now; otherwise, false.</returns>
    public static bool CheckIsWithinDays(this DateTimeOffset? value, int days)
    {
        return value.HasValue && CheckIsWithinDays(value.Value, days);
    }

    /// <summary>
    /// Validates if the specified DateTime value is within the given number of days from now and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is within the specified number of days from now.</returns>
    public static ValidationResult ValidateIsWithinDays(this DateTime value, int days, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsWithinDays(value, days);
        var now = DateTime.Now;
        var actualDays = Math.Abs((value - now).TotalDays);
        
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Days", days),
            ("CurrentTime", now),
            ("ActualDays", actualDays)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must be within {days} days from now. Actual difference: {actualDays:F2} days.", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified nullable DateTime value is within the given number of days from now and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is within the specified number of days from now.</returns>
    public static ValidationResult ValidateIsWithinDays(this DateTime? value, int days, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        return ValidateIsWithinDays(value.Value, days, fieldName, blackboard);
    }

    /// <summary>
    /// Validates if the specified DateTimeOffset value is within the given number of days from now and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is within the specified number of days from now.</returns>
    public static ValidationResult ValidateIsWithinDays(this DateTimeOffset value, int days, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsWithinDays(value, days);
        var now = DateTimeOffset.Now;
        var actualDays = Math.Abs((value - now).TotalDays);
        
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Days", days),
            ("CurrentTime", now),
            ("ActualDays", actualDays)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must be within {days} days from now. Actual difference: {actualDays:F2} days.", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified nullable DateTimeOffset value is within the given number of days from now and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is within the specified number of days from now.</returns>
    public static ValidationResult ValidateIsWithinDays(this DateTimeOffset? value, int days, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        return ValidateIsWithinDays(value.Value, days, fieldName, blackboard);
    }

    /// <summary>
    /// Ensures that the specified DateTime value is within the given number of days from now, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not within the specified number of days from now.</exception>
    public static void EnsureIsWithinDays(this DateTime value, int days, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsWithinDays(value, days);
        if (!isValid)
        {
            var now = DateTime.Now;
            var actualDays = Math.Abs((value - now).TotalDays);
            
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Days", days),
                ("CurrentTime", now),
                ("ActualDays", actualDays)
            };
            throw ValidationException.Create(ValidatorName, $"Value must be within {days} days from now. Actual difference: {actualDays:F2} days.", fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable DateTime value is within the given number of days from now, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or not within the specified number of days from now.</exception>
    public static void EnsureIsWithinDays(this DateTime? value, int days, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        EnsureIsWithinDays(value.Value, days, fieldName, blackboard);
    }

    /// <summary>
    /// Ensures that the specified DateTimeOffset value is within the given number of days from now, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not within the specified number of days from now.</exception>
    public static void EnsureIsWithinDays(this DateTimeOffset value, int days, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsWithinDays(value, days);
        if (!isValid)
        {
            var now = DateTimeOffset.Now;
            var actualDays = Math.Abs((value - now).TotalDays);
            
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Days", days),
                ("CurrentTime", now),
                ("ActualDays", actualDays)
            };
            throw ValidationException.Create(ValidatorName, $"Value must be within {days} days from now. Actual difference: {actualDays:F2} days.", fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable DateTimeOffset value is within the given number of days from now, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or not within the specified number of days from now.</exception>
    public static void EnsureIsWithinDays(this DateTimeOffset? value, int days, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        EnsureIsWithinDays(value.Value, days, fieldName, blackboard);
    }
}
