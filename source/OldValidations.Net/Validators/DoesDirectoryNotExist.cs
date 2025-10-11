using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a directory does not exist.
/// </summary>
public static class DoesDirectoryNotExist
{
    private const string ValidatorName = nameof(DoesDirectoryNotExist);

    /// <summary>
    /// Checks if the directory does not exist.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <returns>True if the directory does not exist; otherwise, false.</returns>
    public static bool CheckDoesDirectoryNotExist(string? directoryPath)
    {
        return !DoesDirectoryExist.CheckDoesDirectoryExist(directoryPath);
    }

    /// <summary>
    /// Ensures that the directory does not exist, throwing a ValidationException if it does.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the directory exists.</exception>
    public static void EnsureDoesDirectoryNotExist(string? directoryPath, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(directoryPath))] string? parameterName = null)
    {
        var isValid = CheckDoesDirectoryNotExist(directoryPath);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("DirectoryPath", directoryPath)
            };
            throw ValidationException.Create(ValidatorName, "Directory must not exist.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Validates that the directory does not exist.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the directory does not exist.</returns>
    public static ValidationResult ValidateDoesDirectoryNotExist(string? directoryPath, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(directoryPath))] string? parameterName = null)
    {
        var isValid = CheckDoesDirectoryNotExist(directoryPath);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("DirectoryPath", directoryPath),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Directory must not exist.",
            parameterName, blackboard, contextList);
    }
}