using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a date is not today.
/// </summary>
public static class IsNotToday
{
    private const string ValidatorName = "IsNotToday";

    #region Check Methods

    /// <summary>
    ///     Checks if a DateTime is not today.
    /// </summary>
    /// <param name="value">The DateTime to check.</param>
    /// <returns>True if the DateTime is not today; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotToday(this DateTime value)
    {
        return value.Date != DateTime.Today;
    }

    /// <summary>
    ///     Checks if a nullable DateTime is not today.
    /// </summary>
    /// <param name="value">The DateTime to check.</param>
    /// <returns>True if the DateTime is not today; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotToday(this DateTime? value)
    {
        return !value.HasValue || value.Value.Date != DateTime.Today;
    }

    /// <summary>
    ///     Checks if a DateTimeOffset is not today.
    /// </summary>
    /// <param name="value">The DateTimeOffset to check.</param>
    /// <returns>True if the DateTimeOffset is not today; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotToday(this DateTimeOffset value)
    {
        return value.Date != DateTime.Today;
    }

    /// <summary>
    ///     Checks if a nullable DateTimeOffset is not today.
    /// </summary>
    /// <param name="value">The DateTimeOffset to check.</param>
    /// <returns>True if the DateTimeOffset is not today; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotToday(this DateTimeOffset? value)
    {
        return !value.HasValue || value.Value.Date != DateTime.Today;
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates if a DateTime is not today.
    /// </summary>
    /// <param name="value">The DateTime to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the DateTime is not today.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotToday(this DateTime value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsNotToday(value))
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
            $"The date must not be today ({DateTime.Today:yyyy-MM-dd}). Actual date: {value:yyyy-MM-dd}",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a nullable DateTime is not today.
    /// </summary>
    /// <param name="value">The DateTime to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the DateTime is not today.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotToday(this DateTime? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsNotToday(value))
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
            $"The date must not be today ({DateTime.Today:yyyy-MM-dd}). Actual date: {value?.ToString("yyyy-MM-dd") ?? "null"}",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a DateTimeOffset is not today.
    /// </summary>
    /// <param name="value">The DateTimeOffset to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the DateTimeOffset is not today.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotToday(this DateTimeOffset value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsNotToday(value))
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
            $"The date must not be today ({DateTime.Today:yyyy-MM-dd}). Actual date: {value:yyyy-MM-dd}",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a nullable DateTimeOffset is not today.
    /// </summary>
    /// <param name="value">The DateTimeOffset to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the DateTimeOffset is not today.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotToday(this DateTimeOffset? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsNotToday(value))
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
            $"The date must not be today ({DateTime.Today:yyyy-MM-dd}). Actual date: {value?.ToString("yyyy-MM-dd") ?? "null"}",
            fieldName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that a DateTime is not today.
    /// </summary>
    /// <param name="value">The DateTime to validate.</param>
    /// <returns>The original DateTime if it is not today.</returns>
    /// <exception cref="ValidationException">Thrown when the DateTime is today.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime EnsureIsNotToday(this DateTime value)
    {
        if (!CheckIsNotToday(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("today", DateTime.Today)
            };
            throw ValidationException.Create(ValidatorName,
                $"The date must not be today ({DateTime.Today:yyyy-MM-dd}). Actual date: {value:yyyy-MM-dd}", null,
                null, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a nullable DateTime is not today.
    /// </summary>
    /// <param name="value">The DateTime to validate.</param>
    /// <returns>The original DateTime if it is not today.</returns>
    /// <exception cref="ValidationException">Thrown when the DateTime is today.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime? EnsureIsNotToday(this DateTime? value)
    {
        if (!CheckIsNotToday(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("today", DateTime.Today)
            };
            throw ValidationException.Create(ValidatorName,
                $"The date must not be today ({DateTime.Today:yyyy-MM-dd}). Actual date: {value?.ToString("yyyy-MM-dd") ?? "null"}",
                null, null, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a DateTimeOffset is not today.
    /// </summary>
    /// <param name="value">The DateTimeOffset to validate.</param>
    /// <returns>The original DateTimeOffset if it is not today.</returns>
    /// <exception cref="ValidationException">Thrown when the DateTimeOffset is today.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTimeOffset EnsureIsNotToday(this DateTimeOffset value)
    {
        if (!CheckIsNotToday(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("today", DateTime.Today)
            };
            throw ValidationException.Create(ValidatorName,
                $"The date must not be today ({DateTime.Today:yyyy-MM-dd}). Actual date: {value:yyyy-MM-dd}", null,
                null, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a nullable DateTimeOffset is not today.
    /// </summary>
    /// <param name="value">The DateTimeOffset to validate.</param>
    /// <returns>The original DateTimeOffset if it is not today.</returns>
    /// <exception cref="ValidationException">Thrown when the DateTimeOffset is today.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTimeOffset? EnsureIsNotToday(this DateTimeOffset? value)
    {
        if (!CheckIsNotToday(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("today", DateTime.Today)
            };
            throw ValidationException.Create(ValidatorName,
                $"The date must not be today ({DateTime.Today:yyyy-MM-dd}). Actual date: {value?.ToString("yyyy-MM-dd") ?? "null"}",
                null, null, contextList);
        }

        return value;
    }

    #endregion
}
