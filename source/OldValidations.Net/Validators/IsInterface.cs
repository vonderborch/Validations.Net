using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods for checking if a type is an interface.
/// </summary>
public static class IsInterface
{
    private const string ValidatorName = nameof(IsInterface);

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
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the type is an interface.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsInterface(this Type? type, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(type))] string? parameterName = null)
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
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if an object's type is an interface.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the object's type is an interface.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsInterface(this object? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
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
            parameterName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that a type is an interface.
    /// </summary>
    /// <param name="type">The type to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original type if it is an interface.</returns>
    /// <exception cref="ValidationException">Thrown when the type is not an interface.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Type? EnsureIsInterface(this Type? type, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(type))] string? parameterName = null)
    {
        var result = ValidateIsInterface(type, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return type;
    }

    /// <summary>
    ///     Ensures that an object's type is an interface.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original object if its type is an interface.</returns>
    /// <exception cref="ValidationException">Thrown when the object's type is not an interface.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsInterface(this object? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsInterface(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    #endregion
}
