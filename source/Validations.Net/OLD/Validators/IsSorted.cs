using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The IsSorted class provides methods for validation to ensure that
/// a sequence is sorted in ascending or descending order. Includes functionality to check, enforce,
/// and validate instances where sorted order is required.
/// </summary>
public static class IsSorted
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsSorted";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must be sorted";

    /// <summary>
    /// Checks if the given non-generic enumerable is sorted (non-generic overload for boxed values).
    /// Elements must implement IComparable.
    /// </summary>
    public static bool CheckIsSorted(this System.Collections.IEnumerable? enumerable, bool descending = false)
    {
        if (enumerable is null) return false;
        IComparable? prev = null;
        bool first = true;
        foreach (var item in enumerable)
        {
            if (item is not IComparable current) return false;
            if (!first)
            {
                int cmp = prev!.CompareTo(current);
                if (descending ? cmp < 0 : cmp > 0) return false;
            }
            prev = current;
            first = false;
        }
        return true;
    }

    /// <summary>
    /// Checks if the given enumerable is sorted.
    /// </summary>
    /// <param name="enumerable">The enumerable to check.</param>
    /// <param name="descending">If true, checks for descending order; otherwise ascending.</param>
    /// <returns>True if the enumerable is sorted; otherwise, false.</returns>
    public static bool CheckIsSorted<T>(this IEnumerable<T>? enumerable, bool descending = false) where T : IComparable<T>
    {
        if (enumerable is null)
            return false;

        using var enumerator = enumerable.GetEnumerator();
        if (!enumerator.MoveNext())
            return true;

        T prev = enumerator.Current;
        while (enumerator.MoveNext())
        {
            T current = enumerator.Current;
            int cmp = prev.CompareTo(current);
            if (descending ? cmp < 0 : cmp > 0)
                return false;
            prev = current;
        }

        return true;
    }

    /// <summary>
    /// Validates whether the given enumerable is sorted.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsSorted<T>(this IEnumerable<T>? enumerable, bool descending = false, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null) where T : IComparable<T>
    {
        if (!enumerable.CheckIsSorted(descending))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", enumerable), ("descending", descending)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given enumerable is sorted, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T>? EnsureIsSorted<T>(this IEnumerable<T>? enumerable, bool descending = false, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null) where T : IComparable<T>
    {
        var validationResult = enumerable.ValidateIsSorted(descending, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return enumerable;
    }
}

public readonly record struct IsSortedParams(bool Descending = false);

public sealed class IsSortedValidator : IValidator
{
    public IsSortedParams Params { get; }

    public IsSortedValidator(bool descending = false)
    {
        this.Params = new IsSortedParams(descending);
    }

    public string Name => IsSorted.ValidatorName;
    public string DefaultFailureMessage => IsSorted.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.Collections.IEnumerable enumerable && enumerable.CheckIsSorted(this.Params.Descending))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("descending", this.Params.Descending)]);
    }
}

public sealed class ValidateIsSortedAttribute(bool descending = false)
    : ValidatorAttribute(new IsSortedValidator(descending));
