using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The EndsWith class provides methods for validation to ensure that
/// a string ends with a specified suffix. Includes functionality to check, enforce,
/// and validate instances.
/// </summary>
public static class EndsWith
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "EndsWith";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must end with the specified suffix";

    /// <summary>
    /// Checks if the given value ends with the specified suffix.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="suffix">The suffix to check for.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <returns>
    /// True if the value ends with the suffix; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckEndsWith(this string? value, string suffix, StringComparison comparison = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(suffix);
        return value is not null && value.EndsWith(suffix, comparison);
    }

    /// <summary>
    /// Ensures the given value ends with the specified suffix, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureEndsWith(this string? value, string suffix, StringComparison comparison = StringComparison.Ordinal,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateEndsWith(suffix, comparison, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates whether the given value ends with the specified suffix.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateEndsWith(this string? value, string suffix, StringComparison comparison = StringComparison.Ordinal,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckEndsWith(suffix, comparison))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("suffix", suffix)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}

public readonly record struct EndsWithParams(string Suffix, StringComparison Comparison = StringComparison.Ordinal);

public sealed class EndsWithValidator : IValidator
{
    public EndsWithParams Params { get; }

    public EndsWithValidator(string suffix, StringComparison comparison = StringComparison.Ordinal)
    {
        this.Params = new EndsWithParams(suffix, comparison);
    }

    public string Name => EndsWith.ValidatorName;
    public string DefaultFailureMessage => EndsWith.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s && s.CheckEndsWith(this.Params.Suffix, this.Params.Comparison))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("suffix", this.Params.Suffix)]);
    }
}

public sealed class ValidateEndsWithAttribute(string suffix)
    : ValidatorAttribute(new EndsWithValidator(suffix));
