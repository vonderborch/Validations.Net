using System;
using System.Collections.Generic;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a numeric value is NOT divisible by another value.
/// </summary>
public static class IsNotDivisibleBy
{
    private const string ValidatorName = nameof(IsNotDivisibleBy);

    /// <summary>
    /// Checks if the specified integer value is NOT divisible by the divisor.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <returns>True if the value is NOT divisible by the divisor; otherwise, false.</returns>
    public static bool CheckIsNotDivisibleBy(this int value, int divisor)
    {
        return divisor == 0 || value % divisor != 0;
    }

    /// <summary>
    /// Checks if the specified nullable integer value is NOT divisible by the divisor.
    /// </summary>
    /// <param name="value">The nullable integer value to check.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <returns>True if the value is NOT divisible by the divisor; otherwise, false.</returns>
    public static bool CheckIsNotDivisibleBy(this int? value, int divisor)
    {
        return value.HasValue && CheckIsNotDivisibleBy(value.Value, divisor);
    }

    /// <summary>
    /// Checks if the specified long value is NOT divisible by the divisor.
    /// </summary>
    /// <param name="value">The long value to check.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <returns>True if the value is NOT divisible by the divisor; otherwise, false.</returns>
    public static bool CheckIsNotDivisibleBy(this long value, long divisor)
    {
        return divisor == 0 || value % divisor != 0;
    }

    /// <summary>
    /// Checks if the specified nullable long value is NOT divisible by the divisor.
    /// </summary>
    /// <param name="value">The nullable long value to check.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <returns>True if the value is NOT divisible by the divisor; otherwise, false.</returns>
    public static bool CheckIsNotDivisibleBy(this long? value, long divisor)
    {
        return value.HasValue && CheckIsNotDivisibleBy(value.Value, divisor);
    }

    /// <summary>
    /// Checks if the specified short value is NOT divisible by the divisor.
    /// </summary>
    /// <param name="value">The short value to check.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <returns>True if the value is NOT divisible by the divisor; otherwise, false.</returns>
    public static bool CheckIsNotDivisibleBy(this short value, short divisor)
    {
        return divisor == 0 || value % divisor != 0;
    }

    /// <summary>
    /// Checks if the specified nullable short value is NOT divisible by the divisor.
    /// </summary>
    /// <param name="value">The nullable short value to check.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <returns>True if the value is NOT divisible by the divisor; otherwise, false.</returns>
    public static bool CheckIsNotDivisibleBy(this short? value, short divisor)
    {
        return value.HasValue && CheckIsNotDivisibleBy(value.Value, divisor);
    }

    /// <summary>
    /// Checks if the specified byte value is NOT divisible by the divisor.
    /// </summary>
    /// <param name="value">The byte value to check.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <returns>True if the value is NOT divisible by the divisor; otherwise, false.</returns>
    public static bool CheckIsNotDivisibleBy(this byte value, byte divisor)
    {
        return divisor == 0 || value % divisor != 0;
    }

    /// <summary>
    /// Checks if the specified nullable byte value is NOT divisible by the divisor.
    /// </summary>
    /// <param name="value">The nullable byte value to check.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <returns>True if the value is NOT divisible by the divisor; otherwise, false.</returns>
    public static bool CheckIsNotDivisibleBy(this byte? value, byte divisor)
    {
        return value.HasValue && CheckIsNotDivisibleBy(value.Value, divisor);
    }

    /// <summary>
    /// Validates if the specified integer value is NOT divisible by the divisor and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is NOT divisible by the divisor.</returns>
    public static ValidationResult ValidateIsNotDivisibleBy(this int value, int divisor, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotDivisibleBy(value, divisor);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Divisor", divisor)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must NOT be divisible by {divisor}.", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified nullable integer value is NOT divisible by the divisor and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable integer value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is NOT divisible by the divisor.</returns>
    public static ValidationResult ValidateIsNotDivisibleBy(this int? value, int divisor, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        return ValidateIsNotDivisibleBy(value.Value, divisor, fieldName, blackboard);
    }

    /// <summary>
    /// Validates if the specified long value is NOT divisible by the divisor and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is NOT divisible by the divisor.</returns>
    public static ValidationResult ValidateIsNotDivisibleBy(this long value, long divisor, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotDivisibleBy(value, divisor);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Divisor", divisor)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must NOT be divisible by {divisor}.", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified nullable long value is NOT divisible by the divisor and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable long value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is NOT divisible by the divisor.</returns>
    public static ValidationResult ValidateIsNotDivisibleBy(this long? value, long divisor, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        return ValidateIsNotDivisibleBy(value.Value, divisor, fieldName, blackboard);
    }

    /// <summary>
    /// Validates if the specified short value is NOT divisible by the divisor and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The short value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is NOT divisible by the divisor.</returns>
    public static ValidationResult ValidateIsNotDivisibleBy(this short value, short divisor, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotDivisibleBy(value, divisor);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Divisor", divisor)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must NOT be divisible by {divisor}.", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified nullable short value is NOT divisible by the divisor and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable short value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is NOT divisible by the divisor.</returns>
    public static ValidationResult ValidateIsNotDivisibleBy(this short? value, short divisor, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        return ValidateIsNotDivisibleBy(value.Value, divisor, fieldName, blackboard);
    }

    /// <summary>
    /// Validates if the specified byte value is NOT divisible by the divisor and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The byte value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is NOT divisible by the divisor.</returns>
    public static ValidationResult ValidateIsNotDivisibleBy(this byte value, byte divisor, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotDivisibleBy(value, divisor);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Divisor", divisor)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must NOT be divisible by {divisor}.", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified nullable byte value is NOT divisible by the divisor and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable byte value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is NOT divisible by the divisor.</returns>
    public static ValidationResult ValidateIsNotDivisibleBy(this byte? value, byte divisor, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        return ValidateIsNotDivisibleBy(value.Value, divisor, fieldName, blackboard);
    }

    /// <summary>
    /// Ensures that the specified integer value is NOT divisible by the divisor, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is divisible by the divisor.</exception>
    public static void EnsureIsNotDivisibleBy(this int value, int divisor, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotDivisibleBy(value, divisor);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Divisor", divisor)
            };
            throw ValidationException.Create(ValidatorName, $"Value must NOT be divisible by {divisor}.", fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable integer value is NOT divisible by the divisor, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The nullable integer value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or divisible by the divisor.</exception>
    public static void EnsureIsNotDivisibleBy(this int? value, int divisor, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        EnsureIsNotDivisibleBy(value.Value, divisor, fieldName, blackboard);
    }

    /// <summary>
    /// Ensures that the specified long value is NOT divisible by the divisor, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is divisible by the divisor.</exception>
    public static void EnsureIsNotDivisibleBy(this long value, long divisor, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotDivisibleBy(value, divisor);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Divisor", divisor)
            };
            throw ValidationException.Create(ValidatorName, $"Value must NOT be divisible by {divisor}.", fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable long value is NOT divisible by the divisor, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The nullable long value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or divisible by the divisor.</exception>
    public static void EnsureIsNotDivisibleBy(this long? value, long divisor, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        EnsureIsNotDivisibleBy(value.Value, divisor, fieldName, blackboard);
    }

    /// <summary>
    /// Ensures that the specified short value is NOT divisible by the divisor, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The short value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is divisible by the divisor.</exception>
    public static void EnsureIsNotDivisibleBy(this short value, short divisor, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotDivisibleBy(value, divisor);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Divisor", divisor)
            };
            throw ValidationException.Create(ValidatorName, $"Value must NOT be divisible by {divisor}.", fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable short value is NOT divisible by the divisor, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The nullable short value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or divisible by the divisor.</exception>
    public static void EnsureIsNotDivisibleBy(this short? value, short divisor, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        EnsureIsNotDivisibleBy(value.Value, divisor, fieldName, blackboard);
    }

    /// <summary>
    /// Ensures that the specified byte value is NOT divisible by the divisor, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The byte value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is divisible by the divisor.</exception>
    public static void EnsureIsNotDivisibleBy(this byte value, byte divisor, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotDivisibleBy(value, divisor);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Divisor", divisor)
            };
            throw ValidationException.Create(ValidatorName, $"Value must NOT be divisible by {divisor}.", fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified nullable byte value is NOT divisible by the divisor, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The nullable byte value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or divisible by the divisor.</exception>
    public static void EnsureIsNotDivisibleBy(this byte? value, byte divisor, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard, contextList);
        }

        EnsureIsNotDivisibleBy(value.Value, divisor, fieldName, blackboard);
    }
}
