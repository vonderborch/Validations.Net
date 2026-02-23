using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsNullOrEmpty class provides methods for validation to ensure that
/// a string or collection is null or empty. Includes functionality to check, enforce,
/// and validate instances where a null or empty value is required.
/// </summary>
public static class IsNullOrEmpty
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNullOrEmpty";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must be null or empty";

    /// <summary>
    /// Checks if the given string is null or empty.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrEmpty(this string? value)
    {
        return string.IsNullOrEmpty(value);
    }

    /// <summary>
    /// Checks if the given non-generic collection is null or empty (non-generic overload for boxed values).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrEmpty(this System.Collections.ICollection? collection)
    {
        return collection is null || collection.Count == 0;
    }

    /// <summary>
    /// Checks if the given non-generic enumerable is null or empty (non-generic overload for boxed values).
    /// </summary>
    public static bool CheckIsNullOrEmpty(this System.Collections.IEnumerable? enumerable)
    {
        if (enumerable is null) return true;
        if (enumerable is System.Collections.ICollection c) return c.Count == 0;
        var enumerator = enumerable.GetEnumerator();
        try { return !enumerator.MoveNext(); }
        finally { (enumerator as IDisposable)?.Dispose(); }
    }

    /// <summary>
    /// Validates whether the given string is null or empty.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNullOrEmpty(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNullOrEmpty())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given collection is null or empty.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNullOrEmpty<T>(this ICollection<T>? collection, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        if (!collection.CheckIsNullOrEmpty())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", collection)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given enumerable is null or empty.
    /// </summary>
    public static ValidationResult ValidateIsNullOrEmpty<T>(this IEnumerable<T>? enumerable, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null)
    {
        if (!enumerable.CheckIsNullOrEmpty())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", enumerable)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given string is null or empty, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNullOrEmpty(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNullOrEmpty(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures the given collection is null or empty, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ICollection<T>? EnsureIsNullOrEmpty<T>(this ICollection<T>? collection, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        var validationResult = collection.ValidateIsNullOrEmpty(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return collection;
    }

    /// <summary>
    /// Ensures the given enumerable is null or empty, throwing an exception if validation fails.
    /// </summary>
    public static IEnumerable<T>? EnsureIsNullOrEmpty<T>(this IEnumerable<T>? enumerable, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null)
    {
        var validationResult = enumerable.ValidateIsNullOrEmpty(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return enumerable;
    }
}

public sealed class NullOrEmptyValidator : IValidator
{
    public static readonly NullOrEmptyValidator Instance = new();
    public string Name => IsNullOrEmpty.ValidatorName;
    public string DefaultFailureMessage => IsNullOrEmpty.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            null => ValidationResult.CreateFromValidationSuccess(),
            string s => s.ValidateIsNullOrEmpty(blackboard, DefaultFailureMessage, memberName),
            System.Collections.IEnumerable e when value is not string => e.Cast<object?>().ValidateIsNullOrEmpty(blackboard, DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
    }
}

public sealed class ValidateIsNullOrEmptyAttribute() : ValidatorAttribute(NullOrEmptyValidator.Instance);
