using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string is a valid file name.
/// </summary>
public static class IsValidFileName
{
    private const string ValidatorName = nameof(IsValidFileName);

    /// <summary>
    /// Checks if the string is a valid file name.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="allowDirectorySeparators">Whether to allow directory separators in the name. Default is false.</param>
    /// <returns>True if the string is a valid file name; otherwise, false.</returns>
    public static bool CheckIsValidFileName(string? value, bool allowDirectorySeparators = false)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        try
        {
            var invalidChars = allowDirectorySeparators 
                ? Path.GetInvalidFileNameChars().Where(c => c != Path.DirectorySeparatorChar && c != Path.AltDirectorySeparatorChar).ToArray()
                : Path.GetInvalidFileNameChars();

            if (value.IndexOfAny(invalidChars) != -1)
                return false;

            // Check for reserved names on Windows
            var nameWithoutExtension = Path.GetFileNameWithoutExtension(value);
            var reservedNames = new[] { "CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };
            
            return !reservedNames.Contains(nameWithoutExtension, StringComparer.OrdinalIgnoreCase) &&
                   !value.EndsWith(" ") && !value.EndsWith(".");
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the string is a valid file name with an extension.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="requiredExtension">The required extension (optional).</param>
    /// <returns>True if the string is a valid file name with an extension; otherwise, false.</returns>
    public static bool CheckIsValidFileNameWithExtension(string? value, string? requiredExtension = null)
    {
        if (!CheckIsValidFileName(value))
            return false;

        if (string.IsNullOrWhiteSpace(value))
            return false;

        var extension = Path.GetExtension(value);
        if (string.IsNullOrEmpty(extension))
            return false;

        if (!string.IsNullOrEmpty(requiredExtension))
        {
            var normalizedRequired = requiredExtension.StartsWith(".") ? requiredExtension : "." + requiredExtension;
            return string.Equals(extension, normalizedRequired, StringComparison.OrdinalIgnoreCase);
        }

        return true;
    }

    /// <summary>
    /// Checks if the string is a valid file name without an extension.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid file name without an extension; otherwise, false.</returns>
    public static bool CheckIsValidFileNameWithoutExtension(string? value)
    {
        if (!CheckIsValidFileName(value))
            return false;

        if (string.IsNullOrWhiteSpace(value))
            return false;

        return string.IsNullOrEmpty(Path.GetExtension(value));
    }

    /// <summary>
    /// Ensures that the string is a valid file name, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="allowDirectorySeparators">Whether to allow directory separators in the name. Default is false.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid file name.</exception>
    public static void EnsureIsValidFileName(string? value, IBlackboard? blackboard = null, bool allowDirectorySeparators = false,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidFileName(value, allowDirectorySeparators);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("AllowDirectorySeparators", allowDirectorySeparators)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a valid file name.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid file name with an extension, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="requiredExtension">The required extension (optional).</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid file name with an extension.</exception>
    public static void EnsureIsValidFileNameWithExtension(string? value, IBlackboard? blackboard = null, string? requiredExtension = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidFileNameWithExtension(value, requiredExtension);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("RequiredExtension", requiredExtension)
            };
            var message = string.IsNullOrEmpty(requiredExtension) 
                ? "Value must be a valid file name with an extension."
                : $"Value must be a valid file name with extension '{requiredExtension}'.";
            throw ValidationException.Create(ValidatorName, message, parameterName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid file name without an extension, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid file name without an extension.</exception>
    public static void EnsureIsValidFileNameWithoutExtension(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidFileNameWithoutExtension(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a valid file name without an extension.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Validates that the string is a valid file name.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="allowDirectorySeparators">Whether to allow directory separators in the name. Default is false.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid file name.</returns>
    public static ValidationResult ValidateIsValidFileName(string? value, IBlackboard? blackboard = null, bool allowDirectorySeparators = false,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidFileName(value, allowDirectorySeparators);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("AllowDirectorySeparators", allowDirectorySeparators),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a valid file name.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid file name with an extension.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="requiredExtension">The required extension (optional).</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid file name with an extension.</returns>
    public static ValidationResult ValidateIsValidFileNameWithExtension(string? value, IBlackboard? blackboard = null, string? requiredExtension = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidFileNameWithExtension(value, requiredExtension);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("RequiredExtension", requiredExtension),
            ("ParameterName", parameterName)
        };

        var message = string.IsNullOrEmpty(requiredExtension) 
            ? "Value must be a valid file name with an extension."
            : $"Value must be a valid file name with extension '{requiredExtension}'.";

        return ValidationResult.CreateFromValidationFailure(ValidatorName, message,
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid file name without an extension.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid file name without an extension.</returns>
    public static ValidationResult ValidateIsValidFileNameWithoutExtension(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidFileNameWithoutExtension(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a valid file name without an extension.",
            parameterName, blackboard, contextList);
    }
}