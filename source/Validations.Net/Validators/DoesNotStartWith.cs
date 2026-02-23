using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The DoesNotStartWith class provides methods for validation to ensure that
/// a string does not start with a specified prefix. Includes functionality to check, enforce,
/// and validate instances.
/// </summary>
public static class DoesNotStartWith
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "DoesNotStartWith";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not start with the specified prefix";

    /// <summary>
    /// Checks if the given value does not start with the specified prefix.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="prefix">The prefix to check for.</param>
    /// <param name="comparison">The string comparison to use.</param>
    /// <returns>
    /// True if the value does not start with the prefix; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotStartWith(this string? value, string prefix, StringComparison comparison = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(prefix);
        return value is null || !value.StartsWith(prefix, comparison);
    }

    /// <summary>
    /// Ensures the given value does not start with the specified prefix, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureDoesNotStartWith(this string? value, string prefix, StringComparison comparison = StringComparison.Ordinal,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateDoesNotStartWith(prefix, comparison, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates whether the given value does not start with the specified prefix.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesNotStartWith(this string? value, string prefix, StringComparison comparison = StringComparison.Ordinal,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckDoesNotStartWith(prefix, comparison))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("prefix", prefix)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}

public readonly record struct DoesNotStartWithParams(string Prefix, StringComparison Comparison = StringComparison.Ordinal);

public sealed class DoesNotStartWithValidator : IValidator
{
    public DoesNotStartWithParams Params { get; }

    public DoesNotStartWithValidator(string prefix, StringComparison comparison = StringComparison.Ordinal)
    {
        Params = new DoesNotStartWithParams(prefix, comparison);
    }

    public string Name => DoesNotStartWith.ValidatorName;
    public string DefaultFailureMessage => DoesNotStartWith.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is not string s)
            return ValidationResult.CreateFromValidationSuccess();
        if (s.CheckDoesNotStartWith(Params.Prefix, Params.Comparison))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("prefix", Params.Prefix)]);
    }
}

public sealed class ValidateDoesNotStartWithAttribute(string prefix)
    : ValidatorAttribute(new DoesNotStartWithValidator(prefix));
