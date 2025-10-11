using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a numeric value is divisible by another value.
/// </summary>
public static class IsDivisibleBy
{
    private const string ValidatorName = nameof(IsDivisibleBy);

    /// <summary>
    /// Checks if the specified integer value is divisible by the divisor.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <returns>True if the value is divisible by the divisor; otherwise, false.</returns>
    public static bool CheckIsDivisibleBy(this int value, int divisor)
    {
        return divisor != 0 && value % divisor == 0;
    }

    /// <summary>
    /// Checks if the specified nullable integer value is divisible by the divisor.
    /// </summary>
    /// <param name="value">The nullable integer value to check.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <returns>True if the value is divisible by the divisor; otherwise, false.</returns>
    public static bool CheckIsDivisibleBy(this int? value, int divisor)
    {
        return value.HasValue && CheckIsDivisibleBy(value.Value, divisor);
    }

    /// <summary>
    /// Checks if the specified long value is divisible by the divisor.
    /// </summary>
    /// <param name="value">The long value to check.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <returns>True if the value is divisible by the divisor; otherwise, false.</returns>
    public static bool CheckIsDivisibleBy(this long value, long divisor)
    {
        return divisor != 0 && value % divisor == 0;
    }

    /// <summary>
    /// Checks if the specified nullable long value is divisible by the divisor.
    /// </summary>
    /// <param name="value">The nullable long value to check.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <returns>True if the value is divisible by the divisor; otherwise, false.</returns>
    public static bool CheckIsDivisibleBy(this long? value, long divisor)
    {
        return value.HasValue && CheckIsDivisibleBy(value.Value, divisor);
    }

    /// <summary>
    /// Checks if the specified short value is divisible by the divisor.
    /// </summary>
    /// <param name="value">The short value to check.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <returns>True if the value is divisible by the divisor; otherwise, false.</returns>
    public static bool CheckIsDivisibleBy(this short value, short divisor)
    {
        return divisor != 0 && value % divisor == 0;
    }

    /// <summary>
    /// Checks if the specified nullable short value is divisible by the divisor.
    /// </summary>
    /// <param name="value">The nullable short value to check.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <returns>True if the value is divisible by the divisor; otherwise, false.</returns>
    public static bool CheckIsDivisibleBy(this short? value, short divisor)
    {
        return value.HasValue && CheckIsDivisibleBy(value.Value, divisor);
    }

    /// <summary>
    /// Checks if the specified byte value is divisible by the divisor.
    /// </summary>
    /// <param name="value">The byte value to check.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <returns>True if the value is divisible by the divisor; otherwise, false.</returns>
    public static bool CheckIsDivisibleBy(this byte value, byte divisor)
    {
        return divisor != 0 && value % divisor == 0;
    }

    /// <summary>
    /// Checks if the specified nullable byte value is divisible by the divisor.
    /// </summary>
    /// <param name="value">The nullable byte value to check.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <returns>True if the value is divisible by the divisor; otherwise, false.</returns>
    public static bool CheckIsDivisibleBy(this byte? value, byte divisor)
    {
        return value.HasValue && CheckIsDivisibleBy(value.Value, divisor);
    }

    /// <summary>
    /// Ensures that the specified integer value is divisible by the divisor, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not divisible by the divisor.</exception>
    public static void EnsureIsDivisibleBy(this int value, int divisor, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsDivisibleBy(value, divisor);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Divisor", divisor)
            };
            throw ValidationException.Create(ValidatorName, $"Value must be divisible by {divisor}.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable integer value is divisible by the divisor, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable integer value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or not divisible by the divisor.</exception>
    public static void EnsureIsDivisibleBy(this int? value, int divisor, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", parameterName, blackboard,
                contextList);
        }

        EnsureIsDivisibleBy(value.Value, divisor, blackboard, parameterName);
    }

    /// <summary>
    /// Ensures that the specified long value is divisible by the divisor, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not divisible by the divisor.</exception>
    public static void EnsureIsDivisibleBy(this long value, long divisor, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsDivisibleBy(value, divisor);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Divisor", divisor)
            };
            throw ValidationException.Create(ValidatorName, $"Value must be divisible by {divisor}.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable long value is divisible by the divisor, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable long value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or not divisible by the divisor.</exception>
    public static void EnsureIsDivisibleBy(this long? value, long divisor, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", parameterName, blackboard,
                contextList);
        }

        EnsureIsDivisibleBy(value.Value, divisor, blackboard, parameterName);
    }

    /// <summary>
    /// Ensures that the specified short value is divisible by the divisor, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The short value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not divisible by the divisor.</exception>
    public static void EnsureIsDivisibleBy(this short value, short divisor, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsDivisibleBy(value, divisor);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Divisor", divisor)
            };
            throw ValidationException.Create(ValidatorName, $"Value must be divisible by {divisor}.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable short value is divisible by the divisor, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable short value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or not divisible by the divisor.</exception>
    public static void EnsureIsDivisibleBy(this short? value, short divisor, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", parameterName, blackboard,
                contextList);
        }

        EnsureIsDivisibleBy(value.Value, divisor, blackboard, parameterName);
    }

    /// <summary>
    /// Ensures that the specified byte value is divisible by the divisor, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The byte value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not divisible by the divisor.</exception>
    public static void EnsureIsDivisibleBy(this byte value, byte divisor, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsDivisibleBy(value, divisor);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Divisor", divisor)
            };
            throw ValidationException.Create(ValidatorName, $"Value must be divisible by {divisor}.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable byte value is divisible by the divisor, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable byte value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or not divisible by the divisor.</exception>
    public static void EnsureIsDivisibleBy(this byte? value, byte divisor, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", parameterName, blackboard,
                contextList);
        }

        EnsureIsDivisibleBy(value.Value, divisor, blackboard, parameterName);
    }

    /// <summary>
    /// Validates that the specified integer value is divisible by the divisor.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is divisible by the divisor.</returns>
    public static ValidationResult ValidateIsDivisibleBy(this int value, int divisor, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsDivisibleBy(value, divisor);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Divisor", divisor),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must be divisible by {divisor}.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified nullable integer value is divisible by the divisor.
    /// </summary>
    /// <param name="value">The nullable integer value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is divisible by the divisor.</returns>
    public static ValidationResult ValidateIsDivisibleBy(this int? value, int divisor, IBlackboard? blackboard = null,
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

        return ValidateIsDivisibleBy(value.Value, divisor, blackboard, parameterName);
    }

    /// <summary>
    /// Validates that the specified long value is divisible by the divisor.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is divisible by the divisor.</returns>
    public static ValidationResult ValidateIsDivisibleBy(this long value, long divisor, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsDivisibleBy(value, divisor);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Divisor", divisor),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must be divisible by {divisor}.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified nullable long value is divisible by the divisor.
    /// </summary>
    /// <param name="value">The nullable long value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is divisible by the divisor.</returns>
    public static ValidationResult ValidateIsDivisibleBy(this long? value, long divisor, IBlackboard? blackboard = null,
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

        return ValidateIsDivisibleBy(value.Value, divisor, blackboard, parameterName);
    }

    /// <summary>
    /// Validates that the specified short value is divisible by the divisor.
    /// </summary>
    /// <param name="value">The short value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is divisible by the divisor.</returns>
    public static ValidationResult ValidateIsDivisibleBy(this short value, short divisor, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsDivisibleBy(value, divisor);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Divisor", divisor),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must be divisible by {divisor}.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified nullable short value is divisible by the divisor.
    /// </summary>
    /// <param name="value">The nullable short value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is divisible by the divisor.</returns>
    public static ValidationResult ValidateIsDivisibleBy(this short? value, short divisor, IBlackboard? blackboard = null,
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

        return ValidateIsDivisibleBy(value.Value, divisor, blackboard, parameterName);
    }

    /// <summary>
    /// Validates that the specified byte value is divisible by the divisor.
    /// </summary>
    /// <param name="value">The byte value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is divisible by the divisor.</returns>
    public static ValidationResult ValidateIsDivisibleBy(this byte value, byte divisor, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsDivisibleBy(value, divisor);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Divisor", divisor),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must be divisible by {divisor}.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified nullable byte value is divisible by the divisor.
    /// </summary>
    /// <param name="value">The nullable byte value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is divisible by the divisor.</returns>
    public static ValidationResult ValidateIsDivisibleBy(this byte? value, byte divisor, IBlackboard? blackboard = null,
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

        return ValidateIsDivisibleBy(value.Value, divisor, blackboard, parameterName);
    }
}
