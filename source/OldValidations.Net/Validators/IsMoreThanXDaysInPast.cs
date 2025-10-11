using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a date/time value is more than a specified number of days in the past.
/// </summary>
public static class IsMoreThanXDaysInPast
{
    private const string ValidatorName = nameof(IsMoreThanXDaysInPast);

    #region Check Methods

    /// <summary>
    ///     Checks if the specified DateTime value is more than the given number of days in the past.
    /// </summary>
    /// <param name="value">The DateTime value to check.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <returns>True if the value is more than the specified number of days in the past; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsMoreThanXDaysInPast(this DateTime value, int days)
    {
        var now = DateTime.Now;
        var difference = (now - value).TotalDays;
        return difference > days;
    }

    /// <summary>
    ///     Checks if the specified nullable DateTime value is more than the given number of days in the past.
    /// </summary>
    /// <param name="value">The nullable DateTime value to check.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <returns>True if the value is more than the specified number of days in the past; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsMoreThanXDaysInPast(this DateTime? value, int days)
    {
        return value.HasValue && CheckIsMoreThanXDaysInPast(value.Value, days);
    }

    /// <summary>
    ///     Checks if the specified DateTimeOffset value is more than the given number of days in the past.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to check.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <returns>True if the value is more than the specified number of days in the past; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsMoreThanXDaysInPast(this DateTimeOffset value, int days)
    {
        var now = DateTimeOffset.Now;
        var difference = (now - value).TotalDays;
        return difference > days;
    }

    /// <summary>
    ///     Checks if the specified nullable DateTimeOffset value is more than the given number of days in the past.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to check.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <returns>True if the value is more than the specified number of days in the past; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsMoreThanXDaysInPast(this DateTimeOffset? value, int days)
    {
        return value.HasValue && CheckIsMoreThanXDaysInPast(value.Value, days);
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates if the specified DateTime value is more than the given number of days in the past and returns a
    ///     ValidationResult.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the value is more than the specified number of days in the past.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsMoreThanXDaysInPast(this DateTime value, int days,
        IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsMoreThanXDaysInPast(value, days))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var now = DateTime.Now;
        var actualDays = (now - value).TotalDays;

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("days", days),
            ("currentTime", now),
            ("actualDays", actualDays)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            $"Value must be more than {days} days in the past. Actual difference: {actualDays:F2} days.", parameterName,
            blackboard, contextList);
    }

    /// <summary>
    ///     Validates if the specified nullable DateTime value is more than the given number of days in the past and returns a
    ///     ValidationResult.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the value is more than the specified number of days in the past.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsMoreThanXDaysInPast(this DateTime? value, int days,
        IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", parameterName,
                blackboard, contextList);
        }

        return ValidateIsMoreThanXDaysInPast(value.Value, days, blackboard, parameterName);
    }

    /// <summary>
    ///     Validates if the specified DateTimeOffset value is more than the given number of days in the past and returns a
    ///     ValidationResult.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the value is more than the specified number of days in the past.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsMoreThanXDaysInPast(this DateTimeOffset value, int days,
        IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsMoreThanXDaysInPast(value, days))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var now = DateTimeOffset.Now;
        var actualDays = (now - value).TotalDays;

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("days", days),
            ("currentTime", now),
            ("actualDays", actualDays)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName,
            $"Value must be more than {days} days in the past. Actual difference: {actualDays:F2} days.", parameterName,
            blackboard, contextList);
    }

    /// <summary>
    ///     Validates if the specified nullable DateTimeOffset value is more than the given number of days in the past and
    ///     returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the value is more than the specified number of days in the past.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsMoreThanXDaysInPast(this DateTimeOffset? value, int days,
        IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", parameterName,
                blackboard, contextList);
        }

        return ValidateIsMoreThanXDaysInPast(value.Value, days, blackboard, parameterName);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that the specified DateTime value is more than the given number of days in the past, throwing a
    ///     ValidationException if it is not.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not more than the specified number of days in the past.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime EnsureIsMoreThanXDaysInPast(this DateTime value, int days, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsMoreThanXDaysInPast(value, days, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the specified nullable DateTime value is more than the given number of days in the past, throwing a
    ///     ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">
    ///     Thrown when the value is null or not more than the specified number of days in
    ///     the past.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime? EnsureIsMoreThanXDaysInPast(this DateTime? value, int days, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsMoreThanXDaysInPast(value, days, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the specified DateTimeOffset value is more than the given number of days in the past, throwing a
    ///     ValidationException if it is not.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not more than the specified number of days in the past.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTimeOffset EnsureIsMoreThanXDaysInPast(this DateTimeOffset value, int days, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsMoreThanXDaysInPast(value, days, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the specified nullable DateTimeOffset value is more than the given number of days in the past,
    ///     throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">
    ///     Thrown when the value is null or not more than the specified number of days in
    ///     the past.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTimeOffset? EnsureIsMoreThanXDaysInPast(this DateTimeOffset? value, int days, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsMoreThanXDaysInPast(value, days, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    #endregion
}
