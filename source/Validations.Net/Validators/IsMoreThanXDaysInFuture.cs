using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a date/time value is more than a specified number of days in the future.
/// </summary>
public static class IsMoreThanXDaysInFuture
{
    private const string ValidatorName = nameof(IsMoreThanXDaysInFuture);

    /// <summary>
    ///     Checks if the specified DateTime value is more than the given number of days in the future.
    /// </summary>
    /// <param name="value">The DateTime value to check.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <returns>True if the value is more than the specified number of days in the future; otherwise, false.</returns>
    public static bool CheckIsMoreThanXDaysInFuture(this DateTime value, int days)
    {
        var now = DateTime.Now;
        var difference = (value - now).TotalDays;
        return difference > days;
    }

    /// <summary>
    ///     Checks if the specified nullable DateTime value is more than the given number of days in the future.
    /// </summary>
    /// <param name="value">The nullable DateTime value to check.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <returns>True if the value is more than the specified number of days in the future; otherwise, false.</returns>
    public static bool CheckIsMoreThanXDaysInFuture(this DateTime? value, int days)
    {
        return value.HasValue && CheckIsMoreThanXDaysInFuture(value.Value, days);
    }

    /// <summary>
    ///     Checks if the specified DateTimeOffset value is more than the given number of days in the future.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to check.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <returns>True if the value is more than the specified number of days in the future; otherwise, false.</returns>
    public static bool CheckIsMoreThanXDaysInFuture(this DateTimeOffset value, int days)
    {
        var now = DateTimeOffset.Now;
        var difference = (value - now).TotalDays;
        return difference > days;
    }

    /// <summary>
    ///     Checks if the specified nullable DateTimeOffset value is more than the given number of days in the future.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to check.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <returns>True if the value is more than the specified number of days in the future; otherwise, false.</returns>
    public static bool CheckIsMoreThanXDaysInFuture(this DateTimeOffset? value, int days)
    {
        return value.HasValue && CheckIsMoreThanXDaysInFuture(value.Value, days);
    }

    /// <summary>
    ///     Ensures that the specified DateTime value is more than the given number of days in the future, throwing a
    ///     ValidationException if it is not.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">
    ///     Thrown when the value is not more than the specified number of days in the
    ///     future.
    /// </exception>
    public static void EnsureIsMoreThanXDaysInFuture(this DateTime value, int days, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsMoreThanXDaysInFuture(value, days);
        if (!isValid)
        {
            var now = DateTime.Now;
            var actualDays = (value - now).TotalDays;

            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Days", days),
                ("CurrentTime", now),
                ("ActualDays", actualDays)
            };
            throw ValidationException.Create(ValidatorName,
                $"Value must be more than {days} days in the future. Actual difference: {actualDays:F2} days.",
                fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified nullable DateTime value is more than the given number of days in the future, throwing a
    ///     ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">
    ///     Thrown when the value is null or not more than the specified number of days in
    ///     the future.
    /// </exception>
    public static void EnsureIsMoreThanXDaysInFuture(this DateTime? value, int days, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard,
                contextList);
        }

        EnsureIsMoreThanXDaysInFuture(value.Value, days, fieldName, blackboard);
    }

    /// <summary>
    ///     Ensures that the specified DateTimeOffset value is more than the given number of days in the future, throwing a
    ///     ValidationException if it is not.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">
    ///     Thrown when the value is not more than the specified number of days in the
    ///     future.
    /// </exception>
    public static void EnsureIsMoreThanXDaysInFuture(this DateTimeOffset value, int days, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsMoreThanXDaysInFuture(value, days);
        if (!isValid)
        {
            var now = DateTimeOffset.Now;
            var actualDays = (value - now).TotalDays;

            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Days", days),
                ("CurrentTime", now),
                ("ActualDays", actualDays)
            };
            throw ValidationException.Create(ValidatorName,
                $"Value must be more than {days} days in the future. Actual difference: {actualDays:F2} days.",
                fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified nullable DateTimeOffset value is more than the given number of days in the future,
    ///     throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">
    ///     Thrown when the value is null or not more than the specified number of days in
    ///     the future.
    /// </exception>
    public static void EnsureIsMoreThanXDaysInFuture(this DateTimeOffset? value, int days, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard,
                contextList);
        }

        EnsureIsMoreThanXDaysInFuture(value.Value, days, fieldName, blackboard);
    }

    /// <summary>
    ///     Validates if the specified DateTime value is more than the given number of days in the future and returns a
    ///     ValidationResult.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is more than the specified number of days in the future.</returns>
    public static ValidationResult ValidateIsMoreThanXDaysInFuture(this DateTime value, int days,
        string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsMoreThanXDaysInFuture(value, days);
        var now = DateTime.Now;
        var actualDays = (value - now).TotalDays;

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Days", days),
            ("CurrentTime", now),
            ("ActualDays", actualDays)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                $"Value must be more than {days} days in the future. Actual difference: {actualDays:F2} days.",
                fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates if the specified nullable DateTime value is more than the given number of days in the future and returns
    ///     a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is more than the specified number of days in the future.</returns>
    public static ValidationResult ValidateIsMoreThanXDaysInFuture(this DateTime? value, int days,
        string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName,
                blackboard, contextList);
        }

        return ValidateIsMoreThanXDaysInFuture(value.Value, days, fieldName, blackboard);
    }

    /// <summary>
    ///     Validates if the specified DateTimeOffset value is more than the given number of days in the future and returns a
    ///     ValidationResult.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is more than the specified number of days in the future.</returns>
    public static ValidationResult ValidateIsMoreThanXDaysInFuture(this DateTimeOffset value, int days,
        string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsMoreThanXDaysInFuture(value, days);
        var now = DateTimeOffset.Now;
        var actualDays = (value - now).TotalDays;

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Days", days),
            ("CurrentTime", now),
            ("ActualDays", actualDays)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                $"Value must be more than {days} days in the future. Actual difference: {actualDays:F2} days.",
                fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates if the specified nullable DateTimeOffset value is more than the given number of days in the future and
    ///     returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="days">The number of days to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is more than the specified number of days in the future.</returns>
    public static ValidationResult ValidateIsMoreThanXDaysInFuture(this DateTimeOffset? value, int days,
        string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName,
                blackboard, contextList);
        }

        return ValidateIsMoreThanXDaysInFuture(value.Value, days, fieldName, blackboard);
    }
}
