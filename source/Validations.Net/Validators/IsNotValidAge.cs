using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Validates that a value does NOT represent a valid age.
/// </summary>
public static class IsNotValidAge
{
    private const string ValidatorName = nameof(IsNotValidAge);

    /// <summary>
    ///     Checks if the specified value is NOT a valid adult age (18+).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a valid adult age; otherwise, false.</returns>
    public static bool CheckIsNotValidAdultAge(int value)
    {
        return !IsValidAge.CheckIsValidAdultAge(value);
    }

    /// <summary>
    ///     Checks if the specified value is NOT a valid age (0-150).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a valid age; otherwise, false.</returns>
    public static bool CheckIsNotValidAge(int value)
    {
        return !IsValidAge.CheckIsValidAge(value);
    }

    /// <summary>
    ///     Checks if the specified value is NOT a valid age within the specified range.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="minAge">The minimum valid age (inclusive).</param>
    /// <param name="maxAge">The maximum valid age (inclusive).</param>
    /// <returns>True if the value is NOT a valid age within the range; otherwise, false.</returns>
    public static bool CheckIsNotValidAge(int value, int minAge, int maxAge)
    {
        return !IsValidAge.CheckIsValidAge(value, minAge, maxAge);
    }

    /// <summary>
    ///     Checks if the specified value is NOT a valid child age (0-17).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a valid child age; otherwise, false.</returns>
    public static bool CheckIsNotValidChildAge(int value)
    {
        return !IsValidAge.CheckIsValidChildAge(value);
    }

    /// <summary>
    ///     Checks if the specified value is NOT a valid senior age (65+).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a valid senior age; otherwise, false.</returns>
    public static bool CheckIsNotValidSeniorAge(int value)
    {
        return !IsValidAge.CheckIsValidSeniorAge(value);
    }

    /// <summary>
    ///     Checks if the specified value is NOT a valid working age (18-65).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a valid working age; otherwise, false.</returns>
    public static bool CheckIsNotValidWorkingAge(int value)
    {
        return !IsValidAge.CheckIsValidWorkingAge(value);
    }

    /// <summary>
    ///     Ensures that the specified value is NOT a valid adult age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a valid adult age.</exception>
    public static void EnsureIsNotValidAdultAge(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidAdultAge(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a valid adult age (18 or older).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified value is NOT a valid age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a valid age.</exception>
    public static void EnsureIsNotValidAge(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidAge(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a valid age (0-150).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified value is NOT a valid age within the specified range.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="minAge">The minimum valid age (inclusive).</param>
    /// <param name="maxAge">The maximum valid age (inclusive).</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a valid age within the range.</exception>
    public static void EnsureIsNotValidAge(int value, int minAge, int maxAge, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidAge(value, minAge, maxAge);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("MinAge", minAge),
            ("MaxAge", maxAge),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a valid age (between {minAge} and {maxAge}).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified value is NOT a valid child age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a valid child age.</exception>
    public static void EnsureIsNotValidChildAge(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidChildAge(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a valid child age (between 0 and 17).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified value is NOT a valid senior age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a valid senior age.</exception>
    public static void EnsureIsNotValidSeniorAge(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidSeniorAge(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a valid senior age (65 or older).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified value is NOT a valid working age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a valid working age.</exception>
    public static void EnsureIsNotValidWorkingAge(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidWorkingAge(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a valid working age (between 18 and 65).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Validates that the specified value is NOT a valid adult age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is NOT a valid adult age.</returns>
    public static ValidationResult ValidateIsNotValidAdultAge(int value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidAdultAge(value);
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
            $"The value '{value}' is a valid adult age (18 or older).",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is NOT a valid age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is NOT a valid age.</returns>
    public static ValidationResult ValidateIsNotValidAge(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidAge(value);
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
            $"The value '{value}' is a valid age (0-150).",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is NOT a valid age within the specified range.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="minAge">The minimum valid age (inclusive).</param>
    /// <param name="maxAge">The maximum valid age (inclusive).</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is NOT a valid age within the range.</returns>
    public static ValidationResult ValidateIsNotValidAge(int value, int minAge, int maxAge, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidAge(value, minAge, maxAge);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("MinAge", minAge),
            ("MaxAge", maxAge),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a valid age (between {minAge} and {maxAge}).",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is NOT a valid child age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is NOT a valid child age.</returns>
    public static ValidationResult ValidateIsNotValidChildAge(int value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidChildAge(value);
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
            $"The value '{value}' is a valid child age (between 0 and 17).",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is NOT a valid senior age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is NOT a valid senior age.</returns>
    public static ValidationResult ValidateIsNotValidSeniorAge(int value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidSeniorAge(value);
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
            $"The value '{value}' is a valid senior age (65 or older).",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is NOT a valid working age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is NOT a valid working age.</returns>
    public static ValidationResult ValidateIsNotValidWorkingAge(int value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidWorkingAge(value);
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
            $"The value '{value}' is a valid working age (between 18 and 65).",
            fieldName,
            blackboard,
            contextList);
    }
}
