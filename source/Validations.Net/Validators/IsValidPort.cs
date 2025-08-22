using System;
using System.Collections.Generic;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Validates that a value is a valid network port number.
/// </summary>
public static class IsValidPort
{
    private const string ValidatorName = nameof(IsValidPort);
    private const int MinPortNumber = 0;
    private const int MaxPortNumber = 65535;
    private const int WellKnownPortsMax = 1023;
    private const int RegisteredPortsMin = 1024;
    private const int RegisteredPortsMax = 49151;
    private const int DynamicPortsMin = 49152;

    /// <summary>
    /// Checks if the value is a valid port number (0-65535).
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <returns>True if the value is a valid port number; otherwise, false.</returns>
    public static bool CheckIsValidPort(this int value)
    {
        return value >= MinPortNumber && value <= MaxPortNumber;
    }

    /// <summary>
    /// Checks if the value is a valid port number (0-65535).
    /// </summary>
    /// <param name="value">The nullable integer value to validate.</param>
    /// <returns>True if the value is a valid port number; otherwise, false.</returns>
    public static bool CheckIsValidPort(this int? value)
    {
        return value.HasValue && CheckIsValidPort(value.Value);
    }

    /// <summary>
    /// Checks if the string represents a valid port number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string represents a valid port number; otherwise, false.</returns>
    public static bool CheckIsValidPort(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return int.TryParse(value, out var port) && CheckIsValidPort(port);
    }

    /// <summary>
    /// Checks if the value is a valid well-known port number (0-1023).
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <returns>True if the value is a valid well-known port number; otherwise, false.</returns>
    public static bool CheckIsValidWellKnownPort(this int value)
    {
        return value >= MinPortNumber && value <= WellKnownPortsMax;
    }

    /// <summary>
    /// Checks if the value is a valid registered port number (1024-49151).
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <returns>True if the value is a valid registered port number; otherwise, false.</returns>
    public static bool CheckIsValidRegisteredPort(this int value)
    {
        return value >= RegisteredPortsMin && value <= RegisteredPortsMax;
    }

    /// <summary>
    /// Checks if the value is a valid dynamic/private port number (49152-65535).
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <returns>True if the value is a valid dynamic/private port number; otherwise, false.</returns>
    public static bool CheckIsValidDynamicPort(this int value)
    {
        return value >= DynamicPortsMin && value <= MaxPortNumber;
    }

    /// <summary>
    /// Checks if the value is a valid non-privileged port number (1024-65535).
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <returns>True if the value is a valid non-privileged port number; otherwise, false.</returns>
    public static bool CheckIsValidNonPrivilegedPort(this int value)
    {
        return value >= RegisteredPortsMin && value <= MaxPortNumber;
    }

    /// <summary>
    /// Checks if the value is within a specific port range.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="minPort">The minimum port number (inclusive).</param>
    /// <param name="maxPort">The maximum port number (inclusive).</param>
    /// <returns>True if the value is within the specified range; otherwise, false.</returns>
    public static bool CheckIsValidPortInRange(this int value, int minPort, int maxPort)
    {
        return value >= minPort && value <= maxPort;
    }

    /// <summary>
    /// Validates that the value is a valid port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidPort(this int value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidPort(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, $"The value must be a valid port number (0-{MaxPortNumber})", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the value is a valid port number.
    /// </summary>
    /// <param name="value">The nullable integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidPort(this int? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidPort(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, $"The value must be a valid port number (0-{MaxPortNumber})", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string represents a valid port number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidPort(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidPort(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, $"The value must be a valid port number (0-{MaxPortNumber})", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the value is a valid well-known port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidWellKnownPort(this int value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidWellKnownPort(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, $"The value must be a valid well-known port number (0-{WellKnownPortsMax})", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the value is a valid registered port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidRegisteredPort(this int value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidRegisteredPort(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, $"The value must be a valid registered port number ({RegisteredPortsMin}-{RegisteredPortsMax})", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the value is a valid dynamic/private port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidDynamicPort(this int value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidDynamicPort(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, $"The value must be a valid dynamic/private port number ({DynamicPortsMin}-{MaxPortNumber})", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the value is a valid non-privileged port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidNonPrivilegedPort(this int value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidNonPrivilegedPort(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, $"The value must be a valid non-privileged port number ({RegisteredPortsMin}-{MaxPortNumber})", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the value is within a specific port range.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="minPort">The minimum port number (inclusive).</param>
    /// <param name="maxPort">The maximum port number (inclusive).</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidPortInRange(this int value, string fieldName, IBlackboard? blackboard, int minPort, int maxPort)
    {
        var isValid = CheckIsValidPortInRange(value, minPort, maxPort);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value),
            ("MinPort", minPort),
            ("MaxPort", maxPort)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, $"The value must be a valid port number in range ({minPort}-{maxPort})", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Ensures that the value is a valid port number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a valid port number.</exception>
    public static void EnsureIsValidPort(this int value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidPort(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"The value must be a valid port number (0-{MaxPortNumber})", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the value is a valid port number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The nullable integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a valid port number.</exception>
    public static void EnsureIsValidPort(this int? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidPort(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"The value must be a valid port number (0-{MaxPortNumber})", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string represents a valid port number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string does not represent a valid port number.</exception>
    public static void EnsureIsValidPort(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidPort(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"The value must be a valid port number (0-{MaxPortNumber})", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the value is a valid well-known port number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a valid well-known port number.</exception>
    public static void EnsureIsValidWellKnownPort(this int value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidWellKnownPort(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"The value must be a valid well-known port number (0-{WellKnownPortsMax})", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the value is a valid registered port number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a valid registered port number.</exception>
    public static void EnsureIsValidRegisteredPort(this int value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidRegisteredPort(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"The value must be a valid registered port number ({RegisteredPortsMin}-{RegisteredPortsMax})", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the value is a valid dynamic/private port number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a valid dynamic/private port number.</exception>
    public static void EnsureIsValidDynamicPort(this int value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidDynamicPort(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"The value must be a valid dynamic/private port number ({DynamicPortsMin}-{MaxPortNumber})", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the value is a valid non-privileged port number, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a valid non-privileged port number.</exception>
    public static void EnsureIsValidNonPrivilegedPort(this int value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidNonPrivilegedPort(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"The value must be a valid non-privileged port number ({RegisteredPortsMin}-{MaxPortNumber})", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the value is within a specific port range, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="minPort">The minimum port number (inclusive).</param>
    /// <param name="maxPort">The maximum port number (inclusive).</param>
    /// <exception cref="ValidationException">Thrown when the value is not within the specified port range.</exception>
    public static void EnsureIsValidPortInRange(this int value, string fieldName, IBlackboard? blackboard, int minPort, int maxPort)
    {
        var isValid = CheckIsValidPortInRange(value, minPort, maxPort);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value),
                ("MinPort", minPort),
                ("MaxPort", maxPort)
            };
            throw ValidationException.Create(ValidatorName, $"The value must be a valid port number in range ({minPort}-{maxPort})", null, null, contextList);
        }
    }
}
