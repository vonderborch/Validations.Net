using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsGreaterThanOrEquals class provides methods for validation to ensure that
/// a value is greater than or equal to a comparand. Includes functionality to check, enforce,
/// and validate instances where ordering is required.
/// </summary>
public static class IsGreaterThanOrEquals
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsGreaterThanOrEquals";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must be greater than or equal to the comparand";

    /// <summary>
    /// Checks if the given value is greater than or equal to the comparand (non-generic overload for boxed values).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsGreaterThanOrEquals(this IComparable? value, object? comparand)
    {
        if (value is null) return false;
        try { return value.CompareTo(comparand) >= 0; }
        catch (ArgumentException) { return false; }
    }

    /// <summary>
    /// Checks if the given value is greater than or equal to the comparand.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsGreaterThanOrEquals<T>(this T value, T comparand) where T : IComparable<T>
    {
        if (value is null) return false;
        return value.CompareTo(comparand) >= 0;
    }

    /// <summary>
    /// Validates whether the given value is greater than or equal to the comparand.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsGreaterThanOrEquals<T>(this T value, T comparand, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        if (!value.CheckIsGreaterThanOrEquals(comparand))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("comparand", comparand)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is greater than or equal to the comparand, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureIsGreaterThanOrEquals<T>(this T value, T comparand, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        var validationResult = value.ValidateIsGreaterThanOrEquals(comparand, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}
