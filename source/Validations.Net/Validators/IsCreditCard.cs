using System.Text.RegularExpressions;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Validates that a string is a valid credit card number.
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
    ///     Checks if the string is a valid American Express card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>True if the string is a valid American Express card number; otherwise, false.</returns>
    public static bool CheckIsAmericanExpress(this string? value, bool validateLuhn = true)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        var cleanValue = value.Replace(" ", "").Replace("-", "");
        if (!AmericanExpressPattern.IsMatch(cleanValue))
        {
            return false;
        }

        return !validateLuhn || ValidateLuhn(cleanValue);
    }

    /// <summary>
    ///     Checks if the string is a valid credit card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>True if the string is a valid credit card number; otherwise, false.</returns>
    public static bool CheckIsCreditCard(this string? value, bool validateLuhn = true)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        // Remove spaces and dashes
        var cleanValue = value.Replace(" ", "").Replace("-", "");

        // Check if it's all digits
        if (!cleanValue.All(char.IsDigit))
        {
            return false;
        }

        // Check length (most cards are 13-19 digits)
        if (cleanValue.Length < 13 || cleanValue.Length > 19)
        {
            return false;
        }

        // Check if it matches any known card pattern
        var matchesPattern = VisaPattern.IsMatch(cleanValue) ||
                             MasterCardPattern.IsMatch(cleanValue) ||
                             AmericanExpressPattern.IsMatch(cleanValue) ||
                             DiscoverPattern.IsMatch(cleanValue) ||
                             DinersClubPattern.IsMatch(cleanValue) ||
                             JcbPattern.IsMatch(cleanValue);

        if (!matchesPattern)
        {
            return false;
        }

        // Validate using Luhn algorithm if requested
        return !validateLuhn || ValidateLuhn(cleanValue);
    }

    /// <summary>
    ///     Checks if the string is a valid MasterCard number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>True if the string is a valid MasterCard number; otherwise, false.</returns>
    public static bool CheckIsMasterCard(this string? value, bool validateLuhn = true)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        var cleanValue = value.Replace(" ", "").Replace("-", "");
        if (!MasterCardPattern.IsMatch(cleanValue))
        {
            return false;
        }

        return !validateLuhn || ValidateLuhn(cleanValue);
    }

    /// <summary>
    ///     Checks if the string is a valid Visa card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>True if the string is a valid Visa card number; otherwise, false.</returns>
    public static bool CheckIsVisaCard(this string? value, bool validateLuhn = true)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        var cleanValue = value.Replace(" ", "").Replace("-", "");
        if (!VisaPattern.IsMatch(cleanValue))
        {
            return false;
        }

        return !validateLuhn || ValidateLuhn(cleanValue);
    }

    /// <summary>
    ///     Ensures that the string is a valid American Express card number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid American Express card number.</exception>
    public static void EnsureIsAmericanExpress(this string? value, string fieldName, IBlackboard? blackboard,
        bool validateLuhn = true)
    {
        var isValid = CheckIsAmericanExpress(value, validateLuhn);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("ValidateLuhn", validateLuhn),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid American Express card number",
                null, null, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is a valid credit card number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid credit card number.</exception>
    public static void EnsureIsCreditCard(this string? value, string fieldName, IBlackboard? blackboard,
        bool validateLuhn = true)
    {
        var isValid = CheckIsCreditCard(value, validateLuhn);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("ValidateLuhn", validateLuhn),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid credit card number", null, null,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is a valid MasterCard number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid MasterCard number.</exception>
    public static void EnsureIsMasterCard(this string? value, string fieldName, IBlackboard? blackboard,
        bool validateLuhn = true)
    {
        var isValid = CheckIsMasterCard(value, validateLuhn);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("ValidateLuhn", validateLuhn),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid MasterCard number", null, null,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is a valid Visa card number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid Visa card number.</exception>
    public static void EnsureIsVisaCard(this string? value, string fieldName, IBlackboard? blackboard,
        bool validateLuhn = true)
    {
        var isValid = CheckIsVisaCard(value, validateLuhn);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("ValidateLuhn", validateLuhn),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid Visa card number", null, null,
                contextList);
        }
    }

    /// <summary>
    ///     Validates that the string is a valid American Express card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsAmericanExpress(this string? value, string fieldName,
        IBlackboard? blackboard, bool validateLuhn = true)
    {
        var isValid = CheckIsAmericanExpress(value, validateLuhn);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("ValidateLuhn", validateLuhn),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "The value must be a valid American Express card number", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid credit card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsCreditCard(this string? value, string fieldName, IBlackboard? blackboard,
        bool validateLuhn = true)
    {
        var isValid = CheckIsCreditCard(value, validateLuhn);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("ValidateLuhn", validateLuhn),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "The value must be a valid credit card number", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid MasterCard number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsMasterCard(this string? value, string fieldName, IBlackboard? blackboard,
        bool validateLuhn = true)
    {
        var isValid = CheckIsMasterCard(value, validateLuhn);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("ValidateLuhn", validateLuhn),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must be a valid MasterCard number",
                fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid Visa card number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validateLuhn">Whether to validate using the Luhn algorithm. Default is true.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsVisaCard(this string? value, string fieldName, IBlackboard? blackboard,
        bool validateLuhn = true)
    {
        var isValid = CheckIsVisaCard(value, validateLuhn);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("ValidateLuhn", validateLuhn),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must be a valid Visa card number",
                fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates a credit card number using the Luhn algorithm.
    /// </summary>
    /// <param name="cardNumber">The card number to validate (digits only).</param>
    /// <returns>True if the card number passes the Luhn check; otherwise, false.</returns>
    private static bool ValidateLuhn(string cardNumber)
    {
        var sum = 0;
        var isEven = false;

        // Loop through values starting from the rightmost side
        for (var i = cardNumber.Length - 1; i >= 0; i--)
        {
            var digit = cardNumber[i] - '0';

            if (isEven)
            {
                digit *= 2;
                if (digit > 9)
                {
                    digit -= 9;
                }
            }

            sum += digit;
            isEven = !isEven;
        }

        return sum % 10 == 0;
    }
}
