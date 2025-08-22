using System;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Validates that a string is not a valid credit card number.
/// </summary>
public static class IsNotCreditCard
{
    private const string ValidatorName = nameof(IsNotCreditCard);

    /// <summary>
    /// Checks if the string is not a valid credit card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>True if the string is not a valid credit card number; otherwise, false.</returns>
    public static bool CheckIsNotCreditCard(this string? value, bool validateLuhn = true)
    {
        return !IsCreditCard.CheckIsCreditCard(value, validateLuhn);
    }

    /// <summary>
    /// Checks if the string is not a valid Visa card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>True if the string is not a valid Visa card number; otherwise, false.</returns>
    public static bool CheckIsNotVisaCard(this string? value, bool validateLuhn = true)
    {
        return !IsCreditCard.CheckIsVisaCard(value, validateLuhn);
    }

    /// <summary>
    /// Checks if the string is not a valid MasterCard number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>True if the string is not a valid MasterCard number; otherwise, false.</returns>
    public static bool CheckIsNotMasterCard(this string? value, bool validateLuhn = true)
    {
        return !IsCreditCard.CheckIsMasterCard(value, validateLuhn);
    }

    /// <summary>
    /// Checks if the string is not a valid American Express card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>True if the string is not a valid American Express card number; otherwise, false.</returns>
    public static bool CheckIsNotAmericanExpress(this string? value, bool validateLuhn = true)
    {
        return !IsCreditCard.CheckIsAmericanExpress(value, validateLuhn);
    }

    /// <summary>
    /// Validates that the string is not a valid credit card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotCreditCard(this string? value, string fieldName, IBlackboard? blackboard, bool validateLuhn = true)
    {
        var isValid = CheckIsNotCreditCard(value, validateLuhn);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("ValidateLuhn", validateLuhn),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must not be a valid credit card number", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is not a valid Visa card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotVisaCard(this string? value, string fieldName, IBlackboard? blackboard, bool validateLuhn = true)
    {
        var isValid = CheckIsNotVisaCard(value, validateLuhn);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("ValidateLuhn", validateLuhn),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must not be a valid Visa card number", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is not a valid MasterCard number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotMasterCard(this string? value, string fieldName, IBlackboard? blackboard, bool validateLuhn = true)
    {
        var isValid = CheckIsNotMasterCard(value, validateLuhn);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("ValidateLuhn", validateLuhn),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must not be a valid MasterCard number", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is not a valid American Express card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotAmericanExpress(this string? value, string fieldName, IBlackboard? blackboard, bool validateLuhn = true)
    {
        var isValid = CheckIsNotAmericanExpress(value, validateLuhn);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("ValidateLuhn", validateLuhn),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must not be a valid American Express card number", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Ensures that the string is not a valid credit card number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid credit card number.</exception>
    public static void EnsureIsNotCreditCard(this string? value, string fieldName, IBlackboard? blackboard, bool validateLuhn = true)
    {
        var isValid = CheckIsNotCreditCard(value, validateLuhn);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("ValidateLuhn", validateLuhn),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid credit card number", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is not a valid Visa card number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid Visa card number.</exception>
    public static void EnsureIsNotVisaCard(this string? value, string fieldName, IBlackboard? blackboard, bool validateLuhn = true)
    {
        var isValid = CheckIsNotVisaCard(value, validateLuhn);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("ValidateLuhn", validateLuhn),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid Visa card number", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is not a valid MasterCard number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid MasterCard number.</exception>
    public static void EnsureIsNotMasterCard(this string? value, string fieldName, IBlackboard? blackboard, bool validateLuhn = true)
    {
        var isValid = CheckIsNotMasterCard(value, validateLuhn);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("ValidateLuhn", validateLuhn),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid MasterCard number", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is not a valid American Express card number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid American Express card number.</exception>
    public static void EnsureIsNotAmericanExpress(this string? value, string fieldName, IBlackboard? blackboard, bool validateLuhn = true)
    {
        var isValid = CheckIsNotAmericanExpress(value, validateLuhn);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("ValidateLuhn", validateLuhn),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid American Express card number", null, null, contextList);
        }
    }
}
