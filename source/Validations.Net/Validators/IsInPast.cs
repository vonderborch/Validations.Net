using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a date/time value is in the past.
/// </summary>
public static class IsInPast
{
    private const string ValidatorName = nameof(IsInPast);

    /// <summary>
    ///     Checks if the specified DateTime value is in the past.
    /// </summary>
    /// <param name="value">The DateTime value to check.</param>
    /// <returns>True if the value is in the past; otherwise, false.</returns>
    public static bool CheckIsInPast(this DateTime value)
    {
        return value < DateTime.Now;
    }

    /// <summary>
    ///     Checks if the specified nullable DateTime value is in the past.
    /// </summary>
    /// <param name="value">The nullable DateTime value to check.</param>
    /// <returns>True if the value is in the past; otherwise, false.</returns>
    public static bool CheckIsInPast(this DateTime? value)
    {
        return value.HasValue && CheckIsInPast(value.Value);
    }

    /// <summary>
    ///     Checks if the specified DateTimeOffset value is in the past.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to check.</param>
    /// <returns>True if the value is in the past; otherwise, false.</returns>
    public static bool CheckIsInPast(this DateTimeOffset value)
    {
        return value < DateTimeOffset.Now;
    }

    /// <summary>
    ///     Checks if the specified nullable DateTimeOffset value is in the past.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to check.</param>
    /// <returns>True if the value is in the past; otherwise, false.</returns>
    public static bool CheckIsInPast(this DateTimeOffset? value)
    {
        return value.HasValue && CheckIsInPast(value.Value);
    }

    /// <summary>
    ///     Ensures that the specified DateTime value is in the past, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not in the past.</exception>
    public static void EnsureIsInPast(this DateTime value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsInPast(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("CurrentTime", DateTime.Now)
            };
            throw ValidationException.Create(ValidatorName, "Value must be in the past.", fieldName, blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified nullable DateTime value is in the past, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or not in the past.</exception>
    public static void EnsureIsInPast(this DateTime? value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard,
                contextList);
        }

        EnsureIsInPast(value.Value, fieldName, blackboard);
    }

    /// <summary>
    ///     Ensures that the specified DateTimeOffset value is in the past, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not in the past.</exception>
    public static void EnsureIsInPast(this DateTimeOffset value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsInPast(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("CurrentTime", DateTimeOffset.Now)
            };
            throw ValidationException.Create(ValidatorName, "Value must be in the past.", fieldName, blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified nullable DateTimeOffset value is in the past, throwing a ValidationException if it is
    ///     not.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or not in the past.</exception>
    public static void EnsureIsInPast(this DateTimeOffset? value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard,
                contextList);
        }

        EnsureIsInPast(value.Value, fieldName, blackboard);
    }

    /// <summary>
    ///     Validates if the specified DateTime value is in the past and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is in the past.</returns>
    public static ValidationResult ValidateIsInPast(this DateTime value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsInPast(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("CurrentTime", DateTime.Now)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be in the past.", fieldName,
                blackboard, contextList);
    }

    /// <summary>
    ///     Validates if the specified nullable DateTime value is in the past and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is in the past.</returns>
    public static ValidationResult ValidateIsInPast(this DateTime? value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName,
                blackboard, contextList);
        }

        return ValidateIsInPast(value.Value, fieldName, blackboard);
    }

    /// <summary>
    ///     Validates if the specified DateTimeOffset value is in the past and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is in the past.</returns>
    public static ValidationResult ValidateIsInPast(this DateTimeOffset value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsInPast(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("CurrentTime", DateTimeOffset.Now)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be in the past.", fieldName,
                blackboard, contextList);
    }

    /// <summary>
    ///     Validates if the specified nullable DateTimeOffset value is in the past and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is in the past.</returns>
    public static ValidationResult ValidateIsInPast(this DateTimeOffset? value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName,
                blackboard, contextList);
        }

        return ValidateIsInPast(value.Value, fieldName, blackboard);
    }
}
