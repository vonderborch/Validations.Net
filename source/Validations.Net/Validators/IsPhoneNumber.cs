using System.Text.RegularExpressions;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Validates that a string is a valid phone number.
/// </summary>
public static class IsPhoneNumber
{
    private const string ValidatorName = nameof(IsPhoneNumber);

    // Common phone number patterns
    private static readonly Regex InternationalPattern = new(
        @"^\+[1-9]\d{1,14}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    private static readonly Regex NorthAmericanPattern = new(
        @"^(\+?1[-.]?)?\(?([0-9]{3})\)?[-.]?([0-9]{3})[-.]?([0-9]{4})$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    private static readonly Regex GeneralPattern = new(
        @"^[\+]?[1-9][\d]{0,15}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    /// <summary>
    ///     Checks if the string is a valid international phone number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid international phone number; otherwise, false.</returns>
    public static bool CheckIsInternationalPhoneNumber(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return InternationalPattern.IsMatch(value);
    }

    /// <summary>
    ///     Checks if the string is a valid North American phone number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid North American phone number; otherwise, false.</returns>
    public static bool CheckIsNorthAmericanPhoneNumber(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return NorthAmericanPattern.IsMatch(value);
    }

    /// <summary>
    ///     Checks if the string is a valid phone number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="pattern">The regex pattern to use for validation. If null, uses a general pattern.</param>
    /// <param name="options">Regex options to use.</param>
    /// <returns>True if the string is a valid phone number; otherwise, false.</returns>
    public static bool CheckIsPhoneNumber(this string? value, string? pattern = null,
        RegexOptions options = RegexOptions.None)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        if (pattern != null)
        {
            try
            {
                var regex = new Regex(pattern, options);
                return regex.IsMatch(value);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        // Try different patterns
        return InternationalPattern.IsMatch(value) ||
               NorthAmericanPattern.IsMatch(value) ||
               GeneralPattern.IsMatch(value);
    }

    /// <summary>
    ///     Ensures that the string is a valid international phone number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid international phone number.</exception>
    public static void EnsureIsInternationalPhoneNumber(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsInternationalPhoneNumber(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid international phone number",
                null, null, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is a valid North American phone number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid North American phone number.</exception>
    public static void EnsureIsNorthAmericanPhoneNumber(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsNorthAmericanPhoneNumber(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid North American phone number",
                null, null, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is a valid phone number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="pattern">The regex pattern to use for validation. If null, uses a general pattern.</param>
    /// <param name="options">Regex options to use.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid phone number.</exception>
    public static void EnsureIsPhoneNumber(this string? value, string fieldName, IBlackboard? blackboard,
        string? pattern = null, RegexOptions options = RegexOptions.None)
    {
        var isValid = CheckIsPhoneNumber(value, pattern, options);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Pattern", pattern),
                ("Options", options),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid phone number", null, null,
                contextList);
        }
    }

    /// <summary>
    ///     Validates that the string is a valid international phone number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsInternationalPhoneNumber(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsInternationalPhoneNumber(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "The value must be a valid international phone number", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid North American phone number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNorthAmericanPhoneNumber(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsNorthAmericanPhoneNumber(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "The value must be a valid North American phone number", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid phone number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="pattern">The regex pattern to use for validation. If null, uses a general pattern.</param>
    /// <param name="options">Regex options to use.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsPhoneNumber(this string? value, string fieldName, IBlackboard? blackboard,
        string? pattern = null, RegexOptions options = RegexOptions.None)
    {
        var isValid = CheckIsPhoneNumber(value, pattern, options);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Pattern", pattern),
            ("Options", options),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must be a valid phone number",
                fieldName, blackboard, contextList);
    }
}
