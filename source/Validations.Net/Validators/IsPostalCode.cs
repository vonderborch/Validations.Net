using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string is a valid postal code.
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
    /// Checks if the string is a valid Canadian postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid Canadian postal code; otherwise, false.</returns>
    public static bool CheckIsCanadianPostalCode(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return CanadianPostalCodePattern.IsMatch(value);
    }

    /// <summary>
    /// Checks if the string is a valid French postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid French postal code; otherwise, false.</returns>
    public static bool CheckIsFrenchPostalCode(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return FrenchPostalCodePattern.IsMatch(value);
    }

    /// <summary>
    /// Checks if the string is a valid German postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid German postal code; otherwise, false.</returns>
    public static bool CheckIsGermanPostalCode(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return GermanPostalCodePattern.IsMatch(value);
    }

    /// <summary>
    /// Checks if the string is a valid Japanese postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid Japanese postal code; otherwise, false.</returns>
    public static bool CheckIsJapanesePostalCode(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return JapanesePostalCodePattern.IsMatch(value);
    }

    /// <summary>
    /// Checks if the string is a valid postal code for the specified country.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="country">The country code for postal code validation.</param>
    /// <returns>True if the string is a valid postal code for the specified country; otherwise, false.</returns>
    public static bool CheckIsPostalCode(string? value, string country)
    {
        if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(country))
            return false;

        return country.ToUpperInvariant() switch
        {
            "US" => CheckIsUsZipCode(value),
            "CA" => CheckIsCanadianPostalCode(value),
            "UK" => CheckIsUkPostalCode(value),
            "DE" => CheckIsGermanPostalCode(value),
            "FR" => CheckIsFrenchPostalCode(value),
            "JP" => CheckIsJapanesePostalCode(value),
            _ => false
        };
    }

    /// <summary>
    /// Checks if the string is a valid UK postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid UK postal code; otherwise, false.</returns>
    public static bool CheckIsUkPostalCode(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return UkPostalCodePattern.IsMatch(value);
    }

    /// <summary>
    /// Checks if the string is a valid US ZIP code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid US ZIP code; otherwise, false.</returns>
    public static bool CheckIsUsZipCode(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return UsZipCodePattern.IsMatch(value);
    }

    /// <summary>
    /// Ensures that the string is a valid Canadian postal code, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid Canadian postal code.</exception>
    public static void EnsureIsCanadianPostalCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsCanadianPostalCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a valid Canadian postal code.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid French postal code, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid French postal code.</exception>
    public static void EnsureIsFrenchPostalCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsFrenchPostalCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a valid French postal code.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid German postal code, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid German postal code.</exception>
    public static void EnsureIsGermanPostalCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsGermanPostalCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a valid German postal code.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid Japanese postal code, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid Japanese postal code.</exception>
    public static void EnsureIsJapanesePostalCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsJapanesePostalCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a valid Japanese postal code.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid postal code for the specified country, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="country">The country code for postal code validation.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid postal code for the specified country.</exception>
    public static void EnsureIsPostalCode(string? value, string country, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPostalCode(value, country);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Country", country)
            };
            throw ValidationException.Create(ValidatorName, $"Value must be a valid postal code for country {country}.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid UK postal code, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid UK postal code.</exception>
    public static void EnsureIsUkPostalCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsUkPostalCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a valid UK postal code.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid US ZIP code, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid US ZIP code.</exception>
    public static void EnsureIsUsZipCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsUsZipCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a valid US ZIP code.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Validates that the string is a valid Canadian postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid Canadian postal code.</returns>
    public static ValidationResult ValidateIsCanadianPostalCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsCanadianPostalCode(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a valid Canadian postal code.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid French postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid French postal code.</returns>
    public static ValidationResult ValidateIsFrenchPostalCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsFrenchPostalCode(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a valid French postal code.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid German postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid German postal code.</returns>
    public static ValidationResult ValidateIsGermanPostalCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsGermanPostalCode(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a valid German postal code.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid Japanese postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid Japanese postal code.</returns>
    public static ValidationResult ValidateIsJapanesePostalCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsJapanesePostalCode(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a valid Japanese postal code.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid postal code for the specified country.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="country">The country code for postal code validation.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid postal code for the specified country.</returns>
    public static ValidationResult ValidateIsPostalCode(string? value, string country, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPostalCode(value, country);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Country", country),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must be a valid postal code for country {country}.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid UK postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid UK postal code.</returns>
    public static ValidationResult ValidateIsUkPostalCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsUkPostalCode(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a valid UK postal code.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid US ZIP code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid US ZIP code.</returns>
    public static ValidationResult ValidateIsUsZipCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsUsZipCode(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a valid US ZIP code.",
            parameterName, blackboard, contextList);
    }
}
