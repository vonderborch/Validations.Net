using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string is a valid credit card number.
/// </summary>
public static class IsCreditCard
{
    private const string ValidatorName = nameof(IsCreditCard);

    // Common credit card patterns
    private static readonly Regex VisaPattern = new(@"^4[0-9]{12}(?:[0-9]{3})?$", RegexOptions.Compiled);
    private static readonly Regex MasterCardPattern = new(@"^5[1-5][0-9]{14}$", RegexOptions.Compiled);
    private static readonly Regex AmericanExpressPattern = new(@"^3[47][0-9]{13}$", RegexOptions.Compiled);
    private static readonly Regex DiscoverPattern = new(@"^6(?:011|5[0-9]{2})[0-9]{12}$", RegexOptions.Compiled);
    private static readonly Regex DinersClubPattern = new(@"^3(?:0[0-5]|[68][0-9])[0-9]{11}$", RegexOptions.Compiled);
    private static readonly Regex JcbPattern = new(@"^(?:2131|1800|35\d{3})\d{11}$", RegexOptions.Compiled);

    /// <summary>
    /// Checks if the string is a valid American Express card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>True if the string is a valid American Express card number; otherwise, false.</returns>
    public static bool CheckIsAmericanExpress(string? value, bool validateLuhn = true)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        var cleanValue = value.Replace(" ", "").Replace("-", "");
        if (!AmericanExpressPattern.IsMatch(cleanValue))
            return false;

        return !validateLuhn || ValidateLuhn(cleanValue);
    }

    /// <summary>
    /// Checks if the string is a valid credit card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>True if the string is a valid credit card number; otherwise, false.</returns>
    public static bool CheckIsCreditCard(string? value, bool validateLuhn = true)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return CheckIsVisa(value, validateLuhn) ||
               CheckIsMasterCard(value, validateLuhn) ||
               CheckIsAmericanExpress(value, validateLuhn) ||
               CheckIsDiscover(value, validateLuhn) ||
               CheckIsDinersClub(value, validateLuhn) ||
               CheckIsJcb(value, validateLuhn);
    }

    /// <summary>
    /// Checks if the string is a valid MasterCard number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>True if the string is a valid MasterCard number; otherwise, false.</returns>
    public static bool CheckIsMasterCard(string? value, bool validateLuhn = true)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        var cleanValue = value.Replace(" ", "").Replace("-", "");
        if (!MasterCardPattern.IsMatch(cleanValue))
            return false;

        return !validateLuhn || ValidateLuhn(cleanValue);
    }

    /// <summary>
    /// Checks if the string is a valid Visa card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>True if the string is a valid Visa card number; otherwise, false.</returns>
    public static bool CheckIsVisa(string? value, bool validateLuhn = true)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        var cleanValue = value.Replace(" ", "").Replace("-", "");
        if (!VisaPattern.IsMatch(cleanValue))
            return false;

        return !validateLuhn || ValidateLuhn(cleanValue);
    }

    /// <summary>
    /// Checks if the string is a valid Discover card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>True if the string is a valid Discover card number; otherwise, false.</returns>
    public static bool CheckIsDiscover(string? value, bool validateLuhn = true)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        var cleanValue = value.Replace(" ", "").Replace("-", "");
        if (!DiscoverPattern.IsMatch(cleanValue))
            return false;

        return !validateLuhn || ValidateLuhn(cleanValue);
    }

    /// <summary>
    /// Checks if the string is a valid Diners Club card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>True if the string is a valid Diners Club card number; otherwise, false.</returns>
    public static bool CheckIsDinersClub(string? value, bool validateLuhn = true)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        var cleanValue = value.Replace(" ", "").Replace("-", "");
        if (!DinersClubPattern.IsMatch(cleanValue))
            return false;

        return !validateLuhn || ValidateLuhn(cleanValue);
    }

    /// <summary>
    /// Checks if the string is a valid JCB card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>True if the string is a valid JCB card number; otherwise, false.</returns>
    public static bool CheckIsJcb(string? value, bool validateLuhn = true)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        var cleanValue = value.Replace(" ", "").Replace("-", "");
        if (!JcbPattern.IsMatch(cleanValue))
            return false;

        return !validateLuhn || ValidateLuhn(cleanValue);
    }

    /// <summary>
    /// Ensures that the string is a valid American Express card number, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid American Express card number.</exception>
    public static void EnsureIsAmericanExpress(string? value, IBlackboard? blackboard = null, bool validateLuhn = true,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsAmericanExpress(value, validateLuhn);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("ValidateLuhn", validateLuhn)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a valid American Express card number.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid credit card number, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid credit card number.</exception>
    public static void EnsureIsCreditCard(string? value, IBlackboard? blackboard = null, bool validateLuhn = true,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsCreditCard(value, validateLuhn);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("ValidateLuhn", validateLuhn)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a valid credit card number.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid MasterCard number, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid MasterCard number.</exception>
    public static void EnsureIsMasterCard(string? value, IBlackboard? blackboard = null, bool validateLuhn = true,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsMasterCard(value, validateLuhn);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("ValidateLuhn", validateLuhn)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a valid MasterCard number.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid Visa card number, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid Visa card number.</exception>
    public static void EnsureIsVisa(string? value, IBlackboard? blackboard = null, bool validateLuhn = true,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsVisa(value, validateLuhn);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("ValidateLuhn", validateLuhn)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a valid Visa card number.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Validates that the string is a valid American Express card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid American Express card number.</returns>
    public static ValidationResult ValidateIsAmericanExpress(string? value, IBlackboard? blackboard = null, bool validateLuhn = true,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsAmericanExpress(value, validateLuhn);
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

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a valid American Express card number.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid credit card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid credit card number.</returns>
    public static ValidationResult ValidateIsCreditCard(string? value, IBlackboard? blackboard = null, bool validateLuhn = true,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsCreditCard(value, validateLuhn);
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

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a valid credit card number.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid MasterCard number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid MasterCard number.</returns>
    public static ValidationResult ValidateIsMasterCard(string? value, IBlackboard? blackboard = null, bool validateLuhn = true,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsMasterCard(value, validateLuhn);
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

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a valid MasterCard number.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid Visa card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid Visa card number.</returns>
    public static ValidationResult ValidateIsVisa(string? value, IBlackboard? blackboard = null, bool validateLuhn = true,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsVisa(value, validateLuhn);
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

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a valid Visa card number.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates a credit card number using the Luhn algorithm.
    /// </summary>
    /// <param name="cardNumber">The credit card number to validate.</param>
    /// <returns>True if the card number passes the Luhn check; otherwise, false.</returns>
    private static bool ValidateLuhn(string cardNumber)
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
            return false;

        var sum = 0;
        var alternate = false;

        for (var i = cardNumber.Length - 1; i >= 0; i--)
        {
            if (!char.IsDigit(cardNumber[i]))
                return false;

            var digit = cardNumber[i] - '0';

            if (alternate)
            {
                digit *= 2;
                if (digit > 9)
                    digit = digit / 10 + digit % 10;
            }

            sum += digit;
            alternate = !alternate;
        }

        return sum % 10 == 0;
    }
}