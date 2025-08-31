using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a numeric value is a perfect square.
/// </summary>
public static class IsPerfectSquare
{
    private const string ValidatorName = nameof(IsPerfectSquare);

    /// <summary>
    /// Checks if the specified integer value is a perfect square.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <returns>True if the value is a perfect square; otherwise, false.</returns>
    public static bool CheckIsPerfectSquare(int value)
    {
        if (value < 0) return false;
        if (value <= 1) return true;

        var sqrt = (int)Math.Sqrt(value);
        return sqrt * sqrt == value;
    }

    /// <summary>
    /// Checks if the specified long value is a perfect square.
    /// </summary>
    /// <param name="value">The long value to check.</param>
    /// <returns>True if the value is a perfect square; otherwise, false.</returns>
    public static bool CheckIsPerfectSquare(long value)
    {
        if (value < 0) return false;
        if (value <= 1) return true;

        var sqrt = (long)Math.Sqrt(value);
        return sqrt * sqrt == value;
    }

    /// <summary>
    /// Checks if the specified double value is a perfect square.
    /// </summary>
    /// <param name="value">The double value to check.</param>
    /// <returns>True if the value is a perfect square; otherwise, false.</returns>
    public static bool CheckIsPerfectSquare(double value)
    {
        if (value < 0) return false;
        if (value <= 1) return true;

        var sqrt = Math.Sqrt(value);
        return Math.Abs(sqrt - Math.Round(sqrt)) < double.Epsilon && Math.Round(sqrt) * Math.Round(sqrt) == value;
    }

    /// <summary>
    /// Ensures that the specified integer value is a perfect square, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a perfect square.</exception>
    public static void EnsureIsPerfectSquare(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPerfectSquare(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"Value must be a perfect square. Actual value: {value}", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified long value is a perfect square, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a perfect square.</exception>
    public static void EnsureIsPerfectSquare(long value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPerfectSquare(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"Value must be a perfect square. Actual value: {value}", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified double value is a perfect square, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The double value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a perfect square.</exception>
    public static void EnsureIsPerfectSquare(double value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPerfectSquare(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"Value must be a perfect square. Actual value: {value}", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Validates that the specified integer value is a perfect square.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is a perfect square.</returns>
    public static ValidationResult ValidateIsPerfectSquare(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPerfectSquare(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must be a perfect square. Actual value: {value}",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified long value is a perfect square.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is a perfect square.</returns>
    public static ValidationResult ValidateIsPerfectSquare(long value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPerfectSquare(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must be a perfect square. Actual value: {value}",
            parameterName, blackboard, contextList);
    }
}
