using System;
using System.IO;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Validates that a string is a valid file system path.
/// </summary>
public static class IsValidPath
{
    private const string ValidatorName = nameof(IsValidPath);

    /// <summary>
    /// Checks if the string is a valid file system path.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="checkExists">Whether to check if the path actually exists on the file system. Default is false.</param>
    /// <returns>True if the string is a valid file system path; otherwise, false.</returns>
    public static bool CheckIsValidPath(this string? value, bool checkExists = false)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        try
        {
            // Check if the path has valid characters
            var invalidChars = Path.GetInvalidPathChars();
            if (value.IndexOfAny(invalidChars) >= 0)
                return false;

            // Check if it's a valid path format
            var fullPath = Path.GetFullPath(value);
            
            // If checkExists is true, verify the path exists
            if (checkExists)
            {
                return File.Exists(fullPath) || Directory.Exists(fullPath);
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the string is a valid absolute path.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid absolute path; otherwise, false.</returns>
    public static bool CheckIsAbsolutePath(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        try
        {
            return Path.IsPathRooted(value);
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the string is a valid relative path.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid relative path; otherwise, false.</returns>
    public static bool CheckIsRelativePath(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        try
        {
            return !Path.IsPathRooted(value);
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the string is a valid directory path.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="checkExists">Whether to check if the directory actually exists. Default is false.</param>
    /// <returns>True if the string is a valid directory path; otherwise, false.</returns>
    public static bool CheckIsValidDirectoryPath(this string? value, bool checkExists = false)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        try
        {
            var fullPath = Path.GetFullPath(value);
            
            if (checkExists)
            {
                return Directory.Exists(fullPath);
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Validates that the string is a valid file system path.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="checkExists">Whether to check if the path actually exists on the file system. Default is false.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidPath(this string? value, string fieldName, IBlackboard? blackboard, bool checkExists = false)
    {
        var isValid = CheckIsValidPath(value, checkExists);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("CheckExists", checkExists),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must be a valid file system path", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid absolute path.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsAbsolutePath(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsAbsolutePath(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must be a valid absolute path", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid relative path.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsRelativePath(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsRelativePath(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must be a valid relative path", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid directory path.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="checkExists">Whether to check if the directory actually exists. Default is false.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidDirectoryPath(this string? value, string fieldName, IBlackboard? blackboard, bool checkExists = false)
    {
        var isValid = CheckIsValidDirectoryPath(value, checkExists);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("CheckExists", checkExists),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must be a valid directory path", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Ensures that the string is a valid file system path, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="checkExists">Whether to check if the path actually exists on the file system. Default is false.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid file system path.</exception>
    public static void EnsureIsValidPath(this string? value, string fieldName, IBlackboard? blackboard, bool checkExists = false)
    {
        var isValid = CheckIsValidPath(value, checkExists);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("CheckExists", checkExists),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid file system path", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid absolute path, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid absolute path.</exception>
    public static void EnsureIsAbsolutePath(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsAbsolutePath(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid absolute path", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid relative path, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid relative path.</exception>
    public static void EnsureIsRelativePath(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsRelativePath(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid relative path", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid directory path, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="checkExists">Whether to check if the directory actually exists. Default is false.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid directory path.</exception>
    public static void EnsureIsValidDirectoryPath(this string? value, string fieldName, IBlackboard? blackboard, bool checkExists = false)
    {
        var isValid = CheckIsValidDirectoryPath(value, checkExists);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("CheckExists", checkExists),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid directory path", null, null, contextList);
        }
    }
}
