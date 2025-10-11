using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods for checking if a type is a class.
/// </summary>
public static class IsClass
{
    private const string ValidatorName = nameof(IsClass);

    #region Check Methods

    /// <summary>
    ///     Checks if a type is a class.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>True if the type is a class; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsClass(this Type? type)
    {
        return type?.IsClass == true;
    }

    /// <summary>
    ///     Checks if an object's type is a class.
    /// </summary>
    /// <param name="value">The object to check.</param>
    /// <returns>True if the object's type is a class; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsClass(this object? value)
    {
        return value?.GetType().IsClass == true;
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates if a type is a class.
    /// </summary>
    /// <param name="type">The type to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the type is a class.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsClass(this Type? type, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(type))] string? parameterName = null)
    {
        if (CheckIsClass(type))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("type", type)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The type must be a class. Actual type: {type?.Name ?? "null"}",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if an object's type is a class.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the object's type is a class.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsClass(this object? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsClass(value))
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
            $"The object's type must be a class. Actual type: {value?.GetType().Name ?? "null"}",
            parameterName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that a type is a class.
    /// </summary>
    /// <param name="type">The type to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original type if it is a class.</returns>
    /// <exception cref="ValidationException">Thrown when the type is not a class.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Type? EnsureIsClass(this Type? type, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(type))] string? parameterName = null)
    {
        var result = ValidateIsClass(type, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return type;
    }

    /// <summary>
    ///     Ensures that an object's type is a class.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original object if its type is a class.</returns>
    /// <exception cref="ValidationException">Thrown when the object's type is not a class.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? EnsureIsClass(this object? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsClass(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    #endregion
}
