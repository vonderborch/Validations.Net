using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The IsMatch class provides methods for validation to ensure that
/// a string matches a regular expression pattern. Includes functionality to check, enforce,
/// and validate instances where pattern matching is required.
/// </summary>
public static class IsMatch
{
    private static readonly TimeSpan RegexMatchTimeout = TimeSpan.FromSeconds(1);
    private static readonly ConcurrentDictionary<string, Regex> RegexCache = new();

    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsMatch";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must match the pattern";

    /// <summary>
    /// Checks if the given value matches the specified regex.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="regex">The regular expression to match against.</param>
    /// <returns>
    /// True if the value matches the pattern; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsMatch(this string? value, Regex regex)
    {
        ArgumentNullException.ThrowIfNull(regex);
        return value is not null && regex.IsMatch(value);
    }

    /// <summary>
    /// Checks if the given value matches the specified pattern.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="pattern">The regular expression pattern to match against.</param>
    /// <returns>
    /// True if the value matches the pattern; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsMatch(this string? value, string pattern)
    {
        ArgumentNullException.ThrowIfNull(pattern);
        var regex = RegexCache.GetOrAdd(pattern, static p => new Regex(p, RegexOptions.None, RegexMatchTimeout));
        return value is not null && regex.IsMatch(value);
    }

    /// <summary>
    /// Ensures the given value matches the specified regex, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsMatch(this string? value, Regex regex, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsMatch(regex, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures the given value matches the specified pattern, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsMatch(this string? value, string pattern, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsMatch(pattern, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates whether the given value matches the specified regex.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsMatch(this string? value, Regex regex, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsMatch(regex))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("pattern", regex.ToString())]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given value matches the specified pattern.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsMatch(this string? value, string pattern, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsMatch(pattern))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("pattern", pattern)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}

public readonly record struct IsMatchParams(string Pattern);

public sealed class IsMatchValidator : IValidator
{
    public IsMatchParams Params { get; }

    public IsMatchValidator(string pattern)
    {
        this.Params = new IsMatchParams(pattern);
    }

    public string Name => IsMatch.ValidatorName;
    public string DefaultFailureMessage => IsMatch.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s && s.CheckIsMatch(this.Params.Pattern))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("pattern", this.Params.Pattern)]);
    }
}

public sealed class ValidateIsMatchAttribute(string pattern)
    : ValidatorAttribute(new IsMatchValidator(pattern));
