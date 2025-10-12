using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Validations.Net;
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
    /// Checks if the specified integer value is NOT a perfect square.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <returns>True if the value is NOT a perfect square; otherwise, false.</returns>
    public static bool CheckIsNotPerfectSquare(int value)
    {
        return !IsPerfectSquare.CheckIsPerfectSquare(value);
    }

    /// <summary>
    /// Checks if the specified long value is NOT a perfect square.
    /// </summary>
    /// <param name="value">The long value to check.</param>
    /// <returns>True if the value is NOT a perfect square; otherwise, false.</returns>
    public static bool CheckIsNotPerfectSquare(long value)
    {
        return !IsPerfectSquare.CheckIsPerfectSquare(value);
    }

    /// <summary>
    /// Checks if the specified double value is NOT a perfect square.
    /// </summary>
    /// <param name="value">The double value to check.</param>
    /// <returns>True if the value is NOT a perfect square; otherwise, false.</returns>
    public static bool CheckIsNotPerfectSquare(double value)
    {
        return !IsPerfectSquare.CheckIsPerfectSquare(value);
    }

    /// <summary>
    /// Ensures that the specified integer value is NOT a perfect square, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is a perfect square.</exception>
    public static void EnsureIsNotPerfectSquare(int value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPerfectSquare(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"Value must NOT be a perfect square. Actual value: {value}", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified long value is NOT a perfect square, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is a perfect square.</exception>
    public static void EnsureIsNotPerfectSquare(long value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPerfectSquare(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"Value must NOT be a perfect square. Actual value: {value}", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified double value is NOT a perfect square, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The double value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is a perfect square.</exception>
    public static void EnsureIsNotPerfectSquare(double value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPerfectSquare(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"Value must NOT be a perfect square. Actual value: {value}", parameterName,
                blackboard, contextList);
        }
    }
}
