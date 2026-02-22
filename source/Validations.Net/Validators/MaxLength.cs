using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The MaxLength class provides methods for validation to ensure that
/// a string does not exceed a specified maximum length. Includes functionality to check, enforce,
/// and validate instances.
/// </summary>
public static class MaxLength
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "MaxLength";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter length must not exceed the maximum";

    /// <summary>
    /// Checks if the given value does not exceed the specified maximum length.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="maxLength">The maximum length allowed.</param>
    /// <returns>
    /// True if the value length is at most maxLength; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckMaxLength(this string? value, int maxLength)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(maxLength);
        return value is not null && value.Length <= maxLength;
    }

    /// <summary>
    /// Ensures the given value does not exceed the specified maximum length, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureMaxLength(this string? value, int maxLength, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateMaxLength(maxLength, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates whether the given value does not exceed the specified maximum length.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateMaxLength(this string? value, int maxLength, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckMaxLength(maxLength))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("maxLength", maxLength), ("actualLength", value?.Length)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}
