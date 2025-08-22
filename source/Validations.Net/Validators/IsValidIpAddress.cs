using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Validates that a string is a valid IP address.
/// </summary>
public static class IsValidIpAddress
{
    private const string ValidatorName = nameof(IsValidIpAddress);

    // IPv4 regex pattern
    private static readonly Regex IPv4Pattern = new(
        @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
        RegexOptions.Compiled
    );

    // IPv6 regex pattern (simplified)
    private static readonly Regex IPv6Pattern = new(
        @"^(?:[0-9a-fA-F]{1,4}:){7}[0-9a-fA-F]{1,4}$|^::1$|^::$|^(?:[0-9a-fA-F]{1,4}:)*::(?:[0-9a-fA-F]{1,4}:)*[0-9a-fA-F]{1,4}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    /// <summary>
    /// Checks if the string is a valid IP address (IPv4 or IPv6).
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid IP address; otherwise, false.</returns>
    public static bool CheckIsValidIpAddress(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return IPAddress.TryParse(value, out _);
    }

    /// <summary>
    /// Checks if the string is a valid IPv4 address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid IPv4 address; otherwise, false.</returns>
    public static bool CheckIsValidIPv4Address(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!IPv4Pattern.IsMatch(value))
            return false;

        return IPAddress.TryParse(value, out var address) && address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork;
    }

    /// <summary>
    /// Checks if the string is a valid IPv6 address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid IPv6 address; otherwise, false.</returns>
    public static bool CheckIsValidIPv6Address(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return IPAddress.TryParse(value, out var address) && address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6;
    }

    /// <summary>
    /// Checks if the string is a valid private IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid private IP address; otherwise, false.</returns>
    public static bool CheckIsValidPrivateIpAddress(this string? value)
    {
        if (!CheckIsValidIPv4Address(value))
            return false;

        var address = IPAddress.Parse(value!);
        var bytes = address.GetAddressBytes();

        // Check for private IPv4 ranges:
        // 10.0.0.0/8 (10.0.0.0 - 10.255.255.255)
        // 172.16.0.0/12 (172.16.0.0 - 172.31.255.255)
        // 192.168.0.0/16 (192.168.0.0 - 192.168.255.255)
        return (bytes[0] == 10) ||
               (bytes[0] == 172 && bytes[1] >= 16 && bytes[1] <= 31) ||
               (bytes[0] == 192 && bytes[1] == 168);
    }

    /// <summary>
    /// Checks if the string is a valid public IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid public IP address; otherwise, false.</returns>
    public static bool CheckIsValidPublicIpAddress(this string? value)
    {
        if (!CheckIsValidIpAddress(value))
            return false;

        // Check if it's not a private, loopback, or multicast address
        var address = IPAddress.Parse(value!);
        
        if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        {
            var bytes = address.GetAddressBytes();
            
            // Check for private ranges
            if (CheckIsValidPrivateIpAddress(value))
                return false;
                
            // Check for loopback (127.0.0.0/8)
            if (bytes[0] == 127)
                return false;
                
            // Check for link-local (169.254.0.0/16)
            if (bytes[0] == 169 && bytes[1] == 254)
                return false;
                
            // Check for multicast (224.0.0.0/4)
            if (bytes[0] >= 224 && bytes[0] <= 239)
                return false;
                
            // Check for reserved ranges
            if (bytes[0] == 0 || bytes[0] >= 240)
                return false;
        }

        return true;
    }

    /// <summary>
    /// Checks if the string is a valid loopback IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid loopback IP address; otherwise, false.</returns>
    public static bool CheckIsValidLoopbackIpAddress(this string? value)
    {
        if (!CheckIsValidIpAddress(value))
            return false;

        var address = IPAddress.Parse(value!);
        return IPAddress.IsLoopback(address);
    }

    /// <summary>
    /// Validates that the string is a valid IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidIpAddress(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidIpAddress(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must be a valid IP address", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid IPv4 address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidIPv4Address(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidIPv4Address(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must be a valid IPv4 address", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid IPv6 address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidIPv6Address(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidIPv6Address(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must be a valid IPv6 address", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid private IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidPrivateIpAddress(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidPrivateIpAddress(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must be a valid private IP address", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid public IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidPublicIpAddress(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidPublicIpAddress(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must be a valid public IP address", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid loopback IP address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidLoopbackIpAddress(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidLoopbackIpAddress(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must be a valid loopback IP address", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Ensures that the string is a valid IP address, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid IP address.</exception>
    public static void EnsureIsValidIpAddress(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidIpAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid IP address", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid IPv4 address, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid IPv4 address.</exception>
    public static void EnsureIsValidIPv4Address(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidIPv4Address(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid IPv4 address", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid IPv6 address, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid IPv6 address.</exception>
    public static void EnsureIsValidIPv6Address(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidIPv6Address(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid IPv6 address", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid private IP address, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid private IP address.</exception>
    public static void EnsureIsValidPrivateIpAddress(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidPrivateIpAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid private IP address", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid public IP address, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid public IP address.</exception>
    public static void EnsureIsValidPublicIpAddress(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidPublicIpAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid public IP address", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid loopback IP address, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid loopback IP address.</exception>
    public static void EnsureIsValidLoopbackIpAddress(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidLoopbackIpAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid loopback IP address", null, null, contextList);
        }
    }
}
