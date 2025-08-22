using System;
using System.Collections.Generic;
using System.Linq;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Validates that a value is NOT a valid network port.
/// </summary>
public static class IsNotValidPort
{
    private const string ValidatorName = nameof(IsNotValidPort);

    /// <summary>
    /// Checks if the specified value is NOT a valid network port (1-65535).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a valid port; otherwise, false.</returns>
    public static bool CheckIsNotValidPort(int value)
    {
        return !IsValidPort.CheckIsValidPort(value);
    }

    /// <summary>
    /// Checks if the specified value is NOT a well-known port (0-1023).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a well-known port; otherwise, false.</returns>
    public static bool CheckIsNotWellKnownPort(int value)
    {
        return !IsValidPort.CheckIsValidWellKnownPort(value);
    }

    /// <summary>
    /// Checks if the specified value is NOT a registered port (1024-49151).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a registered port; otherwise, false.</returns>
    public static bool CheckIsNotRegisteredPort(int value)
    {
        return !IsValidPort.CheckIsValidRegisteredPort(value);
    }

    /// <summary>
    /// Checks if the specified value is NOT a dynamic or private port (49152-65535).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a dynamic or private port; otherwise, false.</returns>
    public static bool CheckIsNotDynamicOrPrivatePort(int value)
    {
        return !IsValidPort.CheckIsValidDynamicPort(value);
    }

    /// <summary>
    /// Validates that the specified value is NOT a valid network port.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is NOT a valid port.</returns>
    public static ValidationResult ValidateIsNotValidPort(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidPort(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a valid network port (1-65535).",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified value is NOT a well-known port.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is NOT a well-known port.</returns>
    public static ValidationResult ValidateIsNotWellKnownPort(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotWellKnownPort(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a well-known port (1-1023).",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified value is NOT a registered port.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is NOT a registered port.</returns>
    public static ValidationResult ValidateIsNotRegisteredPort(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotRegisteredPort(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a registered port (1024-49151).",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified value is NOT a dynamic or private port.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the value is NOT a dynamic or private port.</returns>
    public static ValidationResult ValidateIsNotDynamicOrPrivatePort(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotDynamicOrPrivatePort(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a dynamic or private port (49152-65535).",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Ensures that the specified value is NOT a valid network port.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a valid network port.</exception>
    public static void EnsureIsNotValidPort(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidPort(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a valid network port (1-65535).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified value is NOT a well-known port.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a well-known port.</exception>
    public static void EnsureIsNotWellKnownPort(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotWellKnownPort(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a well-known port (1-1023).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified value is NOT a registered port.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a registered port.</exception>
    public static void EnsureIsNotRegisteredPort(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotRegisteredPort(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a registered port (1024-49151).",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified value is NOT a dynamic or private port.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the value is a dynamic or private port.</exception>
    public static void EnsureIsNotDynamicOrPrivatePort(int value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotDynamicOrPrivatePort(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a dynamic or private port (49152-65535).",
                fieldName,
                blackboard,
                contextList);
        }
    }
}
