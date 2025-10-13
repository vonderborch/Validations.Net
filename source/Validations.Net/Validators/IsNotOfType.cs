using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if an object is not of a specific type.
/// </summary>
public static class IsNotOfType
{
    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotOfType";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not be of the specified type";

    /// <summary>
    ///     Checks if an object is not of the specified type.
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
    ///     Checks if an object is not of the specified type.
    /// </summary>
    /// <param name="value">The object to check.</param>
    /// <param name="type">The type to check against.</param>
    /// <returns>True if the object is not of the specified type; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotOfType(this object? value, Type type)
    {
        if (type == null)
        {
            return true;
        }

        return value == null || !type.IsInstanceOfType(value);
    }

    /// <summary>
    ///     Validates if an object is not of the specified type.
    /// </summary>
    /// <typeparam name="T">The type to check against.</typeparam>
    /// <param name="value">The object to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the object is not of the specified type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotOfType<T>(this object? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
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
            validationFailureMessage,
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if an object is not of the specified type.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <param name="type">The type to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the object is not of the specified type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotOfType(this object? value, Type type, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
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
            validationFailureMessage,
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Ensures that an object is not of the specified type.
    /// </summary>
    /// <typeparam name="T">The type to check against.</typeparam>
    /// <param name="value">The object to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original object if it is not of the specified type.</returns>
    /// <exception cref="ValidationException">Thrown when the object is of the specified type.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsNotOfType<T>(this object? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateIsNotOfType<T>(blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that an object is not of the specified type.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <param name="type">The type to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original object if it is not of the specified type.</returns>
    /// <exception cref="ValidationException">Thrown when the object is of the specified type.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsNotOfType(this object? value, Type type, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateIsNotOfType(type, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }
}
