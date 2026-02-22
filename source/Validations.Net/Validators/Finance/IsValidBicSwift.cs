using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Finance;

/// <summary>
/// The IsValidBicSwift class provides methods for validation to ensure that
/// a string represents a valid BIC/SWIFT code.
/// Format: 8 or 11 chars, first 4 letters, next 2 letters (country), next 2 alphanumeric (location), optional 3 alphanumeric (branch).
/// </summary>
public static class IsValidBicSwift
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsValidBicSwift";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Must be a valid BIC/SWIFT code";

    /// <summary>
    /// Checks if the given string is a valid BIC/SWIFT code.
    /// </summary>
    public static bool CheckIsValidBicSwift(this string? value)
    {
        if (value is null)
            return false;

        int len = value.Length;
        if (len != 8 && len != 11)
            return false;

        for (int i = 0; i < value.Length; i++)
        {
            char c = value[i];
            bool isLetter = (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
            bool isDigit = c >= '0' && c <= '9';

            if (i < 4)
            {
                if (!isLetter)
                    return false;
            }
            else if (i < 6)
            {
                if (!isLetter)
                    return false;
            }
            else
            {
                if (!isLetter && !isDigit)
                    return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Validates whether the given string is a valid BIC/SWIFT code.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidBicSwift(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsValidBicSwift())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given string is a valid BIC/SWIFT code, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidBicSwift(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsValidBicSwift(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}
