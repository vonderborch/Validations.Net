using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The IsNotCount class provides methods for validation to ensure that
/// a collection or enumerable does not have a specific count. Includes functionality to check, enforce,
/// and validate instances where count must not match.
/// </summary>
public static class IsNotCount
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotCount";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter count must not match";

    /// <summary>
    /// Checks if the given non-generic enumerable does not have the exact specified count (non-generic overload for boxed values).
    /// </summary>
    public static bool CheckIsNotCount(this System.Collections.IEnumerable? enumerable, int count)
    {
        if (enumerable is null) return true;
        if (enumerable is System.Collections.ICollection c) return c.Count != count;
        int actual = 0;
        var enumerator = enumerable.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                actual++;
                if (actual > count) return true;
            }
            return actual != count;
        }
        finally { (enumerator as IDisposable)?.Dispose(); }
    }

    /// <summary>
    /// Checks if the given collection does not have the exact specified count.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotCount<T>(this ICollection<T>? collection, int count)
    {
        return collection is null || collection.Count != count;
    }

    /// <summary>
    /// Checks if the given enumerable does not have the exact specified count (LINQ-free).
    /// </summary>
    public static bool CheckIsNotCount<T>(this IEnumerable<T>? enumerable, int count)
    {
        return !enumerable.CheckIsCount(count);
    }

    /// <summary>
    /// Validates whether the given collection does not have the exact specified count.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotCount<T>(this ICollection<T>? collection, int count, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        if (!collection.CheckIsNotCount(count))
        {
            int actualCount = collection?.Count ?? -1;
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", collection), ("expectedCount", count), ("actualCount", actualCount)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given enumerable does not have the exact specified count.
    /// </summary>
    public static ValidationResult ValidateIsNotCount<T>(this IEnumerable<T>? enumerable, int count, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null)
    {
        if (!enumerable.CheckIsNotCount(count))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", enumerable), ("disallowedCount", count), ("actualCount", count)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given collection does not have the exact specified count, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ICollection<T>? EnsureIsNotCount<T>(this ICollection<T>? collection, int count, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        var validationResult = collection.ValidateIsNotCount(count, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return collection;
    }

    /// <summary>
    /// Ensures the given enumerable does not have the exact specified count, throwing an exception if validation fails.
    /// </summary>
    public static IEnumerable<T>? EnsureIsNotCount<T>(this IEnumerable<T>? enumerable, int count, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null)
    {
        var validationResult = enumerable.ValidateIsNotCount(count, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return enumerable;
    }

}

public readonly record struct NotCountParams(int Count);

public sealed class NotCountValidator : IValidator
{
    public NotCountParams Params { get; }

    public NotCountValidator(int count)
    {
        this.Params = new NotCountParams(count);
    }

    public string Name => IsNotCount.ValidatorName;
    public string DefaultFailureMessage => IsNotCount.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is null)
            return ValidationResult.CreateFromValidationSuccess();
        if (value is System.Collections.IEnumerable enumerable && enumerable.CheckIsNotCount(this.Params.Count))
            return ValidationResult.CreateFromValidationSuccess();
        int actualCount = value switch
        {
            System.Collections.ICollection c => c.Count,
            System.Collections.IEnumerable e => CountEnumerable(e),
            _ => -1
        };
        return ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("expectedCount", this.Params.Count), ("actualCount", actualCount)]);
    }

    private static int CountEnumerable(System.Collections.IEnumerable enumerable)
    {
        if (enumerable is System.Collections.ICollection c) return c.Count;
        int count = 0;
        var enumerator = enumerable.GetEnumerator();
        try
        {
            while (enumerator.MoveNext()) count++;
            return count;
        }
        finally { (enumerator as IDisposable)?.Dispose(); }
    }
}

public sealed class ValidateIsNotCountAttribute(int count)
    : ValidatorAttribute(new NotCountValidator(count));
