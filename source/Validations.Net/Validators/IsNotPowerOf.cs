using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Validates that a value is NOT a power of a specified base.
/// </summary>
public static class IsNotPowerOf
{
    private const string ValidatorName = nameof(IsNotPowerOf);

    /// <summary>
    ///     Checks if the specified value is NOT a power of the given base.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="base">The base to check against.</param>
    /// <returns>True if the value is NOT a power of the base; otherwise, false.</returns>
    public static bool CheckIsNotPowerOf(int value, int @base)
    {
        return !IsPowerOf.CheckIsPowerOf(value, @base);
    }

    /// <summary>
    ///     Checks if the specified value is NOT a power of the given base.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="base">The base to check against.</param>
    /// <returns>True if the value is NOT a power of the base; otherwise, false.</returns>
    public static bool CheckIsNotPowerOf(long value, long @base)
    {
        return !IsPowerOf.CheckIsPowerOf(value, @base);
    }

    /// <summary>
    ///     Checks if the specified value is NOT a power of 10.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a power of 10; otherwise, false.</returns>
    public static bool CheckIsNotPowerOf10(int value)
    {
        return !IsPowerOf.CheckIsPowerOf10(value);
    }

    /// <summary>
    ///     Checks if the specified value is NOT a power of 10.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a power of 10; otherwise, false.</returns>
    public static bool CheckIsNotPowerOf10(long value)
    {
        return !IsPowerOf.CheckIsPowerOf10(value);
    }

    /// <summary>
    ///     Checks if the specified value is NOT a power of 2.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a power of 2; otherwise, false.</returns>
    public static bool CheckIsNotPowerOf2(int value)
    {
        return !IsPowerOf.CheckIsPowerOf2(value);
    }

    /// <summary>
    ///     Checks if the specified value is NOT a power of 2.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a power of 2; otherwise, false.</returns>
    public static bool CheckIsNotPowerOf2(long value)
    {
        return !IsPowerOf.CheckIsPowerOf2(value);
    }

    /// <summary>
    ///     Ensures that the specified value is NOT a power of the given base.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="base">The base to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a power of the base.</exception>
    public static void EnsureIsNotPowerOf(int value, int @base, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPowerOf(value, @base);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Base", @base),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a power of {@base}.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified value is NOT a power of the given base.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="base">The base to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a power of the base.</exception>
    public static void EnsureIsNotPowerOf(long value, long @base, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPowerOf(value, @base);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Base", @base),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a power of {@base}.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified value is NOT a power of 10.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a power of 10.</exception>
    public static void EnsureIsNotPowerOf10(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPowerOf10(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a power of 10.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified value is NOT a power of 10.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a power of 10.</exception>
    public static void EnsureIsNotPowerOf10(long value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPowerOf10(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a power of 10.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified value is NOT a power of 2.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a power of 2.</exception>
    public static void EnsureIsNotPowerOf2(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPowerOf2(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a power of 2.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified value is NOT a power of 2.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a power of 2.</exception>
    public static void EnsureIsNotPowerOf2(long value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPowerOf2(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a power of 2.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Validates that the specified value is NOT a power of the given base.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="base">The base to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is NOT a power of the base.</returns>
    public static ValidationResult ValidateIsNotPowerOf(int value, int @base, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPowerOf(value, @base);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Base", @base),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a power of {@base}.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is NOT a power of the given base.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="base">The base to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is NOT a power of the base.</returns>
    public static ValidationResult ValidateIsNotPowerOf(long value, long @base, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPowerOf(value, @base);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Base", @base),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a power of {@base}.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is NOT a power of 10.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is NOT a power of 10.</returns>
    public static ValidationResult ValidateIsNotPowerOf10(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPowerOf10(value);
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
            $"The value '{value}' is a power of 10.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is NOT a power of 10.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is NOT a power of 10.</returns>
    public static ValidationResult ValidateIsNotPowerOf10(long value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPowerOf10(value);
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
            $"The value '{value}' is a power of 10.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is NOT a power of 2.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is NOT a power of 2.</returns>
    public static ValidationResult ValidateIsNotPowerOf2(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPowerOf2(value);
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
            $"The value '{value}' is a power of 2.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is NOT a power of 2.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is NOT a power of 2.</returns>
    public static ValidationResult ValidateIsNotPowerOf2(long value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPowerOf2(value);
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
            $"The value '{value}' is a power of 2.",
            fieldName,
            blackboard,
            contextList);
    }
}
