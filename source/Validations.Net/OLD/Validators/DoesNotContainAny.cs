using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The DoesNotContainAny class provides methods for validation to ensure that
/// a collection or string does not contain any of the specified items or substrings. Includes functionality to check, enforce,
/// and validate instances.
/// </summary>
public static class DoesNotContainAny
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "DoesNotContainAny";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not contain any of the specified items";

    /// <summary>
    /// Checks if the given collection does not contain any of the specified candidates.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this IEnumerable<T>? collection, IEnumerable<T> candidates)
    {
        return !collection.CheckDoesContainAny(candidates);
    }

    /// <summary>
    /// Checks if the given string does not contain any of the specified substrings.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny(this string? value, IEnumerable<string> substrings, StringComparison comparison = StringComparison.Ordinal)
    {
        return !value.CheckDoesContainAny(substrings, comparison);
    }

    /// <summary>
    /// Validates whether the given collection does not contain any of the specified candidates.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesNotContainAny<T>(this IEnumerable<T>? collection, IEnumerable<T> candidates,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        if (!collection.CheckDoesNotContainAny(candidates))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", collection), ("candidates", candidates)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given string does not contain any of the specified substrings.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesNotContainAny(this string? value, IEnumerable<string> substrings,
        StringComparison comparison = StringComparison.Ordinal, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckDoesNotContainAny(substrings, comparison))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("substrings", substrings)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given collection does not contain any of the specified candidates, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T>? EnsureDoesNotContainAny<T>(this IEnumerable<T>? collection, IEnumerable<T> candidates,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        var validationResult = collection.ValidateDoesNotContainAny(candidates, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return collection;
    }

    /// <summary>
    /// Ensures the given string does not contain any of the specified substrings, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureDoesNotContainAny(this string? value, IEnumerable<string> substrings,
        StringComparison comparison = StringComparison.Ordinal, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateDoesNotContainAny(substrings, comparison, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class DoesNotContainAnyValidator : IValidator
{
    public string[] Substrings { get; }

    public DoesNotContainAnyValidator(params string[] substrings)
    {
        this.Substrings = substrings;
    }

    public string Name => DoesNotContainAny.ValidatorName;
    public string DefaultFailureMessage => DoesNotContainAny.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string str && str.CheckDoesNotContainAny(this.Substrings))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("substrings", this.Substrings)]);
    }
}

public sealed class ValidateDoesNotContainAnyAttribute(params string[] values)
    : ValidatorAttribute(new DoesNotContainAnyValidator(values));
