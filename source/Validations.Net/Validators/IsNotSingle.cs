using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsNotSingle class provides methods for validation to ensure that
/// a string or collection does not contain exactly one element. Includes functionality to check, enforce,
/// and validate instances where exactly one element must not be present.
/// </summary>
public static class IsNotSingle
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotSingle";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not contain exactly one element";

    /// <summary>
    /// Checks if the given string does not contain exactly one character.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotSingle(this string? value)
    {
        return value is null || value.Length != 1;
    }

    /// <summary>
    /// Checks if the given collection does not contain exactly one element.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotSingle<T>(this ICollection<T>? collection)
    {
        return collection is null || collection.Count != 1;
    }

    /// <summary>
    /// Checks if the given enumerable does not contain exactly one element.
    /// </summary>
    public static bool CheckIsNotSingle<T>(this IEnumerable<T>? enumerable)
    {
        return !enumerable.CheckIsSingle();
    }

    /// <summary>
    /// Validates whether the given string does not contain exactly one character.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotSingle(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNotSingle())
        {
            int actualCount = value?.Length ?? -1;
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("actualCount", actualCount)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given collection does not contain exactly one element.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotSingle<T>(this ICollection<T>? collection, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        if (!collection.CheckIsNotSingle())
        {
            int actualCount = collection?.Count ?? -1;
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", collection), ("actualCount", actualCount)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given enumerable does not contain exactly one element.
    /// </summary>
    public static ValidationResult ValidateIsNotSingle<T>(this IEnumerable<T>? enumerable, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null)
    {
        if (!enumerable.CheckIsNotSingle())
        {
            int actualCount = enumerable is ICollection<T> c ? c.Count : CountEnumerable(enumerable);
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", enumerable), ("actualCount", actualCount)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given string does not contain exactly one character, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotSingle(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNotSingle(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures the given collection does not contain exactly one element, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ICollection<T>? EnsureIsNotSingle<T>(this ICollection<T>? collection, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        var validationResult = collection.ValidateIsNotSingle(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return collection;
    }

    /// <summary>
    /// Ensures the given enumerable does not contain exactly one element, throwing an exception if validation fails.
    /// </summary>
    public static IEnumerable<T>? EnsureIsNotSingle<T>(this IEnumerable<T>? enumerable, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null)
    {
        var validationResult = enumerable.ValidateIsNotSingle(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return enumerable;
    }

    private static int CountEnumerable<T>(IEnumerable<T> enumerable)
    {
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
