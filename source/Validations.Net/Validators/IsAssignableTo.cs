using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a type is assignable to another type.
/// </summary>
public static class IsAssignableTo
{
    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsAssignableTo";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must be assignable to the specified type";

    #region Check Methods

    /// <summary>
    ///     Checks if a type is assignable to the specified type.
    /// </summary>
    /// <typeparam name="T">The target type.</typeparam>
    /// <param name="value">The object to check.</param>
    /// <returns>True if the type is assignable to the specified type; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsAssignableTo<T>(this object? value)
    {
        return value is T;
    }

    /// <summary>
    ///     Checks if a type is assignable to the specified type.
    /// </summary>
    /// <param name="value">The object to check.</param>
    /// <param name="targetType">The target type.</param>
    /// <returns>True if the type is assignable to the specified type; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsAssignableTo(this object? value, Type targetType)
    {
        if (targetType == null)
        {
            return false;
        }

        return value != null && targetType.IsAssignableFrom(value.GetType());
    }

    /// <summary>
    ///     Checks if a type is assignable to the specified type.
    /// </summary>
    /// <param name="sourceType">The source type to check.</param>
    /// <param name="targetType">The target type.</param>
    /// <returns>True if the type is assignable to the specified type; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsAssignableTo(this Type? sourceType, Type targetType)
    {
        if (sourceType == null || targetType == null)
        {
            return false;
        }

        return targetType.IsAssignableFrom(sourceType);
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates if a type is assignable to the specified type.
    /// </summary>
    /// <typeparam name="T">The target type.</typeparam>
    /// <param name="value">The object to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the type is assignable to the specified type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsAssignableTo<T>(this object? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsAssignableTo<T>(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("targetType", typeof(T))
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            validationFailureMessage,
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a type is assignable to the specified type.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <param name="targetType">The target type.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the type is assignable to the specified type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsAssignableTo(this object? value, Type targetType,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsAssignableTo(value, targetType))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("targetType", targetType)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            validationFailureMessage,
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a type is assignable to the specified type.
    /// </summary>
    /// <param name="sourceType">The source type to validate.</param>
    /// <param name="targetType">The target type.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the type is assignable to the specified type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsAssignableTo(this Type? sourceType, Type targetType,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(sourceType))] string? parameterName = null)
    {
        if (CheckIsAssignableTo(sourceType, targetType))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("sourceType", sourceType),
            ("targetType", targetType)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            validationFailureMessage,
            parameterName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that a type is assignable to the specified type.
    /// </summary>
    /// <typeparam name="T">The target type.</typeparam>
    /// <param name="value">The object to validate.</param>
    /// <returns>The original object if it is assignable to the specified type.</returns>
    /// <exception cref="ValidationException">Thrown when the type is not assignable to the specified type.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsAssignableTo<T>(this object? value)
    {
        if (!CheckIsAssignableTo<T>(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("targetType", typeof(T))
            };
            throw ValidationException.Create(ValidatorName,
                $"The value must be assignable to type {typeof(T).Name}. Actual type: {value?.GetType().Name ?? "null"}",
                null, null, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a type is assignable to the specified type.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <param name="targetType">The target type.</param>
    /// <returns>The original object if it is assignable to the specified type.</returns>
    /// <exception cref="ValidationException">Thrown when the type is not assignable to the specified type.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsAssignableTo(this object? value, Type targetType)
    {
        if (!CheckIsAssignableTo(value, targetType))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("targetType", targetType)
            };
            throw ValidationException.Create(ValidatorName,
                $"The value must be assignable to type {targetType?.Name ?? "null"}. Actual type: {value?.GetType().Name ?? "null"}",
                null, null, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a type is assignable to the specified type.
    /// </summary>
    /// <param name="sourceType">The source type to validate.</param>
    /// <param name="targetType">The target type.</param>
    /// <returns>The original type if it is assignable to the specified type.</returns>
    /// <exception cref="ValidationException">Thrown when the type is not assignable to the specified type.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Type? EnsureIsAssignableTo(this Type? sourceType, Type targetType)
    {
        if (!CheckIsAssignableTo(sourceType, targetType))
        {
            var contextList = new List<(string, object?)>
            {
                ("sourceType", sourceType),
                ("targetType", targetType)
            };
            throw ValidationException.Create(ValidatorName,
                $"The type {sourceType?.Name ?? "null"} must be assignable to type {targetType?.Name ?? "null"}.", null,
                null, contextList);
        }

        return sourceType;
    }

    #endregion
}
