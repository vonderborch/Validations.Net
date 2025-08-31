using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a date/time value is within a specified number of days from now.
/// </summary>
public static class IsWithinDays
{
    private const string ValidatorName = nameof(IsWithinDays);

    #region Check Methods

    /// <summary>
    ///     Checks if the specified DateTime value is within the given number of days from now.
    /// </summary>
    /// <param name="value">The DateTime value to check.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <returns>True if the value is within the specified number of days from now; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsWithinDays(this DateTime value, int days)
    {
        var now = DateTime.Now;
        var difference = Math.Abs((value - now).TotalDays);
        return difference <= days;
    }

    /// <summary>
    ///     Checks if the specified nullable DateTime value is within the given number of days from now.
    /// </summary>
    /// <param name="value">The nullable DateTime value to check.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <returns>True if the value is within the specified number of days from now; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsWithinDays(this DateTime? value, int days)
    {
        return value.HasValue && CheckIsWithinDays(value.Value, days);
    }

    /// <summary>
    ///     Checks if the specified DateTimeOffset value is within the given number of days from now.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to check.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <returns>True if the value is within the specified number of days from now; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsWithinDays(this DateTimeOffset value, int days)
    {
        var now = DateTimeOffset.Now;
        var difference = Math.Abs((value - now).TotalDays);
        return difference <= days;
    }

    /// <summary>
    ///     Checks if the specified nullable DateTimeOffset value is within the given number of days from now.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to check.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <returns>True if the value is within the specified number of days from now; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsWithinDays(this DateTimeOffset? value, int days)
    {
        return value.HasValue && CheckIsWithinDays(value.Value, days);
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates if the specified DateTime value is within the given number of days from now and returns a
    ///     ValidationResult.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the value is within the specified number of days from now.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsWithinDays(this DateTime value, int days, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsWithinDays(value, days))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var now = DateTime.Now;
        var actualDays = Math.Abs((value - now).TotalDays);

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("days", days),
            ("currentTime", now),
            ("actualDays", actualDays)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            $"Value must be within {days} days from now. Actual difference: {actualDays:F2} days.", parameterName,
            blackboard, contextList);
    }

    /// <summary>
    ///     Validates if the specified nullable DateTime value is within the given number of days from now and returns a
    ///     ValidationResult.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the value is within the specified number of days from now.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsWithinDays(this DateTime? value, int days, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", parameterName,
                blackboard, contextList);
        }

        return ValidateIsWithinDays(value.Value, days, blackboard, parameterName);
    }

    /// <summary>
    ///     Validates if the specified DateTimeOffset value is within the given number of days from now and returns a
    ///     ValidationResult.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the value is within the specified number of days from now.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsWithinDays(this DateTimeOffset value, int days, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsWithinDays(value, days))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var now = DateTimeOffset.Now;
        var actualDays = Math.Abs((value - now).TotalDays);

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("days", days),
            ("currentTime", now),
            ("actualDays", actualDays)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            $"Value must be within {days} days from now. Actual difference: {actualDays:F2} days.", parameterName,
            blackboard, contextList);
    }

    /// <summary>
    ///     Validates if the specified nullable DateTimeOffset value is within the given number of days from now and returns a
    ///     ValidationResult.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the value is within the specified number of days from now.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsWithinDays(this DateTimeOffset? value, int days, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", parameterName,
                blackboard, contextList);
        }

        return ValidateIsWithinDays(value.Value, days, blackboard, parameterName);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that the specified DateTime value is within the given number of days from now, throwing a
    ///     ValidationException if it is not.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not within the specified number of days from now.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime EnsureIsWithinDays(this DateTime value, int days, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsWithinDays(value, days, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the specified nullable DateTime value is within the given number of days from now, throwing a
    ///     ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">
    ///     Thrown when the value is null or not within the specified number of days from
    ///     now.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime? EnsureIsWithinDays(this DateTime? value, int days, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsWithinDays(value, days, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the specified DateTimeOffset value is within the given number of days from now, throwing a
    ///     ValidationException if it is not.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not within the specified number of days from now.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTimeOffset EnsureIsWithinDays(this DateTimeOffset value, int days, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsWithinDays(value, days, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the specified nullable DateTimeOffset value is within the given number of days from now, throwing a
    ///     ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="days">The number of days to check within.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">
    ///     Thrown when the value is null or not within the specified number of days from
    ///     now.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTimeOffset? EnsureIsWithinDays(this DateTimeOffset? value, int days, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsWithinDays(value, days, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    #endregion
}
