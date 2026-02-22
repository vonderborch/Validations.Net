using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsLength class provides methods for validation to ensure that
/// a string or collection has a specific length. Includes functionality to check, enforce,
/// and validate instances where length constraints are required.
/// </summary>
public static class IsLength
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsLength";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter length does not match the expected length";

    /// <summary>
    /// Checks if the given string has the exact specified length.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsLength(this string? value, int length)
    {
        return value is not null && value.Length == length;
    }

    /// <summary>
    /// Checks if the given non-generic collection has the specified length (non-generic overload for boxed values).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsLength(this System.Collections.ICollection? collection, int length)
    {
        return collection is not null && collection.Count == length;
    }

    /// <summary>
    /// Checks if the given collection has the exact specified length.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsLength<T>(this ICollection<T>? collection, int length)
    {
        return collection is not null && collection.Count == length;
    }

    /// <summary>
    /// Checks if the given span has the exact specified length.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsLength<T>(ReadOnlySpan<T> span, int length)
    {
        return span.Length == length;
    }

    /// <summary>
    /// Checks if the given string's length satisfies the specified mode.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsLength(this string? value, int length, LengthCheckMode mode)
    {
        if (value is null)
            return false;

        int actualLen = value.Length;
        return SatisfiesLengthMode(actualLen, length, mode);
    }

    /// <summary>
    /// Checks if the given collection's count satisfies the specified mode.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsLength<T>(this ICollection<T>? collection, int length, LengthCheckMode mode)
    {
        if (collection is null)
            return false;

        int actualLen = collection.Count;
        return SatisfiesLengthMode(actualLen, length, mode);
    }

    /// <summary>
    /// Checks if the given string's length is within the specified range.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsLength(this string? value, int minLength, int maxLength)
    {
        return value is not null && value.Length >= minLength && value.Length <= maxLength;
    }

    /// <summary>
    /// Checks if the given collection's count is within the specified range.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsLength<T>(this ICollection<T>? collection, int minLength, int maxLength)
    {
        return collection is not null && collection.Count >= minLength && collection.Count <= maxLength;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool SatisfiesLengthMode(int actualLen, int length, LengthCheckMode mode)
    {
        return mode switch
        {
            LengthCheckMode.ExactLength => actualLen == length,
            LengthCheckMode.LessThan => actualLen < length,
            LengthCheckMode.LessThanOrEqual => actualLen <= length,
            LengthCheckMode.GreaterThan => actualLen > length,
            LengthCheckMode.GreaterThanOrEqual => actualLen >= length,
            _ => false
        };
    }

    /// <summary>
    /// Validates whether the given string has the exact specified length.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsLength(this string? value, int length, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsLength(length))
        {
            int actualLen = value?.Length ?? -1;
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("expectedLength", length), ("actualLength", actualLen)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given collection has the exact specified length.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsLength<T>(this ICollection<T>? collection, int length, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        if (!collection.CheckIsLength(length))
        {
            int actualLen = collection?.Count ?? -1;
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", collection), ("expectedLength", length), ("actualLength", actualLen)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given span has the exact specified length.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsLength<T>(ReadOnlySpan<T> span, int length, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(span))] string? parameterName = null)
    {
        if (!CheckIsLength(span, length))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("expectedLength", length), ("actualLength", span.Length)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given string's length satisfies the specified mode.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsLength(this string? value, int length, LengthCheckMode mode, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsLength(length, mode))
        {
            int actualLen = value?.Length ?? -1;
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("expectedLength", length), ("actualLength", actualLen), ("mode", mode)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given collection's count satisfies the specified mode.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsLength<T>(this ICollection<T>? collection, int length, LengthCheckMode mode, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        if (!collection.CheckIsLength(length, mode))
        {
            int actualLen = collection?.Count ?? -1;
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", collection), ("expectedLength", length), ("actualLength", actualLen), ("mode", mode)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given string's length is within the specified range.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsLength(this string? value, int minLength, int maxLength, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsLength(minLength, maxLength))
        {
            int actualLen = value?.Length ?? -1;
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("minLength", minLength), ("maxLength", maxLength), ("actualLength", actualLen)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given collection's count is within the specified range.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsLength<T>(this ICollection<T>? collection, int minLength, int maxLength, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        if (!collection.CheckIsLength(minLength, maxLength))
        {
            int actualLen = collection?.Count ?? -1;
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", collection), ("minLength", minLength), ("maxLength", maxLength), ("actualLength", actualLen)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given string has the exact specified length, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsLength(this string? value, int length, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsLength(length, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures the given collection has the exact specified length, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ICollection<T>? EnsureIsLength<T>(this ICollection<T>? collection, int length, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        var validationResult = collection.ValidateIsLength(length, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return collection;
    }

    /// <summary>
    /// Ensures the given span has the exact specified length, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<T> EnsureIsLength<T>(ReadOnlySpan<T> span, int length, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(span))] string? parameterName = null)
    {
        var validationResult = ValidateIsLength(span, length, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return span;
    }

    /// <summary>
    /// Ensures the given string's length satisfies the specified mode, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsLength(this string? value, int length, LengthCheckMode mode, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsLength(length, mode, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures the given collection's count satisfies the specified mode, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ICollection<T>? EnsureIsLength<T>(this ICollection<T>? collection, int length, LengthCheckMode mode, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        var validationResult = collection.ValidateIsLength(length, mode, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return collection;
    }

    /// <summary>
    /// Ensures the given string's length is within the specified range, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsLength(this string? value, int minLength, int maxLength, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsLength(minLength, maxLength, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures the given collection's count is within the specified range, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ICollection<T>? EnsureIsLength<T>(this ICollection<T>? collection, int minLength, int maxLength, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        var validationResult = collection.ValidateIsLength(minLength, maxLength, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return collection;
    }
}
