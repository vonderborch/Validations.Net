using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Finance;

/// <summary>
/// The IsValidIban class provides methods for validation to ensure that
/// a string represents a valid IBAN format (basic format validation only).
/// </summary>
public static class IsValidIban
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsValidIban";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Must be a valid IBAN";

    /// <summary>
    /// Checks if the given string is a valid IBAN format.
    /// Basic format: strip spaces, length 15-34, first 2 letters (country), next 2 digits (check), remainder alphanumeric.
    /// </summary>
    public static bool CheckIsValidIban(this string? value)
    {
        if (value is null)
            return false;

        int len = 0;
        for (int i = 0; i < value.Length; i++)
        {
            if (value[i] == ' ')
                continue;
            len++;
        }

        if (len < 15 || len > 34)
            return false;

        int pos = 0;
        for (int i = 0; i < value.Length; i++)
        {
            char c = value[i];
            if (c == ' ')
                continue;

            if (pos < 2)
            {
                if ((c < 'A' || c > 'Z') && (c < 'a' || c > 'z'))
                    return false;
            }
            else if (pos < 4)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            else
            {
                bool isLetter = (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
                bool isDigit = c >= '0' && c <= '9';
                if (!isLetter && !isDigit)
                    return false;
            }
            pos++;
        }

        return true;
    }

    /// <summary>
    /// Validates whether the given string is a valid IBAN format.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidIban(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsValidIban())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given string is a valid IBAN format, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidIban(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsValidIban(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}
