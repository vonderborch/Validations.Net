using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The DoesContainAny class provides methods for validation to ensure that
/// a collection or string contains at least one of the specified items or substrings. Includes functionality to check, enforce,
/// and validate instances.
/// </summary>
public static class DoesContainAny
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "DoesContainAny";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must contain at least one of the specified items";

    /// <summary>
    /// Checks if the given collection contains at least one of the specified candidates.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny<T>(this IEnumerable<T>? collection, IEnumerable<T> candidates)
    {
        ArgumentNullException.ThrowIfNull(candidates);
        if (collection is null)
            return false;

        var candidateSet = candidates as HashSet<T> ?? new HashSet<T>(candidates);
        foreach (var element in collection)
        {
            if (candidateSet.Contains(element))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if the given string contains at least one of the specified substrings.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny(this string? value, IEnumerable<string> substrings, StringComparison comparison = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(substrings);
        if (value is null)
            return false;

        foreach (var substring in substrings)
        {
            if (value.Contains(substring, comparison))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Validates whether the given collection contains at least one of the specified candidates.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesContainAny<T>(this IEnumerable<T>? collection, IEnumerable<T> candidates,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        if (!collection.CheckDoesContainAny(candidates))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", collection), ("candidates", candidates)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given string contains at least one of the specified substrings.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesContainAny(this string? value, IEnumerable<string> substrings,
        StringComparison comparison = StringComparison.Ordinal, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckDoesContainAny(substrings, comparison))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("substrings", substrings)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given collection contains at least one of the specified candidates, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T>? EnsureDoesContainAny<T>(this IEnumerable<T>? collection, IEnumerable<T> candidates,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        var validationResult = collection.ValidateDoesContainAny(candidates, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return collection;
    }

    /// <summary>
    /// Ensures the given string contains at least one of the specified substrings, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureDoesContainAny(this string? value, IEnumerable<string> substrings,
        StringComparison comparison = StringComparison.Ordinal, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateDoesContainAny(substrings, comparison, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class DoesContainAnyValidator : IValidator
{
    public string[] Substrings { get; }

    public DoesContainAnyValidator(params string[] substrings)
    {
        this.Substrings = substrings;
    }

    public string Name => DoesContainAny.ValidatorName;
    public string DefaultFailureMessage => DoesContainAny.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string str && str.CheckDoesContainAny(this.Substrings))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("substrings", this.Substrings)]);
    }
}

public sealed class ValidateDoesContainAnyAttribute(params string[] values)
    : ValidatorAttribute(new DoesContainAnyValidator(values));
