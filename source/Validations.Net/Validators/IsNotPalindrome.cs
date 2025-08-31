using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string is NOT a palindrome.
/// </summary>
public static class IsNotPalindrome
{
    private const string ValidatorName = nameof(IsNotPalindrome);

    /// <summary>
    /// Checks if the specified string is NOT a palindrome.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT a palindrome; otherwise, false.</returns>
    public static bool CheckIsNotPalindrome(string? value)
    {
        return !IsPalindrome.CheckIsPalindrome(value);
    }

    /// <summary>
    /// Checks if the specified string is NOT a palindrome with the specified string comparison.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <returns>True if the string is NOT a palindrome; otherwise, false.</returns>
    public static bool CheckIsNotPalindrome(string? value, StringComparison comparison)
    {
        return !IsPalindrome.CheckIsPalindrome(value, comparison);
    }

    /// <summary>
    /// Checks if the specified string is NOT a palindrome (case-sensitive).
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT a palindrome; otherwise, false.</returns>
    public static bool CheckIsNotPalindromeCaseSensitive(string? value)
    {
        return !IsPalindrome.CheckIsPalindromeCaseSensitive(value);
    }

    /// <summary>
    /// Checks if the specified string is NOT a palindrome (case-insensitive).
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT a palindrome; otherwise, false.</returns>
    public static bool CheckIsNotPalindromeCaseInsensitive(string? value)
    {
        return !IsPalindrome.CheckIsPalindromeCaseInsensitive(value);
    }

    /// <summary>
    /// Checks if the specified string is NOT a palindrome, ignoring spaces and punctuation.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT a palindrome; otherwise, false.</returns>
    public static bool CheckIsNotPalindromeIgnoreSpacesAndPunctuation(string? value)
    {
        return !IsPalindrome.CheckIsPalindromeIgnoreSpacesAndPunctuation(value);
    }

    /// <summary>
    /// Ensures that the specified string is NOT a palindrome, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a palindrome.</exception>
    /// <returns>The validated string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotPalindrome(this string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotPalindrome(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a palindrome.", parameterName,
                blackboard, contextList);
        }
        return value;
    }

    /// <summary>
    /// Ensures that the specified string is NOT a palindrome with the specified string comparison, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a palindrome.</exception>
    /// <returns>The validated string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotPalindrome(this string? value, StringComparison comparison, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotPalindrome(value, comparison, blackboard, parameterName);
        if (!result.IsValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("comparison", comparison)
            };
            throw ValidationException.Create(ValidatorName, $"Value must NOT be a palindrome using {comparison} comparison.", parameterName,
                blackboard, contextList);
        }
        return value;
    }

    /// <summary>
    /// Ensures that the specified string is NOT a palindrome (case-sensitive), throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a palindrome.</exception>
    /// <returns>The validated string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotPalindromeCaseSensitive(this string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotPalindromeCaseSensitive(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a palindrome (case-sensitive).", parameterName,
                blackboard, contextList);
        }
        return value;
    }

    /// <summary>
    /// Ensures that the specified string is NOT a palindrome (case-insensitive), throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a palindrome.</exception>
    /// <returns>The validated string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotPalindromeCaseInsensitive(this string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotPalindromeCaseInsensitive(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a palindrome (case-insensitive).", parameterName,
                blackboard, contextList);
        }
        return value;
    }

    /// <summary>
    /// Ensures that the specified string is NOT a palindrome, ignoring spaces and punctuation, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a palindrome.</exception>
    /// <returns>The validated string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotPalindromeIgnoreSpacesAndPunctuation(this string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotPalindromeIgnoreSpacesAndPunctuation(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a palindrome (ignoring spaces and punctuation).", parameterName,
                blackboard, contextList);
        }
        return value;
    }

    /// <summary>
    /// Validates that the specified string is NOT a palindrome.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a palindrome.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotPalindrome(this string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPalindrome(value);
        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a palindrome.",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified string is NOT a palindrome with the specified comparison.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a palindrome.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotPalindrome(this string? value, StringComparison comparison,
        IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPalindrome(value, comparison);
        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("comparison", comparison)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a palindrome (using {comparison} comparison).",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified string is NOT a palindrome (case-insensitive).
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a palindrome.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotPalindromeCaseInsensitive(this string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPalindromeCaseInsensitive(value);
        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a palindrome (case-insensitive).",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified string is NOT a palindrome (case-sensitive).
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a palindrome.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotPalindromeCaseSensitive(this string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPalindromeCaseSensitive(value);
        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a palindrome (case-sensitive).",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified string is NOT a palindrome, ignoring spaces and punctuation.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a palindrome.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotPalindromeIgnoreSpacesAndPunctuation(this string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPalindromeIgnoreSpacesAndPunctuation(value);
        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a palindrome (ignoring spaces and punctuation).",
            parameterName,
            blackboard,
            contextList);
    }
}
