using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a numeric value is a power of a specified base.
/// </summary>
public static class IsPowerOf
{
    private const string ValidatorName = nameof(IsPowerOf);

    /// <summary>
    /// Checks if the specified integer value is a power of the given base.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <param name="base">The base to check against.</param>
    /// <returns>True if the value is a power of the base; otherwise, false.</returns>
    public static bool CheckIsPowerOf(int value, int @base)
    {
        if (value <= 0 || @base <= 1) return false;
        if (value == 1) return true;

        while (value > 1)
        {
            if (value % @base != 0) return false;
            value /= @base;
        }

        return value == 1;
    }

    /// <summary>
    /// Checks if the specified long value is a power of the given base.
    /// </summary>
    /// <param name="value">The long value to check.</param>
    /// <param name="base">The base to check against.</param>
    /// <returns>True if the value is a power of the base; otherwise, false.</returns>
    public static bool CheckIsPowerOf(long value, long @base)
    {
        if (value <= 0 || @base <= 1) return false;
        if (value == 1) return true;

        while (value > 1)
        {
            if (value % @base != 0) return false;
            value /= @base;
        }

        return value == 1;
    }

    /// <summary>
    /// Checks if the specified integer value is a power of 2.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <returns>True if the value is a power of 2; otherwise, false.</returns>
    public static bool CheckIsPowerOf2(int value)
    {
        return value > 0 && (value & (value - 1)) == 0;
    }

    /// <summary>
    /// Checks if the specified long value is a power of 2.
    /// </summary>
    /// <param name="value">The long value to check.</param>
    /// <returns>True if the value is a power of 2; otherwise, false.</returns>
    public static bool CheckIsPowerOf2(long value)
    {
        return value > 0 && (value & (value - 1)) == 0;
    }

    /// <summary>
    /// Checks if the specified integer value is a power of 10.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <returns>True if the value is a power of 10; otherwise, false.</returns>
    public static bool CheckIsPowerOf10(int value)
    {
        return CheckIsPowerOf(value, 10);
    }

    /// <summary>
    /// Checks if the specified long value is a power of 10.
    /// </summary>
    /// <param name="value">The long value to check.</param>
    /// <returns>True if the value is a power of 10; otherwise, false.</returns>
    public static bool CheckIsPowerOf10(long value)
    {
        return CheckIsPowerOf(value, 10);
    }

    /// <summary>
    /// Ensures that the specified integer value is a power of the given base, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="base">The base to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a power of the base.</exception>
    public static void EnsureIsPowerOf(int value, int @base, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPowerOf(value, @base);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Base", @base)
            };
            throw ValidationException.Create(ValidatorName, $"Value must be a power of {@base}.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified long value is a power of the given base, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="base">The base to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a power of the base.</exception>
    public static void EnsureIsPowerOf(long value, long @base, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPowerOf(value, @base);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Base", @base)
            };
            throw ValidationException.Create(ValidatorName, $"Value must be a power of {@base}.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified integer value is a power of 10, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a power of 10.</exception>
    public static void EnsureIsPowerOf10(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPowerOf10(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a power of 10.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified long value is a power of 10, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a power of 10.</exception>
    public static void EnsureIsPowerOf10(long value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPowerOf10(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a power of 10.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified integer value is a power of 2, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a power of 2.</exception>
    public static void EnsureIsPowerOf2(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPowerOf2(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a power of 2.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified long value is a power of 2, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a power of 2.</exception>
    public static void EnsureIsPowerOf2(long value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPowerOf2(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a power of 2.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Validates that the specified integer value is a power of the given base.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="base">The base to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is a power of the base.</returns>
    public static ValidationResult ValidateIsPowerOf(int value, int @base, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPowerOf(value, @base);
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

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must be a power of {@base}.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified long value is a power of the given base.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="base">The base to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is a power of the base.</returns>
    public static ValidationResult ValidateIsPowerOf(long value, long @base, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPowerOf(value, @base);
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

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must be a power of {@base}.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified integer value is a power of 10.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is a power of 10.</returns>
    public static ValidationResult ValidateIsPowerOf10(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPowerOf10(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a power of 10.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified long value is a power of 10.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is a power of 10.</returns>
    public static ValidationResult ValidateIsPowerOf10(long value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPowerOf10(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a power of 10.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified integer value is a power of 2.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is a power of 2.</returns>
    public static ValidationResult ValidateIsPowerOf2(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPowerOf2(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a power of 2.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified long value is a power of 2.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is a power of 2.</returns>
    public static ValidationResult ValidateIsPowerOf2(long value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPowerOf2(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a power of 2.",
            parameterName, blackboard, contextList);
    }
}
