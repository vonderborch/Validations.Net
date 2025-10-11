using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a numeric value is NOT prime.
/// </summary>
public static class IsNotPrime
{
    private const string ValidatorName = nameof(IsNotPrime);

    /// <summary>
    /// Checks if the specified integer value is NOT prime.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <returns>True if the value is NOT prime; otherwise, false.</returns>
    public static bool CheckIsNotPrime(this int value)
    {
        return !IsPrime.CheckIsPrime(value);
    }

    /// <summary>
    /// Checks if the specified nullable integer value is NOT prime.
    /// </summary>
    /// <param name="value">The nullable integer value to check.</param>
    /// <returns>True if the value is NOT prime; otherwise, false.</returns>
    public static bool CheckIsNotPrime(this int? value)
    {
        return !value.HasValue || !IsPrime.CheckIsPrime(value.Value);
    }

    /// <summary>
    /// Checks if the specified long value is NOT prime.
    /// </summary>
    /// <param name="value">The long value to check.</param>
    /// <returns>True if the value is NOT prime; otherwise, false.</returns>
    public static bool CheckIsNotPrime(this long value)
    {
        return !IsPrime.CheckIsPrime(value);
    }

    /// <summary>
    /// Checks if the specified nullable long value is NOT prime.
    /// </summary>
    /// <param name="value">The nullable long value to check.</param>
    /// <returns>True if the value is NOT prime; otherwise, false.</returns>
    public static bool CheckIsNotPrime(this long? value)
    {
        return !value.HasValue || !IsPrime.CheckIsPrime(value.Value);
    }

    /// <summary>
    /// Checks if the specified short value is NOT prime.
    /// </summary>
    /// <param name="value">The short value to check.</param>
    /// <returns>True if the value is NOT prime; otherwise, false.</returns>
    public static bool CheckIsNotPrime(this short value)
    {
        return !IsPrime.CheckIsPrime(value);
    }

    /// <summary>
    /// Checks if the specified nullable short value is NOT prime.
    /// </summary>
    /// <param name="value">The nullable short value to check.</param>
    /// <returns>True if the value is NOT prime; otherwise, false.</returns>
    public static bool CheckIsNotPrime(this short? value)
    {
        return !value.HasValue || !IsPrime.CheckIsPrime(value.Value);
    }

    /// <summary>
    /// Checks if the specified byte value is NOT prime.
    /// </summary>
    /// <param name="value">The byte value to check.</param>
    /// <returns>True if the value is NOT prime; otherwise, false.</returns>
    public static bool CheckIsNotPrime(this byte value)
    {
        return !IsPrime.CheckIsPrime(value);
    }

    /// <summary>
    /// Checks if the specified nullable byte value is NOT prime.
    /// </summary>
    /// <param name="value">The nullable byte value to check.</param>
    /// <returns>True if the value is NOT prime; otherwise, false.</returns>
    public static bool CheckIsNotPrime(this byte? value)
    {
        return !value.HasValue || !IsPrime.CheckIsPrime(value.Value);
    }

    /// <summary>
    /// Ensures that the specified integer value is NOT prime, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is prime.</exception>
    public static void EnsureIsNotPrime(this int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPrime(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"Value must NOT be prime. Actual value: {value}", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable integer value is NOT prime, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The nullable integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or prime.</exception>
    public static void EnsureIsNotPrime(this int? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", parameterName, blackboard,
                contextList);
        }

        EnsureIsNotPrime(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Ensures that the specified long value is NOT prime, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is prime.</exception>
    public static void EnsureIsNotPrime(this long value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPrime(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"Value must NOT be prime. Actual value: {value}", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable long value is NOT prime, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The nullable long value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or prime.</exception>
    public static void EnsureIsNotPrime(this long? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", parameterName, blackboard,
                contextList);
        }

        EnsureIsNotPrime(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Ensures that the specified short value is NOT prime, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The short value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is prime.</exception>
    public static void EnsureIsNotPrime(this short value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPrime(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"Value must NOT be prime. Actual value: {value}", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable short value is NOT prime, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The nullable short value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or prime.</exception>
    public static void EnsureIsNotPrime(this short? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", parameterName, blackboard,
                contextList);
        }

        EnsureIsNotPrime(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Ensures that the specified byte value is NOT prime, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The byte value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is prime.</exception>
    public static void EnsureIsNotPrime(this byte value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPrime(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"Value must NOT be prime. Actual value: {value}", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable byte value is NOT prime, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The nullable byte value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or prime.</exception>
    public static void EnsureIsNotPrime(this byte? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", parameterName, blackboard,
                contextList);
        }

        EnsureIsNotPrime(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Validates that the specified integer value is NOT prime.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT prime.</returns>
    public static ValidationResult ValidateIsNotPrime(this int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPrime(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must NOT be prime. Actual value: {value}",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified nullable integer value is NOT prime.
    /// </summary>
    /// <param name="value">The nullable integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT prime.</returns>
    public static ValidationResult ValidateIsNotPrime(this int? value, IBlackboard? blackboard = null,
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

        return ValidateIsNotPrime(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Validates that the specified long value is NOT prime.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT prime.</returns>
    public static ValidationResult ValidateIsNotPrime(this long value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPrime(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must NOT be prime. Actual value: {value}",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified nullable long value is NOT prime.
    /// </summary>
    /// <param name="value">The nullable long value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT prime.</returns>
    public static ValidationResult ValidateIsNotPrime(this long? value, IBlackboard? blackboard = null,
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

        return ValidateIsNotPrime(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Validates that the specified short value is NOT prime.
    /// </summary>
    /// <param name="value">The short value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT prime.</returns>
    public static ValidationResult ValidateIsNotPrime(this short value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPrime(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must NOT be prime. Actual value: {value}",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified nullable short value is NOT prime.
    /// </summary>
    /// <param name="value">The nullable short value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT prime.</returns>
    public static ValidationResult ValidateIsNotPrime(this short? value, IBlackboard? blackboard = null,
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

        return ValidateIsNotPrime(value.Value, blackboard, parameterName);
    }

    /// <summary>
    /// Validates that the specified byte value is NOT prime.
    /// </summary>
    /// <param name="value">The byte value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT prime.</returns>
    public static ValidationResult ValidateIsNotPrime(this byte value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotPrime(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must NOT be prime. Actual value: {value}",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified nullable byte value is NOT prime.
    /// </summary>
    /// <param name="value">The nullable byte value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT prime.</returns>
    public static ValidationResult ValidateIsNotPrime(this byte? value, IBlackboard? blackboard = null,
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

        return ValidateIsNotPrime(value.Value, blackboard, parameterName);
    }
}
