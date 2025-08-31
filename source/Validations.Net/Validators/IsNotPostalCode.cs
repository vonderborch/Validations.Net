using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string is NOT a valid postal code.
/// </summary>
public static class IsNotPostalCode
{
    private const string ValidatorName = nameof(IsNotPostalCode);

    /// <summary>
    /// Checks if the string is NOT a valid Canadian postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is NOT a valid Canadian postal code; otherwise, false.</returns>
    public static bool CheckIsNotCanadianPostalCode(string? value)
    {
        return !IsPostalCode.CheckIsCanadianPostalCode(value);
    }

    /// <summary>
    /// Checks if the string is NOT a valid French postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is NOT a valid French postal code; otherwise, false.</returns>
    public static bool CheckIsNotFrenchPostalCode(string? value)
    {
        return !IsPostalCode.CheckIsFrenchPostalCode(value);
    }

    /// <summary>
    /// Checks if the string is NOT a valid German postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is NOT a valid German postal code; otherwise, false.</returns>
    public static bool CheckIsNotGermanPostalCode(string? value)
    {
        return !IsPostalCode.CheckIsGermanPostalCode(value);
    }

    /// <summary>
    /// Checks if the string is NOT a valid Japanese postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is NOT a valid Japanese postal code; otherwise, false.</returns>
    public static bool CheckIsNotJapanesePostalCode(string? value)
    {
        return !IsPostalCode.CheckIsJapanesePostalCode(value);
    }

    /// <summary>
    /// Checks if the string is NOT a valid postal code for the specified country.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="country">The country code for postal code validation.</param>
    /// <returns>True if the string is NOT a valid postal code for the specified country; otherwise, false.</returns>
    public static bool CheckIsNotPostalCode(string? value, string country)
    {
        return !IsPostalCode.CheckIsPostalCode(value, country);
    }

    /// <summary>
    /// Checks if the string is NOT a valid UK postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is NOT a valid UK postal code; otherwise, false.</returns>
    public static bool CheckIsNotUkPostalCode(string? value)
    {
        return !IsPostalCode.CheckIsUkPostalCode(value);
    }

    /// <summary>
    /// Checks if the string is NOT a valid US ZIP code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is NOT a valid US ZIP code; otherwise, false.</returns>
    public static bool CheckIsNotUsZipCode(string? value)
    {
        return !IsPostalCode.CheckIsUsZipCode(value);
    }

    /// <summary>
    /// Ensures that the string is NOT a valid Canadian postal code, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid Canadian postal code.</exception>
    public static void EnsureIsNotCanadianPostalCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotCanadianPostalCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a valid Canadian postal code.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is NOT a valid French postal code, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid French postal code.</exception>
    public static void EnsureIsNotFrenchPostalCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotFrenchPostalCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a valid French postal code.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is NOT a valid German postal code, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid German postal code.</exception>
    public static void EnsureIsNotGermanPostalCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotGermanPostalCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a valid German postal code.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is NOT a valid Japanese postal code, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid Japanese postal code.</exception>
    public static void EnsureIsNotJapanesePostalCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotJapanesePostalCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a valid Japanese postal code.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is NOT a valid postal code for the specified country, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="country">The country code for postal code validation.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid postal code for the specified country.</exception>
    public static void EnsureIsNotPostalCode(string? value, string country, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPostalCode(value, country);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Country", country)
            };
            throw ValidationException.Create(ValidatorName, $"Value must NOT be a valid postal code for country {country}.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is NOT a valid UK postal code, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid UK postal code.</exception>
    public static void EnsureIsNotUkPostalCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotUkPostalCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a valid UK postal code.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is NOT a valid US ZIP code, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid US ZIP code.</exception>
    public static void EnsureIsNotUsZipCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotUsZipCode(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a valid US ZIP code.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Validates that the string is NOT a valid Canadian postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a valid Canadian postal code.</returns>
    public static ValidationResult ValidateIsNotCanadianPostalCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotCanadianPostalCode(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a valid Canadian postal code.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is NOT a valid French postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a valid French postal code.</returns>
    public static ValidationResult ValidateIsNotFrenchPostalCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotFrenchPostalCode(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a valid French postal code.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is NOT a valid German postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a valid German postal code.</returns>
    public static ValidationResult ValidateIsNotGermanPostalCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotGermanPostalCode(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a valid German postal code.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is NOT a valid Japanese postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a valid Japanese postal code.</returns>
    public static ValidationResult ValidateIsNotJapanesePostalCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotJapanesePostalCode(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a valid Japanese postal code.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is NOT a valid postal code for the specified country.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="country">The country code for postal code validation.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a valid postal code for the specified country.</returns>
    public static ValidationResult ValidateIsNotPostalCode(string? value, string country, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPostalCode(value, country);
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

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must NOT be a valid postal code for country {country}.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is NOT a valid UK postal code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a valid UK postal code.</returns>
    public static ValidationResult ValidateIsNotUkPostalCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotUkPostalCode(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a valid UK postal code.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is NOT a valid US ZIP code.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a valid US ZIP code.</returns>
    public static ValidationResult ValidateIsNotUsZipCode(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotUsZipCode(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a valid US ZIP code.",
            parameterName, blackboard, contextList);
    }
}
