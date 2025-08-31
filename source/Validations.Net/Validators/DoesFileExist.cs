using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a file exists.
/// </summary>
public static class DoesFileExist
{
    private const string ValidatorName = nameof(DoesFileExist);

    /// <summary>
    /// Checks if the file exists.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <returns>True if the file exists; otherwise, false.</returns>
    public static bool CheckDoesFileExist(string? filePath)
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
    public static bool CheckDoesFileExistAndIsReadable(string? filePath)
    {
        if (!CheckDoesFileExist(filePath))
            return false;

        try
        {
            using var stream = File.OpenRead(filePath!);
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
    public static bool CheckDoesFileExistAndIsWritable(string? filePath)
    {
        if (!CheckDoesFileExist(filePath))
            return false;

        try
        {
            using var stream = File.OpenWrite(filePath!);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the file exists and has at least the minimum size.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="minimumSize">The minimum size in bytes.</param>
    /// <returns>True if the file exists and has at least the minimum size; otherwise, false.</returns>
    public static bool CheckDoesFileExistWithMinimumSize(string? filePath, long minimumSize)
    {
        if (!CheckDoesFileExist(filePath))
            return false;

        try
        {
            var fileInfo = new FileInfo(filePath!);
            return fileInfo.Length >= minimumSize;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the file exists and has the exact size.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="expectedSize">The expected size in bytes.</param>
    /// <returns>True if the file exists and has the exact size; otherwise, false.</returns>
    public static bool CheckDoesFileExistWithSize(string? filePath, long expectedSize)
    {
        if (!CheckDoesFileExist(filePath))
            return false;

        try
        {
            var fileInfo = new FileInfo(filePath!);
            return fileInfo.Length == expectedSize;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Ensures that the file exists, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the file does not exist.</exception>
    public static void EnsureDoesFileExist(string? filePath, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(filePath))] string? parameterName = null)
    {
        var isValid = CheckDoesFileExist(filePath);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FilePath", filePath)
            };
            throw ValidationException.Create(ValidatorName, "File must exist.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the file exists and is readable, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the file does not exist or is not readable.</exception>
    public static void EnsureDoesFileExistAndIsReadable(string? filePath, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(filePath))] string? parameterName = null)
    {
        var isValid = CheckDoesFileExistAndIsReadable(filePath);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FilePath", filePath)
            };
            throw ValidationException.Create(ValidatorName, "File must exist and be readable.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the file exists and is writable, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the file does not exist or is not writable.</exception>
    public static void EnsureDoesFileExistAndIsWritable(string? filePath, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(filePath))] string? parameterName = null)
    {
        var isValid = CheckDoesFileExistAndIsWritable(filePath);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FilePath", filePath)
            };
            throw ValidationException.Create(ValidatorName, "File must exist and be writable.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the file exists and has at least the minimum size, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="minimumSize">The minimum size in bytes.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the file does not exist or does not have the minimum size.</exception>
    public static void EnsureDoesFileExistWithMinimumSize(string? filePath, long minimumSize, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(filePath))] string? parameterName = null)
    {
        var isValid = CheckDoesFileExistWithMinimumSize(filePath, minimumSize);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FilePath", filePath),
                ("MinimumSize", minimumSize)
            };
            throw ValidationException.Create(ValidatorName, $"File must exist and have at least {minimumSize} bytes.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the file exists and has the exact size, throwing a ValidationException if it does not.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="expectedSize">The expected size in bytes.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the file does not exist or does not have the exact size.</exception>
    public static void EnsureDoesFileExistWithSize(string? filePath, long expectedSize, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(filePath))] string? parameterName = null)
    {
        var isValid = CheckDoesFileExistWithSize(filePath, expectedSize);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FilePath", filePath),
                ("ExpectedSize", expectedSize)
            };
            throw ValidationException.Create(ValidatorName, $"File must exist and have exactly {expectedSize} bytes.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Validates that the file exists.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the file exists.</returns>
    public static ValidationResult ValidateDoesFileExist(string? filePath, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(filePath))] string? parameterName = null)
    {
        var isValid = CheckDoesFileExist(filePath);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("FilePath", filePath),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "File must exist.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the file exists and is readable.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the file exists and is readable.</returns>
    public static ValidationResult ValidateDoesFileExistAndIsReadable(string? filePath, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(filePath))] string? parameterName = null)
    {
        var isValid = CheckDoesFileExistAndIsReadable(filePath);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("FilePath", filePath),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "File must exist and be readable.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the file exists and is writable.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the file exists and is writable.</returns>
    public static ValidationResult ValidateDoesFileExistAndIsWritable(string? filePath, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(filePath))] string? parameterName = null)
    {
        var isValid = CheckDoesFileExistAndIsWritable(filePath);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("FilePath", filePath),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "File must exist and be writable.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the file exists and has at least the minimum size.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="minimumSize">The minimum size in bytes.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the file exists and has at least the minimum size.</returns>
    public static ValidationResult ValidateDoesFileExistWithMinimumSize(string? filePath, long minimumSize, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(filePath))] string? parameterName = null)
    {
        var isValid = CheckDoesFileExistWithMinimumSize(filePath, minimumSize);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("FilePath", filePath),
            ("MinimumSize", minimumSize),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"File must exist and have at least {minimumSize} bytes.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the file exists and has the exact size.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="expectedSize">The expected size in bytes.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the file exists and has the exact size.</returns>
    public static ValidationResult ValidateDoesFileExistWithSize(string? filePath, long expectedSize, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(filePath))] string? parameterName = null)
    {
        var isValid = CheckDoesFileExistWithSize(filePath, expectedSize);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("FilePath", filePath),
            ("ExpectedSize", expectedSize),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"File must exist and have exactly {expectedSize} bytes.",
            parameterName, blackboard, contextList);
    }
}