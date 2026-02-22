using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsDistinct class provides methods for validation to ensure that
/// a sequence contains only distinct elements. Includes functionality to check, enforce,
/// and validate instances where uniqueness is required.
/// </summary>
public static class IsDistinct
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsDistinct";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must contain only distinct elements";

    /// <summary>
    /// Checks if the given non-generic enumerable contains only distinct elements (non-generic overload for boxed values).
    /// </summary>
    public static bool CheckIsDistinct(this System.Collections.IEnumerable? enumerable)
    {
        if (enumerable is null) return false;
        var seen = new HashSet<object?>();
        foreach (var item in enumerable)
        {
            if (!seen.Add(item)) return false;
        }
        return true;
    }

    /// <summary>
    /// Checks if the given enumerable contains only distinct elements.
    /// </summary>
    /// <param name="enumerable">The enumerable to check.</param>
    /// <returns>True if all elements are distinct; otherwise, false.</returns>
    public static bool CheckIsDistinct<T>(this IEnumerable<T>? enumerable)
    {
        if (enumerable is null)
            return false;

        var seen = new HashSet<T>();
        foreach (var item in enumerable)
        {
            if (!seen.Add(item))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Validates whether the given enumerable contains only distinct elements.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsDistinct<T>(this IEnumerable<T>? enumerable, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null)
    {
        if (!enumerable.CheckIsDistinct())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", enumerable)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given enumerable contains only distinct elements, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T>? EnsureIsDistinct<T>(this IEnumerable<T>? enumerable, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null)
    {
        var validationResult = enumerable.ValidateIsDistinct(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return enumerable;
    }
}
