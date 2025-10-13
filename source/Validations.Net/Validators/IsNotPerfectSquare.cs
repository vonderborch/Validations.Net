using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a numeric value is NOT a perfect square.
/// </summary>
public static class IsNotPerfectSquare
{
    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotPerfectSquare";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not be a perfect square";

    /// <summary>
    /// Checks if the specified numeric value is NOT a perfect square.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a perfect square; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotPerfectSquare<T>(T value) where T : INumber<T>, IRootFunctions<T>
    {
        return !IsPerfectSquare.CheckIsPerfectSquare(value);
    }

    /// <summary>
    /// Validates if the specified numeric value is NOT a perfect square.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the value is NOT a perfect square.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotPerfectSquare<T>(T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : INumber<T>, IRootFunctions<T>
    {
        if (CheckIsNotPerfectSquare(value))
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
    /// Ensures that the specified numeric value is NOT a perfect square, throwing a ValidationException if it is.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is a perfect square.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void EnsureIsNotPerfectSquare<T>(T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : INumber<T>, IRootFunctions<T>
    {
        var result = ValidateIsNotPerfectSquare(value, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }
    }
}
