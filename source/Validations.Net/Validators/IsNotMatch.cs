using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string does not match a specified regex pattern.
/// </summary>
public static class IsNotMatch
{
    private const string ValidatorName = "IsNotMatch";

    #region Check Methods

    /// <summary>
    /// Checks if a string does not match the specified regex pattern.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="pattern">The regex pattern to match against.</param>
    /// <returns>True if the string does not match the pattern; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotMatch(this string? value, string pattern)
    {
        if (value == null || pattern == null)
            return true;

        try
        {
            return !Regex.IsMatch(value, pattern);
        }
        catch (ArgumentException)
        {
            return true;
        }
    }

    /// <summary>
    /// Checks if a string does not match the specified regex pattern with options.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="pattern">The regex pattern to match against.</param>
    /// <param name="options">The regex options to use.</param>
    /// <returns>True if the string does not match the pattern; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotMatch(this string? value, string pattern, RegexOptions options)
    {
        if (value == null || pattern == null)
            return true;

        try
        {
            return !Regex.IsMatch(value, pattern, options);
        }
        catch (ArgumentException)
        {
            return true;
        }
    }

    /// <summary>
    /// Checks if a string does not match the specified regex pattern with a timeout.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="pattern">The regex pattern to match against.</param>
    /// <param name="options">The regex options to use.</param>
    /// <param name="matchTimeout">The timeout for the regex match.</param>
    /// <returns>True if the string does not match the pattern; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotMatch(this string? value, string pattern, RegexOptions options, TimeSpan matchTimeout)
    {
        if (value == null || pattern == null)
            return true;

        try
        {
            return !Regex.IsMatch(value, pattern, options, matchTimeout);
        }
        catch (ArgumentException)
        {
            return true;
        }
    }

    #endregion

    #region Validate Methods

    /// <summary>
    /// Validates if a string does not match the specified regex pattern.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="pattern">The regex pattern to match against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the string does not match the pattern.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotMatch(this string? value, string pattern, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsNotMatch(value, pattern))
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
            $"The value matches the specified pattern '{pattern}'.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates if a string does not match the specified regex pattern with options.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="pattern">The regex pattern to match against.</param>
    /// <param name="options">The regex options to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the string does not match the pattern.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotMatch(this string? value, string pattern, RegexOptions options, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsNotMatch(value, pattern, options))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("pattern", pattern),
            ("options", options)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value matches the specified pattern '{pattern}' with options {options}.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates if a string does not match the specified regex pattern with options and timeout.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="pattern">The regex pattern to match against.</param>
    /// <param name="options">The regex options to use.</param>
    /// <param name="matchTimeout">The timeout for the regex match.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the string does not match the pattern.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotMatch(this string? value, string pattern, RegexOptions options, TimeSpan matchTimeout, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsNotMatch(value, pattern, options, matchTimeout))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("pattern", pattern),
            ("options", options),
            ("matchTimeout", matchTimeout)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value matches the specified pattern '{pattern}' with options {options} and timeout {matchTimeout}.",
            fieldName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    /// Ensures that a string does not match the specified regex pattern.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="pattern">The regex pattern to match against.</param>
    /// <returns>The original string if it does not match the pattern.</returns>
    /// <exception cref="ValidationException">Thrown when the string matches the pattern.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotMatch(this string? value, string pattern)
    {
        if (!CheckIsNotMatch(value, pattern))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("pattern", pattern)
            };
            throw ValidationException.Create(ValidatorName, $"The value matches the specified pattern '{pattern}'.", null, null, contextList);
        }

        return value;
    }

    /// <summary>
    /// Ensures that a string does not match the specified regex pattern with options.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="pattern">The regex pattern to match against.</param>
    /// <param name="options">The regex options to use.</param>
    /// <returns>The original string if it does not match the pattern.</returns>
    /// <exception cref="ValidationException">Thrown when the string matches the pattern.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotMatch(this string? value, string pattern, RegexOptions options)
    {
        if (!CheckIsNotMatch(value, pattern, options))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("pattern", pattern),
                ("options", options)
            };
            throw ValidationException.Create(ValidatorName, $"The value matches the specified pattern '{pattern}' with options {options}.", null, null, contextList);
        }

        return value;
    }

    /// <summary>
    /// Ensures that a string does not match the specified regex pattern with options and timeout.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="pattern">The regex pattern to match against.</param>
    /// <param name="options">The regex options to use.</param>
    /// <param name="matchTimeout">The timeout for the regex match.</param>
    /// <returns>The original string if it does not match the pattern.</returns>
    /// <exception cref="ValidationException">Thrown when the string matches the pattern.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotMatch(this string? value, string pattern, RegexOptions options, TimeSpan matchTimeout)
    {
        if (!CheckIsNotMatch(value, pattern, options, matchTimeout))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("pattern", pattern),
                ("options", options),
                ("matchTimeout", matchTimeout)
            };
            throw ValidationException.Create(ValidatorName, $"The value matches the specified pattern '{pattern}' with options {options} and timeout {matchTimeout}.", null, null, contextList);
        }

        return value;
    }

    #endregion
}
