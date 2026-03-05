using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The IsNotNullOrEmpty class provides methods for validation to ensure that
/// a string or collection is not null and not empty. Includes functionality to check, enforce,
/// and validate instances where a non-null, non-empty value is required.
/// </summary>
public static class IsNotNullOrEmpty
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotNullOrEmpty";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not be null or empty";

    /// <summary>
    /// Checks if the given string is not null and not empty.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrEmpty(this string? value)
    {
        return !string.IsNullOrEmpty(value);
    }

    /// <summary>
    /// Checks if the given non-generic collection is not null and not empty (non-generic overload for boxed values).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrEmpty(this System.Collections.ICollection? collection)
    {
        return collection is not null && collection.Count != 0;
    }

    /// <summary>
    /// Checks if the given non-generic enumerable is not null and not empty (non-generic overload for boxed values).
    /// </summary>
    public static bool CheckIsNotNullOrEmpty(this System.Collections.IEnumerable? enumerable)
    {
        if (enumerable is null) return false;
        if (enumerable is System.Collections.ICollection c) return c.Count != 0;
        var enumerator = enumerable.GetEnumerator();
        try { return enumerator.MoveNext(); }
        finally { (enumerator as IDisposable)?.Dispose(); }
    }

    /// <summary>
    /// Checks if the given collection is not null and not empty.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrEmpty<T>(this ICollection<T>? collection)
    {
        return collection is not null && collection.Count != 0;
    }

    /// <summary>
    /// Checks if the given enumerable is not null and not empty (no LINQ).
    /// </summary>
    public static bool CheckIsNotNullOrEmpty<T>(this IEnumerable<T>? enumerable)
    {
        if (enumerable is null)
            return false;

        using var enumerator = enumerable.GetEnumerator();
        return enumerator.MoveNext();
    }

    /// <summary>
    /// Validates whether the given string is not null and not empty.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotNullOrEmpty(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNotNullOrEmpty())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given collection is not null and not empty.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotNullOrEmpty<T>(this ICollection<T>? collection, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        if (!collection.CheckIsNotNullOrEmpty())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", collection)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given enumerable is not null and not empty.
    /// </summary>
    public static ValidationResult ValidateIsNotNullOrEmpty<T>(this IEnumerable<T>? enumerable, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null)
    {
        if (!enumerable.CheckIsNotNullOrEmpty())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", enumerable)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given string is not null and not empty, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string EnsureIsNotNullOrEmpty(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNotNullOrEmpty(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value!;
    }

    /// <summary>
    /// Ensures the given collection is not null and not empty, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ICollection<T> EnsureIsNotNullOrEmpty<T>(this ICollection<T>? collection, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        var validationResult = collection.ValidateIsNotNullOrEmpty(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return collection!;
    }

    /// <summary>
    /// Ensures the given enumerable is not null and not empty, throwing an exception if validation fails.
    /// </summary>
    public static IEnumerable<T> EnsureIsNotNullOrEmpty<T>(this IEnumerable<T>? enumerable, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null)
    {
        var validationResult = enumerable.ValidateIsNotNullOrEmpty(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return enumerable!;
    }
}

public sealed class NotNullOrEmptyValidator : IValidator
{
    public static readonly NotNullOrEmptyValidator Instance = new();
    public string Name => IsNotNullOrEmpty.ValidatorName;
    public string DefaultFailureMessage => IsNotNullOrEmpty.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            string s => s.ValidateIsNotNullOrEmpty(blackboard, this.DefaultFailureMessage, memberName),
            System.Collections.IEnumerable e when value is not string => e.Cast<object?>().ValidateIsNotNullOrEmpty(blackboard, this.DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
    }
}

public sealed class ValidateIsNotNullOrEmptyAttribute() : ValidatorAttribute(NotNullOrEmptyValidator.Instance);
