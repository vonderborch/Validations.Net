using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string is not a valid URL.
/// </summary>
public static class IsNotUrl
{
    private const string ValidatorName = "IsNotUrl";
    
    // URL regex pattern
    private static readonly Regex UrlRegex = new(
        @"^(https?|ftp|file)://[-A-Za-z0-9+&@#/%?=~_|!:,.;]*[-A-Za-z0-9+&@#/%=~_|]",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    #region Check Methods

    /// <summary>
    /// Checks if a string is not a valid URL.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not a valid URL; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotUrl(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return true;

        return !UrlRegex.IsMatch(value);
    }

    /// <summary>
    /// Checks if a string is not a valid URL using Uri.TryCreate.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="uriKind">The kind of URI to validate.</param>
    /// <returns>True if the string is not a valid URL; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotUrl(this string? value, UriKind uriKind)
    {
        if (string.IsNullOrWhiteSpace(value))
            return true;

        return !Uri.TryCreate(value, uriKind, out _);
    }

    /// <summary>
    /// Checks if a string is not a valid URL using a custom regex pattern.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="pattern">The custom regex pattern to use for URL validation.</param>
    /// <returns>True if the string is not a valid URL; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotUrl(this string? value, string pattern)
    {
        if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(pattern))
            return true;

        try
        {
            return !Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase);
        }
        catch (ArgumentException)
        {
            return true;
        }
    }

    #endregion

    #region Validate Methods

    /// <summary>
    /// Validates if a string is not a valid URL.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the string is not a valid URL.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotUrl(this string? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsNotUrl(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must not be a valid URL.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates if a string is not a valid URL using Uri.TryCreate.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="uriKind">The kind of URI to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the string is not a valid URL.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotUrl(this string? value, UriKind uriKind, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsNotUrl(value, uriKind))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("uriKind", uriKind)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must not be a valid URL with URI kind {uriKind}.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates if a string is not a valid URL using a custom regex pattern.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="pattern">The custom regex pattern to use for URL validation.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the string is not a valid URL.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotUrl(this string? value, string pattern, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsNotUrl(value, pattern))
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
            $"The value must not be a valid URL according to pattern '{pattern}'.",
            fieldName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    /// Ensures that a string is not a valid URL.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>The original string if it is not a valid URL.</returns>
    /// <exception cref="ValidationException">Thrown when the string is a valid URL.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotUrl(this string? value)
    {
        if (!CheckIsNotUrl(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid URL.", null, null, contextList);
        }

        return value;
    }

    /// <summary>
    /// Ensures that a string is not a valid URL using Uri.TryCreate.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="uriKind">The kind of URI to validate.</param>
    /// <returns>The original string if it is not a valid URL.</returns>
    /// <exception cref="ValidationException">Thrown when the string is a valid URL.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotUrl(this string? value, UriKind uriKind)
    {
        if (!CheckIsNotUrl(value, uriKind))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("uriKind", uriKind)
            };
            throw ValidationException.Create(ValidatorName, $"The value must not be a valid URL with URI kind {uriKind}.", null, null, contextList);
        }

        return value;
    }

    /// <summary>
    /// Ensures that a string is not a valid URL using a custom regex pattern.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="pattern">The custom regex pattern to use for URL validation.</param>
    /// <returns>The original string if it is not a valid URL.</returns>
    /// <exception cref="ValidationException">Thrown when the string is a valid URL.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotUrl(this string? value, string pattern)
    {
        if (!CheckIsNotUrl(value, pattern))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("pattern", pattern)
            };
            throw ValidationException.Create(ValidatorName, $"The value must not be a valid URL according to pattern '{pattern}'.", null, null, contextList);
        }

        return value;
    }

    #endregion
}
