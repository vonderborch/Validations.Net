using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Phone;

/// <summary>
/// The IsValidPhoneNumber class provides methods for validation to ensure that
/// a string represents a valid phone number (basic format).
/// </summary>
public static class IsValidPhoneNumber
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsValidPhoneNumber";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Must be a valid phone number";

    /// <summary>
    /// Checks if the given string is a valid phone number.
    /// Strips spaces, dashes, parens. Must start with optional '+', remaining all digits, length 7-15 after stripping.
    /// </summary>
    public static bool CheckIsValidPhoneNumber(this string? value)
    {
        if (value is null)
            return false;

        int digitCount = 0;
        bool seenPlus = false;

        for (int i = 0; i < value.Length; i++)
        {
            char c = value[i];
            if (c == ' ' || c == '-' || c == '(' || c == ')')
                continue;
            if (c == '+')
            {
                if (seenPlus || digitCount > 0)
                    return false;
                seenPlus = true;
                continue;
            }
            if (c >= '0' && c <= '9')
            {
                digitCount++;
                continue;
            }
            return false;
        }

        return digitCount >= 7 && digitCount <= 15;
    }

    /// <summary>
    /// Validates whether the given string is a valid phone number.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidPhoneNumber(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsValidPhoneNumber())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given string is a valid phone number, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidPhoneNumber(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsValidPhoneNumber(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}
