using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a type is a struct.
/// </summary>
public static class IsStruct
{
    private const string ValidatorName = "IsStruct";

    #region Check Methods

    /// <summary>
    /// Checks if a type is a struct.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>True if the type is a struct; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsStruct(this Type? type)
    {
        return type?.IsValueType == true && !type.IsEnum && !type.IsPrimitive;
    }

    /// <summary>
    /// Checks if an object's type is a struct.
    /// </summary>
    /// <param name="value">The object to check.</param>
    /// <returns>True if the object's type is a struct; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsStruct(this object? value)
    {
        if (value == null) return false;
        var type = value.GetType();
        return type.IsValueType && !type.IsEnum && !type.IsPrimitive;
    }

    #endregion

    #region Validate Methods

    /// <summary>
    /// Validates if a type is a struct.
    /// </summary>
    /// <param name="type">The type to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the type is a struct.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsStruct(this Type? type, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(type))] string? fieldName = null)
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
            $"The type must be a struct. Actual type: {type?.Name ?? "null"}",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates if an object's type is a struct.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the object's type is a struct.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsStruct(this object? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? fieldName = null)
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
            $"The object's type must be a struct. Actual type: {value?.GetType().Name ?? "null"}",
            fieldName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    /// Ensures that a type is a struct.
    /// </summary>
    /// <param name="type">The type to validate.</param>
    /// <returns>The original type if it is a struct.</returns>
    /// <exception cref="ValidationException">Thrown when the type is not a struct.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Type? EnsureIsStruct(this Type? type)
    {
        if (!CheckIsStruct(type))
        {
            var contextList = new List<(string, object?)>
            {
                ("type", type)
            };
            throw ValidationException.Create(ValidatorName, $"The type must be a struct. Actual type: {type?.Name ?? "null"}", null, null, contextList);
        }

        return type;
    }

    /// <summary>
    /// Ensures that an object's type is a struct.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <returns>The original object if its type is a struct.</returns>
    /// <exception cref="ValidationException">Thrown when the object's type is not a struct.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsStruct(this object? value)
    {
        if (!CheckIsStruct(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("type", value?.GetType())
            };
            throw ValidationException.Create(ValidatorName, $"The object's type must be a struct. Actual type: {value?.GetType().Name ?? "null"}", null, null, contextList);
        }

        return value;
    }

    #endregion
}
