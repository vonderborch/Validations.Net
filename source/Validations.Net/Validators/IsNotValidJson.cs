using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Validates that a value is NOT valid JSON.
/// </summary>
public static class IsNotValidJson
{
    private const string ValidatorName = nameof(IsNotValidJson);

    /// <summary>
    /// Checks if the specified string is NOT valid JSON.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT valid JSON; otherwise, false.</returns>
    public static bool CheckIsNotValidJson(string? value)
    {
        return !IsValidJson.CheckIsValidJson(value);
    }

    /// <summary>
    /// Checks if the specified string is NOT valid JSON with specific options.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="options">The JSON parsing options to use.</param>
    /// <returns>True if the string is NOT valid JSON; otherwise, false.</returns>
    public static bool CheckIsNotValidJson(string? value, JsonDocumentOptions options)
    {
        return !IsValidJson.CheckIsValidJson(value, options);
    }

    /// <summary>
    /// Checks if the specified string is NOT valid JSON and represents an object.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT valid JSON object; otherwise, false.</returns>
    public static bool CheckIsNotValidJsonObject(string? value)
    {
        return !IsValidJson.CheckIsValidJsonObject(value);
    }

    /// <summary>
    /// Checks if the specified string is NOT valid JSON and represents an array.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT valid JSON array; otherwise, false.</returns>
    public static bool CheckIsNotValidJsonArray(string? value)
    {
        return !IsValidJson.CheckIsValidJsonArray(value);
    }

    /// <summary>
    /// Checks if the specified string is NOT valid JSON and represents a primitive value.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT valid JSON primitive; otherwise, false.</returns>
    public static bool CheckIsNotValidJsonPrimitive(string? value)
    {
        return !IsValidJson.CheckIsValidJsonPrimitive(value);
    }

    /// <summary>
    /// Validates that the specified string is NOT valid JSON.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is NOT valid JSON.</returns>
    public static ValidationResult ValidateIsNotValidJson(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidJson(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is valid JSON.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified string is NOT valid JSON with specific options.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="options">The JSON parsing options to use.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is NOT valid JSON.</returns>
    public static ValidationResult ValidateIsNotValidJson(string? value, JsonDocumentOptions options, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidJson(value, options);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Options", options),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is valid JSON with the specified options.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified string is NOT valid JSON and represents an object.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is NOT valid JSON object.</returns>
    public static ValidationResult ValidateIsNotValidJsonObject(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidJsonObject(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a valid JSON object.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified string is NOT valid JSON and represents an array.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is NOT valid JSON array.</returns>
    public static ValidationResult ValidateIsNotValidJsonArray(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidJsonArray(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a valid JSON array.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified string is NOT valid JSON and represents a primitive value.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is NOT valid JSON primitive.</returns>
    public static ValidationResult ValidateIsNotValidJsonPrimitive(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidJsonPrimitive(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a valid JSON primitive.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Ensures that the specified string is NOT valid JSON.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is valid JSON.</exception>
    public static void EnsureIsNotValidJson(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidJson(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is valid JSON.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is NOT valid JSON with specific options.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="options">The JSON parsing options to use.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is valid JSON.</exception>
    public static void EnsureIsNotValidJson(string? value, JsonDocumentOptions options, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidJson(value, options);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Options", options),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is valid JSON with the specified options.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is NOT valid JSON and represents an object.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is valid JSON object.</exception>
    public static void EnsureIsNotValidJsonObject(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidJsonObject(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a valid JSON object.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is NOT valid JSON and represents an array.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is valid JSON array.</exception>
    public static void EnsureIsNotValidJsonArray(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidJsonArray(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a valid JSON array.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is NOT valid JSON and represents a primitive value.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is valid JSON primitive.</exception>
    public static void EnsureIsNotValidJsonPrimitive(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidJsonPrimitive(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a valid JSON primitive.",
                fieldName,
                blackboard,
                contextList);
        }
    }
}
