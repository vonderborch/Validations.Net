using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Security;

/// <summary>
/// The IsSecurePassword class provides methods for validation to ensure that
/// a password meets security requirements.
/// </summary>
public static class IsSecurePassword
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsSecurePassword";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Password does not meet security requirements";

    /// <summary>
    /// Checks if the given password meets the specified security requirements.
    /// </summary>
    public static bool CheckIsSecurePassword(this string? value, int minLength = 8, bool requireUppercase = true,
        bool requireLowercase = true, bool requireDigit = true, bool requireSpecialChar = true)
    {
        if (value is null)
            return false;

        if (value.Length < minLength)
            return false;

        bool hasUpper = false;
        bool hasLower = false;
        bool hasDigit = false;
        bool hasSpecial = false;

        for (int i = 0; i < value.Length; i++)
        {
            char c = value[i];
            if (c >= 'A' && c <= 'Z') hasUpper = true;
            else if (c >= 'a' && c <= 'z') hasLower = true;
            else if (c >= '0' && c <= '9') hasDigit = true;
            else hasSpecial = true;
        }

        if (requireUppercase && !hasUpper) return false;
        if (requireLowercase && !hasLower) return false;
        if (requireDigit && !hasDigit) return false;
        if (requireSpecialChar && !hasSpecial) return false;

        return true;
    }

    /// <summary>
    /// Validates whether the given password meets the specified security requirements.
    /// </summary>
    public static ValidationResult ValidateIsSecurePassword(this string? value, int minLength = 8,
        bool requireUppercase = true, bool requireLowercase = true, bool requireDigit = true,
        bool requireSpecialChar = true, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value.CheckIsSecurePassword(minLength, requireUppercase, requireLowercase, requireDigit, requireSpecialChar))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var context = new List<(string key, object? value)> { ("value", value) };
        var failed = new List<string>();
        if (value is null || value.Length < minLength)
            failed.Add("minLength");
        else
        {
            bool hasUpper = false, hasLower = false, hasDigit = false, hasSpecial = false;
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                if (c >= 'A' && c <= 'Z') hasUpper = true;
                else if (c >= 'a' && c <= 'z') hasLower = true;
                else if (c >= '0' && c <= '9') hasDigit = true;
                else hasSpecial = true;
            }
            if (requireUppercase && !hasUpper) failed.Add("uppercase");
            if (requireLowercase && !hasLower) failed.Add("lowercase");
            if (requireDigit && !hasDigit) failed.Add("digit");
            if (requireSpecialChar && !hasSpecial) failed.Add("specialChar");
        }
        if (failed.Count > 0)
            context.Add(("failedRequirements", string.Join(", ", failed)));

        return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, context);
    }

    /// <summary>
    /// Ensures the given password meets the specified security requirements, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsSecurePassword(this string? value, int minLength = 8,
        bool requireUppercase = true, bool requireLowercase = true, bool requireDigit = true,
        bool requireSpecialChar = true, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsSecurePassword(minLength, requireUppercase, requireLowercase, requireDigit, requireSpecialChar, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}
