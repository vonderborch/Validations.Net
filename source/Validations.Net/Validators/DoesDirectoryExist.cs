using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a directory exists.
/// </summary>
public static class DoesDirectoryExist
{
    private const string ValidatorName = nameof(DoesDirectoryExist);

    /// <summary>
    /// Checks if the directory exists.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <returns>True if the directory exists; otherwise, false.</returns>
    public static bool CheckDoesDirectoryExist(string? directoryPath)
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
    /// Checks if the directory exists and contains files.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <returns>True if the directory exists and contains files; otherwise, false.</returns>
    public static bool CheckDoesDirectoryExistAndContainsFiles(string? directoryPath)
    {
        if (!CheckDoesDirectoryExist(directoryPath))
            return false;

        try
        {
            return Directory.EnumerateFileSystemEntries(directoryPath!).Any();
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
    public static bool CheckDoesDirectoryExistAndIsEmpty(string? directoryPath)
    {
        if (!CheckDoesDirectoryExist(directoryPath))
            return false;

        try
        {
            return !Directory.EnumerateFileSystemEntries(directoryPath!).Any();
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
    public static bool CheckDoesDirectoryExistAndIsReadable(string? directoryPath)
    {
        if (!CheckDoesDirectoryExist(directoryPath))
            return false;

        try
        {
            _ = Directory.EnumerateFileSystemEntries(directoryPath!).Take(1).ToArray();
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
    public static bool CheckDoesDirectoryExistAndIsWritable(string? directoryPath)
    {
        if (!CheckDoesDirectoryExist(directoryPath))
            return false;

        try
        {
            var testFile = Path.Combine(directoryPath!, Guid.NewGuid().ToString());
            File.WriteAllText(testFile, "test");
            File.Delete(testFile);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Ensures that the directory exists, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the directory does not exist.</exception>
    public static void EnsureDoesDirectoryExist(string? directoryPath, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(directoryPath))] string? parameterName = null)
    {
        var isValid = CheckDoesDirectoryExist(directoryPath);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("DirectoryPath", directoryPath)
            };
            throw ValidationException.Create(ValidatorName, "Directory must exist.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the directory exists and contains files, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the directory does not exist or does not contain files.</exception>
    public static void EnsureDoesDirectoryExistAndContainsFiles(string? directoryPath, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(directoryPath))] string? parameterName = null)
    {
        var isValid = CheckDoesDirectoryExistAndContainsFiles(directoryPath);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("DirectoryPath", directoryPath)
            };
            throw ValidationException.Create(ValidatorName, "Directory must exist and contain files.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the directory exists and is empty, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the directory does not exist or is not empty.</exception>
    public static void EnsureDoesDirectoryExistAndIsEmpty(string? directoryPath, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(directoryPath))] string? parameterName = null)
    {
        var isValid = CheckDoesDirectoryExistAndIsEmpty(directoryPath);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("DirectoryPath", directoryPath)
            };
            throw ValidationException.Create(ValidatorName, "Directory must exist and be empty.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the directory exists and is readable, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the directory does not exist or is not readable.</exception>
    public static void EnsureDoesDirectoryExistAndIsReadable(string? directoryPath, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(directoryPath))] string? parameterName = null)
    {
        var isValid = CheckDoesDirectoryExistAndIsReadable(directoryPath);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("DirectoryPath", directoryPath)
            };
            throw ValidationException.Create(ValidatorName, "Directory must exist and be readable.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the directory exists and is writable, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the directory does not exist or is not writable.</exception>
    public static void EnsureDoesDirectoryExistAndIsWritable(string? directoryPath, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(directoryPath))] string? parameterName = null)
    {
        var isValid = CheckDoesDirectoryExistAndIsWritable(directoryPath);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("DirectoryPath", directoryPath)
            };
            throw ValidationException.Create(ValidatorName, "Directory must exist and be writable.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Validates that the directory exists.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the directory exists.</returns>
    public static ValidationResult ValidateDoesDirectoryExist(string? directoryPath, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(directoryPath))] string? parameterName = null)
    {
        var isValid = CheckDoesDirectoryExist(directoryPath);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("DirectoryPath", directoryPath),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Directory must exist.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the directory exists and contains files.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the directory exists and contains files.</returns>
    public static ValidationResult ValidateDoesDirectoryExistAndContainsFiles(string? directoryPath, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(directoryPath))] string? parameterName = null)
    {
        var isValid = CheckDoesDirectoryExistAndContainsFiles(directoryPath);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("DirectoryPath", directoryPath),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Directory must exist and contain files.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the directory exists and is empty.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the directory exists and is empty.</returns>
    public static ValidationResult ValidateDoesDirectoryExistAndIsEmpty(string? directoryPath, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(directoryPath))] string? parameterName = null)
    {
        var isValid = CheckDoesDirectoryExistAndIsEmpty(directoryPath);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("DirectoryPath", directoryPath),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Directory must exist and be empty.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the directory exists and is readable.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the directory exists and is readable.</returns>
    public static ValidationResult ValidateDoesDirectoryExistAndIsReadable(string? directoryPath, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(directoryPath))] string? parameterName = null)
    {
        var isValid = CheckDoesDirectoryExistAndIsReadable(directoryPath);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("DirectoryPath", directoryPath),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Directory must exist and be readable.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the directory exists and is writable.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the directory exists and is writable.</returns>
    public static ValidationResult ValidateDoesDirectoryExistAndIsWritable(string? directoryPath, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(directoryPath))] string? parameterName = null)
    {
        var isValid = CheckDoesDirectoryExistAndIsWritable(directoryPath);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("DirectoryPath", directoryPath),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Directory must exist and be writable.",
            parameterName, blackboard, contextList);
    }
}