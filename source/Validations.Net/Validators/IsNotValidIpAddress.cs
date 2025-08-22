using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Validates that a string is not a valid IP address.
/// </summary>
public static class IsNotValidIpAddress
{
    private const string ValidatorName = nameof(IsNotValidIpAddress);

    /// <summary>
    ///     Checks if the string is not a valid IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is not a valid IP address; otherwise, false.</returns>
    public static bool CheckIsNotValidIpAddress(this string? value)
    {
        return !value.CheckIsValidIpAddress();
    }

    /// <summary>
    ///     Checks if the string is not a valid IPv4 address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is not a valid IPv4 address; otherwise, false.</returns>
    public static bool CheckIsNotValidIPv4Address(this string? value)
    {
        return !value.CheckIsValidIPv4Address();
    }

    /// <summary>
    ///     Checks if the string is not a valid IPv6 address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is not a valid IPv6 address; otherwise, false.</returns>
    public static bool CheckIsNotValidIPv6Address(this string? value)
    {
        return !value.CheckIsValidIPv6Address();
    }

    /// <summary>
    ///     Checks if the string is not a valid loopback IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is not a valid loopback IP address; otherwise, false.</returns>
    public static bool CheckIsNotValidLoopbackIpAddress(this string? value)
    {
        return !value.CheckIsValidLoopbackIpAddress();
    }

    /// <summary>
    ///     Checks if the string is not a valid private IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is not a valid private IP address; otherwise, false.</returns>
    public static bool CheckIsNotValidPrivateIpAddress(this string? value)
    {
        return !value.CheckIsValidPrivateIpAddress();
    }

    /// <summary>
    ///     Checks if the string is not a valid public IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is not a valid public IP address; otherwise, false.</returns>
    public static bool CheckIsNotValidPublicIpAddress(this string? value)
    {
        return !value.CheckIsValidPublicIpAddress();
    }

    /// <summary>
    ///     Ensures that the string is not a valid IP address, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid IP address.</exception>
    public static void EnsureIsNotValidIpAddress(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsNotValidIpAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid IP address", null, null,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is not a valid IPv4 address, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid IPv4 address.</exception>
    public static void EnsureIsNotValidIPv4Address(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsNotValidIPv4Address(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid IPv4 address", null, null,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is not a valid IPv6 address, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid IPv6 address.</exception>
    public static void EnsureIsNotValidIPv6Address(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsNotValidIPv6Address(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid IPv6 address", null, null,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is not a valid loopback IP address, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid loopback IP address.</exception>
    public static void EnsureIsNotValidLoopbackIpAddress(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsNotValidLoopbackIpAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid loopback IP address", null,
                null, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is not a valid private IP address, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid private IP address.</exception>
    public static void EnsureIsNotValidPrivateIpAddress(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsNotValidPrivateIpAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid private IP address", null,
                null, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is not a valid public IP address, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid public IP address.</exception>
    public static void EnsureIsNotValidPublicIpAddress(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsNotValidPublicIpAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid public IP address", null,
                null, contextList);
        }
    }

    /// <summary>
    ///     Validates that the string is not a valid IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotValidIpAddress(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsNotValidIpAddress(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must not be a valid IP address",
                fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid IPv4 address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotValidIPv4Address(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsNotValidIPv4Address(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must not be a valid IPv4 address",
                fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid IPv6 address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotValidIPv6Address(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsNotValidIPv6Address(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must not be a valid IPv6 address",
                fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid loopback IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotValidLoopbackIpAddress(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsNotValidLoopbackIpAddress(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "The value must not be a valid loopback IP address", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid private IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotValidPrivateIpAddress(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsNotValidPrivateIpAddress(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "The value must not be a valid private IP address", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid public IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotValidPublicIpAddress(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsNotValidPublicIpAddress(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "The value must not be a valid public IP address", fieldName, blackboard, contextList);
    }
}
