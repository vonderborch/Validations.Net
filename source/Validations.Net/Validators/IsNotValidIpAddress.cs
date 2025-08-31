using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string is NOT a valid IP address.
/// </summary>
public static class IsNotValidIpAddress
{
    private const string ValidatorName = nameof(IsNotValidIpAddress);

    /// <summary>
    /// Checks if the specified string is NOT a valid IP address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT a valid IP address; otherwise, false.</returns>
    public static bool CheckIsNotValidIpAddress(string? value)
    {
        return !IsValidIpAddress.CheckIsValidIpAddress(value);
    }

    /// <summary>
    /// Checks if the specified string is NOT a valid IPv4 address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT a valid IPv4 address; otherwise, false.</returns>
    public static bool CheckIsNotValidIPv4Address(string? value)
    {
        return !IsValidIpAddress.CheckIsValidIPv4Address(value);
    }

    /// <summary>
    /// Checks if the specified string is NOT a valid IPv6 address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT a valid IPv6 address; otherwise, false.</returns>
    public static bool CheckIsNotValidIPv6Address(string? value)
    {
        return !IsValidIpAddress.CheckIsValidIPv6Address(value);
    }

    /// <summary>
    /// Checks if the specified string is NOT a valid loopback IP address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT a valid loopback IP address; otherwise, false.</returns>
    public static bool CheckIsNotValidLoopbackIpAddress(string? value)
    {
        return !IsValidIpAddress.CheckIsValidLoopbackIpAddress(value);
    }

    /// <summary>
    /// Checks if the specified string is NOT a valid private IP address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT a valid private IP address; otherwise, false.</returns>
    public static bool CheckIsNotValidPrivateIpAddress(string? value)
    {
        return !IsValidIpAddress.CheckIsValidPrivateIpAddress(value);
    }

    /// <summary>
    /// Checks if the specified string is NOT a valid public IP address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT a valid public IP address; otherwise, false.</returns>
    public static bool CheckIsNotValidPublicIpAddress(string? value)
    {
        return !IsValidIpAddress.CheckIsValidPublicIpAddress(value);
    }

    /// <summary>
    /// Ensures that the specified string is NOT a valid IP address, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid IP address.</exception>
    public static void EnsureIsNotValidIpAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotValidIpAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a valid IP address.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is NOT a valid IPv4 address, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid IPv4 address.</exception>
    public static void EnsureIsNotValidIPv4Address(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotValidIPv4Address(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a valid IPv4 address.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is NOT a valid IPv6 address, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid IPv6 address.</exception>
    public static void EnsureIsNotValidIPv6Address(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotValidIPv6Address(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a valid IPv6 address.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is NOT a valid loopback IP address, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid loopback IP address.</exception>
    public static void EnsureIsNotValidLoopbackIpAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotValidLoopbackIpAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a valid loopback IP address.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is NOT a valid private IP address, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid private IP address.</exception>
    public static void EnsureIsNotValidPrivateIpAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotValidPrivateIpAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a valid private IP address.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is NOT a valid public IP address, throwing a ValidationException if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid public IP address.</exception>
    public static void EnsureIsNotValidPublicIpAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotValidPublicIpAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must NOT be a valid public IP address.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Validates that the specified string is NOT a valid IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a valid IP address.</returns>
    public static ValidationResult ValidateIsNotValidIpAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotValidIpAddress(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a valid IP address.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified string is NOT a valid IPv4 address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a valid IPv4 address.</returns>
    public static ValidationResult ValidateIsNotValidIPv4Address(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotValidIPv4Address(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a valid IPv4 address.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified string is NOT a valid IPv6 address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a valid IPv6 address.</returns>
    public static ValidationResult ValidateIsNotValidIPv6Address(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotValidIPv6Address(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a valid IPv6 address.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified string is NOT a valid loopback IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a valid loopback IP address.</returns>
    public static ValidationResult ValidateIsNotValidLoopbackIpAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotValidLoopbackIpAddress(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a valid loopback IP address.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified string is NOT a valid private IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a valid private IP address.</returns>
    public static ValidationResult ValidateIsNotValidPrivateIpAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotValidPrivateIpAddress(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a valid private IP address.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified string is NOT a valid public IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is NOT a valid public IP address.</returns>
    public static ValidationResult ValidateIsNotValidPublicIpAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsNotValidPublicIpAddress(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must NOT be a valid public IP address.",
            parameterName, blackboard, contextList);
    }
}