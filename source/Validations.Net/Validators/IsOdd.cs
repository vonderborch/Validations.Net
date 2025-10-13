using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a numeric value is odd.
/// </summary>
public static class IsOdd
{
    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsOdd";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must be odd";

    /// <summary>
    ///     Checks if a numeric value is odd.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is odd; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsOdd<T>(this T value) where T : INumber<T>, IModulusOperators<T, T, T>
    {
        return value % T.CreateChecked(2) != T.Zero;
    }

    /// <summary>
    ///     Checks if a nullable numeric value is odd.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is odd; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsOdd<T>(this T? value) where T : struct, INumber<T>, IModulusOperators<T, T, T>
    {
        return value.HasValue && value.Value % T.CreateChecked(2) != T.Zero;
    }

    /// <summary>
    ///     Validates if a numeric value is odd.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the value is odd.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsOdd<T>(this T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) 
        where T : INumber<T>, IModulusOperators<T, T, T>
    {
        if (CheckIsOdd(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            validationFailureMessage,
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a nullable numeric value is odd.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the value is odd.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsOdd<T>(this T? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) 
        where T : struct, INumber<T>, IModulusOperators<T, T, T>
    {
        if (CheckIsOdd(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            validationFailureMessage,
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Ensures that a numeric value is odd.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the field being validated.</param>
    /// <returns>The original value if it is odd.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not odd.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureIsOdd<T>(this T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) 
        where T : INumber<T>, IModulusOperators<T, T, T>
    {
        var result = value.ValidateIsOdd(blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a nullable numeric value is odd.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the field being validated.</param>
    /// <returns>The original value if it is odd.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not odd.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureIsOdd<T>(this T? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) 
        where T : struct, INumber<T>, IModulusOperators<T, T, T>
    {
        var result = value.ValidateIsOdd(blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }
}
