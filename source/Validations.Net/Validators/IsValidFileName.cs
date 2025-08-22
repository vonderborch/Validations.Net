using System;
using System.IO;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Validates that a string is a valid file name.
/// </summary>
public static class IsValidFileName
{
    private const string ValidatorName = nameof(IsValidFileName);

    /// <summary>
    /// Checks if the string is a valid file name.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="checkExists">Whether to check if the file actually exists. Default is false.</param>
    /// <returns>True if the string is a valid file name; otherwise, false.</returns>
    public static bool CheckIsValidFileName(this string? value, bool checkExists = false)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        try
        {
            // Check if the file name has valid characters
            var invalidChars = Path.GetInvalidFileNameChars();
            if (value.IndexOfAny(invalidChars) >= 0)
                return false;

            // Check if it's not a reserved name (like CON, PRN, etc.)
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(value);
            if (IsReservedName(fileNameWithoutExtension))
                return false;

            // If checkExists is true, verify the file exists
            if (checkExists)
            {
                return File.Exists(value);
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the string is a valid file name with a specific extension.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="allowedExtensions">The allowed file extensions (without the dot).</param>
    /// <returns>True if the string is a valid file name with an allowed extension; otherwise, false.</returns>
    public static bool CheckIsValidFileNameWithExtension(this string? value, params string[] allowedExtensions)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!CheckIsValidFileName(value))
            return false;

        if (allowedExtensions == null || allowedExtensions.Length == 0)
            return true;

        var extension = Path.GetExtension(value);
        if (string.IsNullOrEmpty(extension))
            return false;

        // Remove the dot from the extension
        extension = extension.Substring(1);

        return Array.Exists(allowedExtensions, ext => string.Equals(ext, extension, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Checks if the string is a valid file name without extension.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid file name without extension; otherwise, false.</returns>
    public static bool CheckIsValidFileNameWithoutExtension(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        try
        {
            // Check if the file name has valid characters
            var invalidChars = Path.GetInvalidFileNameChars();
            if (value.IndexOfAny(invalidChars) >= 0)
                return false;

            // Check if it's not a reserved name
            return !IsReservedName(value);
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if a file name is a reserved system name.
    /// </summary>
    /// <param name="fileName">The file name to check.</param>
    /// <returns>True if the file name is a reserved system name; otherwise, false.</returns>
    private static bool IsReservedName(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return false;

        var reservedNames = new[]
        {
            "CON", "PRN", "AUX", "NUL",
            "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
            "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
        };

        return Array.Exists(reservedNames, name => string.Equals(fileName, name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Validates that the string is a valid file name.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="checkExists">Whether to check if the file actually exists. Default is false.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidFileName(this string? value, string fieldName, IBlackboard? blackboard, bool checkExists = false)
    {
        var isValid = CheckIsValidFileName(value, checkExists);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("CheckExists", checkExists),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must be a valid file name", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid file name with a specific extension.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="allowedExtensions">The allowed file extensions (without the dot).</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidFileNameWithExtension(this string? value, string fieldName, IBlackboard? blackboard, params string[] allowedExtensions)
    {
        var isValid = CheckIsValidFileNameWithExtension(value, allowedExtensions);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("AllowedExtensions", allowedExtensions),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must be a valid file name with an allowed extension", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid file name without extension.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidFileNameWithoutExtension(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidFileNameWithoutExtension(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must be a valid file name without extension", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Ensures that the string is a valid file name, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="checkExists">Whether to check if the file actually exists. Default is false.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid file name.</exception>
    public static void EnsureIsValidFileName(this string? value, string fieldName, IBlackboard? blackboard, bool checkExists = false)
    {
        var isValid = CheckIsValidFileName(value, checkExists);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("CheckExists", checkExists),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid file name", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid file name with a specific extension, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="allowedExtensions">The allowed file extensions (without the dot).</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid file name with an allowed extension.</exception>
    public static void EnsureIsValidFileNameWithExtension(this string? value, string fieldName, IBlackboard? blackboard, params string[] allowedExtensions)
    {
        var isValid = CheckIsValidFileNameWithExtension(value, allowedExtensions);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("AllowedExtensions", allowedExtensions),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid file name with an allowed extension", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid file name without extension, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid file name without extension.</exception>
    public static void EnsureIsValidFileNameWithoutExtension(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidFileNameWithoutExtension(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid file name without extension", null, null, contextList);
        }
    }
}
