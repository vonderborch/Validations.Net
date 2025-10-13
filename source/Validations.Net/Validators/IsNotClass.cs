using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods for checking if a type is not a class.
/// </summary>
public static class IsNotClass
{
    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotClass";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not be a class";

    /// <summary>
    /// Checks if a type is not a class.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>True if the type is not a class; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotClass(this Type? type)
    {
        return type?.IsClass != true;
    }

    /// <summary>
    /// Checks if an object's type is not a class.
    /// </summary>
    /// <param name="value">The object to check.</param>
    /// <returns>True if the object's type is not a class; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotClass(this object? value)
    {
        return value?.GetType().IsClass != true;
    }

    /// <summary>
    /// Ensures that a type is not a class, throwing an exception if validation fails.
    /// </summary>
    /// <param name="type">The type to validate.</param>
    /// <param name="blackboard">An optional blackboard providing additional context for the validation.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated, automatically captured by the compiler.</param>
    /// <returns>The original type if validation is successful.</returns>
    /// <exception cref="ValidationException">Thrown when the validation fails and the type is a class.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Type? EnsureIsNotClass(this Type? type, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(type))] string? parameterName = null)
    {
        var validationResult = type.ValidateIsNotClass(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return type;
    }

    /// <summary>
    /// Ensures that an object's type is not a class, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <param name="blackboard">An optional blackboard providing additional context for the validation.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated, automatically captured by the compiler.</param>
    /// <returns>The original object if validation is successful.</returns>
    /// <exception cref="ValidationException">Thrown when the validation fails and the object's type is a class.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsNotClass(this object? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNotClass(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates whether a type is not a class.
    /// </summary>
    /// <param name="type">The type to validate.</param>
    /// <param name="blackboard">An optional blackboard for additional validation context information.</param>
    /// <param name="validationFailureMessage">A custom message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated, automatically captured by the compiler.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating whether the validation was successful or failed.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotClass(this Type? type, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(type))] string? parameterName = null)
    {
        if (!type.CheckIsNotClass())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("type", type)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether an object's type is not a class.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <param name="blackboard">An optional blackboard for additional validation context information.</param>
    /// <param name="validationFailureMessage">A custom message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated, automatically captured by the compiler.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating whether the validation was successful or failed.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotClass(this object? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNotClass())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}

