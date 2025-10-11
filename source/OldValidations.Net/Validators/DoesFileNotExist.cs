using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a file does not exist.
/// </summary>
public static class DoesFileNotExist
{
    private const string ValidatorName = nameof(DoesFileNotExist);

    /// <summary>
    /// Checks if the file does not exist.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <returns>True if the file does not exist; otherwise, false.</returns>
    public static bool CheckDoesFileNotExist(string? filePath)
    {
        return !DoesFileExist.CheckDoesFileExist(filePath);
    }

    /// <summary>
    /// Ensures that the file does not exist, throwing a ValidationException if it does.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the file exists.</exception>
    public static void EnsureDoesFileNotExist(string? filePath, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(filePath))] string? parameterName = null)
    {
        var isValid = CheckDoesFileNotExist(filePath);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FilePath", filePath)
            };
            throw ValidationException.Create(ValidatorName, "File must not exist.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Validates that the file does not exist.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the file does not exist.</returns>
    public static ValidationResult ValidateDoesFileNotExist(string? filePath, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(filePath))] string? parameterName = null)
    {
        var isValid = CheckDoesFileNotExist(filePath);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("FilePath", filePath),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "File must not exist.",
            parameterName, blackboard, contextList);
    }
}