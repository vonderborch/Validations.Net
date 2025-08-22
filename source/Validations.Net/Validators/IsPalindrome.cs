using System.Text;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Validates that a value is a palindrome.
/// </summary>
public static class IsPalindrome
{
    private const string ValidatorName = nameof(IsPalindrome);

    /// <summary>
    ///     Checks if the specified string is a palindrome (case-insensitive).
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a palindrome; otherwise, false.</returns>
    public static bool CheckIsPalindrome(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        var normalized = NormalizeString(value);
        return IsPalindromeInternal(normalized);
    }

    /// <summary>
    ///     Checks if the specified string is a palindrome with the specified comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <returns>True if the string is a palindrome; otherwise, false.</returns>
    public static bool CheckIsPalindrome(string? value, StringComparison comparison)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        var normalized = NormalizeString(value, comparison);
        return IsPalindromeInternal(normalized);
    }

    /// <summary>
    ///     Checks if the specified string is a palindrome (case-insensitive).
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a palindrome; otherwise, false.</returns>
    public static bool CheckIsPalindromeCaseInsensitive(string? value)
    {
        return CheckIsPalindrome(value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///     Checks if the specified string is a palindrome (case-sensitive).
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a palindrome; otherwise, false.</returns>
    public static bool CheckIsPalindromeCaseSensitive(string? value)
    {
        return CheckIsPalindrome(value, StringComparison.Ordinal);
    }

    /// <summary>
    ///     Checks if the specified string is a palindrome, ignoring spaces and punctuation.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a palindrome; otherwise, false.</returns>
    public static bool CheckIsPalindromeIgnoreSpacesAndPunctuation(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        var cleaned = CleanString(value);
        return IsPalindromeInternal(cleaned);
    }

    private static string CleanString(string value)
    {
        var sb = new StringBuilder();
        foreach (var c in value)
        {
            if (char.IsLetterOrDigit(c))
            {
                sb.Append(char.ToLowerInvariant(c));
            }
        }

        return sb.ToString();
    }

    /// <summary>
    ///     Ensures that the specified string is a palindrome.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a palindrome.</exception>
    public static void EnsureIsPalindrome(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPalindrome(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not a palindrome.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified string is a palindrome with the specified comparison.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a palindrome.</exception>
    public static void EnsureIsPalindrome(string? value, StringComparison comparison, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPalindrome(value, comparison);
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
                $"The value '{value}' is not a palindrome (using {comparison} comparison).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified string is a palindrome (case-insensitive).
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a palindrome.</exception>
    public static void EnsureIsPalindromeCaseInsensitive(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPalindromeCaseInsensitive(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not a palindrome (case-insensitive).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified string is a palindrome (case-sensitive).
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a palindrome.</exception>
    public static void EnsureIsPalindromeCaseSensitive(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPalindromeCaseSensitive(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not a palindrome (case-sensitive).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified string is a palindrome, ignoring spaces and punctuation.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a palindrome.</exception>
    public static void EnsureIsPalindromeIgnoreSpacesAndPunctuation(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPalindromeIgnoreSpacesAndPunctuation(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not a palindrome (ignoring spaces and punctuation).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    private static bool IsPalindromeInternal(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        var length = value.Length;
        for (var i = 0; i < length / 2; i++)
        {
            if (value[i] != value[length - 1 - i])
            {
                return false;
            }
        }

        return true;
    }

    private static string NormalizeString(string value,
        StringComparison comparison = StringComparison.OrdinalIgnoreCase)
    {
        return value.Normalize();
    }

    /// <summary>
    ///     Validates that the specified string is a palindrome.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is a palindrome.</returns>
    public static ValidationResult ValidateIsPalindrome(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPalindrome(value);
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
            $"The value '{value}' is not a palindrome.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified string is a palindrome with the specified comparison.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is a palindrome.</returns>
    public static ValidationResult ValidateIsPalindrome(string? value, StringComparison comparison, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPalindrome(value, comparison);
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
            $"The value '{value}' is not a palindrome (using {comparison} comparison).",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified string is a palindrome (case-insensitive).
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is a palindrome.</returns>
    public static ValidationResult ValidateIsPalindromeCaseInsensitive(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPalindromeCaseInsensitive(value);
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
            $"The value '{value}' is not a palindrome (case-insensitive).",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified string is a palindrome (case-sensitive).
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is a palindrome.</returns>
    public static ValidationResult ValidateIsPalindromeCaseSensitive(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPalindromeCaseSensitive(value);
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
            $"The value '{value}' is not a palindrome (case-sensitive).",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified string is a palindrome, ignoring spaces and punctuation.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is a palindrome.</returns>
    public static ValidationResult ValidateIsPalindromeIgnoreSpacesAndPunctuation(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPalindromeIgnoreSpacesAndPunctuation(value);
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
            $"The value '{value}' is not a palindrome (ignoring spaces and punctuation).",
            fieldName,
            blackboard,
            contextList);
    }
}
