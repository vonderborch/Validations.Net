using System;
using System.Collections.Generic;
using System.Linq;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Validates that a value is valid Base64 encoded data.
/// </summary>
public static class IsValidBase64
{
    private const string ValidatorName = nameof(IsValidBase64);

    /// <summary>
    /// Checks if the specified string is valid Base64 encoded data.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is valid Base64; otherwise, false.</returns>
    public static bool CheckIsValidBase64(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        try
        {
            Convert.FromBase64String(value);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the specified string is valid Base64 encoded data with padding.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is valid Base64 with padding; otherwise, false.</returns>
    public static bool CheckIsValidBase64WithPadding(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        // Check if the string has proper padding
        var length = value.Length;
        if (length % 4 != 0)
            return false;

        // Check for proper padding characters
        var paddingCount = 0;
        for (int i = length - 1; i >= 0 && value[i] == '='; i--)
        {
            paddingCount++;
        }

        if (paddingCount > 2)
            return false;

        return CheckIsValidBase64(value);
    }

    /// <summary>
    /// Checks if the specified string is valid Base64 encoded data without padding.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is valid Base64 without padding; otherwise, false.</returns>
    public static bool CheckIsValidBase64WithoutPadding(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        // Check if the string doesn't have padding
        if (value.Contains('='))
            return false;

        return CheckIsValidBase64(value);
    }

    /// <summary>
    /// Checks if the specified string is valid Base64 encoded data and has a specific length.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="expectedLength">The expected length of the decoded data in bytes.</param>
    /// <returns>True if the string is valid Base64 with the expected decoded length; otherwise, false.</returns>
    public static bool CheckIsValidBase64WithLength(string? value, int expectedLength)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        try
        {
            var decodedBytes = Convert.FromBase64String(value);
            return decodedBytes.Length == expectedLength;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the specified string is valid Base64 encoded data and has a minimum length.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="minimumLength">The minimum length of the decoded data in bytes.</param>
    /// <returns>True if the string is valid Base64 with at least the minimum decoded length; otherwise, false.</returns>
    public static bool CheckIsValidBase64WithMinimumLength(string? value, int minimumLength)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        try
        {
            var decodedBytes = Convert.FromBase64String(value);
            return decodedBytes.Length >= minimumLength;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    /// <summary>
    /// Validates that the specified string is valid Base64 encoded data.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is valid Base64.</returns>
    public static ValidationResult ValidateIsValidBase64(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidBase64(value);
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
            $"The value '{value}' is not valid Base64 encoded data.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified string is valid Base64 encoded data with padding.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is valid Base64 with padding.</returns>
    public static ValidationResult ValidateIsValidBase64WithPadding(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidBase64WithPadding(value);
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
            $"The value '{value}' is not valid Base64 encoded data with proper padding.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified string is valid Base64 encoded data without padding.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is valid Base64 without padding.</returns>
    public static ValidationResult ValidateIsValidBase64WithoutPadding(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidBase64WithoutPadding(value);
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
            $"The value '{value}' is not valid Base64 encoded data without padding.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified string is valid Base64 encoded data and has a specific length.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="expectedLength">The expected length of the decoded data in bytes.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is valid Base64 with the expected decoded length.</returns>
    public static ValidationResult ValidateIsValidBase64WithLength(string? value, int expectedLength, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidBase64WithLength(value, expectedLength);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ExpectedLength", expectedLength),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is not valid Base64 encoded data with decoded length {expectedLength} bytes.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified string is valid Base64 encoded data and has a minimum length.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="minimumLength">The minimum length of the decoded data in bytes.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is valid Base64 with at least the minimum decoded length.</returns>
    public static ValidationResult ValidateIsValidBase64WithMinimumLength(string? value, int minimumLength, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidBase64WithMinimumLength(value, minimumLength);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("MinimumLength", minimumLength),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is not valid Base64 encoded data with at least {minimumLength} bytes when decoded.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Ensures that the specified string is valid Base64 encoded data.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not valid Base64.</exception>
    public static void EnsureIsValidBase64(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidBase64(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not valid Base64 encoded data.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is valid Base64 encoded data with padding.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not valid Base64 with padding.</exception>
    public static void EnsureIsValidBase64WithPadding(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidBase64WithPadding(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not valid Base64 encoded data with proper padding.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is valid Base64 encoded data without padding.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not valid Base64 without padding.</exception>
    public static void EnsureIsValidBase64WithoutPadding(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidBase64WithoutPadding(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not valid Base64 encoded data without padding.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is valid Base64 encoded data and has a specific length.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="expectedLength">The expected length of the decoded data in bytes.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not valid Base64 with the expected decoded length.</exception>
    public static void EnsureIsValidBase64WithLength(string? value, int expectedLength, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidBase64WithLength(value, expectedLength);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ExpectedLength", expectedLength),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not valid Base64 encoded data with decoded length {expectedLength} bytes.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is valid Base64 encoded data and has a minimum length.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="minimumLength">The minimum length of the decoded data in bytes.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not valid Base64 with at least the minimum decoded length.</exception>
    public static void EnsureIsValidBase64WithMinimumLength(string? value, int minimumLength, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidBase64WithMinimumLength(value, minimumLength);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("MinimumLength", minimumLength),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not valid Base64 encoded data with at least {minimumLength} bytes when decoded.",
                fieldName,
                blackboard,
                contextList);
        }
    }
}
