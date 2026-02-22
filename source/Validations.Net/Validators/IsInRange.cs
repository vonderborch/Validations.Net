using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsInRange class provides methods for validation to ensure that
/// a value is within a specified range. Includes functionality to check, enforce,
/// and validate instances where range constraints are required.
/// </summary>
public static class IsInRange
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsInRange";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must be within the specified range";

    /// <summary>
    /// Checks if the given value is within the specified range.
    /// </summary>
    /// <param name="minInclusive">If true, the minimum bound is inclusive; otherwise exclusive.</param>
    /// <param name="maxInclusive">If true, the maximum bound is inclusive; otherwise exclusive.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsInRange<T>(this T value, T min, T max, bool minInclusive = true, bool maxInclusive = true) where T : IComparable<T>
    {
        if (value is null) return false;
        int lower = value.CompareTo(min);
        if (minInclusive ? lower < 0 : lower <= 0)
            return false;

        int upper = value.CompareTo(max);
        return maxInclusive ? upper <= 0 : upper < 0;
    }

    /// <summary>
    /// Validates whether the given value is within the specified range.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsInRange<T>(this T value, T min, T max, bool minInclusive = true, bool maxInclusive = true,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        if (!value.CheckIsInRange(min, max, minInclusive, maxInclusive))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("min", min), ("max", max), ("minInclusive", minInclusive), ("maxInclusive", maxInclusive)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is within the specified range, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureIsInRange<T>(this T value, T min, T max, bool minInclusive = true, bool maxInclusive = true,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        var validationResult = value.ValidateIsInRange(min, max, minInclusive, maxInclusive, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}
