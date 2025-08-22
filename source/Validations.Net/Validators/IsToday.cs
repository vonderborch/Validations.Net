using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a date is today.
/// </summary>
public static class IsToday
{
    private const string ValidatorName = "IsToday";

    #region Check Methods

    /// <summary>
    /// Checks if a DateTime is today.
    /// </summary>
    /// <param name="value">The DateTime to check.</param>
    /// <returns>True if the DateTime is today; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsToday(this DateTime value)
    {
        return value.Date == DateTime.Today;
    }

    /// <summary>
    /// Checks if a nullable DateTime is today.
    /// </summary>
    /// <param name="value">The DateTime to check.</param>
    /// <returns>True if the DateTime is today; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsToday(this DateTime? value)
    {
        return value.HasValue && value.Value.Date == DateTime.Today;
    }

    /// <summary>
    /// Checks if a DateTimeOffset is today.
    /// </summary>
    /// <param name="value">The DateTimeOffset to check.</param>
    /// <returns>True if the DateTimeOffset is today; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsToday(this DateTimeOffset value)
    {
        return value.Date == DateTime.Today;
    }

    /// <summary>
    /// Checks if a nullable DateTimeOffset is today.
    /// </summary>
    /// <param name="value">The DateTimeOffset to check.</param>
    /// <returns>True if the DateTimeOffset is today; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsToday(this DateTimeOffset? value)
    {
        return value.HasValue && value.Value.Date == DateTime.Today;
    }

    #endregion

    #region Validate Methods

    /// <summary>
    /// Validates if a DateTime is today.
    /// </summary>
    /// <param name="value">The DateTime to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the DateTime is today.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsToday(this DateTime value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsToday(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("today", DateTime.Today)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The date must be today ({DateTime.Today:yyyy-MM-dd}). Actual date: {value:yyyy-MM-dd}",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates if a nullable DateTime is today.
    /// </summary>
    /// <param name="value">The DateTime to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the DateTime is today.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsToday(this DateTime? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsToday(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("today", DateTime.Today)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The date must be today ({DateTime.Today:yyyy-MM-dd}). Actual date: {value?.ToString("yyyy-MM-dd") ?? "null"}",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates if a DateTimeOffset is today.
    /// </summary>
    /// <param name="value">The DateTimeOffset to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the DateTimeOffset is today.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsToday(this DateTimeOffset value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsToday(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("today", DateTime.Today)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The date must be today ({DateTime.Today:yyyy-MM-dd}). Actual date: {value:yyyy-MM-dd}",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates if a nullable DateTimeOffset is today.
    /// </summary>
    /// <param name="value">The DateTimeOffset to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the DateTimeOffset is today.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsToday(this DateTimeOffset? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsToday(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("today", DateTime.Today)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The date must be today ({DateTime.Today:yyyy-MM-dd}). Actual date: {value?.ToString("yyyy-MM-dd") ?? "null"}",
            fieldName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    /// Ensures that a DateTime is today.
    /// </summary>
    /// <param name="value">The DateTime to validate.</param>
    /// <returns>The original DateTime if it is today.</returns>
    /// <exception cref="ValidationException">Thrown when the DateTime is not today.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime EnsureIsToday(this DateTime value)
    {
        if (!CheckIsToday(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("today", DateTime.Today)
            };
            throw ValidationException.Create(ValidatorName, $"The date must be today ({DateTime.Today:yyyy-MM-dd}). Actual date: {value:yyyy-MM-dd}", null, null, contextList);
        }

        return value;
    }

    /// <summary>
    /// Ensures that a nullable DateTime is today.
    /// </summary>
    /// <param name="value">The DateTime to validate.</param>
    /// <returns>The original DateTime if it is today.</returns>
    /// <exception cref="ValidationException">Thrown when the DateTime is not today.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime? EnsureIsToday(this DateTime? value)
    {
        if (!CheckIsToday(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("today", DateTime.Today)
            };
            throw ValidationException.Create(ValidatorName, $"The date must be today ({DateTime.Today:yyyy-MM-dd}). Actual date: {value?.ToString("yyyy-MM-dd") ?? "null"}", null, null, contextList);
        }

        return value;
    }

    /// <summary>
    /// Ensures that a DateTimeOffset is today.
    /// </summary>
    /// <param name="value">The DateTimeOffset to validate.</param>
    /// <returns>The original DateTimeOffset if it is today.</returns>
    /// <exception cref="ValidationException">Thrown when the DateTimeOffset is not today.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTimeOffset EnsureIsToday(this DateTimeOffset value)
    {
        if (!CheckIsToday(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("today", DateTime.Today)
            };
            throw ValidationException.Create(ValidatorName, $"The date must be today ({DateTime.Today:yyyy-MM-dd}). Actual date: {value:yyyy-MM-dd}", null, null, contextList);
        }

        return value;
    }

    /// <summary>
    /// Ensures that a nullable DateTimeOffset is today.
    /// </summary>
    /// <param name="value">The DateTimeOffset to validate.</param>
    /// <returns>The original DateTimeOffset if it is today.</returns>
    /// <exception cref="ValidationException">Thrown when the DateTimeOffset is not today.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTimeOffset? EnsureIsToday(this DateTimeOffset? value)
    {
        if (!CheckIsToday(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("today", DateTime.Today)
            };
            throw ValidationException.Create(ValidatorName, $"The date must be today ({DateTime.Today:yyyy-MM-dd}). Actual date: {value?.ToString("yyyy-MM-dd") ?? "null"}", null, null, contextList);
        }

        return value;
    }

    #endregion
}
