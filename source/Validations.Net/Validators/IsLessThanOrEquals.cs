using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsLessThanOrEquals class provides methods for validation to ensure that
/// a value is less than or equal to a comparand. Includes functionality to check, enforce,
/// and validate instances where ordering is required.
/// </summary>
public static class IsLessThanOrEquals
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsLessThanOrEquals";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must be less than or equal to the comparand";

    /// <summary>
    /// Checks if the given value is less than or equal to the comparand.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsLessThanOrEquals<T>(this T value, T comparand) where T : IComparable<T>
    {
        if (value is null) return false;
        return value.CompareTo(comparand) <= 0;
    }

    /// <summary>
    /// Validates whether the given value is less than or equal to the comparand.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsLessThanOrEquals<T>(this T value, T comparand, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        if (!value.CheckIsLessThanOrEquals(comparand))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("comparand", comparand)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is less than or equal to the comparand, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureIsLessThanOrEquals<T>(this T value, T comparand, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        var validationResult = value.ValidateIsLessThanOrEquals(comparand, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}
