using System.Runtime.CompilerServices;
using System.Text.Json;
using SimpleBlackboard.Net;
using Validations.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods for checking if a value is not valid JSON.
/// </summary>
public static class IsNotValidJson
{
    private const string ValidatorName = nameof(IsNotValidJson);

    #region Check Methods

    /// <summary>
    ///     Checks if the string is not valid JSON.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not valid JSON; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidJson(string? value)
    {
        return !IsValidJson.CheckIsValidJson(value);
    }

    /// <summary>
    ///     Checks if the string is not a valid JSON array.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not a valid JSON array; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidJsonArray(string? value)
    {
        return !IsValidJson.CheckIsValidJsonArray(value);
    }

    /// <summary>
    ///     Checks if the string is not a valid JSON object.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not a valid JSON object; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidJsonObject(string? value)
    {
        return !IsValidJson.CheckIsValidJsonObject(value);
    }

    /// <summary>
    ///     Checks if the string is not a valid JSON primitive.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not a valid JSON primitive; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidJsonPrimitive(string? value)
    {
        return !IsValidJson.CheckIsValidJsonPrimitive(value);
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates that the string is not valid JSON.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidJson(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidJson(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must not be valid JSON",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid JSON array.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidJsonArray(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidJsonArray(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must not be a valid JSON array",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid JSON object.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidJsonObject(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidJsonObject(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must not be a valid JSON object",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid JSON primitive.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidJsonPrimitive(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidJsonPrimitive(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must not be a valid JSON primitive",
            parameterName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that the string is not valid JSON.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not valid JSON.</returns>
    /// <exception cref="ValidationException">Thrown when the value is valid JSON.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotValidJson(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidJson(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is not a valid JSON array.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not a valid JSON array.</returns>
    /// <exception cref="ValidationException">Thrown when the value is a valid JSON array.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotValidJsonArray(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidJsonArray(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is not a valid JSON object.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not a valid JSON object.</returns>
    /// <exception cref="ValidationException">Thrown when the value is a valid JSON object.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotValidJsonObject(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidJsonObject(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is not a valid JSON primitive.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not a valid JSON primitive.</returns>
    /// <exception cref="ValidationException">Thrown when the value is a valid JSON primitive.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotValidJsonPrimitive(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidJsonPrimitive(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    #endregion
}
