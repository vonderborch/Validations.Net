using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The IsNotMatch class provides methods for validation to ensure that
/// a string does not match a regular expression pattern. Includes functionality to check, enforce,
/// and validate instances where pattern non-matching is required.
/// </summary>
public static class IsNotMatch
{
    private static readonly TimeSpan RegexMatchTimeout = TimeSpan.FromSeconds(1);
    private static readonly ConcurrentDictionary<string, Regex> RegexCache = new();

    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotMatch";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not match the pattern";

    /// <summary>
    /// Checks if the given value does not match the specified regex.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="regex">The regular expression to match against.</param>
    /// <returns>
    /// True if the value does not match the pattern; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotMatch(this string? value, Regex regex)
    {
        ArgumentNullException.ThrowIfNull(regex);
        return value is null || !regex.IsMatch(value);
    }

    /// <summary>
    /// Checks if the given value does not match the specified pattern.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="pattern">The regular expression pattern to match against.</param>
    /// <returns>
    /// True if the value does not match the pattern; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotMatch(this string? value, string pattern)
    {
        ArgumentNullException.ThrowIfNull(pattern);
        var regex = RegexCache.GetOrAdd(pattern, static p => new Regex(p, RegexOptions.None, RegexMatchTimeout));
        return value is null || !regex.IsMatch(value);
    }

    /// <summary>
    /// Ensures the given value does not match the specified regex, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotMatch(this string? value, Regex regex, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNotMatch(regex, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures the given value does not match the specified pattern, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotMatch(this string? value, string pattern, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNotMatch(pattern, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates whether the given value does not match the specified regex.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotMatch(this string? value, Regex regex, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNotMatch(regex))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("pattern", regex.ToString())]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given value does not match the specified pattern.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotMatch(this string? value, string pattern, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNotMatch(pattern))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("pattern", pattern)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}

public readonly record struct NotMatchParams(string Pattern);

public sealed class NotMatchValidator : IValidator
{
    public NotMatchParams Params { get; }

    public NotMatchValidator(string pattern)
    {
        this.Params = new NotMatchParams(pattern);
    }

    public string Name => IsNotMatch.ValidatorName;
    public string DefaultFailureMessage => IsNotMatch.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is not string s)
            return ValidationResult.CreateFromValidationSuccess();
        if (s.CheckIsNotMatch(this.Params.Pattern))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("pattern", this.Params.Pattern)]);
    }
}

public sealed class ValidateIsNotMatchAttribute(string pattern)
    : ValidatorAttribute(new NotMatchValidator(pattern));
