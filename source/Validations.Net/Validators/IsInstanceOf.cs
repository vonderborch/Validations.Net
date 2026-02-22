using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsInstanceOf class provides methods for validation to ensure that
/// a value is an exact instance of the target type (not a subclass). Includes functionality to check, enforce,
/// and validate instances where exact type matching is required.
/// </summary>
public static class IsInstanceOf
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsInstanceOf";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must be an instance of the target type";

    /// <summary>
    /// Checks if the given value is an exact instance of the target type.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value's runtime type equals TTarget; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsInstanceOf<TTarget>(this object? value)
    {
        return value is not null && value.GetType() == typeof(TTarget);
    }

    /// <summary>
    /// Checks if the given value is an exact instance of the target type.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="targetType">The target type.</param>
    /// <returns>True if the value's runtime type equals targetType; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsInstanceOf(this object? value, Type targetType)
    {
        return value is not null && value.GetType() == targetType;
    }

    /// <summary>
    /// Validates whether the given value is an exact instance of the target type.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsInstanceOf<TTarget>(this object? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsInstanceOf<TTarget>())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("targetType", typeof(TTarget).FullName ?? typeof(TTarget).Name), ("actualType", value?.GetType()?.FullName ?? value?.GetType()?.Name)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given value is an exact instance of the target type.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsInstanceOf(this object? value, Type targetType, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsInstanceOf(targetType))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("targetType", targetType.FullName ?? targetType.Name), ("actualType", value?.GetType()?.FullName ?? value?.GetType()?.Name)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is an exact instance of the target type, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsInstanceOf<TTarget>(this object? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsInstanceOf<TTarget>(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures the given value is an exact instance of the target type, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsInstanceOf(this object? value, Type targetType, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsInstanceOf(targetType, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}
