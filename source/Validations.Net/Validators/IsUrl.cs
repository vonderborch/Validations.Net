using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a string is a valid URL.
/// </summary>
public static class IsUrl
{
    private const string ValidatorName = "IsUrl";

    // URL regex pattern
    private static readonly Regex UrlRegex = new(
        @"^(https?|ftp|file)://[-A-Za-z0-9+&@#/%?=~_|!:,.;]*[-A-Za-z0-9+&@#/%=~_|]",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    #region Check Methods

    /// <summary>
    ///     Checks if a string is a valid URL.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid URL; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsUrl(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return UrlRegex.IsMatch(value);
    }

    /// <summary>
    ///     Checks if a string is a valid URL using Uri.TryCreate.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="uriKind">The kind of URI to validate.</param>
    /// <returns>True if the string is a valid URL; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsUrl(this string? value, UriKind uriKind)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return Uri.TryCreate(value, uriKind, out _);
    }

    /// <summary>
    ///     Checks if a string is a valid URL using a custom regex pattern.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="pattern">The custom regex pattern to use for URL validation.</param>
    /// <returns>True if the string is a valid URL; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsUrl(this string? value, string pattern)
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
    ///     Validates if a string is a valid URL.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the string is a valid URL.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsUrl(this string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsUrl(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must be a valid URL.",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a string is a valid URL using Uri.TryCreate.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="uriKind">The kind of URI to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the string is a valid URL.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsUrl(this string? value, UriKind uriKind, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsUrl(value, uriKind))
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
            $"The value must be a valid URL with URI kind {uriKind}.",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a string is a valid URL using a custom regex pattern.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="pattern">The custom regex pattern to use for URL validation.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the string is a valid URL.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsUrl(this string? value, string pattern, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsUrl(value, pattern))
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
            $"The value must be a valid URL according to pattern '{pattern}'.",
            parameterName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that a string is a valid URL.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>The original string if it is a valid URL.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not a valid URL.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsUrl(this string? value)
    {
        if (!CheckIsUrl(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid URL.", null, null, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a string is a valid URL using Uri.TryCreate.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="uriKind">The kind of URI to validate.</param>
    /// <returns>The original string if it is a valid URL.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not a valid URL.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsUrl(this string? value, UriKind uriKind)
    {
        if (!CheckIsUrl(value, uriKind))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("uriKind", uriKind)
            };
            throw ValidationException.Create(ValidatorName, $"The value must be a valid URL with URI kind {uriKind}.",
                null, null, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a string is a valid URL using a custom regex pattern.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="pattern">The custom regex pattern to use for URL validation.</param>
    /// <returns>The original string if it is a valid URL.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not a valid URL.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsUrl(this string? value, string pattern)
    {
        if (!CheckIsUrl(value, pattern))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("pattern", pattern)
            };
            throw ValidationException.Create(ValidatorName,
                $"The value must be a valid URL according to pattern '{pattern}'.", null, null, contextList);
        }

        return value;
    }

    #endregion
}
