using System;
using System.Text.RegularExpressions;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Validates that a string is not a valid postal code.
/// </summary>
public static class IsNotPostalCode
{
    private const string ValidatorName = nameof(IsNotPostalCode);

    /// <summary>
    /// Checks if the string is not a valid postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="pattern">The regex pattern to use for validation. If null, uses common patterns.</param>
    /// <param name="options">Regex options to use.</param>
    /// <returns>True if the string is not a valid postal code; otherwise, false.</returns>
    public static bool CheckIsNotPostalCode(this string? value, string? pattern = null, RegexOptions options = RegexOptions.None)
    {
        return !IsPostalCode.CheckIsPostalCode(value, pattern, options);
    }

    /// <summary>
    /// Checks if the string is not a valid US ZIP code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is not a valid US ZIP code; otherwise, false.</returns>
    public static bool CheckIsNotUsZipCode(this string? value)
    {
        return !IsPostalCode.CheckIsUsZipCode(value);
    }

    /// <summary>
    /// Checks if the string is not a valid Canadian postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is not a valid Canadian postal code; otherwise, false.</returns>
    public static bool CheckIsNotCanadianPostalCode(this string? value)
    {
        return !IsPostalCode.CheckIsCanadianPostalCode(value);
    }

    /// <summary>
    /// Checks if the string is not a valid UK postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is not a valid UK postal code; otherwise, false.</returns>
    public static bool CheckIsNotUkPostalCode(this string? value)
    {
        return !IsPostalCode.CheckIsUkPostalCode(value);
    }

    /// <summary>
    /// Checks if the string is not a valid German postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is not a valid German postal code; otherwise, false.</returns>
    public static bool CheckIsNotGermanPostalCode(this string? value)
    {
        return !IsPostalCode.CheckIsGermanPostalCode(value);
    }

    /// <summary>
    /// Checks if the string is not a valid French postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is not a valid French postal code; otherwise, false.</returns>
    public static bool CheckIsNotFrenchPostalCode(this string? value)
    {
        return !IsPostalCode.CheckIsFrenchPostalCode(value);
    }

    /// <summary>
    /// Checks if the string is not a valid Japanese postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is not a valid Japanese postal code; otherwise, false.</returns>
    public static bool CheckIsNotJapanesePostalCode(this string? value)
    {
        return !IsPostalCode.CheckIsJapanesePostalCode(value);
    }

    /// <summary>
    /// Validates that the string is not a valid postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="pattern">The regex pattern to use for validation. If null, uses common patterns.</param>
    /// <param name="options">Regex options to use.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotPostalCode(this string? value, string fieldName, IBlackboard? blackboard, string? pattern = null, RegexOptions options = RegexOptions.None)
    {
        var isValid = CheckIsNotPostalCode(value, pattern, options);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Pattern", pattern),
            ("Options", options),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must not be a valid postal code", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is not a valid US ZIP code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotUsZipCode(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsNotUsZipCode(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must not be a valid US ZIP code", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is not a valid Canadian postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotCanadianPostalCode(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsNotCanadianPostalCode(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must not be a valid Canadian postal code", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is not a valid UK postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotUkPostalCode(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsNotUkPostalCode(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must not be a valid UK postal code", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is not a valid German postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotGermanPostalCode(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsNotGermanPostalCode(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must not be a valid German postal code", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is not a valid French postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotFrenchPostalCode(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsNotFrenchPostalCode(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must not be a valid French postal code", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is not a valid Japanese postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotJapanesePostalCode(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsNotJapanesePostalCode(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must not be a valid Japanese postal code", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Ensures that the string is not a valid postal code, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="pattern">The regex pattern to use for validation. If null, uses common patterns.</param>
    /// <param name="options">Regex options to use.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid postal code.</exception>
    public static void EnsureIsNotPostalCode(this string? value, string fieldName, IBlackboard? blackboard, string? pattern = null, RegexOptions options = RegexOptions.None)
    {
        var isValid = CheckIsNotPostalCode(value, pattern, options);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Pattern", pattern),
                ("Options", options),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid postal code", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is not a valid US ZIP code, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid US ZIP code.</exception>
    public static void EnsureIsNotUsZipCode(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsNotUsZipCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid US ZIP code", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is not a valid Canadian postal code, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid Canadian postal code.</exception>
    public static void EnsureIsNotCanadianPostalCode(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsNotCanadianPostalCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid Canadian postal code", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is not a valid UK postal code, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid UK postal code.</exception>
    public static void EnsureIsNotUkPostalCode(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsNotUkPostalCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid UK postal code", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is not a valid German postal code, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid German postal code.</exception>
    public static void EnsureIsNotGermanPostalCode(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsNotGermanPostalCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid German postal code", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is not a valid French postal code, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid French postal code.</exception>
    public static void EnsureIsNotFrenchPostalCode(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsNotFrenchPostalCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid French postal code", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is not a valid Japanese postal code, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid Japanese postal code.</exception>
    public static void EnsureIsNotJapanesePostalCode(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsNotJapanesePostalCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid Japanese postal code", null, null, contextList);
        }
    }
}
