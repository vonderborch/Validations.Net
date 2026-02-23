using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsEmpty class provides methods for validation to ensure that
/// a string or collection is empty. Includes functionality to check, enforce,
/// and validate instances where an empty value is required.
/// </summary>
public static class IsEmpty
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsEmpty";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must be empty";

    /// <summary>
    /// Checks if the given string is empty.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEmpty(this string? value)
    {
        return value is not null && value.Length == 0;
    }

    /// <summary>
    /// Checks if the given non-generic collection is empty (non-generic overload for boxed values).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEmpty(this System.Collections.ICollection? collection)
    {
        return collection is not null && collection.Count == 0;
    }

    /// <summary>
    /// Checks if the given non-generic enumerable is empty (non-generic overload for boxed values).
    /// </summary>
    public static bool CheckIsEmpty(this System.Collections.IEnumerable? enumerable)
    {
        if (enumerable is null) return false;
        if (enumerable is System.Collections.ICollection c) return c.Count == 0;
        var enumerator = enumerable.GetEnumerator();
        try { return !enumerator.MoveNext(); }
        finally { (enumerator as IDisposable)?.Dispose(); }
    }

    /// <summary>
    /// Checks if the given span is empty.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEmpty<T>(ReadOnlySpan<T> span)
    {
        return span.Length == 0;
    }

    /// <summary>
    /// Validates whether the given string is empty.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsEmpty(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsEmpty())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given collection is empty.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsEmpty<T>(this ICollection<T>? collection, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        if (!collection.CheckIsEmpty())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", collection)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given enumerable is empty.
    /// </summary>
    public static ValidationResult ValidateIsEmpty<T>(this IEnumerable<T>? enumerable, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null)
    {
        if (!enumerable.CheckIsEmpty())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", enumerable)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given span is empty.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsEmpty<T>(ReadOnlySpan<T> span, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(span))] string? parameterName = null)
    {
        if (!CheckIsEmpty(span))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", span.ToArray())]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given string is empty, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsEmpty(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsEmpty(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures the given collection is empty, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ICollection<T>? EnsureIsEmpty<T>(this ICollection<T>? collection, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        var validationResult = collection.ValidateIsEmpty(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return collection;
    }

    /// <summary>
    /// Ensures the given enumerable is empty, throwing an exception if validation fails.
    /// </summary>
    public static IEnumerable<T>? EnsureIsEmpty<T>(this IEnumerable<T>? enumerable, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null)
    {
        var validationResult = enumerable.ValidateIsEmpty(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return enumerable;
    }

    /// <summary>
    /// Ensures the given span is empty, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<T> EnsureIsEmpty<T>(ReadOnlySpan<T> span, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(span))] string? parameterName = null)
    {
        var validationResult = ValidateIsEmpty(span, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return span;
    }
}

public sealed class EmptyValidator : IValidator
{
    public static readonly EmptyValidator Instance = new();
    public string Name => IsEmpty.ValidatorName;
    public string DefaultFailureMessage => IsEmpty.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            string s => s.ValidateIsEmpty(blackboard, DefaultFailureMessage, memberName),
            System.Collections.IEnumerable e when value is not string => e.Cast<object?>().ValidateIsEmpty(blackboard, DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
    }
}

public sealed class ValidateIsEmptyAttribute() : ValidatorAttribute(EmptyValidator.Instance);
