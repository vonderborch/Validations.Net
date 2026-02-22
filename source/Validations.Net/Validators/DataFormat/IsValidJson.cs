using System.Runtime.CompilerServices;
using System.Text.Json;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.DataFormat;

/// <summary>
/// The IsValidJson class provides methods for validation to ensure that
/// a string represents valid JSON.
/// </summary>
public static class IsValidJson
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsValidJson";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Must be valid JSON";

    /// <summary>
    /// Checks if the given value is valid JSON.
    /// </summary>
    public static bool CheckIsValidJson(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        try
        {
            using var doc = JsonDocument.Parse(value);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }

    /// <summary>
    /// Validates whether the given value is valid JSON.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidJson(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsValidJson())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is valid JSON, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidJson(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsValidJson(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}
