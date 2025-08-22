using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if an object is not of a specific type.
/// </summary>
public static class IsNotOfType
{
    private const string ValidatorName = "IsNotOfType";

    #region Check Methods

    /// <summary>
    /// Checks if an object is not of the specified type.
    /// </summary>
    /// <typeparam name="T">The type to check against.</typeparam>
    /// <param name="value">The object to check.</param>
    /// <returns>True if the object is not of the specified type; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotOfType<T>(this object? value)
    {
        return value is not T;
    }

    /// <summary>
    /// Checks if an object is not of the specified type.
    /// </summary>
    /// <param name="value">The object to check.</param>
    /// <param name="type">The type to check against.</param>
    /// <returns>True if the object is not of the specified type; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotOfType(this object? value, Type type)
    {
        if (type == null)
            return true;

        return value == null || !type.IsInstanceOfType(value);
    }

    #endregion

    #region Validate Methods

    /// <summary>
    /// Validates if an object is not of the specified type.
    /// </summary>
    /// <typeparam name="T">The type to check against.</typeparam>
    /// <param name="value">The object to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the object is not of the specified type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotOfType<T>(this object? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsNotOfType<T>(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("unexpectedType", typeof(T))
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must not be of type {typeof(T).Name}. Actual type: {value?.GetType().Name ?? "null"}",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates if an object is not of the specified type.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <param name="type">The type to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the object is not of the specified type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotOfType(this object? value, Type type, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsNotOfType(value, type))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("unexpectedType", type)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must not be of type {type?.Name ?? "null"}. Actual type: {value?.GetType().Name ?? "null"}",
            fieldName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    /// Ensures that an object is not of the specified type.
    /// </summary>
    /// <typeparam name="T">The type to check against.</typeparam>
    /// <param name="value">The object to validate.</param>
    /// <returns>The original object if it is not of the specified type.</returns>
    /// <exception cref="ValidationException">Thrown when the object is of the specified type.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsNotOfType<T>(this object? value)
    {
        if (!CheckIsNotOfType<T>(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("unexpectedType", typeof(T))
            };
            throw ValidationException.Create(ValidatorName, $"The value must not be of type {typeof(T).Name}. Actual type: {value?.GetType().Name ?? "null"}", null, null, contextList);
        }

        return value;
    }

    /// <summary>
    /// Ensures that an object is not of the specified type.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <param name="type">The type to check against.</param>
    /// <returns>The original object if it is not of the specified type.</returns>
    /// <exception cref="ValidationException">Thrown when the object is of the specified type.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsNotOfType(this object? value, Type type)
    {
        if (!CheckIsNotOfType(value, type))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("unexpectedType", type)
            };
            throw ValidationException.Create(ValidatorName, $"The value must not be of type {type?.Name ?? "null"}. Actual type: {value?.GetType().Name ?? "null"}", null, null, contextList);
        }

        return value;
    }

    #endregion
}
