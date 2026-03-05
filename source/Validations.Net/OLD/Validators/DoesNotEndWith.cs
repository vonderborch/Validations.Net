using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The DoesNotEndWith class provides methods for validation to ensure that
/// a string does not end with a specified suffix. Includes functionality to check, enforce,
/// and validate instances.
/// </summary>
public static class DoesNotEndWith
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "DoesNotEndWith";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not end with the specified suffix";

    /// <summary>
    /// Checks if the given value does not end with the specified suffix.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="suffix">The suffix to check for.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <returns>
    /// True if the value does not end with the suffix; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotEndWith(this string? value, string suffix, StringComparison comparison = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(suffix);
        return value is null || !value.EndsWith(suffix, comparison);
    }

    /// <summary>
    /// Ensures the given value does not end with the specified suffix, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureDoesNotEndWith(this string? value, string suffix, StringComparison comparison = StringComparison.Ordinal,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateDoesNotEndWith(suffix, comparison, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates whether the given value does not end with the specified suffix.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesNotEndWith(this string? value, string suffix, StringComparison comparison = StringComparison.Ordinal,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckDoesNotEndWith(suffix, comparison))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("suffix", suffix)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}

public readonly record struct DoesNotEndWithParams(string Suffix, StringComparison Comparison = StringComparison.Ordinal);

public sealed class DoesNotEndWithValidator : IValidator
{
    public DoesNotEndWithParams Params { get; }

    public DoesNotEndWithValidator(string suffix, StringComparison comparison = StringComparison.Ordinal)
    {
        this.Params = new DoesNotEndWithParams(suffix, comparison);
    }

    public string Name => DoesNotEndWith.ValidatorName;
    public string DefaultFailureMessage => DoesNotEndWith.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is not string s)
            return ValidationResult.CreateFromValidationSuccess();
        if (s.CheckDoesNotEndWith(this.Params.Suffix, this.Params.Comparison))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("suffix", this.Params.Suffix)]);
    }
}

public sealed class ValidateDoesNotEndWithAttribute(string suffix)
    : ValidatorAttribute(new DoesNotEndWithValidator(suffix));
