using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a numeric value is NOT a power of a specified base.
/// </summary>
public static class IsNotPowerOf
{
    private const string ValidatorName = nameof(IsNotPowerOf);

    /// <summary>
    /// Checks if the specified integer value is NOT a power of the given base.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <param name="base">The base to check against.</param>
    /// <returns>True if the value is NOT a power of the base; otherwise, false.</returns>
    public static bool CheckIsNotPowerOf(int value, int @base)
    {
        return !IsPowerOf.CheckIsPowerOf(value, @base);
    }

    /// <summary>
    /// Checks if the specified long value is NOT a power of the given base.
    /// </summary>
    /// <param name="value">The long value to check.</param>
    /// <param name="base">The base to check against.</param>
    /// <returns>True if the value is NOT a power of the base; otherwise, false.</returns>
    public static bool CheckIsNotPowerOf(long value, long @base)
    {
        return !IsPowerOf.CheckIsPowerOf(value, @base);
    }

    /// <summary>
    /// Checks if the specified integer value is NOT a power of 2.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <returns>True if the value is NOT a power of 2; otherwise, false.</returns>
    public static bool CheckIsNotPowerOf2(int value)
    {
        return !IsPowerOf.CheckIsPowerOf2(value);
    }

    /// <summary>
    /// Checks if the specified long value is NOT a power of 2.
    /// </summary>
    /// <param name="value">The long value to check.</param>
    /// <returns>True if the value is NOT a power of 2; otherwise, false.</returns>
    public static bool CheckIsNotPowerOf2(long value)
    {
        return !IsPowerOf.CheckIsPowerOf2(value);
    }

    /// <summary>
    /// Checks if the specified integer value is NOT a power of 10.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <returns>True if the value is NOT a power of 10; otherwise, false.</returns>
    public static bool CheckIsNotPowerOf10(int value)
    {
        return !IsPowerOf.CheckIsPowerOf10(value);
    }

    /// <summary>
    /// Checks if the specified long value is NOT a power of 10.
    /// </summary>
    /// <param name="value">The long value to check.</param>
    /// <returns>True if the value is NOT a power of 10; otherwise, false.</returns>
    public static bool CheckIsNotPowerOf10(long value)
    {
        return !IsPowerOf.CheckIsPowerOf10(value);
    }

    /// <summary>
    /// Ensures that the specified integer value is NOT a power of the given base, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="base">The base to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is a power of the base.</exception>
    public static void EnsureIsNotPowerOf(int value, int @base, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPowerOf(value, @base);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Base", @base)
            };
            throw ValidationException.Create(ValidatorName, $"Value must NOT be a power of {@base}.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified long value is NOT a power of the given base, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="base">The base to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is a power of the base.</exception>
    public static void EnsureIsNotPowerOf(long value, long @base, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPowerOf(value, @base);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Base", @base)
            };
            throw ValidationException.Create(ValidatorName, $"Value must NOT be a power of {@base}.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified integer value is NOT a power of 10, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is a power of 10.</exception>
    public static void EnsureIsNotPowerOf10(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPowerOf10(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a power of 10.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified long value is NOT a power of 10, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is a power of 10.</exception>
    public static void EnsureIsNotPowerOf10(long value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPowerOf10(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a power of 10.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified integer value is NOT a power of 2, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is a power of 2.</exception>
    public static void EnsureIsNotPowerOf2(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPowerOf2(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a power of 2.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified long value is NOT a power of 2, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is a power of 2.</exception>
    public static void EnsureIsNotPowerOf2(long value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPowerOf2(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a power of 2.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Validates that the specified integer value is NOT a power of the given base.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="base">The base to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT a power of the base.</returns>
    public static ValidationResult ValidateIsNotPowerOf(int value, int @base, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPowerOf(value, @base);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Base", @base),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must NOT be a power of {@base}.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified long value is NOT a power of the given base.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="base">The base to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT a power of the base.</returns>
    public static ValidationResult ValidateIsNotPowerOf(long value, long @base, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPowerOf(value, @base);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Base", @base),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must NOT be a power of {@base}.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified integer value is NOT a power of 10.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT a power of 10.</returns>
    public static ValidationResult ValidateIsNotPowerOf10(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPowerOf10(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a power of 10.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified long value is NOT a power of 10.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT a power of 10.</returns>
    public static ValidationResult ValidateIsNotPowerOf10(long value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPowerOf10(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a power of 10.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified integer value is NOT a power of 2.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT a power of 2.</returns>
    public static ValidationResult ValidateIsNotPowerOf2(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPowerOf2(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a power of 2.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified long value is NOT a power of 2.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT a power of 2.</returns>
    public static ValidationResult ValidateIsNotPowerOf2(long value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPowerOf2(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a power of 2.",
            parameterName, blackboard, contextList);
    }
}
