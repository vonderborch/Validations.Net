using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a numeric value is NOT a prime number.
/// </summary>
public static class IsNotPrime
{
    private const string ValidatorName = nameof(IsNotPrime);

    /// <summary>
    ///     Checks if the specified integer value is NOT a prime number.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <returns>True if the value is NOT a prime number; otherwise, false.</returns>
    public static bool CheckIsNotPrime(this int value)
    {
        return !value.CheckIsPrime();
    }

    /// <summary>
    ///     Checks if the specified nullable integer value is NOT a prime number.
    /// </summary>
    /// <param name="value">The nullable integer value to check.</param>
    /// <returns>True if the value is NOT a prime number; otherwise, false.</returns>
    public static bool CheckIsNotPrime(this int? value)
    {
        return value.HasValue && CheckIsNotPrime(value.Value);
    }

    /// <summary>
    ///     Checks if the specified long value is NOT a prime number.
    /// </summary>
    /// <param name="value">The long value to check.</param>
    /// <returns>True if the value is NOT a prime number; otherwise, false.</returns>
    public static bool CheckIsNotPrime(this long value)
    {
        return !value.CheckIsPrime();
    }

    /// <summary>
    ///     Checks if the specified nullable long value is NOT a prime number.
    /// </summary>
    /// <param name="value">The nullable long value to check.</param>
    /// <returns>True if the value is NOT a prime number; otherwise, false.</returns>
    public static bool CheckIsNotPrime(this long? value)
    {
        return value.HasValue && CheckIsNotPrime(value.Value);
    }

    /// <summary>
    ///     Checks if the specified short value is NOT a prime number.
    /// </summary>
    /// <param name="value">The short value to check.</param>
    /// <returns>True if the value is NOT a prime number; otherwise, false.</returns>
    public static bool CheckIsNotPrime(this short value)
    {
        return !value.CheckIsPrime();
    }

    /// <summary>
    ///     Checks if the specified nullable short value is NOT a prime number.
    /// </summary>
    /// <param name="value">The nullable short value to check.</param>
    /// <returns>True if the value is NOT a prime number; otherwise, false.</returns>
    public static bool CheckIsNotPrime(this short? value)
    {
        return value.HasValue && CheckIsNotPrime(value.Value);
    }

    /// <summary>
    ///     Checks if the specified byte value is NOT a prime number.
    /// </summary>
    /// <param name="value">The byte value to check.</param>
    /// <returns>True if the value is NOT a prime number; otherwise, false.</returns>
    public static bool CheckIsNotPrime(this byte value)
    {
        return !value.CheckIsPrime();
    }

    /// <summary>
    ///     Checks if the specified nullable byte value is NOT a prime number.
    /// </summary>
    /// <param name="value">The nullable byte value to check.</param>
    /// <returns>True if the value is NOT a prime number; otherwise, false.</returns>
    public static bool CheckIsNotPrime(this byte? value)
    {
        return value.HasValue && CheckIsNotPrime(value.Value);
    }

    /// <summary>
    ///     Ensures that the specified integer value is NOT a prime number, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a prime number.</exception>
    public static void EnsureIsNotPrime(this int value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPrime(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a prime number.", fieldName, blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified nullable integer value is NOT a prime number, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The nullable integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or a prime number.</exception>
    public static void EnsureIsNotPrime(this int? value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard,
                contextList);
        }

        EnsureIsNotPrime(value.Value, fieldName, blackboard);
    }

    /// <summary>
    ///     Ensures that the specified long value is NOT a prime number, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a prime number.</exception>
    public static void EnsureIsNotPrime(this long value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPrime(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a prime number.", fieldName, blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified nullable long value is NOT a prime number, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The nullable long value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or a prime number.</exception>
    public static void EnsureIsNotPrime(this long? value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard,
                contextList);
        }

        EnsureIsNotPrime(value.Value, fieldName, blackboard);
    }

    /// <summary>
    ///     Ensures that the specified short value is NOT a prime number, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The short value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a prime number.</exception>
    public static void EnsureIsNotPrime(this short value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPrime(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a prime number.", fieldName, blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified nullable short value is NOT a prime number, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The nullable short value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or a prime number.</exception>
    public static void EnsureIsNotPrime(this short? value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard,
                contextList);
        }

        EnsureIsNotPrime(value.Value, fieldName, blackboard);
    }

    /// <summary>
    ///     Ensures that the specified byte value is NOT a prime number, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The byte value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a prime number.</exception>
    public static void EnsureIsNotPrime(this byte value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPrime(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a prime number.", fieldName, blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified nullable byte value is NOT a prime number, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The nullable byte value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or a prime number.</exception>
    public static void EnsureIsNotPrime(this byte? value, string? fieldName = null, IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            throw ValidationException.Create(ValidatorName, "Value cannot be null.", fieldName, blackboard,
                contextList);
        }

        EnsureIsNotPrime(value.Value, fieldName, blackboard);
    }

    /// <summary>
    ///     Validates if the specified integer value is NOT a prime number and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is NOT a prime number.</returns>
    public static ValidationResult ValidateIsNotPrime(this int value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPrime(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a prime number.",
                fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates if the specified nullable integer value is NOT a prime number and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is NOT a prime number.</returns>
    public static ValidationResult ValidateIsNotPrime(this int? value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName,
                blackboard, contextList);
        }

        return ValidateIsNotPrime(value.Value, fieldName, blackboard);
    }

    /// <summary>
    ///     Validates if the specified long value is NOT a prime number and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The long value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is NOT a prime number.</returns>
    public static ValidationResult ValidateIsNotPrime(this long value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPrime(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a prime number.",
                fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates if the specified nullable long value is NOT a prime number and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable long value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is NOT a prime number.</returns>
    public static ValidationResult ValidateIsNotPrime(this long? value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName,
                blackboard, contextList);
        }

        return ValidateIsNotPrime(value.Value, fieldName, blackboard);
    }

    /// <summary>
    ///     Validates if the specified short value is NOT a prime number and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The short value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is NOT a prime number.</returns>
    public static ValidationResult ValidateIsNotPrime(this short value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPrime(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a prime number.",
                fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates if the specified nullable short value is NOT a prime number and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable short value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is NOT a prime number.</returns>
    public static ValidationResult ValidateIsNotPrime(this short? value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName,
                blackboard, contextList);
        }

        return ValidateIsNotPrime(value.Value, fieldName, blackboard);
    }

    /// <summary>
    ///     Validates if the specified byte value is NOT a prime number and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The byte value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is NOT a prime number.</returns>
    public static ValidationResult ValidateIsNotPrime(this byte value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotPrime(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a prime number.",
                fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates if the specified nullable byte value is NOT a prime number and returns a ValidationResult.
    /// </summary>
    /// <param name="value">The nullable byte value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the value is NOT a prime number.</returns>
    public static ValidationResult ValidateIsNotPrime(this byte? value, string? fieldName = null,
        IBlackboard? blackboard = null)
    {
        if (!value.HasValue)
        {
            var contextList = new List<(string, object?)> { ("Value", null) };
            return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value cannot be null.", fieldName,
                blackboard, contextList);
        }

        return ValidateIsNotPrime(value.Value, fieldName, blackboard);
    }
}
