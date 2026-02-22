using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

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
