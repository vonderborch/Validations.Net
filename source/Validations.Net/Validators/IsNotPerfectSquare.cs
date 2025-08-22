using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Validates that a value is NOT a perfect square.
/// </summary>
public static class IsNotPerfectSquare
{
    private const string ValidatorName = nameof(IsNotPerfectSquare);

    /// <summary>
    ///     Checks if the specified value is NOT a perfect square.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a perfect square; otherwise, false.</returns>
    public static bool CheckIsNotPerfectSquare(int value)
    {
        return !IsPerfectSquare.CheckIsPerfectSquare(value);
    }

    /// <summary>
    ///     Checks if the specified value is NOT a perfect square.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a perfect square; otherwise, false.</returns>
    public static bool CheckIsNotPerfectSquare(long value)
    {
        return !IsPerfectSquare.CheckIsPerfectSquare(value);
    }

    /// <summary>
    ///     Checks if the specified value is NOT a perfect square.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a perfect square; otherwise, false.</returns>
    public static bool CheckIsNotPerfectSquare(double value)
    {
        return !IsPerfectSquare.CheckIsPerfectSquare(value);
    }

    /// <summary>
    ///     Ensures that the specified value is NOT a perfect square.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a perfect square.</exception>
    public static void EnsureIsNotPerfectSquare(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPerfectSquare(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a perfect square.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified value is NOT a perfect square.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a perfect square.</exception>
    public static void EnsureIsNotPerfectSquare(long value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPerfectSquare(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a perfect square.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified value is NOT a perfect square.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a perfect square.</exception>
    public static void EnsureIsNotPerfectSquare(double value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPerfectSquare(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a perfect square.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Validates that the specified value is NOT a perfect square.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is NOT a perfect square.</returns>
    public static ValidationResult ValidateIsNotPerfectSquare(int value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPerfectSquare(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a perfect square.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is NOT a perfect square.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is NOT a perfect square.</returns>
    public static ValidationResult ValidateIsNotPerfectSquare(long value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPerfectSquare(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a perfect square.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is NOT a perfect square.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is NOT a perfect square.</returns>
    public static ValidationResult ValidateIsNotPerfectSquare(double value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPerfectSquare(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a perfect square.",
            fieldName,
            blackboard,
            contextList);
    }
}
