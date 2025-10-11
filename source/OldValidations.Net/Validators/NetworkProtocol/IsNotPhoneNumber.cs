using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string is NOT a valid phone number.
/// </summary>
public static class IsNotPhoneNumber
{
    private const string ValidatorName = nameof(IsNotPhoneNumber);

    // International phone number regex pattern (E.164 format)
    private static readonly Regex InternationalPhoneRegex = new(
        @"^\+[1-9]\d{1,14}$",
        RegexOptions.Compiled);

    // North American phone number regex pattern
    private static readonly Regex NorthAmericanPhoneRegex = new(
        @"^(\+1\s?)?\(?([0-9]{3})\)?[\s.-]?([0-9]{3})[\s.-]?([0-9]{4})$",
        RegexOptions.Compiled);

    // General phone number regex pattern
    private static readonly Regex GeneralPhoneRegex = new(
        @"^[\+]?[1-9][\d]{0,15}$",
        RegexOptions.Compiled);

    /// <summary>
    /// Checks if the specified string is NOT a valid international phone number (E.164 format).
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT a valid international phone number; otherwise, false.</returns>
    public static bool CheckIsNotInternationalPhoneNumber(string? value)
    {
        return !IsPhoneNumber.CheckIsInternationalPhoneNumber(value);
    }

    /// <summary>
    /// Checks if the specified string is NOT a valid North American phone number.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT a valid North American phone number; otherwise, false.</returns>
    public static bool CheckIsNotNorthAmericanPhoneNumber(string? value)
    {
        return !IsPhoneNumber.CheckIsNorthAmericanPhoneNumber(value);
    }

    /// <summary>
    /// Checks if the specified string is NOT a valid phone number.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT a valid phone number; otherwise, false.</returns>
    public static bool CheckIsNotPhoneNumber(string? value)
    {
        return !IsPhoneNumber.CheckIsPhoneNumber(value);
    }

    /// <summary>
    /// Checks if the specified string is NOT a valid phone number using a custom regex pattern.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="pattern">The custom regex pattern to use for phone number validation.</param>
    /// <returns>True if the string is NOT a valid phone number; otherwise, false.</returns>
    public static bool CheckIsNotPhoneNumber(string? value, string pattern)
    {
        return !IsPhoneNumber.CheckIsPhoneNumber(value, pattern);
    }

    /// <summary>
    /// Ensures that the specified string is NOT a valid international phone number, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid international phone number.</exception>
    public static void EnsureIsNotInternationalPhoneNumber(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotInternationalPhoneNumber(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a valid international phone number (E.164 format).", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is NOT a valid North American phone number, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid North American phone number.</exception>
    public static void EnsureIsNotNorthAmericanPhoneNumber(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotNorthAmericanPhoneNumber(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a valid North American phone number.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is NOT a valid phone number, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid phone number.</exception>
    public static void EnsureIsNotPhoneNumber(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPhoneNumber(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a valid phone number.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is NOT a valid phone number using a custom regex pattern, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="pattern">The custom regex pattern to use for phone number validation.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid phone number.</exception>
    public static void EnsureIsNotPhoneNumber(string? value, string pattern, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPhoneNumber(value, pattern);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Pattern", pattern)
            };
            throw ValidationException.Create(ValidatorName, $"Value must NOT be a valid phone number according to pattern '{pattern}'.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Validates that the specified string is NOT a valid international phone number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a valid international phone number.</returns>
    public static ValidationResult ValidateIsNotInternationalPhoneNumber(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotInternationalPhoneNumber(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a valid international phone number (E.164 format).",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified string is NOT a valid North American phone number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a valid North American phone number.</returns>
    public static ValidationResult ValidateIsNotNorthAmericanPhoneNumber(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotNorthAmericanPhoneNumber(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a valid North American phone number.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified string is NOT a valid phone number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a valid phone number.</returns>
    public static ValidationResult ValidateIsNotPhoneNumber(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPhoneNumber(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a valid phone number.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified string is NOT a valid phone number using a custom regex pattern.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="pattern">The custom regex pattern to use for phone number validation.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a valid phone number.</returns>
    public static ValidationResult ValidateIsNotPhoneNumber(string? value, string pattern, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPhoneNumber(value, pattern);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Pattern", pattern),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must NOT be a valid phone number according to pattern '{pattern}'.",
            parameterName, blackboard, contextList);
    }
}
