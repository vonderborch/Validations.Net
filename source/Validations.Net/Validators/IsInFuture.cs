using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a date/time value is in the future.
/// </summary>
public static class IsInFuture
{
    private const string ValidatorName = nameof(IsInFuture);

    /// <summary>
    /// Checks if the specified DateTime value is in the future.
    /// </summary>
    /// <param name="value">The DateTime value to check.</param>
    /// <returns>True if the value is in the future; otherwise, false.</returns>
    public static bool CheckIsInFuture(this DateTime value)
    {
        return value > DateTime.Now;
    }

    /// <summary>
    /// Checks if the specified nullable DateTime value is in the future.
    /// </summary>
    /// <param name="value">The nullable DateTime value to check.</param>
    /// <returns>True if the value is in the future; otherwise, false.</returns>
    public static bool CheckIsInFuture(this DateTime? value)
    {
        return value.HasValue && CheckIsInFuture(value.Value);
    }

    /// <summary>
    /// Checks if the specified DateTimeOffset value is in the future.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to check.</param>
    /// <returns>True if the value is in the future; otherwise, false.</returns>
    public static bool CheckIsInFuture(this DateTimeOffset value)
    {
        return value > DateTimeOffset.Now;
    }

    /// <summary>
    /// Checks if the specified nullable DateTimeOffset value is in the future.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to check.</param>
    /// <returns>True if the value is in the future; otherwise, false.</returns>
    public static bool CheckIsInFuture(this DateTimeOffset? value)
    {
        return value.HasValue && CheckIsInFuture(value.Value);
    }

    /// <summary>
    /// Ensures that the specified DateTime value is in the future, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not in the future.</exception>
    public static void EnsureIsInFuture(this DateTime value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsInFuture(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("CurrentTime", DateTime.Now)
            };
            throw ValidationException.Create(ValidatorName, "Value must be in the future.",
                parameterName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable DateTime value is in the future, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or not in the future.</exception>
    public static void EnsureIsInFuture(this DateTime? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", parameterName, blackboard,
                contextList);
        }

        EnsureIsInFuture(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Ensures that the specified DateTimeOffset value is in the future, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not in the future.</exception>
    public static void EnsureIsInFuture(this DateTimeOffset value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsInFuture(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("CurrentTime", DateTimeOffset.Now)
            };
            throw ValidationException.Create(ValidatorName, "Value must be in the future.",
                parameterName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable DateTimeOffset value is in the future, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or not in the future.</exception>
    public static void EnsureIsInFuture(this DateTimeOffset? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", parameterName, blackboard,
                contextList);
        }

        EnsureIsInFuture(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Validates that the specified DateTime value is in the future.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is in the future.</returns>
    public static ValidationResult ValidateIsInFuture(this DateTime value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsInFuture(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("CurrentTime", DateTime.Now),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "Value must be in the future.",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified nullable DateTime value is in the future.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is in the future.</returns>
    public static ValidationResult ValidateIsInFuture(this DateTime? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", null),
                ("ParameterName", parameterName)
            };
            return ValidationResult.CreateFromValidationFailure(
                ValidatorName,
                "Value cannot be null.",
                parameterName,
                blackboard,
                contextList);
        }

        return ValidateIsInFuture(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Validates that the specified DateTimeOffset value is in the future.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is in the future.</returns>
    public static ValidationResult ValidateIsInFuture(this DateTimeOffset value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsInFuture(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("CurrentTime", DateTimeOffset.Now),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "Value must be in the future.",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified nullable DateTimeOffset value is in the future.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is in the future.</returns>
    public static ValidationResult ValidateIsInFuture(this DateTimeOffset? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", null),
                ("ParameterName", parameterName)
            };
            return ValidationResult.CreateFromValidationFailure(
                ValidatorName,
                "Value cannot be null.",
                parameterName,
                blackboard,
                contextList);
        }

        return ValidateIsInFuture(value.Value, blackboard, parameterName);
    }
}
