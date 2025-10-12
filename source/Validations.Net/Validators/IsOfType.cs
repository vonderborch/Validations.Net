using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if an object is of a specific type.
/// </summary>
public static class IsOfType
{
    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsOfType";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must be of the specified type";

    #region Check Methods

    /// <summary>
    ///     Checks if an object is of the specified type.
    /// </summary>
    /// <typeparam name="T">The expected type.</typeparam>
    /// <param name="value">The object to check.</param>
    /// <returns>True if the object is of the specified type; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsOfType<T>(this object? value)
    {
        return value is T;
    }

    /// <summary>
    ///     Checks if an object is of the specified type.
    /// </summary>
    /// <param name="value">The object to check.</param>
    /// <param name="type">The expected type.</param>
    /// <returns>True if the object is of the specified type; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsOfType(this object? value, Type type)
    {
        if (type == null)
        {
            return false;
        }

        return value != null && type.IsInstanceOfType(value);
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates if an object is of the specified type.
    /// </summary>
    /// <typeparam name="T">The expected type.</typeparam>
    /// <param name="value">The object to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the object is of the specified type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsOfType<T>(this object? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsOfType<T>(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("expectedType", typeof(T))
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            validationFailureMessage,
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if an object is of the specified type.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <param name="type">The expected type.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the object is of the specified type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsOfType(this object? value, Type type, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsOfType(value, type))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("expectedType", type)
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
    ///     Ensures that an object is of the specified type.
    /// </summary>
    /// <typeparam name="T">The expected type.</typeparam>
    /// <param name="value">The object to validate.</param>
    /// <returns>The original object if it is of the specified type.</returns>
    /// <exception cref="ValidationException">Thrown when the object is not of the specified type.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsOfType<T>(this object? value)
    {
        if (!CheckIsOfType<T>(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("expectedType", typeof(T))
            };
            throw ValidationException.Create(ValidatorName,
                $"The value must be of type {typeof(T).Name}. Actual type: {value?.GetType().Name ?? "null"}", null,
                null, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that an object is of the specified type.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <param name="type">The expected type.</param>
    /// <returns>The original object if it is of the specified type.</returns>
    /// <exception cref="ValidationException">Thrown when the object is not of the specified type.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsOfType(this object? value, Type type)
    {
        if (!CheckIsOfType(value, type))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("expectedType", type)
            };
            throw ValidationException.Create(ValidatorName,
                $"The value must be of type {type?.Name ?? "null"}. Actual type: {value?.GetType().Name ?? "null"}",
                null, null, contextList);
        }

        return value;
    }

    #endregion
}
