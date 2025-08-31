using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string is a valid file system path.
/// </summary>
public static class IsValidPath
{
    private const string ValidatorName = nameof(IsValidPath);

    /// <summary>
    /// Checks if the string is a valid absolute path.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid absolute path; otherwise, false.</returns>
    public static bool CheckIsAbsolutePath(string? value)
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
    public static bool CheckIsRelativePath(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        try
        {
            return !Path.IsPathRooted(value) && CheckIsValidPath(value);
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
    /// <param name="mustExist">Whether the directory must exist. Default is false.</param>
    /// <returns>True if the string is a valid directory path; otherwise, false.</returns>
    public static bool CheckIsValidDirectoryPath(string? value, bool mustExist = false)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        try
        {
            var fullPath = Path.GetFullPath(value);
            return !mustExist || Directory.Exists(fullPath);
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the string is a valid path.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="allowRelative">Whether to allow relative paths. Default is true.</param>
    /// <returns>True if the string is a valid path; otherwise, false.</returns>
    public static bool CheckIsValidPath(string? value, bool allowRelative = true)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        try
        {
            var invalidChars = Path.GetInvalidPathChars();
            if (value.IndexOfAny(invalidChars) != -1)
                return false;

            if (!allowRelative && !Path.IsPathRooted(value))
                return false;

            // Try to get the full path to validate
            _ = Path.GetFullPath(value);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Ensures that the string is a valid absolute path, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid absolute path.</exception>
    public static void EnsureIsAbsolutePath(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsAbsolutePath(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a valid absolute path.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid relative path, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid relative path.</exception>
    public static void EnsureIsRelativePath(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsRelativePath(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "Value must be a valid relative path.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid directory path, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="mustExist">Whether the directory must exist. Default is false.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid directory path.</exception>
    public static void EnsureIsValidDirectoryPath(string? value, IBlackboard? blackboard = null, bool mustExist = false,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidDirectoryPath(value, mustExist);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("MustExist", mustExist)
            };
            throw ValidationException.Create(ValidatorName, $"Value must be a valid directory path{(mustExist ? " that exists" : "")}.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the string is a valid path, throwing a ValidationException if it is not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="allowRelative">Whether to allow relative paths. Default is true.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid path.</exception>
    public static void EnsureIsValidPath(string? value, IBlackboard? blackboard = null, bool allowRelative = true,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidPath(value, allowRelative);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("AllowRelative", allowRelative)
            };
            throw ValidationException.Create(ValidatorName, $"Value must be a valid{(allowRelative ? "" : " absolute")} path.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Validates that the string is a valid absolute path.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid absolute path.</returns>
    public static ValidationResult ValidateIsAbsolutePath(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsAbsolutePath(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a valid absolute path.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid relative path.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid relative path.</returns>
    public static ValidationResult ValidateIsRelativePath(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsRelativePath(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Value must be a valid relative path.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid directory path.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="mustExist">Whether the directory must exist. Default is false.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid directory path.</returns>
    public static ValidationResult ValidateIsValidDirectoryPath(string? value, IBlackboard? blackboard = null, bool mustExist = false,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidDirectoryPath(value, mustExist);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("MustExist", mustExist),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must be a valid directory path{(mustExist ? " that exists" : "")}.",
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the string is a valid path.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="allowRelative">Whether to allow relative paths. Default is true.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the string is a valid path.</returns>
    public static ValidationResult ValidateIsValidPath(string? value, IBlackboard? blackboard = null, bool allowRelative = true,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var isValid = CheckIsValidPath(value, allowRelative);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("AllowRelative", allowRelative),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, $"Value must be a valid{(allowRelative ? "" : " absolute")} path.",
            parameterName, blackboard, contextList);
    }
}