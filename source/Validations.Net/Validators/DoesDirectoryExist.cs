using System;
using System.IO;
using System.Collections.Generic;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Validates that a directory exists on the file system.
/// </summary>
public static class DoesDirectoryExist
{
    private const string ValidatorName = nameof(DoesDirectoryExist);

    /// <summary>
    /// Checks if the directory exists on the file system.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <returns>True if the directory exists; otherwise, false.</returns>
    public static bool CheckDoesDirectoryExist(this string? directoryPath)
    {
        if (string.IsNullOrWhiteSpace(directoryPath))
            return false;

        try
        {
            return Directory.Exists(directoryPath);
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the directory exists and is readable.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <returns>True if the directory exists and is readable; otherwise, false.</returns>
    public static bool CheckDoesDirectoryExistAndIsReadable(this string? directoryPath)
    {
        if (string.IsNullOrWhiteSpace(directoryPath))
            return false;

        try
        {
            if (!Directory.Exists(directoryPath))
                return false;

            // Try to enumerate the directory to check if it's accessible
            Directory.EnumerateFileSystemEntries(directoryPath).Take(1).ToList();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the directory exists and is writable.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <returns>True if the directory exists and is writable; otherwise, false.</returns>
    public static bool CheckDoesDirectoryExistAndIsWritable(this string? directoryPath)
    {
        if (string.IsNullOrWhiteSpace(directoryPath))
            return false;

        try
        {
            if (!Directory.Exists(directoryPath))
                return false;

            // Try to create a temporary file to check write permissions
            var tempFileName = Path.Combine(directoryPath, $"temp_{Guid.NewGuid()}.tmp");
            File.WriteAllText(tempFileName, "test");
            File.Delete(tempFileName);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the directory exists and is empty.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <returns>True if the directory exists and is empty; otherwise, false.</returns>
    public static bool CheckDoesDirectoryExistAndIsEmpty(this string? directoryPath)
    {
        if (string.IsNullOrWhiteSpace(directoryPath))
            return false;

        try
        {
            if (!Directory.Exists(directoryPath))
                return false;

            return !Directory.EnumerateFileSystemEntries(directoryPath).Any();
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the directory exists and contains files.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <returns>True if the directory exists and contains files; otherwise, false.</returns>
    public static bool CheckDoesDirectoryExistAndContainsFiles(this string? directoryPath)
    {
        if (string.IsNullOrWhiteSpace(directoryPath))
            return false;

        try
        {
            if (!Directory.Exists(directoryPath))
                return false;

            return Directory.EnumerateFiles(directoryPath).Any();
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Validates that the directory exists on the file system.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesDirectoryExist(this string? directoryPath, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckDoesDirectoryExist(directoryPath);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("DirectoryPath", directoryPath)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The directory must exist", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the directory exists and is readable.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesDirectoryExistAndIsReadable(this string? directoryPath, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckDoesDirectoryExistAndIsReadable(directoryPath);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("DirectoryPath", directoryPath)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The directory must exist and be readable", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the directory exists and is writable.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesDirectoryExistAndIsWritable(this string? directoryPath, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckDoesDirectoryExistAndIsWritable(directoryPath);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("DirectoryPath", directoryPath)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The directory must exist and be writable", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the directory exists and is empty.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesDirectoryExistAndIsEmpty(this string? directoryPath, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckDoesDirectoryExistAndIsEmpty(directoryPath);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("DirectoryPath", directoryPath)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The directory must exist and be empty", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the directory exists and contains files.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesDirectoryExistAndContainsFiles(this string? directoryPath, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckDoesDirectoryExistAndContainsFiles(directoryPath);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("DirectoryPath", directoryPath)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The directory must exist and contain files", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Ensures that the directory exists on the file system, throwing an exception if validation fails.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the directory does not exist.</exception>
    public static void EnsureDoesDirectoryExist(this string? directoryPath, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckDoesDirectoryExist(directoryPath);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("DirectoryPath", directoryPath)
            };
            throw ValidationException.Create(ValidatorName, "The directory must exist", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the directory exists and is readable, throwing an exception if validation fails.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the directory does not exist or is not readable.</exception>
    public static void EnsureDoesDirectoryExistAndIsReadable(this string? directoryPath, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckDoesDirectoryExistAndIsReadable(directoryPath);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("DirectoryPath", directoryPath)
            };
            throw ValidationException.Create(ValidatorName, "The directory must exist and be readable", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the directory exists and is writable, throwing an exception if validation fails.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the directory does not exist or is not writable.</exception>
    public static void EnsureDoesDirectoryExistAndIsWritable(this string? directoryPath, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckDoesDirectoryExistAndIsWritable(directoryPath);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("DirectoryPath", directoryPath)
            };
            throw ValidationException.Create(ValidatorName, "The directory must exist and be writable", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the directory exists and is empty, throwing an exception if validation fails.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the directory does not exist or is not empty.</exception>
    public static void EnsureDoesDirectoryExistAndIsEmpty(this string? directoryPath, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckDoesDirectoryExistAndIsEmpty(directoryPath);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("DirectoryPath", directoryPath)
            };
            throw ValidationException.Create(ValidatorName, "The directory must exist and be empty", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the directory exists and contains files, throwing an exception if validation fails.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the directory does not exist or does not contain files.</exception>
    public static void EnsureDoesDirectoryExistAndContainsFiles(this string? directoryPath, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckDoesDirectoryExistAndContainsFiles(directoryPath);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("DirectoryPath", directoryPath)
            };
            throw ValidationException.Create(ValidatorName, "The directory must exist and contain files", null, null, contextList);
        }
    }
}
