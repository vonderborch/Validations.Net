using System;
using System.IO;
using SimpleBlackboard.Net;
using System.Collections.Generic;

namespace Validations.Net.Validators;

/// <summary>
/// Validates that a file exists on the file system.
/// </summary>
public static class DoesFileExist
{
    private const string ValidatorName = nameof(DoesFileExist);

    /// <summary>
    /// Checks if the file exists on the file system.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <returns>True if the file exists; otherwise, false.</returns>
    public static bool CheckDoesFileExist(this string? filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return false;

        try
        {
            return File.Exists(filePath);
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the file exists and is readable.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <returns>True if the file exists and is readable; otherwise, false.</returns>
    public static bool CheckDoesFileExistAndIsReadable(this string? filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return false;

        try
        {
            if (!File.Exists(filePath))
                return false;

            // Try to open the file for reading to check if it's accessible
            using var stream = File.OpenRead(filePath);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the file exists and is writable.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <returns>True if the file exists and is writable; otherwise, false.</returns>
    public static bool CheckDoesFileExistAndIsWritable(this string? filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return false;

        try
        {
            if (!File.Exists(filePath))
                return false;

            // Try to open the file for writing to check if it's accessible
            using var stream = File.OpenWrite(filePath);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the file exists and has a specific size.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="expectedSize">The expected file size in bytes.</param>
    /// <returns>True if the file exists and has the expected size; otherwise, false.</returns>
    public static bool CheckDoesFileExistWithSize(this string? filePath, long expectedSize)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return false;

        try
        {
            if (!File.Exists(filePath))
                return false;

            var fileInfo = new FileInfo(filePath);
            return fileInfo.Length == expectedSize;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the file exists and has a size greater than the specified value.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="minimumSize">The minimum file size in bytes.</param>
    /// <returns>True if the file exists and has a size greater than the minimum; otherwise, false.</returns>
    public static bool CheckDoesFileExistWithMinimumSize(this string? filePath, long minimumSize)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return false;

        try
        {
            if (!File.Exists(filePath))
                return false;

            var fileInfo = new FileInfo(filePath);
            return fileInfo.Length >= minimumSize;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Validates that the file exists on the file system.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesFileExist(this string? filePath, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckDoesFileExist(filePath);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("FilePath", filePath)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The file must exist", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the file exists and is readable.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesFileExistAndIsReadable(this string? filePath, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckDoesFileExistAndIsReadable(filePath);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("FilePath", filePath)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The file must exist and be readable", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the file exists and is writable.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesFileExistAndIsWritable(this string? filePath, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckDoesFileExistAndIsWritable(filePath);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("FilePath", filePath)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The file must exist and be writable", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the file exists and has a specific size.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="expectedSize">The expected file size in bytes.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesFileExistWithSize(this string? filePath, string fieldName, IBlackboard? blackboard, long expectedSize)
    {
        var isValid = CheckDoesFileExistWithSize(filePath, expectedSize);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("FilePath", filePath),
            ("ExpectedSize", expectedSize)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, $"The file must exist and have a size of {expectedSize} bytes", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the file exists and has a size greater than the specified value.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="minimumSize">The minimum file size in bytes.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesFileExistWithMinimumSize(this string? filePath, string fieldName, IBlackboard? blackboard, long minimumSize)
    {
        var isValid = CheckDoesFileExistWithMinimumSize(filePath, minimumSize);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("FilePath", filePath),
            ("MinimumSize", minimumSize)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, $"The file must exist and have a size of at least {minimumSize} bytes", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Ensures that the file exists on the file system, throwing an exception if validation fails.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the file does not exist.</exception>
    public static void EnsureDoesFileExist(this string? filePath, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckDoesFileExist(filePath);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("FilePath", filePath)
            };
            throw ValidationException.Create(ValidatorName, "The file must exist", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the file exists and is readable, throwing an exception if validation fails.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the file does not exist or is not readable.</exception>
    public static void EnsureDoesFileExistAndIsReadable(this string? filePath, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckDoesFileExistAndIsReadable(filePath);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("FilePath", filePath)
            };
            throw ValidationException.Create(ValidatorName, "The file must exist and be readable", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the file exists and is writable, throwing an exception if validation fails.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the file does not exist or is not writable.</exception>
    public static void EnsureDoesFileExistAndIsWritable(this string? filePath, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckDoesFileExistAndIsWritable(filePath);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("FilePath", filePath)
            };
            throw ValidationException.Create(ValidatorName, "The file must exist and be writable", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the file exists and has a specific size, throwing an exception if validation fails.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="expectedSize">The expected file size in bytes.</param>
    /// <exception cref="ValidationException">Thrown when the file does not exist or does not have the expected size.</exception>
    public static void EnsureDoesFileExistWithSize(this string? filePath, string fieldName, IBlackboard? blackboard, long expectedSize)
    {
        var isValid = CheckDoesFileExistWithSize(filePath, expectedSize);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("FilePath", filePath),
                ("ExpectedSize", expectedSize)
            };
            throw ValidationException.Create(ValidatorName, $"The file must exist and have a size of {expectedSize} bytes", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the file exists and has a size greater than the specified value, throwing an exception if validation fails.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="minimumSize">The minimum file size in bytes.</param>
    /// <exception cref="ValidationException">Thrown when the file does not exist or does not have the minimum size.</exception>
    public static void EnsureDoesFileExistWithMinimumSize(this string? filePath, string fieldName, IBlackboard? blackboard, long minimumSize)
    {
        var isValid = CheckDoesFileExistWithMinimumSize(filePath, minimumSize);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("FilePath", filePath),
                ("MinimumSize", minimumSize)
            };
            throw ValidationException.Create(ValidatorName, $"The file must exist and have a size of at least {minimumSize} bytes", null, null, contextList);
        }
    }
}
