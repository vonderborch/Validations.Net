using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a string is a valid email address.
/// </summary>
public static class IsEmail
{
    private const string ValidatorName = "IsEmail";

    // RFC 5322 compliant email regex pattern
    private static readonly Regex EmailRegex = new(
        @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    #region Check Methods

    /// <summary>
    ///     Checks if a string is a valid email address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid email address; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEmail(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return EmailRegex.IsMatch(value);
    }

    /// <summary>
    ///     Checks if a string is a valid email address using a custom regex pattern.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="pattern">The custom regex pattern to use for email validation.</param>
    /// <returns>True if the string is a valid email address; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEmail(this string? value, string pattern)
    {
        if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(pattern))
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase);
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates if a string is a valid email address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the string is a valid email address.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsEmail(this string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsEmail(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value is not a valid email address.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a string is a valid email address using a custom regex pattern.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="pattern">The custom regex pattern to use for email validation.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the string is a valid email address.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsEmail(this string? value, string pattern, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsEmail(value, pattern))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("pattern", pattern)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value is not a valid email address according to pattern '{pattern}'.",
            fieldName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that a string is a valid email address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>The original string if it is a valid email address.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not a valid email address.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsEmail(this string? value)
    {
        if (!CheckIsEmail(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value is not a valid email address.", null, null,
                contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a string is a valid email address using a custom regex pattern.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="pattern">The custom regex pattern to use for email validation.</param>
    /// <returns>The original string if it is a valid email address.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not a valid email address.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsEmail(this string? value, string pattern)
    {
        if (!CheckIsEmail(value, pattern))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("pattern", pattern)
            };
            throw ValidationException.Create(ValidatorName,
                $"The value is not a valid email address according to pattern '{pattern}'.", null, null, contextList);
        }

        return value;
    }

    #endregion
}
