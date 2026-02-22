using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsLessThan class provides methods for validation to ensure that
/// a value is less than a comparand. Includes functionality to check, enforce,
/// and validate instances where ordering is required.
/// </summary>
public static class IsLessThan
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsLessThan";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must be less than the comparand";

    /// <summary>
    /// Checks if the given value is less than the comparand (non-generic overload for boxed values).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsLessThan(this IComparable? value, object? comparand)
    {
        if (value is null) return false;
        try { return value.CompareTo(comparand) < 0; }
        catch (ArgumentException) { return false; }
    }

    /// <summary>
    /// Checks if the given value is less than the comparand.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsLessThan<T>(this T value, T comparand) where T : IComparable<T>
    {
        if (value is null) return false;
        return value.CompareTo(comparand) < 0;
    }

    /// <summary>
    /// Validates whether the given value is less than the comparand.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsLessThan<T>(this T value, T comparand, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        if (!value.CheckIsLessThan(comparand))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("comparand", comparand)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is less than the comparand, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureIsLessThan<T>(this T value, T comparand, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        var validationResult = value.ValidateIsLessThan(comparand, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}
