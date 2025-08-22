using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Validates that a value is NOT valid Base64 encoded data.
/// </summary>
public static class IsNotValidBase64
{
    private const string ValidatorName = nameof(IsNotValidBase64);

    /// <summary>
    ///     Checks if the specified string is NOT valid Base64 encoded data.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT valid Base64; otherwise, false.</returns>
    public static bool CheckIsNotValidBase64(string? value)
    {
        return !IsValidBase64.CheckIsValidBase64(value);
    }

    /// <summary>
    ///     Checks if the specified string is NOT valid Base64 encoded data and has a specific length.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="expectedLength">The expected length of the decoded data in bytes.</param>
    /// <returns>True if the string is NOT valid Base64 with the expected decoded length; otherwise, false.</returns>
    public static bool CheckIsNotValidBase64WithLength(string? value, int expectedLength)
    {
        return !IsValidBase64.CheckIsValidBase64WithLength(value, expectedLength);
    }

    /// <summary>
    ///     Checks if the specified string is NOT valid Base64 encoded data and has a minimum length.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="minimumLength">The minimum length of the decoded data in bytes.</param>
    /// <returns>True if the string is NOT valid Base64 with at least the minimum decoded length; otherwise, false.</returns>
    public static bool CheckIsNotValidBase64WithMinimumLength(string? value, int minimumLength)
    {
        return !IsValidBase64.CheckIsValidBase64WithMinimumLength(value, minimumLength);
    }

    /// <summary>
    ///     Checks if the specified string is NOT valid Base64 encoded data without padding.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT valid Base64 without padding; otherwise, false.</returns>
    public static bool CheckIsNotValidBase64WithoutPadding(string? value)
    {
        return !IsValidBase64.CheckIsValidBase64WithoutPadding(value);
    }

    /// <summary>
    ///     Checks if the specified string is NOT valid Base64 encoded data with padding.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT valid Base64 with padding; otherwise, false.</returns>
    public static bool CheckIsNotValidBase64WithPadding(string? value)
    {
        return !IsValidBase64.CheckIsValidBase64WithPadding(value);
    }

    /// <summary>
    ///     Ensures that the specified string is NOT valid Base64 encoded data.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is valid Base64.</exception>
    public static void EnsureIsNotValidBase64(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isNotValid = CheckIsNotValidBase64(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isNotValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is valid Base64 encoded data, but it should not be.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified string is NOT valid Base64 encoded data and has a specific length.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="expectedLength">The expected length of the decoded data in bytes.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is valid Base64 with the expected decoded length.</exception>
    public static void EnsureIsNotValidBase64WithLength(string? value, int expectedLength, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isNotValid = CheckIsNotValidBase64WithLength(value, expectedLength);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ExpectedLength", expectedLength),
            ("FieldName", fieldName)
        };

        if (!isNotValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is valid Base64 encoded data with decoded length {expectedLength} bytes, but it should not be.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified string is NOT valid Base64 encoded data and has a minimum length.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="minimumLength">The minimum length of the decoded data in bytes.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is valid Base64 with at least the minimum decoded length.</exception>
    public static void EnsureIsNotValidBase64WithMinimumLength(string? value, int minimumLength, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isNotValid = CheckIsNotValidBase64WithMinimumLength(value, minimumLength);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("MinimumLength", minimumLength),
            ("FieldName", fieldName)
        };

        if (!isNotValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is valid Base64 encoded data with at least {minimumLength} bytes when decoded, but it should not be.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified string is NOT valid Base64 encoded data without padding.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is valid Base64 without padding.</exception>
    public static void EnsureIsNotValidBase64WithoutPadding(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isNotValid = CheckIsNotValidBase64WithoutPadding(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isNotValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is valid Base64 encoded data without padding, but it should not be.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified string is NOT valid Base64 encoded data with padding.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is valid Base64 with padding.</exception>
    public static void EnsureIsNotValidBase64WithPadding(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isNotValid = CheckIsNotValidBase64WithPadding(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isNotValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is valid Base64 encoded data with proper padding, but it should not be.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Validates that the specified string is NOT valid Base64 encoded data.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is NOT valid Base64.</returns>
    public static ValidationResult ValidateIsNotValidBase64(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isNotValid = CheckIsNotValidBase64(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (isNotValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is valid Base64 encoded data, but it should not be.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified string is NOT valid Base64 encoded data and has a specific length.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="expectedLength">The expected length of the decoded data in bytes.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is NOT valid Base64 with the expected decoded length.</returns>
    public static ValidationResult ValidateIsNotValidBase64WithLength(string? value, int expectedLength,
        string fieldName, IBlackboard? blackboard = null)
    {
        var isNotValid = CheckIsNotValidBase64WithLength(value, expectedLength);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ExpectedLength", expectedLength),
            ("FieldName", fieldName)
        };

        if (isNotValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is valid Base64 encoded data with decoded length {expectedLength} bytes, but it should not be.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified string is NOT valid Base64 encoded data and has a minimum length.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="minimumLength">The minimum length of the decoded data in bytes.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>
    ///     A validation result indicating whether the string is NOT valid Base64 with at least the minimum decoded
    ///     length.
    /// </returns>
    public static ValidationResult ValidateIsNotValidBase64WithMinimumLength(string? value, int minimumLength,
        string fieldName, IBlackboard? blackboard = null)
    {
        var isNotValid = CheckIsNotValidBase64WithMinimumLength(value, minimumLength);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("MinimumLength", minimumLength),
            ("FieldName", fieldName)
        };

        if (isNotValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is valid Base64 encoded data with at least {minimumLength} bytes when decoded, but it should not be.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified string is NOT valid Base64 encoded data without padding.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is NOT valid Base64 without padding.</returns>
    public static ValidationResult ValidateIsNotValidBase64WithoutPadding(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isNotValid = CheckIsNotValidBase64WithoutPadding(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (isNotValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is valid Base64 encoded data without padding, but it should not be.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified string is NOT valid Base64 encoded data with padding.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is NOT valid Base64 with padding.</returns>
    public static ValidationResult ValidateIsNotValidBase64WithPadding(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isNotValid = CheckIsNotValidBase64WithPadding(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (isNotValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is valid Base64 encoded data with proper padding, but it should not be.",
            fieldName,
            blackboard,
            contextList);
    }
}
