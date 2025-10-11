using System.Runtime.CompilerServices;
using System.Text.Json;
using SimpleBlackboard.Net;
using Validations.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods for checking if a value is valid JSON.
/// </summary>
public static class IsValidJson
{
    private const string ValidatorName = nameof(IsValidJson);

    #region Check Methods

    /// <summary>
    ///     Checks if the string is valid JSON.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is valid JSON; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    ///     Checks if the string is a valid JSON array.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid JSON array; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    ///     Checks if the string is a valid JSON object.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid JSON object; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    ///     Checks if the string is a valid JSON primitive.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid JSON primitive; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidJsonPrimitive(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        try
        {
            using var document = JsonDocument.Parse(value);
            var valueKind = document.RootElement.ValueKind;
            return valueKind == JsonValueKind.String || valueKind == JsonValueKind.Number ||
                   valueKind == JsonValueKind.True || valueKind == JsonValueKind.False || valueKind == JsonValueKind.Null;
        }
        catch (JsonException)
        {
            return false;
        }
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates that the string is valid JSON.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidJson(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidJson(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must be valid JSON",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid JSON array.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidJsonArray(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidJsonArray(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must be a valid JSON array",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid JSON object.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidJsonObject(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidJsonObject(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must be a valid JSON object",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid JSON primitive.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidJsonPrimitive(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidJsonPrimitive(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must be a valid JSON primitive",
            parameterName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that the string is valid JSON.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is valid JSON.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not valid JSON.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidJson(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidJson(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is a valid JSON array.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is a valid JSON array.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a valid JSON array.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidJsonArray(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidJsonArray(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is a valid JSON object.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is a valid JSON object.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a valid JSON object.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidJsonObject(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidJsonObject(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is a valid JSON primitive.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is a valid JSON primitive.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a valid JSON primitive.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidJsonPrimitive(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidJsonPrimitive(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    #endregion
}
