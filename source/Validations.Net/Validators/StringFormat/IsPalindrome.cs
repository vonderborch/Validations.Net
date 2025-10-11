using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string is a palindrome.
/// </summary>
public static class IsPalindrome
{
    private const string ValidatorName = nameof(IsPalindrome);

    /// <summary>
    /// Checks if the specified string is a palindrome.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a palindrome; otherwise, false.</returns>
    public static bool CheckIsPalindrome(string? value)
    {
        return CheckIsPalindrome(value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Checks if the specified string is a palindrome with the specified string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <returns>True if the string is a palindrome; otherwise, false.</returns>
    public static bool CheckIsPalindrome(string? value, StringComparison comparison)
    {
        if (string.IsNullOrEmpty(value)) return false;
        return IsPalindromeInternal(value, comparison);
    }

    /// <summary>
    /// Checks if the specified string is a palindrome (case-sensitive).
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a palindrome; otherwise, false.</returns>
    public static bool CheckIsPalindromeCaseSensitive(string? value)
    {
        return CheckIsPalindrome(value, StringComparison.Ordinal);
    }

    /// <summary>
    /// Checks if the specified string is a palindrome (case-insensitive).
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a palindrome; otherwise, false.</returns>
    public static bool CheckIsPalindromeCaseInsensitive(string? value)
    {
        return CheckIsPalindrome(value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Checks if the specified string is a palindrome, ignoring spaces and punctuation.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a palindrome; otherwise, false.</returns>
    public static bool CheckIsPalindromeIgnoreSpacesAndPunctuation(string? value)
    {
        if (string.IsNullOrEmpty(value)) return false;
        var cleaned = CleanString(value);
        return IsPalindromeInternal(cleaned, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Ensures that the specified string is a palindrome, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a palindrome.</exception>
    public static void EnsureIsPalindrome(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPalindrome(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a palindrome.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is a palindrome with the specified string comparison, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a palindrome.</exception>
    public static void EnsureIsPalindrome(string? value, StringComparison comparison, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPalindrome(value, comparison);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Comparison", comparison)
            };
            throw ValidationException.Create(ValidatorName, $"Value must be a palindrome using {comparison} comparison.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is a palindrome (case-sensitive), throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a palindrome.</exception>
    public static void EnsureIsPalindromeCaseSensitive(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPalindromeCaseSensitive(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a palindrome (case-sensitive).", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is a palindrome (case-insensitive), throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a palindrome.</exception>
    public static void EnsureIsPalindromeCaseInsensitive(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPalindromeCaseInsensitive(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a palindrome (case-insensitive).", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is a palindrome, ignoring spaces and punctuation, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a palindrome.</exception>
    public static void EnsureIsPalindromeIgnoreSpacesAndPunctuation(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPalindromeIgnoreSpacesAndPunctuation(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a palindrome (ignoring spaces and punctuation).", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Validates that the specified string is a palindrome.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a palindrome.</returns>
    public static ValidationResult ValidateIsPalindrome(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPalindrome(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a palindrome.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified string is a palindrome with the specified string comparison.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a palindrome.</returns>
    public static ValidationResult ValidateIsPalindrome(string? value, StringComparison comparison, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPalindrome(value, comparison);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Comparison", comparison),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must be a palindrome using {comparison} comparison.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified string is a palindrome (case-sensitive).
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a palindrome.</returns>
    public static ValidationResult ValidateIsPalindromeCaseSensitive(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPalindromeCaseSensitive(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a palindrome (case-sensitive).",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified string is a palindrome (case-insensitive).
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a palindrome.</returns>
    public static ValidationResult ValidateIsPalindromeCaseInsensitive(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPalindromeCaseInsensitive(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a palindrome (case-insensitive).",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified string is a palindrome, ignoring spaces and punctuation.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a palindrome.</returns>
    public static ValidationResult ValidateIsPalindromeIgnoreSpacesAndPunctuation(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPalindromeIgnoreSpacesAndPunctuation(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a palindrome (ignoring spaces and punctuation).",
            parameterName, blackboard, contextList);
    }

    private static bool IsPalindromeInternal(string value, StringComparison comparison)
    {
        var normalized = NormalizeString(value, comparison);
        var length = normalized.Length;
        
        for (int i = 0; i < length / 2; i++)
        {
            if (string.Compare(normalized[i].ToString(), normalized[length - 1 - i].ToString(), comparison) != 0)
            {
                return false;
            }
        }
        
        return true;
    }

    private static string NormalizeString(string value, StringComparison comparison)
    {
        return comparison == StringComparison.OrdinalIgnoreCase ? value.ToLowerInvariant() : value;
    }

    private static string CleanString(string value)
    {
        var result = new StringBuilder();
        foreach (char c in value)
        {
            if (char.IsLetterOrDigit(c))
            {
                result.Append(char.ToLowerInvariant(c));
            }
        }
        return result.ToString();
    }
}
