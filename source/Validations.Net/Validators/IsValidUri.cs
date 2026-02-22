using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsValidUri class provides methods for validation to ensure that
/// a string represents a valid URI. Includes functionality to check, enforce,
/// and validate instances where URI validation is required.
/// </summary>
public static class IsValidUri
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsValidUri";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must be a valid URI";

    /// <summary>
    /// Checks if the given value is a valid URI.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="uriKind">The kind of URI to validate (Absolute, Relative, or RelativeOrAbsolute).</param>
    /// <returns>
    /// True if the value is a valid URI; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidUri(this string? value, UriKind uriKind = UriKind.Absolute)
    {
        return Uri.TryCreate(value, uriKind, out _);
    }

    /// <summary>
    /// Ensures the given value is a valid URI, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidUri(this string? value, UriKind uriKind = UriKind.Absolute, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsValidUri(uriKind, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates whether the given value is a valid URI.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidUri(this string? value, UriKind uriKind = UriKind.Absolute, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsValidUri(uriKind))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}
