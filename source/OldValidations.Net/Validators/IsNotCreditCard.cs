using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string is NOT a valid credit card number.
/// </summary>
public static class IsNotCreditCard
{
    private const string ValidatorName = nameof(IsNotCreditCard);

    /// <summary>
    /// Checks if the string is NOT a valid American Express card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>True if the string is NOT a valid American Express card number; otherwise, false.</returns>
    public static bool CheckIsNotAmericanExpress(string? value, bool validateLuhn = true)
    {
        return !IsCreditCard.CheckIsAmericanExpress(value, validateLuhn);
    }

    /// <summary>
    /// Checks if the string is NOT a valid credit card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>True if the string is NOT a valid credit card number; otherwise, false.</returns>
    public static bool CheckIsNotCreditCard(string? value, bool validateLuhn = true)
    {
        return !IsCreditCard.CheckIsCreditCard(value, validateLuhn);
    }

    /// <summary>
    /// Checks if the string is NOT a valid MasterCard number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>True if the string is NOT a valid MasterCard number; otherwise, false.</returns>
    public static bool CheckIsNotMasterCard(string? value, bool validateLuhn = true)
    {
        return !IsCreditCard.CheckIsMasterCard(value, validateLuhn);
    }

    /// <summary>
    /// Checks if the string is NOT a valid Visa card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>True if the string is NOT a valid Visa card number; otherwise, false.</returns>
    public static bool CheckIsNotVisa(string? value, bool validateLuhn = true)
    {
        return !IsCreditCard.CheckIsVisa(value, validateLuhn);
    }

    /// <summary>
    /// Ensures that the string is NOT a valid American Express card number, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid American Express card number.</exception>
    public static void EnsureIsNotAmericanExpress(string? value, IBlackboard? blackboard = null, bool validateLuhn = true,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotAmericanExpress(value, validateLuhn);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("ValidateLuhn", validateLuhn)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a valid American Express card number.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is NOT a valid credit card number, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid credit card number.</exception>
    public static void EnsureIsNotCreditCard(string? value, IBlackboard? blackboard = null, bool validateLuhn = true,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotCreditCard(value, validateLuhn);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("ValidateLuhn", validateLuhn)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a valid credit card number.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is NOT a valid MasterCard number, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid MasterCard number.</exception>
    public static void EnsureIsNotMasterCard(string? value, IBlackboard? blackboard = null, bool validateLuhn = true,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotMasterCard(value, validateLuhn);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("ValidateLuhn", validateLuhn)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a valid MasterCard number.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is NOT a valid Visa card number, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid Visa card number.</exception>
    public static void EnsureIsNotVisa(string? value, IBlackboard? blackboard = null, bool validateLuhn = true,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotVisa(value, validateLuhn);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("ValidateLuhn", validateLuhn)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a valid Visa card number.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Validates that the string is NOT a valid American Express card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a valid American Express card number.</returns>
    public static ValidationResult ValidateIsNotAmericanExpress(string? value, IBlackboard? blackboard = null, bool validateLuhn = true,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotAmericanExpress(value, validateLuhn);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ValidateLuhn", validateLuhn),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a valid American Express card number.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is NOT a valid credit card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a valid credit card number.</returns>
    public static ValidationResult ValidateIsNotCreditCard(string? value, IBlackboard? blackboard = null, bool validateLuhn = true,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotCreditCard(value, validateLuhn);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ValidateLuhn", validateLuhn),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a valid credit card number.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is NOT a valid MasterCard number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a valid MasterCard number.</returns>
    public static ValidationResult ValidateIsNotMasterCard(string? value, IBlackboard? blackboard = null, bool validateLuhn = true,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotMasterCard(value, validateLuhn);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ValidateLuhn", validateLuhn),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a valid MasterCard number.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is NOT a valid Visa card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a valid Visa card number.</returns>
    public static ValidationResult ValidateIsNotVisa(string? value, IBlackboard? blackboard = null, bool validateLuhn = true,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotVisa(value, validateLuhn);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ValidateLuhn", validateLuhn),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a valid Visa card number.",
            parameterName, blackboard, contextList);
    }
}