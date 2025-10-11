using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a numeric value is prime.
/// </summary>
public static class IsPrime
{
    private const string ValidatorName = nameof(IsPrime);

    /// <summary>
    /// Checks if the specified integer value is prime.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <returns>True if the value is prime; otherwise, false.</returns>
    public static bool CheckIsPrime(this int value)
    {
        if (value < 2) return false;
        if (value == 2) return true;
        if (value % 2 == 0) return false;

        var sqrt = (int)Math.Sqrt(value);
        for (var i = 3; i <= sqrt; i += 2)
        {
            if (value % i == 0) return false;
        }

        return true;
    }

    /// <summary>
    /// Checks if the specified nullable integer value is prime.
    /// </summary>
    /// <param name="value">The nullable integer value to check.</param>
    /// <returns>True if the value is prime; otherwise, false.</returns>
    public static bool CheckIsPrime(this int? value)
    {
        return value.HasValue && CheckIsPrime(value.Value);
    }

    /// <summary>
    /// Checks if the specified long value is prime.
    /// </summary>
    /// <param name="value">The long value to check.</param>
    /// <returns>True if the value is prime; otherwise, false.</returns>
    public static bool CheckIsPrime(this long value)
    {
        if (value < 2) return false;
        if (value == 2) return true;
        if (value % 2 == 0) return false;

        var sqrt = (long)Math.Sqrt(value);
        for (var i = 3L; i <= sqrt; i += 2)
        {
            if (value % i == 0) return false;
        }

        return true;
    }

    /// <summary>
    /// Checks if the specified nullable long value is prime.
    /// </summary>
    /// <param name="value">The nullable long value to check.</param>
    /// <returns>True if the value is prime; otherwise, false.</returns>
    public static bool CheckIsPrime(this long? value)
    {
        return value.HasValue && CheckIsPrime(value.Value);
    }

    /// <summary>
    /// Checks if the specified short value is prime.
    /// </summary>
    /// <param name="value">The short value to check.</param>
    /// <returns>True if the value is prime; otherwise, false.</returns>
    public static bool CheckIsPrime(this short value)
    {
        return CheckIsPrime((int)value);
    }

    /// <summary>
    /// Checks if the specified nullable short value is prime.
    /// </summary>
    /// <param name="value">The nullable short value to check.</param>
    /// <returns>True if the value is prime; otherwise, false.</returns>
    public static bool CheckIsPrime(this short? value)
    {
        return value.HasValue && CheckIsPrime(value.Value);
    }

    /// <summary>
    /// Checks if the specified byte value is prime.
    /// </summary>
    /// <param name="value">The byte value to check.</param>
    /// <returns>True if the value is prime; otherwise, false.</returns>
    public static bool CheckIsPrime(this byte value)
    {
        return CheckIsPrime((int)value);
    }

    /// <summary>
    /// Checks if the specified nullable byte value is prime.
    /// </summary>
    /// <param name="value">The nullable byte value to check.</param>
    /// <returns>True if the value is prime; otherwise, false.</returns>
    public static bool CheckIsPrime(this byte? value)
    {
        return value.HasValue && CheckIsPrime(value.Value);
    }

    /// <summary>
    /// Ensures that the specified integer value is prime, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not prime.</exception>
    public static void EnsureIsPrime(this int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPrime(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"Value must be prime. Actual value: {value}", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable integer value is prime, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or not prime.</exception>
    public static void EnsureIsPrime(this int? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", parameterName, blackboard,
                contextList);
        }

        EnsureIsPrime(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Ensures that the specified long value is prime, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not prime.</exception>
    public static void EnsureIsPrime(this long value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPrime(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"Value must be prime. Actual value: {value}", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable long value is prime, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable long value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or not prime.</exception>
    public static void EnsureIsPrime(this long? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", parameterName, blackboard,
                contextList);
        }

        EnsureIsPrime(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Ensures that the specified short value is prime, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The short value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not prime.</exception>
    public static void EnsureIsPrime(this short value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPrime(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"Value must be prime. Actual value: {value}", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable short value is prime, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable short value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or not prime.</exception>
    public static void EnsureIsPrime(this short? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", parameterName, blackboard,
                contextList);
        }

        EnsureIsPrime(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Ensures that the specified byte value is prime, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The byte value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not prime.</exception>
    public static void EnsureIsPrime(this byte value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPrime(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"Value must be prime. Actual value: {value}", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable byte value is prime, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable byte value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or not prime.</exception>
    public static void EnsureIsPrime(this byte? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", parameterName, blackboard,
                contextList);
        }

        EnsureIsPrime(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Validates that the specified integer value is prime.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is prime.</returns>
    public static ValidationResult ValidateIsPrime(this int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPrime(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must be prime. Actual value: {value}",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified nullable integer value is prime.
    /// </summary>
    /// <param name="value">The nullable integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is prime.</returns>
    public static ValidationResult ValidateIsPrime(this int? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", null),
                ("ParameterName", parameterName)
            };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", parameterName,
                blackboard, contextList);
        }

        return ValidateIsPrime(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Validates that the specified long value is prime.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is prime.</returns>
    public static ValidationResult ValidateIsPrime(this long value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPrime(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must be prime. Actual value: {value}",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified nullable long value is prime.
    /// </summary>
    /// <param name="value">The nullable long value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is prime.</returns>
    public static ValidationResult ValidateIsPrime(this long? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", null),
                ("ParameterName", parameterName)
            };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", parameterName,
                blackboard, contextList);
        }

        return ValidateIsPrime(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Validates that the specified short value is prime.
    /// </summary>
    /// <param name="value">The short value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is prime.</returns>
    public static ValidationResult ValidateIsPrime(this short value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPrime(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must be prime. Actual value: {value}",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified nullable short value is prime.
    /// </summary>
    /// <param name="value">The nullable short value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is prime.</returns>
    public static ValidationResult ValidateIsPrime(this short? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", null),
                ("ParameterName", parameterName)
            };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", parameterName,
                blackboard, contextList);
        }

        return ValidateIsPrime(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Validates that the specified byte value is prime.
    /// </summary>
    /// <param name="value">The byte value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is prime.</returns>
    public static ValidationResult ValidateIsPrime(this byte value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsPrime(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must be prime. Actual value: {value}",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified nullable byte value is prime.
    /// </summary>
    /// <param name="value">The nullable byte value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is prime.</returns>
    public static ValidationResult ValidateIsPrime(this byte? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", null),
                ("ParameterName", parameterName)
            };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", parameterName,
                blackboard, contextList);
        }

        return ValidateIsPrime(value.Value, blackboard, parameterName);
    }
}
