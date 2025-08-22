using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Validates that a value is NOT a palindrome.
/// </summary>
public static class IsNotPalindrome
{
    private const string ValidatorName = nameof(IsNotPalindrome);

    /// <summary>
    ///     Checks if the specified string is NOT a palindrome (case-insensitive).
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT a palindrome; otherwise, false.</returns>
    public static bool CheckIsNotPalindrome(string? value)
    {
        return !IsPalindrome.CheckIsPalindrome(value);
    }

    /// <summary>
    ///     Checks if the specified string is NOT a palindrome with the specified comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <returns>True if the string is NOT a palindrome; otherwise, false.</returns>
    public static bool CheckIsNotPalindrome(string? value, StringComparison comparison)
    {
        return !IsPalindrome.CheckIsPalindrome(value, comparison);
    }

    /// <summary>
    ///     Checks if the specified string is NOT a palindrome (case-insensitive).
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT a palindrome; otherwise, false.</returns>
    public static bool CheckIsNotPalindromeCaseInsensitive(string? value)
    {
        return !IsPalindrome.CheckIsPalindromeCaseInsensitive(value);
    }

    /// <summary>
    ///     Checks if the specified string is NOT a palindrome (case-sensitive).
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT a palindrome; otherwise, false.</returns>
    public static bool CheckIsNotPalindromeCaseSensitive(string? value)
    {
        return !IsPalindrome.CheckIsPalindromeCaseSensitive(value);
    }

    /// <summary>
    ///     Checks if the specified string is NOT a palindrome, ignoring spaces and punctuation.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT a palindrome; otherwise, false.</returns>
    public static bool CheckIsNotPalindromeIgnoreSpacesAndPunctuation(string? value)
    {
        return !IsPalindrome.CheckIsPalindromeIgnoreSpacesAndPunctuation(value);
    }

    /// <summary>
    ///     Ensures that the specified string is NOT a palindrome.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a palindrome.</exception>
    public static void EnsureIsNotPalindrome(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPalindrome(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a palindrome.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified string is NOT a palindrome with the specified comparison.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a palindrome.</exception>
    public static void EnsureIsNotPalindrome(string? value, StringComparison comparison, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPalindrome(value, comparison);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Comparison", comparison),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a palindrome (using {comparison} comparison).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified string is NOT a palindrome (case-insensitive).
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a palindrome.</exception>
    public static void EnsureIsNotPalindromeCaseInsensitive(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPalindromeCaseInsensitive(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a palindrome (case-insensitive).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified string is NOT a palindrome (case-sensitive).
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a palindrome.</exception>
    public static void EnsureIsNotPalindromeCaseSensitive(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPalindromeCaseSensitive(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a palindrome (case-sensitive).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified string is NOT a palindrome, ignoring spaces and punctuation.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a palindrome.</exception>
    public static void EnsureIsNotPalindromeIgnoreSpacesAndPunctuation(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPalindromeIgnoreSpacesAndPunctuation(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a palindrome (ignoring spaces and punctuation).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Validates that the specified string is NOT a palindrome.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is NOT a palindrome.</returns>
    public static ValidationResult ValidateIsNotPalindrome(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPalindrome(value);
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
            $"The value '{value}' is a palindrome.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified string is NOT a palindrome with the specified comparison.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is NOT a palindrome.</returns>
    public static ValidationResult ValidateIsNotPalindrome(string? value, StringComparison comparison, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPalindrome(value, comparison);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Comparison", comparison),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a palindrome (using {comparison} comparison).",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified string is NOT a palindrome (case-insensitive).
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is NOT a palindrome.</returns>
    public static ValidationResult ValidateIsNotPalindromeCaseInsensitive(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPalindromeCaseInsensitive(value);
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
            $"The value '{value}' is a palindrome (case-insensitive).",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified string is NOT a palindrome (case-sensitive).
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is NOT a palindrome.</returns>
    public static ValidationResult ValidateIsNotPalindromeCaseSensitive(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPalindromeCaseSensitive(value);
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
            $"The value '{value}' is a palindrome (case-sensitive).",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified string is NOT a palindrome, ignoring spaces and punctuation.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is NOT a palindrome.</returns>
    public static ValidationResult ValidateIsNotPalindromeIgnoreSpacesAndPunctuation(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPalindromeIgnoreSpacesAndPunctuation(value);
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
            $"The value '{value}' is a palindrome (ignoring spaces and punctuation).",
            fieldName,
            blackboard,
            contextList);
    }
}
