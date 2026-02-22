using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsSingle class provides methods for validation to ensure that
/// a string or collection contains exactly one element. Includes functionality to check, enforce,
/// and validate instances where exactly one element is required.
/// </summary>
public static class IsSingle
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsSingle";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must contain exactly one element";

    /// <summary>
    /// Checks if the given string contains exactly one character.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsSingle(this string? value)
    {
        return value is not null && value.Length == 1;
    }

    /// <summary>
    /// Checks if the given collection contains exactly one element.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsSingle<T>(this ICollection<T>? collection)
    {
        return collection is not null && collection.Count == 1;
    }

    /// <summary>
    /// Checks if the given enumerable contains exactly one element (LINQ-free).
    /// </summary>
    public static bool CheckIsSingle<T>(this IEnumerable<T>? enumerable)
    {
        if (enumerable is null)
            return false;

        using var enumerator = enumerable.GetEnumerator();
        if (!enumerator.MoveNext())
            return false;

        return !enumerator.MoveNext();
    }

    /// <summary>
    /// Validates whether the given string contains exactly one character.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsSingle(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsSingle())
        {
            int actualCount = value?.Length ?? -1;
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("actualCount", actualCount)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given collection contains exactly one element.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsSingle<T>(this ICollection<T>? collection, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        if (!collection.CheckIsSingle())
        {
            int actualCount = collection?.Count ?? -1;
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", collection), ("actualCount", actualCount)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given enumerable contains exactly one element.
    /// </summary>
    public static ValidationResult ValidateIsSingle<T>(this IEnumerable<T>? enumerable, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null)
    {
        if (!enumerable.CheckIsSingle())
        {
            int actualCount = enumerable is ICollection<T> c ? c.Count : CountEnumerable(enumerable);
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", enumerable), ("actualCount", actualCount)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given string contains exactly one character, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsSingle(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsSingle(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures the given collection contains exactly one element, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ICollection<T>? EnsureIsSingle<T>(this ICollection<T>? collection, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        var validationResult = collection.ValidateIsSingle(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return collection;
    }

    /// <summary>
    /// Ensures the given enumerable contains exactly one element, throwing an exception if validation fails.
    /// </summary>
    public static IEnumerable<T>? EnsureIsSingle<T>(this IEnumerable<T>? enumerable, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null)
    {
        var validationResult = enumerable.ValidateIsSingle(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return enumerable;
    }

    private static int CountEnumerable<T>(IEnumerable<T>? enumerable)
    {
        if (enumerable is null)
            return -1;

        if (enumerable is ICollection<T> collection)
            return collection.Count;

        int count = 0;
        foreach (var _ in enumerable)
        {
            count++;
        }

        return count;
    }
}
