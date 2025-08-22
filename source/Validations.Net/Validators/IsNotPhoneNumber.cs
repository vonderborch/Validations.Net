using System.Text.RegularExpressions;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Validates that a string is not a valid phone number.
/// </summary>
public static class IsNotPhoneNumber
{
    private const string ValidatorName = nameof(IsNotPhoneNumber);

    /// <summary>
    ///     Checks if the string is not a valid international phone number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is not a valid international phone number; otherwise, false.</returns>
    public static bool CheckIsNotInternationalPhoneNumber(this string? value)
    {
        return !value.CheckIsInternationalPhoneNumber();
    }

    /// <summary>
    ///     Checks if the string is not a valid North American phone number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is not a valid North American phone number; otherwise, false.</returns>
    public static bool CheckIsNotNorthAmericanPhoneNumber(this string? value)
    {
        return !value.CheckIsNorthAmericanPhoneNumber();
    }

    /// <summary>
    ///     Checks if the string is not a valid phone number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="pattern">The regex pattern to use for validation. If null, uses a general pattern.</param>
    /// <param name="options">Regex options to use.</param>
    /// <returns>True if the string is not a valid phone number; otherwise, false.</returns>
    public static bool CheckIsNotPhoneNumber(this string? value, string? pattern = null,
        RegexOptions options = RegexOptions.None)
    {
        return !value.CheckIsPhoneNumber(pattern, options);
    }

    /// <summary>
    ///     Ensures that the string is not a valid international phone number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid international phone number.</exception>
    public static void EnsureIsNotInternationalPhoneNumber(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsNotInternationalPhoneNumber(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid international phone number",
                null, null, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is not a valid North American phone number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid North American phone number.</exception>
    public static void EnsureIsNotNorthAmericanPhoneNumber(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsNotNorthAmericanPhoneNumber(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid North American phone number",
                null, null, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is not a valid phone number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="pattern">The regex pattern to use for validation. If null, uses a general pattern.</param>
    /// <param name="options">Regex options to use.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid phone number.</exception>
    public static void EnsureIsNotPhoneNumber(this string? value, string fieldName, IBlackboard? blackboard,
        string? pattern = null, RegexOptions options = RegexOptions.None)
    {
        var isValid = CheckIsNotPhoneNumber(value, pattern, options);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Pattern", pattern),
                ("Options", options),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid phone number", null, null,
                contextList);
        }
    }

    /// <summary>
    ///     Validates that the string is not a valid international phone number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotInternationalPhoneNumber(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsNotInternationalPhoneNumber(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "The value must not be a valid international phone number", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid North American phone number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotNorthAmericanPhoneNumber(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsNotNorthAmericanPhoneNumber(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "The value must not be a valid North American phone number", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid phone number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="pattern">The regex pattern to use for validation. If null, uses a general pattern.</param>
    /// <param name="options">Regex options to use.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotPhoneNumber(this string? value, string fieldName,
        IBlackboard? blackboard, string? pattern = null, RegexOptions options = RegexOptions.None)
    {
        var isValid = CheckIsNotPhoneNumber(value, pattern, options);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Pattern", pattern),
            ("Options", options),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must not be a valid phone number",
                fieldName, blackboard, contextList);
    }
}
