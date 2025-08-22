using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a type is an interface.
/// </summary>
public static class IsInterface
{
    private const string ValidatorName = "IsInterface";

    #region Check Methods

    /// <summary>
    ///     Checks if a type is an interface.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>True if the type is an interface; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsInterface(this Type? type)
    {
        return type?.IsInterface == true;
    }

    /// <summary>
    ///     Checks if an object's type is an interface.
    /// </summary>
    /// <param name="value">The object to check.</param>
    /// <returns>True if the object's type is an interface; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsInterface(this object? value)
    {
        return value?.GetType().IsInterface == true;
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates if a type is an interface.
    /// </summary>
    /// <param name="type">The type to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the type is an interface.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsInterface(this Type? type, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(type))] string? fieldName = null)
    {
        if (CheckIsInterface(type))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("type", type)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The type must be an interface. Actual type: {type?.Name ?? "null"}",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if an object's type is an interface.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the object's type is an interface.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsInterface(this object? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsInterface(value))
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
            $"The object's type must be an interface. Actual type: {value?.GetType().Name ?? "null"}",
            fieldName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that a type is an interface.
    /// </summary>
    /// <param name="type">The type to validate.</param>
    /// <returns>The original type if it is an interface.</returns>
    /// <exception cref="ValidationException">Thrown when the type is not an interface.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Type? EnsureIsInterface(this Type? type)
    {
        if (!CheckIsInterface(type))
        {
            var contextList = new List<(string, object?)>
            {
                ("type", type)
            };
            throw ValidationException.Create(ValidatorName,
                $"The type must be an interface. Actual type: {type?.Name ?? "null"}", null, null, contextList);
        }

        return type;
    }

    /// <summary>
    ///     Ensures that an object's type is an interface.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <returns>The original object if its type is an interface.</returns>
    /// <exception cref="ValidationException">Thrown when the object's type is not an interface.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsInterface(this object? value)
    {
        if (!CheckIsInterface(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("type", value?.GetType())
            };
            throw ValidationException.Create(ValidatorName,
                $"The object's type must be an interface. Actual type: {value?.GetType().Name ?? "null"}", null, null,
                contextList);
        }

        return value;
    }

    #endregion
}
