using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods for checking if a type is not a child class of another type.
/// </summary>
public static class IsNotChildClassOf
{
    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotChildClassOf";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not be a child class of the specified type";

    /// <summary>
    /// Checks if a type is not a child class of another type.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotChildClassOf(this Type? type, Type parentType)
    {
        if (type == null || parentType == null)
        {
            return false;
        }

        return type == parentType || !parentType.IsAssignableFrom(type);
    }

    /// <summary>
    /// Checks if an object's type is not a child class of another type.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotChildClassOf(this object? value, Type parentType)
    {
        if (value == null || parentType == null)
        {
            return false;
        }

        var type = value.GetType();
        return type == parentType || !parentType.IsAssignableFrom(type);
    }

    /// <summary>
    /// Ensures that a type is not a child class of another type.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Type? EnsureIsNotChildClassOf(this Type? type, Type parentType, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(type))] string? parameterName = null)
    {
        var validationResult = type.ValidateIsNotChildClassOf(parentType, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return type;
    }

    /// <summary>
    /// Ensures that an object's type is not a child class of another type.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsNotChildClassOf(this object? value, Type parentType, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNotChildClassOf(parentType, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates whether a type is not a child class of another type.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotChildClassOf(this Type? type, Type parentType, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(type))] string? parameterName = null)
    {
        if (!type.CheckIsNotChildClassOf(parentType))
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("type", type), ("parentType", parentType)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether an object's type is not a child class of another type.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotChildClassOf(this object? value, Type parentType, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNotChildClassOf(parentType))
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value), ("parentType", parentType)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}

