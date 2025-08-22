using System;
using System.Collections.Generic;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a numeric value is a prime number.
/// </summary>
public static class IsPrime
{
    private const string ValidatorName = nameof(IsPrime);

    /// <summary>
    /// Checks if the specified integer value is a prime number.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <returns>True if the value is a prime number; otherwise, false.</returns>
    public static bool CheckIsPrime(this int value)
    {
        if (value < 2) return false;
        if (value == 2) return true;
        if (value % 2 == 0) return false;

        var sqrt = (int)Math.Sqrt(value);
        for (int i = 3; i <= sqrt; i += 2)
        {
            if (value % i == 0) return false;
        }
        return true;
    }

    /// <summary>
    /// Checks if the specified nullable integer value is a prime number.
    /// </summary>
    /// <param name="value">The nullable integer value to check.</param>
    /// <returns>True if the value is a prime number; otherwise, false.</returns>
    public static bool CheckIsPrime(this int? value)
    {
        return value.HasValue && CheckIsPrime(value.Value);
    }

    /// <summary>
    /// Checks if the specified long value is a prime number.
    /// </summary>
    /// <param name="value">The long value to check.</param>
    /// <returns>True if the value is a prime number; otherwise, false.</returns>
    public static bool CheckIsPrime(this long value)
    {
        if (value < 2) return false;
        if (value == 2) return true;
        if (value % 2 == 0) return false;

        var sqrt = (long)Math.Sqrt(value);
        for (long i = 3; i <= sqrt; i += 2)
        {
            if (value % i == 0) return false;
        }
        return true;
    }

    /// <summary>
    /// Checks if the specified nullable long value is a prime number.
    /// </summary>
    /// <param name="value">The nullable long value to check.</param>
    /// <returns>True if the value is a prime number; otherwise, false.</returns>
    public static bool CheckIsPrime(this long? value)
    {
        return value.HasValue && CheckIsPrime(value.Value);
    }

    /// <summary>
    /// Checks if the specified short value is a prime number.
    /// </summary>
    /// <param name="value">The short value to check.</param>
    /// <returns>True if the value is a prime number; otherwise, false.</returns>
    public static bool CheckIsPrime(this short value)
    {
        return CheckIsPrime((int)value);
    }

    /// <summary>
    /// Checks if the specified nullable short value is a prime number.
    /// </summary>
    /// <param name="value">The nullable short value to check.</param>
    /// <returns>True if the value is a prime number; otherwise, false.</returns>
    public static bool CheckIsPrime(this short? value)
    {
        return value.HasValue && CheckIsPrime(value.Value);
    }

    /// <summary>
    /// Checks if the specified byte value is a prime number.
    /// </summary>
    /// <param name="value">The byte value to check.</param>
    /// <returns>True if the value is a prime number; otherwise, false.</returns>
    public static bool CheckIsPrime(this byte value)
    {
        return CheckIsPrime((int)value);
    }

    /// <summary>
    /// Checks if the specified nullable byte value is a prime number.
    /// </summary>
    /// <param name="value">The nullable byte value to check.</param>
    /// <returns>True if the value is a prime number; otherwise, false.</returns>
    public static bool CheckIsPrime(this byte? value)
    {
        return value.HasValue && CheckIsPrime(value.Value);
    }

    /// <summary>
    /// Validates if the specified integer value is a prime number and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is a prime number.</returns>
    public static ValidationResult ValidateIsPrime(this int value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPrime(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a prime number.", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified nullable integer value is a prime number and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is a prime number.</returns>
    public static ValidationResult ValidateIsPrime(this int? value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        return ValidateIsPrime(value.Value, fieldName, blackboard);
    }

    /// <summary>
    /// Validates if the specified long value is a prime number and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is a prime number.</returns>
    public static ValidationResult ValidateIsPrime(this long value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPrime(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a prime number.", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified nullable long value is a prime number and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable long value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is a prime number.</returns>
    public static ValidationResult ValidateIsPrime(this long? value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        return ValidateIsPrime(value.Value, fieldName, blackboard);
    }

    /// <summary>
    /// Validates if the specified short value is a prime number and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The short value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is a prime number.</returns>
    public static ValidationResult ValidateIsPrime(this short value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPrime(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a prime number.", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified nullable short value is a prime number and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable short value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is a prime number.</returns>
    public static ValidationResult ValidateIsPrime(this short? value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        return ValidateIsPrime(value.Value, fieldName, blackboard);
    }

    /// <summary>
    /// Validates if the specified byte value is a prime number and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The byte value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is a prime number.</returns>
    public static ValidationResult ValidateIsPrime(this byte value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPrime(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a prime number.", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified nullable byte value is a prime number and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable byte value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is a prime number.</returns>
    public static ValidationResult ValidateIsPrime(this byte? value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        return ValidateIsPrime(value.Value, fieldName, blackboard);
    }

    /// <summary>
    /// Ensures that the specified integer value is a prime number, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a prime number.</exception>
    public static void EnsureIsPrime(this int value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPrime(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a prime number.", fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable integer value is a prime number, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or not a prime number.</exception>
    public static void EnsureIsPrime(this int? value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        EnsureIsPrime(value.Value, fieldName, blackboard);
    }

    /// <summary>
    /// Ensures that the specified long value is a prime number, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a prime number.</exception>
    public static void EnsureIsPrime(this long value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPrime(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a prime number.", fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable long value is a prime number, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable long value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or not a prime number.</exception>
    public static void EnsureIsPrime(this long? value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        EnsureIsPrime(value.Value, fieldName, blackboard);
    }

    /// <summary>
    /// Ensures that the specified short value is a prime number, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The short value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a prime number.</exception>
    public static void EnsureIsPrime(this short value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPrime(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a prime number.", fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable short value is a prime number, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable short value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or not a prime number.</exception>
    public static void EnsureIsPrime(this short? value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        EnsureIsPrime(value.Value, fieldName, blackboard);
    }

    /// <summary>
    /// Ensures that the specified byte value is a prime number, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The byte value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a prime number.</exception>
    public static void EnsureIsPrime(this byte value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsPrime(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a prime number.", fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable byte value is a prime number, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The nullable byte value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or not a prime number.</exception>
    public static void EnsureIsPrime(this byte? value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        EnsureIsPrime(value.Value, fieldName, blackboard);
    }
}
