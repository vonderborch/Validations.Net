using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Validates that a value is a power of a specified base.
/// </summary>
public static class IsPowerOf
{
    private const string ValidatorName = nameof(IsPowerOf);

    /// <summary>
    ///     Checks if the specified value is a power of the given base.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="base">The base to check against.</param>
    /// <returns>True if the value is a power of the base; otherwise, false.</returns>
    public static bool CheckIsPowerOf(int value, int @base)
    {
        if (value <= 0 || @base <= 1)
        {
            return false;
        }

        if (value == 1)
        {
            return true;
        }

        while (value > 1)
        {
            if (value % @base != 0)
            {
                return false;
            }

            value /= @base;
        }

        return value == 1;
    }

    /// <summary>
    ///     Checks if the specified value is a power of the given base.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="base">The base to check against.</param>
    /// <returns>True if the value is a power of the base; otherwise, false.</returns>
    public static bool CheckIsPowerOf(long value, long @base)
    {
        if (value <= 0 || @base <= 1)
        {
            return false;
        }

        if (value == 1)
        {
            return true;
        }

        while (value > 1)
        {
            if (value % @base != 0)
            {
                return false;
            }

            value /= @base;
        }

        return value == 1;
    }

    /// <summary>
    ///     Checks if the specified value is a power of 10.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is a power of 10; otherwise, false.</returns>
    public static bool CheckIsPowerOf10(int value)
    {
        return CheckIsPowerOf(value, 10);
    }

    /// <summary>
    ///     Checks if the specified value is a power of 10.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is a power of 10; otherwise, false.</returns>
    public static bool CheckIsPowerOf10(long value)
    {
        return CheckIsPowerOf(value, 10);
    }

    /// <summary>
    ///     Checks if the specified value is a power of 2.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is a power of 2; otherwise, false.</returns>
    public static bool CheckIsPowerOf2(int value)
    {
        return value > 0 && (value & (value - 1)) == 0;
    }

    /// <summary>
    ///     Checks if the specified value is a power of 2.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is a power of 2; otherwise, false.</returns>
    public static bool CheckIsPowerOf2(long value)
    {
        return value > 0 && (value & (value - 1)) == 0;
    }

    /// <summary>
    ///     Ensures that the specified value is a power of the given base.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="base">The base to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a power of the base.</exception>
    public static void EnsureIsPowerOf(int value, int @base, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPowerOf(value, @base);
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
                $"The value '{value}' is not a power of {@base}.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified value is a power of the given base.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="base">The base to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a power of the base.</exception>
    public static void EnsureIsPowerOf(long value, long @base, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPowerOf(value, @base);
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
                $"The value '{value}' is not a power of {@base}.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified value is a power of 10.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a power of 10.</exception>
    public static void EnsureIsPowerOf10(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPowerOf10(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not a power of 10.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified value is a power of 10.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a power of 10.</exception>
    public static void EnsureIsPowerOf10(long value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPowerOf10(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not a power of 10.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified value is a power of 2.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a power of 2.</exception>
    public static void EnsureIsPowerOf2(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPowerOf2(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not a power of 2.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified value is a power of 2.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a power of 2.</exception>
    public static void EnsureIsPowerOf2(long value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPowerOf2(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not a power of 2.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Validates that the specified value is a power of the given base.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="base">The base to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is a power of the base.</returns>
    public static ValidationResult ValidateIsPowerOf(int value, int @base, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPowerOf(value, @base);
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
            $"The value '{value}' is not a power of {@base}.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is a power of the given base.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="base">The base to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is a power of the base.</returns>
    public static ValidationResult ValidateIsPowerOf(long value, long @base, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPowerOf(value, @base);
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
            $"The value '{value}' is not a power of {@base}.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is a power of 10.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is a power of 10.</returns>
    public static ValidationResult ValidateIsPowerOf10(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPowerOf10(value);
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
            $"The value '{value}' is not a power of 10.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is a power of 10.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is a power of 10.</returns>
    public static ValidationResult ValidateIsPowerOf10(long value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPowerOf10(value);
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
            $"The value '{value}' is not a power of 10.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is a power of 2.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is a power of 2.</returns>
    public static ValidationResult ValidateIsPowerOf2(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPowerOf2(value);
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
            $"The value '{value}' is not a power of 2.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is a power of 2.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is a power of 2.</returns>
    public static ValidationResult ValidateIsPowerOf2(long value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPowerOf2(value);
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
            $"The value '{value}' is not a power of 2.",
            fieldName,
            blackboard,
            contextList);
    }
}
