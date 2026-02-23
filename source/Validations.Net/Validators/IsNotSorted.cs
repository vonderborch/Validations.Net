using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsNotSorted class provides methods for validation to ensure that
/// a sequence is not sorted. Includes functionality to check, enforce,
/// and validate instances where unsorted order is required.
/// </summary>
public static class IsNotSorted
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotSorted";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not be sorted";

    /// <summary>
    /// Checks if the given non-generic enumerable is not sorted (non-generic overload for boxed values).
    /// </summary>
    public static bool CheckIsNotSorted(this System.Collections.IEnumerable? enumerable, bool descending = false)
    {
        if (enumerable is null) return false;
        return !enumerable.CheckIsSorted(descending);
    }

    /// <summary>
    /// Checks if the given enumerable is not sorted.
    /// </summary>
    /// <param name="enumerable">The enumerable to check.</param>
    /// <param name="descending">If true, checks that it is not descending-sorted; otherwise not ascending-sorted.</param>
    /// <returns>True if the enumerable is not sorted; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotSorted<T>(this IEnumerable<T>? enumerable, bool descending = false) where T : IComparable<T>
    {
        return !enumerable.CheckIsSorted(descending);
    }

    /// <summary>
    /// Validates whether the given enumerable is not sorted.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotSorted<T>(this IEnumerable<T>? enumerable, bool descending = false, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null) where T : IComparable<T>
    {
        if (!enumerable.CheckIsNotSorted(descending))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", enumerable), ("descending", descending)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given enumerable is not sorted, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T>? EnsureIsNotSorted<T>(this IEnumerable<T>? enumerable, bool descending = false, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null) where T : IComparable<T>
    {
        var validationResult = enumerable.ValidateIsNotSorted(descending, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return enumerable;
    }
}

public readonly record struct NotSortedParams(bool Descending = false);

public sealed class NotSortedValidator : IValidator
{
    public NotSortedParams Params { get; }

    public NotSortedValidator(bool descending = false)
    {
        Params = new NotSortedParams(descending);
    }

    public string Name => IsNotSorted.ValidatorName;
    public string DefaultFailureMessage => IsNotSorted.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.Collections.IEnumerable enumerable && enumerable.CheckIsNotSorted(Params.Descending))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("descending", Params.Descending)]);
    }
}

public sealed class ValidateIsNotSortedAttribute(bool descending = false)
    : ValidatorAttribute(new NotSortedValidator(descending));
