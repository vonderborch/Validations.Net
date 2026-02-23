using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsCount class provides methods for validation to ensure that
/// a collection or enumerable has a specific count. Includes functionality to check, enforce,
/// and validate instances where count constraints are required.
/// </summary>
public static class IsCount
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsCount";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter count does not match the expected count";

    /// <summary>
    /// Checks if the given non-generic enumerable has the exact specified count (non-generic overload for boxed values).
    /// </summary>
    public static bool CheckIsCount(this System.Collections.IEnumerable? enumerable, int count)
    {
        if (enumerable is null) return false;
        if (enumerable is System.Collections.ICollection c) return c.Count == count;
        int actual = 0;
        var enumerator = enumerable.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                actual++;
                if (actual > count) return false;
            }
            return actual == count;
        }
        finally { (enumerator as IDisposable)?.Dispose(); }
    }

    /// <summary>
    /// Checks if the given collection has the exact specified count.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsCount<T>(this ICollection<T>? collection, int count)
    {
        return collection is not null && collection.Count == count;
    }

    /// <summary>
    /// Checks if the given enumerable has the exact specified count (LINQ-free, short-circuits at count+1).
    /// </summary>
    public static bool CheckIsCount<T>(this IEnumerable<T>? enumerable, int count)
    {
        if (enumerable is null)
            return false;

        int current = 0;
        using var enumerator = enumerable.GetEnumerator();
        while (enumerator.MoveNext())
        {
            current++;
            if (current > count)
                return false;
        }

        return current == count;
    }

    /// <summary>
    /// Validates whether the given collection has the exact specified count.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsCount<T>(this ICollection<T>? collection, int count, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        if (!collection.CheckIsCount(count))
        {
            int actualCount = collection?.Count ?? -1;
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", collection), ("expectedCount", count), ("actualCount", actualCount)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given enumerable has the exact specified count.
    /// </summary>
    public static ValidationResult ValidateIsCount<T>(this IEnumerable<T>? enumerable, int count, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null)
    {
        if (enumerable is null)
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", enumerable), ("expectedCount", count), ("actualCount", -1)]);
        }

        if (enumerable is ICollection<T> collection)
        {
            if (collection.Count != count)
            {
                return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                    [("value", enumerable), ("expectedCount", count), ("actualCount", collection.Count)]);
            }
            return ValidationResult.CreateFromValidationSuccess();
        }

        int actualCount = 0;
        using var enumerator = enumerable.GetEnumerator();
        while (enumerator.MoveNext())
        {
            actualCount++;
            if (actualCount > count)
            {
                return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                    [("value", enumerable), ("expectedCount", count), ("actualCount", ">" + count)]);
            }
        }

        if (actualCount != count)
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", enumerable), ("expectedCount", count), ("actualCount", actualCount)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given collection has the exact specified count, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ICollection<T>? EnsureIsCount<T>(this ICollection<T>? collection, int count, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        var validationResult = collection.ValidateIsCount(count, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return collection;
    }

    /// <summary>
    /// Ensures the given enumerable has the exact specified count, throwing an exception if validation fails.
    /// </summary>
    public static IEnumerable<T>? EnsureIsCount<T>(this IEnumerable<T>? enumerable, int count, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null)
    {
        var validationResult = enumerable.ValidateIsCount(count, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return enumerable;
    }

}

public readonly record struct IsCountParams(int Count);

public sealed class IsCountValidator : IValidator
{
    public IsCountParams Params { get; }

    public IsCountValidator(int count)
    {
        Params = new IsCountParams(count);
    }

    public string Name => IsCount.ValidatorName;
    public string DefaultFailureMessage => IsCount.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.Collections.IEnumerable enumerable && enumerable.CheckIsCount(Params.Count))
            return ValidationResult.CreateFromValidationSuccess();
        int actualCount = value switch
        {
            System.Collections.ICollection c => c.Count,
            System.Collections.IEnumerable e => CountEnumerable(e),
            _ => -1
        };
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("expectedCount", Params.Count), ("actualCount", actualCount)]);
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

public sealed class ValidateIsCountAttribute(int count)
    : ValidatorAttribute(new IsCountValidator(count));
