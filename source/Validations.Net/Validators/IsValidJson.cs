using System.Text.Json;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Validates that a value is valid JSON.
/// </summary>
public static class IsValidJson
{
    private const string ValidatorName = nameof(IsValidJson);

    /// <summary>
    ///     Checks if the specified string is valid JSON.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is valid JSON; otherwise, false.</returns>
    public static bool CheckIsValidJson(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        try
        {
            using var document = JsonDocument.Parse(value);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }

    /// <summary>
    ///     Checks if the specified string is valid JSON with specific options.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="options">The JSON parsing options to use.</param>
    /// <returns>True if the string is valid JSON; otherwise, false.</returns>
    public static bool CheckIsValidJson(string? value, JsonDocumentOptions options)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        try
        {
            using var document = JsonDocument.Parse(value, options);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }

    /// <summary>
    ///     Checks if the specified string is valid JSON and represents an array.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is valid JSON array; otherwise, false.</returns>
    public static bool CheckIsValidJsonArray(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        try
        {
            using var document = JsonDocument.Parse(value);
            return document.RootElement.ValueKind == JsonValueKind.Array;
        }
        catch (JsonException)
        {
            return false;
        }
    }

    /// <summary>
    ///     Checks if the specified string is valid JSON and represents an object.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is valid JSON object; otherwise, false.</returns>
    public static bool CheckIsValidJsonObject(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        try
        {
            using var document = JsonDocument.Parse(value);
            return document.RootElement.ValueKind == JsonValueKind.Object;
        }
        catch (JsonException)
        {
            return false;
        }
    }

    /// <summary>
    ///     Checks if the specified string is valid JSON and represents a primitive value.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is valid JSON primitive; otherwise, false.</returns>
    public static bool CheckIsValidJsonPrimitive(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        try
        {
            using var document = JsonDocument.Parse(value);
            var kind = document.RootElement.ValueKind;
            return kind == JsonValueKind.String ||
                   kind == JsonValueKind.Number ||
                   kind == JsonValueKind.True ||
                   kind == JsonValueKind.False ||
                   kind == JsonValueKind.Null;
        }
        catch (JsonException)
        {
            return false;
        }
    }

    /// <summary>
    ///     Ensures that the specified string is valid JSON.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not valid JSON.</exception>
    public static void EnsureIsValidJson(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidJson(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not valid JSON.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified string is valid JSON with specific options.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="options">The JSON parsing options to use.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not valid JSON.</exception>
    public static void EnsureIsValidJson(string? value, JsonDocumentOptions options, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidJson(value, options);
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
                $"The value '{value}' is not valid JSON with the specified options.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified string is valid JSON and represents an array.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not valid JSON array.</exception>
    public static void EnsureIsValidJsonArray(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidJsonArray(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not a valid JSON array.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified string is valid JSON and represents an object.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not valid JSON object.</exception>
    public static void EnsureIsValidJsonObject(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidJsonObject(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not a valid JSON object.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified string is valid JSON and represents a primitive value.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not valid JSON primitive.</exception>
    public static void EnsureIsValidJsonPrimitive(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidJsonPrimitive(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not a valid JSON primitive.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Validates that the specified string is valid JSON.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is valid JSON.</returns>
    public static ValidationResult ValidateIsValidJson(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidJson(value);
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
            $"The value '{value}' is not valid JSON.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified string is valid JSON with specific options.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="options">The JSON parsing options to use.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is valid JSON.</returns>
    public static ValidationResult ValidateIsValidJson(string? value, JsonDocumentOptions options, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidJson(value, options);
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
            $"The value '{value}' is not valid JSON with the specified options.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified string is valid JSON and represents an array.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is valid JSON array.</returns>
    public static ValidationResult ValidateIsValidJsonArray(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidJsonArray(value);
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
            $"The value '{value}' is not a valid JSON array.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified string is valid JSON and represents an object.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is valid JSON object.</returns>
    public static ValidationResult ValidateIsValidJsonObject(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidJsonObject(value);
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
            $"The value '{value}' is not a valid JSON object.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified string is valid JSON and represents a primitive value.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is valid JSON primitive.</returns>
    public static ValidationResult ValidateIsValidJsonPrimitive(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidJsonPrimitive(value);
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
            $"The value '{value}' is not a valid JSON primitive.",
            fieldName,
            blackboard,
            contextList);
    }
}
