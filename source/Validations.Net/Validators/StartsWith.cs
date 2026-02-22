using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The StartsWith class provides methods for validation to ensure that
/// a string starts with a specified prefix. Includes functionality to check, enforce,
/// and validate instances.
/// </summary>
public static class StartsWith
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "StartsWith";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must start with the specified prefix";

    /// <summary>
    /// Checks if the given value starts with the specified prefix.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="prefix">The prefix to check for.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <returns>
    /// True if the value starts with the prefix; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckStartsWith(this string? value, string prefix, StringComparison comparison = StringComparison.Ordinal)
    {
        return value is not null && value.StartsWith(prefix, comparison);
    }

    /// <summary>
    /// Ensures the given value starts with the specified prefix, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureStartsWith(this string? value, string prefix, StringComparison comparison = StringComparison.Ordinal,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateStartsWith(prefix, comparison, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates whether the given value starts with the specified prefix.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateStartsWith(this string? value, string prefix, StringComparison comparison = StringComparison.Ordinal,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckStartsWith(prefix, comparison))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("prefix", prefix)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}
