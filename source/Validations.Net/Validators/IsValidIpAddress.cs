using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string is a valid IP address.
/// </summary>
public static class IsValidIpAddress
{
    private const string ValidatorName = nameof(IsValidIpAddress);

    /// <summary>
    /// Checks if the specified string is a valid IP address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid IP address; otherwise, false.</returns>
    public static bool CheckIsValidIpAddress(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return IPAddress.TryParse(value, out _);
    }

    /// <summary>
    /// Checks if the specified string is a valid IPv4 address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid IPv4 address; otherwise, false.</returns>
    public static bool CheckIsValidIPv4Address(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!IPAddress.TryParse(value, out var ipAddress))
            return false;

        return ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork;
    }

    /// <summary>
    /// Checks if the specified string is a valid IPv6 address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid IPv6 address; otherwise, false.</returns>
    public static bool CheckIsValidIPv6Address(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!IPAddress.TryParse(value, out var ipAddress))
            return false;

        return ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6;
    }

    /// <summary>
    /// Checks if the specified string is a valid loopback IP address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid loopback IP address; otherwise, false.</returns>
    public static bool CheckIsValidLoopbackIpAddress(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!IPAddress.TryParse(value, out var ipAddress))
            return false;

        return IPAddress.IsLoopback(ipAddress);
    }

    /// <summary>
    /// Checks if the specified string is a valid private IP address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid private IP address; otherwise, false.</returns>
    public static bool CheckIsValidPrivateIpAddress(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!IPAddress.TryParse(value, out var ipAddress))
            return false;

        var bytes = ipAddress.GetAddressBytes();

        // Check for private IP ranges
        if (bytes.Length == 4) // IPv4
        {
            // 10.0.0.0 - 10.255.255.255
            if (bytes[0] == 10)
                return true;

            // 172.16.0.0 - 172.31.255.255
            if (bytes[0] == 172 && bytes[1] >= 16 && bytes[1] <= 31)
                return true;

            // 192.168.0.0 - 192.168.255.255
            if (bytes[0] == 192 && bytes[1] == 168)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if the specified string is a valid public IP address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid public IP address; otherwise, false.</returns>
    public static bool CheckIsValidPublicIpAddress(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!IPAddress.TryParse(value, out var ipAddress))
            return false;

        // Check if it's not a loopback or private address
        return !IPAddress.IsLoopback(ipAddress) && !CheckIsValidPrivateIpAddress(value);
    }

    /// <summary>
    /// Ensures that the specified string is a valid IP address, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid IP address.</exception>
    public static void EnsureIsValidIpAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidIpAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a valid IP address.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is a valid IPv4 address, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid IPv4 address.</exception>
    public static void EnsureIsValidIPv4Address(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidIPv4Address(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a valid IPv4 address.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is a valid IPv6 address, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid IPv6 address.</exception>
    public static void EnsureIsValidIPv6Address(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidIPv6Address(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a valid IPv6 address.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is a valid loopback IP address, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid loopback IP address.</exception>
    public static void EnsureIsValidLoopbackIpAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidLoopbackIpAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a valid loopback IP address.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is a valid private IP address, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid private IP address.</exception>
    public static void EnsureIsValidPrivateIpAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidPrivateIpAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a valid private IP address.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is a valid public IP address, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid public IP address.</exception>
    public static void EnsureIsValidPublicIpAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidPublicIpAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a valid public IP address.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Validates that the specified string is a valid IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid IP address.</returns>
    public static ValidationResult ValidateIsValidIpAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidIpAddress(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a valid IP address.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified string is a valid IPv4 address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid IPv4 address.</returns>
    public static ValidationResult ValidateIsValidIPv4Address(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidIPv4Address(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a valid IPv4 address.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified string is a valid IPv6 address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid IPv6 address.</returns>
    public static ValidationResult ValidateIsValidIPv6Address(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidIPv6Address(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a valid IPv6 address.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified string is a valid loopback IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid loopback IP address.</returns>
    public static ValidationResult ValidateIsValidLoopbackIpAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidLoopbackIpAddress(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a valid loopback IP address.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified string is a valid private IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid private IP address.</returns>
    public static ValidationResult ValidateIsValidPrivateIpAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidPrivateIpAddress(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a valid private IP address.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the specified string is a valid public IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid public IP address.</returns>
    public static ValidationResult ValidateIsValidPublicIpAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidPublicIpAddress(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a valid public IP address.",
            parameterName, blackboard, contextList);
    }
}
