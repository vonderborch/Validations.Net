using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsAssignableTo class provides methods for validation to ensure that
/// a value is assignable to a target type. Includes functionality to check, enforce,
/// and validate instances where type compatibility is required.
/// </summary>
public static class IsAssignableTo
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsAssignableTo";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must be assignable to the target type";

    /// <summary>
    /// Checks if the given value is assignable to the target type.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is assignable to TTarget; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsAssignableTo<TTarget>(this object? value)
    {
        return value is not null && typeof(TTarget).IsAssignableFrom(value.GetType());
    }

    /// <summary>
    /// Checks if the given value is assignable to the target type.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="targetType">The target type.</param>
    /// <returns>True if the value is assignable to targetType; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsAssignableTo(this object? value, Type targetType)
    {
        return value is not null && targetType.IsAssignableFrom(value.GetType());
    }

    /// <summary>
    /// Validates whether the given value is assignable to the target type.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsAssignableTo<TTarget>(this object? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsAssignableTo<TTarget>())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("targetType", typeof(TTarget).FullName ?? typeof(TTarget).Name)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given value is assignable to the target type.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsAssignableTo(this object? value, Type targetType, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsAssignableTo(targetType))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("targetType", targetType.FullName ?? targetType.Name)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is assignable to the target type, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsAssignableTo<TTarget>(this object? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsAssignableTo<TTarget>(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures the given value is assignable to the target type, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsAssignableTo(this object? value, Type targetType, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsAssignableTo(targetType, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}
