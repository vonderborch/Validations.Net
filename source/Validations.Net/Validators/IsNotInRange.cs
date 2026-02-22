using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsNotInRange class provides methods for validation to ensure that
/// a value is not within a specified range. Includes functionality to check, enforce,
/// and validate instances where values must fall outside a range.
/// </summary>
public static class IsNotInRange
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotInRange";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must not be within the specified range";

    /// <summary>
    /// Checks if the given value is not within the specified range.
    /// </summary>
    /// <param name="minInclusive">If true, the minimum bound is inclusive; otherwise exclusive.</param>
    /// <param name="maxInclusive">If true, the maximum bound is inclusive; otherwise exclusive.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotInRange<T>(this T value, T min, T max, bool minInclusive = true, bool maxInclusive = true) where T : IComparable<T>
    {
        return !value.CheckIsInRange(min, max, minInclusive, maxInclusive);
    }

    /// <summary>
    /// Validates whether the given value is not within the specified range.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotInRange<T>(this T value, T min, T max, bool minInclusive = true, bool maxInclusive = true,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        if (!value.CheckIsNotInRange(min, max, minInclusive, maxInclusive))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("min", min), ("max", max), ("minInclusive", minInclusive), ("maxInclusive", maxInclusive)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is not within the specified range, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureIsNotInRange<T>(this T value, T min, T max, bool minInclusive = true, bool maxInclusive = true,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        var validationResult = value.ValidateIsNotInRange(min, max, minInclusive, maxInclusive, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}
