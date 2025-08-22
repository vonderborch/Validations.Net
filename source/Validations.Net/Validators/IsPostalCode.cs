using System.Text.RegularExpressions;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Validates that a string is a valid postal code.
/// </summary>
public static class IsPostalCode
{
    private const string ValidatorName = nameof(IsPostalCode);

    // Common postal code patterns
    private static readonly Regex UsZipCodePattern = new(
        @"^\d{5}(-\d{4})?$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    private static readonly Regex CanadianPostalCodePattern = new(
        @"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    private static readonly Regex UkPostalCodePattern = new(
        @"^[A-Z]{1,2}\d[A-Z\d]? ?\d[A-Z]{2}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    private static readonly Regex GermanPostalCodePattern = new(
        @"^\d{5}$",
        RegexOptions.Compiled
    );

    private static readonly Regex FrenchPostalCodePattern = new(
        @"^\d{5}$",
        RegexOptions.Compiled
    );

    private static readonly Regex JapanesePostalCodePattern = new(
        @"^\d{3}-\d{4}$",
        RegexOptions.Compiled
    );

    /// <summary>
    ///     Checks if the string is a valid Canadian postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid Canadian postal code; otherwise, false.</returns>
    public static bool CheckIsCanadianPostalCode(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return CanadianPostalCodePattern.IsMatch(value);
    }

    /// <summary>
    ///     Checks if the string is a valid French postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid French postal code; otherwise, false.</returns>
    public static bool CheckIsFrenchPostalCode(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return FrenchPostalCodePattern.IsMatch(value);
    }

    /// <summary>
    ///     Checks if the string is a valid German postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid German postal code; otherwise, false.</returns>
    public static bool CheckIsGermanPostalCode(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return GermanPostalCodePattern.IsMatch(value);
    }

    /// <summary>
    ///     Checks if the string is a valid Japanese postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid Japanese postal code; otherwise, false.</returns>
    public static bool CheckIsJapanesePostalCode(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return JapanesePostalCodePattern.IsMatch(value);
    }

    /// <summary>
    ///     Checks if the string is a valid postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="pattern">The regex pattern to use for validation. If null, uses common patterns.</param>
    /// <param name="options">Regex options to use.</param>
    /// <returns>True if the string is a valid postal code; otherwise, false.</returns>
    public static bool CheckIsPostalCode(this string? value, string? pattern = null,
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
        return UsZipCodePattern.IsMatch(value) ||
               CanadianPostalCodePattern.IsMatch(value) ||
               UkPostalCodePattern.IsMatch(value) ||
               GermanPostalCodePattern.IsMatch(value) ||
               FrenchPostalCodePattern.IsMatch(value) ||
               JapanesePostalCodePattern.IsMatch(value);
    }

    /// <summary>
    ///     Checks if the string is a valid UK postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid UK postal code; otherwise, false.</returns>
    public static bool CheckIsUkPostalCode(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return UkPostalCodePattern.IsMatch(value);
    }

    /// <summary>
    ///     Checks if the string is a valid US ZIP code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid US ZIP code; otherwise, false.</returns>
    public static bool CheckIsUsZipCode(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return UsZipCodePattern.IsMatch(value);
    }

    /// <summary>
    ///     Ensures that the string is a valid Canadian postal code, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid Canadian postal code.</exception>
    public static void EnsureIsCanadianPostalCode(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsCanadianPostalCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid Canadian postal code", null,
                null, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is a valid French postal code, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid French postal code.</exception>
    public static void EnsureIsFrenchPostalCode(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsFrenchPostalCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid French postal code", null, null,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is a valid German postal code, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid German postal code.</exception>
    public static void EnsureIsGermanPostalCode(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsGermanPostalCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid German postal code", null, null,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is a valid Japanese postal code, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid Japanese postal code.</exception>
    public static void EnsureIsJapanesePostalCode(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsJapanesePostalCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid Japanese postal code", null,
                null, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is a valid postal code, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="pattern">The regex pattern to use for validation. If null, uses common patterns.</param>
    /// <param name="options">Regex options to use.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid postal code.</exception>
    public static void EnsureIsPostalCode(this string? value, string fieldName, IBlackboard? blackboard,
        string? pattern = null, RegexOptions options = RegexOptions.None)
    {
        var isValid = CheckIsPostalCode(value, pattern, options);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Pattern", pattern),
                ("Options", options),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid postal code", null, null,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is a valid UK postal code, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid UK postal code.</exception>
    public static void EnsureIsUkPostalCode(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsUkPostalCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid UK postal code", null, null,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is a valid US ZIP code, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid US ZIP code.</exception>
    public static void EnsureIsUsZipCode(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsUsZipCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid US ZIP code", null, null,
                contextList);
        }
    }

    /// <summary>
    ///     Validates that the string is a valid Canadian postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsCanadianPostalCode(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsCanadianPostalCode(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "The value must be a valid Canadian postal code", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid French postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsFrenchPostalCode(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsFrenchPostalCode(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "The value must be a valid French postal code", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid German postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsGermanPostalCode(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsGermanPostalCode(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "The value must be a valid German postal code", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid Japanese postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsJapanesePostalCode(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsJapanesePostalCode(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "The value must be a valid Japanese postal code", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="pattern">The regex pattern to use for validation. If null, uses common patterns.</param>
    /// <param name="options">Regex options to use.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsPostalCode(this string? value, string fieldName, IBlackboard? blackboard,
        string? pattern = null, RegexOptions options = RegexOptions.None)
    {
        var isValid = CheckIsPostalCode(value, pattern, options);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Pattern", pattern),
            ("Options", options),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must be a valid postal code",
                fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid UK postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsUkPostalCode(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsUkPostalCode(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must be a valid UK postal code",
                fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid US ZIP code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsUsZipCode(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsUsZipCode(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must be a valid US ZIP code",
                fieldName, blackboard, contextList);
    }
}
