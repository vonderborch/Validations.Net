using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a type is not assignable to another type.
/// </summary>
public static class IsNotAssignableTo
{
    private const string ValidatorName = "IsNotAssignableTo";

    #region Check Methods

    /// <summary>
    /// Checks if a type is not assignable to the specified type.
    /// </summary>
    /// <typeparam name="T">The target type.</typeparam>
    /// <param name="value">The object to check.</param>
    /// <returns>True if the type is not assignable to the specified type; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotAssignableTo<T>(this object? value)
    {
        return value is not T;
    }

    /// <summary>
    /// Checks if a type is not assignable to the specified type.
    /// </summary>
    /// <param name="value">The object to check.</param>
    /// <param name="targetType">The target type.</param>
    /// <returns>True if the type is not assignable to the specified type; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotAssignableTo(this object? value, Type targetType)
    {
        if (targetType == null)
            return true;

        return value == null || !targetType.IsAssignableFrom(value.GetType());
    }

    /// <summary>
    /// Checks if a type is not assignable to the specified type.
    /// </summary>
    /// <param name="sourceType">The source type to check.</param>
    /// <param name="targetType">The target type.</param>
    /// <returns>True if the type is not assignable to the specified type; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotAssignableTo(this Type? sourceType, Type targetType)
    {
        if (sourceType == null || targetType == null)
            return true;

        return !targetType.IsAssignableFrom(sourceType);
    }

    #endregion

    #region Validate Methods

    /// <summary>
    /// Validates if a type is not assignable to the specified type.
    /// </summary>
    /// <typeparam name="T">The target type.</typeparam>
    /// <param name="value">The object to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the type is not assignable to the specified type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotAssignableTo<T>(this object? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsNotAssignableTo<T>(value))
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
            $"The value must not be assignable to type {typeof(T).Name}. Actual type: {value?.GetType().Name ?? "null"}",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates if a type is not assignable to the specified type.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <param name="targetType">The target type.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the type is not assignable to the specified type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotAssignableTo(this object? value, Type targetType, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsNotAssignableTo(value, targetType))
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
            $"The value must not be assignable to type {targetType?.Name ?? "null"}. Actual type: {value?.GetType().Name ?? "null"}",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates if a type is not assignable to the specified type.
    /// </summary>
    /// <param name="sourceType">The source type to validate.</param>
    /// <param name="targetType">The target type.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the type is not assignable to the specified type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotAssignableTo(this Type? sourceType, Type targetType, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(sourceType))] string? fieldName = null)
    {
        if (CheckIsNotAssignableTo(sourceType, targetType))
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
            $"The type {sourceType?.Name ?? "null"} must not be assignable to type {targetType?.Name ?? "null"}.",
            fieldName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    /// Ensures that a type is not assignable to the specified type.
    /// </summary>
    /// <typeparam name="T">The target type.</typeparam>
    /// <param name="value">The object to validate.</param>
    /// <returns>The original object if it is not assignable to the specified type.</returns>
    /// <exception cref="ValidationException">Thrown when the type is assignable to the specified type.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsNotAssignableTo<T>(this object? value)
    {
        if (!CheckIsNotAssignableTo<T>(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("targetType", typeof(T))
            };
            throw ValidationException.Create(ValidatorName, $"The value must not be assignable to type {typeof(T).Name}. Actual type: {value?.GetType().Name ?? "null"}", null, null, contextList);
        }

        return value;
    }

    /// <summary>
    /// Ensures that a type is not assignable to the specified type.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <param name="targetType">The target type.</param>
    /// <returns>The original object if it is not assignable to the specified type.</returns>
    /// <exception cref="ValidationException">Thrown when the type is assignable to the specified type.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsNotAssignableTo(this object? value, Type targetType)
    {
        if (!CheckIsNotAssignableTo(value, targetType))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("targetType", targetType)
            };
            throw ValidationException.Create(ValidatorName, $"The value must not be assignable to type {targetType?.Name ?? "null"}. Actual type: {value?.GetType().Name ?? "null"}", null, null, contextList);
        }

        return value;
    }

    /// <summary>
    /// Ensures that a type is not assignable to the specified type.
    /// </summary>
    /// <param name="sourceType">The source type to validate.</param>
    /// <param name="targetType">The target type.</param>
    /// <returns>The original type if it is not assignable to the specified type.</returns>
    /// <exception cref="ValidationException">Thrown when the type is assignable to the specified type.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Type? EnsureIsNotAssignableTo(this Type? sourceType, Type targetType)
    {
        if (!CheckIsNotAssignableTo(sourceType, targetType))
        {
            var contextList = new List<(string, object?)>
            {
                ("sourceType", sourceType),
                ("targetType", targetType)
            };
            throw ValidationException.Create(ValidatorName, $"The type {sourceType?.Name ?? "null"} must not be assignable to type {targetType?.Name ?? "null"}.", null, null, contextList);
        }

        return sourceType;
    }

    #endregion
}
