using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The DoesNotContain class provides methods for validation to ensure that
/// a collection or string does not contain a specified item or substring. Includes functionality to check, enforce,
/// and validate instances.
/// </summary>
public static class DoesNotContain
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "DoesNotContain";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not contain the specified item";

    /// <summary>
    /// Checks if the given collection does not contain the specified item.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain<T>(this IEnumerable<T>? collection, T item)
    {
        return !collection.CheckDoesContain(item);
    }

    /// <summary>
    /// Checks if the given string does not contain the specified substring.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain(this string? value, string substring, StringComparison comparison = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(substring);
        return !value.CheckDoesContain(substring, comparison);
    }

    /// <summary>
    /// Validates whether the given collection does not contain the specified item.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesNotContain<T>(this IEnumerable<T>? collection, T item, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        if (!collection.CheckDoesNotContain(item))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", collection), ("item", item)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given string does not contain the specified substring.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesNotContain(this string? value, string substring, StringComparison comparison = StringComparison.Ordinal,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckDoesNotContain(substring, comparison))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("substring", substring)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given collection does not contain the specified item, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T>? EnsureDoesNotContain<T>(this IEnumerable<T>? collection, T item, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        var validationResult = collection.ValidateDoesNotContain(item, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return collection;
    }

    /// <summary>
    /// Ensures the given string does not contain the specified substring, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureDoesNotContain(this string? value, string substring, StringComparison comparison = StringComparison.Ordinal,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateDoesNotContain(substring, comparison, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}
