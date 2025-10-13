using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods for checking if a type is a struct.
/// </summary>
public static class IsStruct
{
    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsStruct";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must be a struct";

    /// <summary>
    ///     Checks if a type is a struct.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>True if the type is a struct; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsStruct(this Type? type)
    {
        return type?.IsValueType == true && !type.IsEnum && !type.IsPrimitive;
    }

    /// <summary>
    ///     Checks if an object's type is a struct.
    /// </summary>
    /// <param name="value">The object to check.</param>
    /// <returns>True if the object's type is a struct; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsStruct(this object? value)
    {
        var type = value?.GetType();
        return type?.IsValueType == true && !type.IsEnum && !type.IsPrimitive;
    }

    /// <summary>
    ///     Validates if a type is a struct.
    /// </summary>
    /// <param name="type">The type to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the type is a struct.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsStruct(this Type? type, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(type))] string? parameterName = null)
    {
        if (CheckIsStruct(type))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("type", type)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            validationFailureMessage,
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if an object's type is a struct.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the object's type is a struct.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsStruct(this object? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsStruct(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("type", value?.GetType())
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            validationFailureMessage,
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Ensures that a type is a struct.
    /// </summary>
    /// <param name="type">The type to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original type if it is a struct.</returns>
    /// <exception cref="ValidationException">Thrown when the type is not a struct.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Type? EnsureIsStruct(this Type? type, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(type))] string? parameterName = null)
    {
        var result = ValidateIsStruct(type, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return type;
    }

    /// <summary>
    ///     Ensures that an object's type is a struct.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original object if its type is a struct.</returns>
    /// <exception cref="ValidationException">Thrown when the object's type is not a struct.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsStruct(this object? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsStruct(value, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }
}
