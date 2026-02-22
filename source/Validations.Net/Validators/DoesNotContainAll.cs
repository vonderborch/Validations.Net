using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The DoesNotContainAll class provides methods for validation to ensure that
/// a collection or string does not contain all of the specified items or substrings. Includes functionality to check, enforce,
/// and validate instances.
/// </summary>
public static class DoesNotContainAll
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "DoesNotContainAll";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not contain all of the specified items";

    /// <summary>
    /// Checks if the given collection does not contain all of the specified required items.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this IEnumerable<T>? collection, IEnumerable<T> required)
    {
        return !collection.CheckDoesContainAll(required);
    }

    /// <summary>
    /// Checks if the given string does not contain all of the specified substrings.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this string? value, IEnumerable<string> substrings, StringComparison comparison = StringComparison.Ordinal)
    {
        return !value.CheckDoesContainAll(substrings, comparison);
    }

    /// <summary>
    /// Validates whether the given collection does not contain all of the specified required items.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesNotContainAll<T>(this IEnumerable<T>? collection, IEnumerable<T> required,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        if (!collection.CheckDoesNotContainAll(required))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", collection), ("required", required)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given string does not contain all of the specified substrings.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesNotContainAll(this string? value, IEnumerable<string> substrings,
        StringComparison comparison = StringComparison.Ordinal, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckDoesNotContainAll(substrings, comparison))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("substrings", substrings)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given collection does not contain all of the specified required items, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T>? EnsureDoesNotContainAll<T>(this IEnumerable<T>? collection, IEnumerable<T> required,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        var validationResult = collection.ValidateDoesNotContainAll(required, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return collection;
    }

    /// <summary>
    /// Ensures the given string does not contain all of the specified substrings, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureDoesNotContainAll(this string? value, IEnumerable<string> substrings,
        StringComparison comparison = StringComparison.Ordinal, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateDoesNotContainAll(substrings, comparison, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}
