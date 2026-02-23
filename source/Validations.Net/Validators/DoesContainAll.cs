using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The DoesContainAll class provides methods for validation to ensure that
/// a collection or string contains all of the specified items or substrings. Includes functionality to check, enforce,
/// and validate instances.
/// </summary>
public static class DoesContainAll
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "DoesContainAll";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must contain all of the specified items";

    /// <summary>
    /// Checks if the given collection contains all of the specified required items.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll<T>(this IEnumerable<T>? collection, IEnumerable<T> required)
    {
        ArgumentNullException.ThrowIfNull(required);
        if (collection is null)
            return false;

        var collectionSet = collection as HashSet<T> ?? new HashSet<T>(collection);
        foreach (var item in required)
        {
            if (!collectionSet.Contains(item))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Checks if the given string contains all of the specified substrings.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll(this string? value, IEnumerable<string> substrings, StringComparison comparison = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(substrings);
        if (value is null)
            return false;

        foreach (var substring in substrings)
        {
            if (!value.Contains(substring, comparison))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Validates whether the given collection contains all of the specified required items.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesContainAll<T>(this IEnumerable<T>? collection, IEnumerable<T> required,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        if (!collection.CheckDoesContainAll(required))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", collection), ("required", required)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given string contains all of the specified substrings.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesContainAll(this string? value, IEnumerable<string> substrings,
        StringComparison comparison = StringComparison.Ordinal, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckDoesContainAll(substrings, comparison))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("substrings", substrings)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given collection contains all of the specified required items, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T>? EnsureDoesContainAll<T>(this IEnumerable<T>? collection, IEnumerable<T> required,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        var validationResult = collection.ValidateDoesContainAll(required, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return collection;
    }

    /// <summary>
    /// Ensures the given string contains all of the specified substrings, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureDoesContainAll(this string? value, IEnumerable<string> substrings,
        StringComparison comparison = StringComparison.Ordinal, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateDoesContainAll(substrings, comparison, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class DoesContainAllValidator : IValidator
{
    public string[] Substrings { get; }

    public DoesContainAllValidator(params string[] substrings)
    {
        Substrings = substrings;
    }

    public string Name => DoesContainAll.ValidatorName;
    public string DefaultFailureMessage => DoesContainAll.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string str && str.CheckDoesContainAll(Substrings))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("substrings", Substrings)]);
    }
}

public sealed class ValidateDoesContainAllAttribute(params string[] values)
    : ValidatorAttribute(new DoesContainAllValidator(values));
