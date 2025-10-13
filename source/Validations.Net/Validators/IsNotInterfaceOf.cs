using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods for checking if a type does not implement a specified interface.
/// </summary>
public static class IsNotInterfaceOf
{
    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotInterfaceOf";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not implement the specified interface";

    /// <summary>
    /// Checks if a type does not implement a specified interface.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotInterfaceOf(this Type? type, Type interfaceType)
    {
        if (type == null || interfaceType == null)
        {
            return false;
        }

        return !interfaceType.IsAssignableFrom(type);
    }

    /// <summary>
    /// Checks if an object's type does not implement a specified interface.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotInterfaceOf(this object? value, Type interfaceType)
    {
        if (value == null || interfaceType == null)
        {
            return false;
        }

        return !interfaceType.IsAssignableFrom(value.GetType());
    }

    /// <summary>
    /// Ensures that a type does not implement a specified interface.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Type? EnsureIsNotInterfaceOf(this Type? type, Type interfaceType, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(type))] string? parameterName = null)
    {
        var validationResult = type.ValidateIsNotInterfaceOf(interfaceType, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return type;
    }

    /// <summary>
    /// Ensures that an object's type does not implement a specified interface.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsNotInterfaceOf(this object? value, Type interfaceType, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNotInterfaceOf(interfaceType, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates whether a type does not implement a specified interface.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotInterfaceOf(this Type? type, Type interfaceType, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(type))] string? parameterName = null)
    {
        if (!type.CheckIsNotInterfaceOf(interfaceType))
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("type", type), ("interfaceType", interfaceType)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether an object's type does not implement a specified interface.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotInterfaceOf(this object? value, Type interfaceType, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNotInterfaceOf(interfaceType))
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value), ("interfaceType", interfaceType)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}

