using System;
using System.Collections.Generic;
using System.Linq;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Validates that a value represents a valid age.
/// </summary>
public static class IsValidAge
{
    private const string ValidatorName = nameof(IsValidAge);
    private const int MinAge = 0;
    private const int MaxAge = 150;

    /// <summary>
    /// Checks if the specified value is a valid age (0-150).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is a valid age; otherwise, false.</returns>
    public static bool CheckIsValidAge(int value)
    {
        return value >= MinAge && value <= MaxAge;
    }

    /// <summary>
    /// Checks if the specified value is a valid age within the specified range.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="minAge">The minimum valid age (inclusive).</param>
    /// <param name="maxAge">The maximum valid age (inclusive).</param>
    /// <returns>True if the value is a valid age within the range; otherwise, false.</returns>
    public static bool CheckIsValidAge(int value, int minAge, int maxAge)
    {
        return value >= minAge && value <= maxAge;
    }

    /// <summary>
    /// Checks if the specified value is a valid adult age (18+).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is a valid adult age; otherwise, false.</returns>
    public static bool CheckIsValidAdultAge(int value)
    {
        return CheckIsValidAge(value, 18, MaxAge);
    }

    /// <summary>
    /// Checks if the specified value is a valid child age (0-17).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is a valid child age; otherwise, false.</returns>
    public static bool CheckIsValidChildAge(int value)
    {
        return CheckIsValidAge(value, 0, 17);
    }

    /// <summary>
    /// Checks if the specified value is a valid senior age (65+).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is a valid senior age; otherwise, false.</returns>
    public static bool CheckIsValidSeniorAge(int value)
    {
        return CheckIsValidAge(value, 65, MaxAge);
    }

    /// <summary>
    /// Checks if the specified value is a valid working age (18-65).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is a valid working age; otherwise, false.</returns>
    public static bool CheckIsValidWorkingAge(int value)
    {
        return CheckIsValidAge(value, 18, 65);
    }

    /// <summary>
    /// Validates that the specified value is a valid age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is a valid age.</returns>
    public static ValidationResult ValidateIsValidAge(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidAge(value);
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
            $"The value '{value}' is not a valid age (must be between {MinAge} and {MaxAge}).",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified value is a valid age within the specified range.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="minAge">The minimum valid age (inclusive).</param>
    /// <param name="maxAge">The maximum valid age (inclusive).</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is a valid age within the range.</returns>
    public static ValidationResult ValidateIsValidAge(int value, int minAge, int maxAge, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidAge(value, minAge, maxAge);
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
            $"The value '{value}' is not a valid age (must be between {minAge} and {maxAge}).",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified value is a valid adult age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is a valid adult age.</returns>
    public static ValidationResult ValidateIsValidAdultAge(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidAdultAge(value);
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
            $"The value '{value}' is not a valid adult age (must be 18 or older).",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified value is a valid child age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is a valid child age.</returns>
    public static ValidationResult ValidateIsValidChildAge(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidChildAge(value);
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
            $"The value '{value}' is not a valid child age (must be between 0 and 17).",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified value is a valid senior age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is a valid senior age.</returns>
    public static ValidationResult ValidateIsValidSeniorAge(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidSeniorAge(value);
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
            $"The value '{value}' is not a valid senior age (must be 65 or older).",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified value is a valid working age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is a valid working age.</returns>
    public static ValidationResult ValidateIsValidWorkingAge(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidWorkingAge(value);
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
            $"The value '{value}' is not a valid working age (must be between 18 and 65).",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Ensures that the specified value is a valid age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a valid age.</exception>
    public static void EnsureIsValidAge(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidAge(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not a valid age (must be between {MinAge} and {MaxAge}).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified value is a valid age within the specified range.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="minAge">The minimum valid age (inclusive).</param>
    /// <param name="maxAge">The maximum valid age (inclusive).</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a valid age within the range.</exception>
    public static void EnsureIsValidAge(int value, int minAge, int maxAge, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidAge(value, minAge, maxAge);
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
                $"The value '{value}' is not a valid age (must be between {minAge} and {maxAge}).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified value is a valid adult age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a valid adult age.</exception>
    public static void EnsureIsValidAdultAge(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidAdultAge(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not a valid adult age (must be 18 or older).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified value is a valid child age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a valid child age.</exception>
    public static void EnsureIsValidChildAge(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidChildAge(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not a valid child age (must be between 0 and 17).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified value is a valid senior age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a valid senior age.</exception>
    public static void EnsureIsValidSeniorAge(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidSeniorAge(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not a valid senior age (must be 65 or older).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified value is a valid working age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a valid working age.</exception>
    public static void EnsureIsValidWorkingAge(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidWorkingAge(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not a valid working age (must be between 18 and 65).",
                fieldName,
                blackboard,
                contextList);
        }
    }
}
