using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a date/time value is in the past.
/// </summary>
public static class IsInPast
{
    private const string ValidatorName = nameof(IsInPast);

    /// <summary>
    /// Checks if the specified DateTime value is in the past.
    /// </summary>
    /// <param name="value">The DateTime value to check.</param>
    /// <returns>True if the value is in the past; otherwise, false.</returns>
    public static bool CheckIsInPast(this DateTime value)
    {
        return value < DateTime.Now;
    }

    /// <summary>
    /// Checks if the specified nullable DateTime value is in the past.
    /// </summary>
    /// <param name="value">The nullable DateTime value to check.</param>
    /// <returns>True if the value is in the past; otherwise, false.</returns>
    public static bool CheckIsInPast(this DateTime? value)
    {
        return value.HasValue && CheckIsInPast(value.Value);
    }

    /// <summary>
    /// Checks if the specified DateTimeOffset value is in the past.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to check.</param>
    /// <returns>True if the value is in the past; otherwise, false.</returns>
    public static bool CheckIsInPast(this DateTimeOffset value)
    {
        return value < DateTimeOffset.Now;
    }

    /// <summary>
    /// Checks if the specified nullable DateTimeOffset value is in the past.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to check.</param>
    /// <returns>True if the value is in the past; otherwise, false.</returns>
    public static bool CheckIsInPast(this DateTimeOffset? value)
    {
        return value.HasValue && CheckIsInPast(value.Value);
    }

    /// <summary>
    /// Ensures that the specified DateTime value is in the past, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not in the past.</exception>
    public static void EnsureIsInPast(this DateTime value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsInPast(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("CurrentTime", DateTime.Now)
            };
            throw ValidationException.Create(ValidatorName, "Value must be in the past.",
                parameterName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable DateTime value is in the past, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or not in the past.</exception>
    public static void EnsureIsInPast(this DateTime? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", parameterName, blackboard,
                contextList);
        }

        EnsureIsInPast(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Ensures that the specified DateTimeOffset value is in the past, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not in the past.</exception>
    public static void EnsureIsInPast(this DateTimeOffset value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsInPast(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("CurrentTime", DateTimeOffset.Now)
            };
            throw ValidationException.Create(ValidatorName, "Value must be in the past.",
                parameterName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable DateTimeOffset value is in the past, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or not in the past.</exception>
    public static void EnsureIsInPast(this DateTimeOffset? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", parameterName, blackboard,
                contextList);
        }

        EnsureIsInPast(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Validates that the specified DateTime value is in the past.
    /// </summary>
    /// <param name="value">The DateTime value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is in the past.</returns>
    public static ValidationResult ValidateIsInPast(this DateTime value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsInPast(value);
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
            "Value must be in the past.",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified nullable DateTime value is in the past.
    /// </summary>
    /// <param name="value">The nullable DateTime value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is in the past.</returns>
    public static ValidationResult ValidateIsInPast(this DateTime? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
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

        return ValidateIsInPast(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Validates that the specified DateTimeOffset value is in the past.
    /// </summary>
    /// <param name="value">The DateTimeOffset value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is in the past.</returns>
    public static ValidationResult ValidateIsInPast(this DateTimeOffset value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsInPast(value);
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
            "Value must be in the past.",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified nullable DateTimeOffset value is in the past.
    /// </summary>
    /// <param name="value">The nullable DateTimeOffset value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is in the past.</returns>
    public static ValidationResult ValidateIsInPast(this DateTimeOffset? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
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

        return ValidateIsInPast(value.Value, blackboard, parameterName);
    }
}
