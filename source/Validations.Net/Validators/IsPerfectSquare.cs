using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Validates that a value is a perfect square.
/// </summary>
public static class IsPerfectSquare
{
    private const string ValidatorName = nameof(IsPerfectSquare);

    /// <summary>
    ///     Checks if the specified value is a perfect square.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is a perfect square; otherwise, false.</returns>
    public static bool CheckIsPerfectSquare(int value)
    {
        if (value < 0)
        {
            return false;
        }

        if (value <= 1)
        {
            return true;
        }

        var sqrt = (int)Math.Sqrt(value);
        return sqrt * sqrt == value;
    }

    /// <summary>
    ///     Checks if the specified value is a perfect square.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is a perfect square; otherwise, false.</returns>
    public static bool CheckIsPerfectSquare(long value)
    {
        if (value < 0)
        {
            return false;
        }

        if (value <= 1)
        {
            return true;
        }

        var sqrt = (long)Math.Sqrt(value);
        return sqrt * sqrt == value;
    }

    /// <summary>
    ///     Checks if the specified value is a perfect square.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is a perfect square; otherwise, false.</returns>
    public static bool CheckIsPerfectSquare(double value)
    {
        if (value < 0)
        {
            return false;
        }

        if (value <= 1)
        {
            return true;
        }

        var sqrt = Math.Sqrt(value);
        var roundedSqrt = Math.Round(sqrt);
        return Math.Abs(sqrt - roundedSqrt) < double.Epsilon && roundedSqrt * roundedSqrt == value;
    }

    /// <summary>
    ///     Ensures that the specified value is a perfect square.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a perfect square.</exception>
    public static void EnsureIsPerfectSquare(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPerfectSquare(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not a perfect square.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified value is a perfect square.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a perfect square.</exception>
    public static void EnsureIsPerfectSquare(long value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPerfectSquare(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not a perfect square.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified value is a perfect square.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a perfect square.</exception>
    public static void EnsureIsPerfectSquare(double value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPerfectSquare(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not a perfect square.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Validates that the specified value is a perfect square.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is a perfect square.</returns>
    public static ValidationResult ValidateIsPerfectSquare(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPerfectSquare(value);
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
            $"The value '{value}' is not a perfect square.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is a perfect square.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is a perfect square.</returns>
    public static ValidationResult ValidateIsPerfectSquare(long value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPerfectSquare(value);
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
            $"The value '{value}' is not a perfect square.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is a perfect square.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is a perfect square.</returns>
    public static ValidationResult ValidateIsPerfectSquare(double value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPerfectSquare(value);
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
            $"The value '{value}' is not a perfect square.",
            fieldName,
            blackboard,
            contextList);
    }
}
