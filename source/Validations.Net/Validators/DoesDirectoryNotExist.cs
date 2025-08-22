using System;
using System.Collections.Generic;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Validates that a directory does not exist on the file system.
/// </summary>
public static class DoesDirectoryNotExist
{
    private const string ValidatorName = nameof(DoesDirectoryNotExist);

    /// <summary>
    /// Checks if the directory does not exist on the file system.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <returns>True if the directory does not exist; otherwise, false.</returns>
    public static bool CheckDoesDirectoryNotExist(this string? directoryPath)
    {
        return !DoesDirectoryExist.CheckDoesDirectoryExist(directoryPath);
    }

    /// <summary>
    /// Validates that the directory does not exist on the file system.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesDirectoryNotExist(this string? directoryPath, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckDoesDirectoryNotExist(directoryPath);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("DirectoryPath", directoryPath)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The directory must not exist", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Ensures that the directory does not exist on the file system, throwing an exception if validation fails.
    /// </summary>
    /// <param name="directoryPath">The directory path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the directory exists.</exception>
    public static void EnsureDoesDirectoryNotExist(this string? directoryPath, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckDoesDirectoryNotExist(directoryPath);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("DirectoryPath", directoryPath)
            };
            throw ValidationException.Create(ValidatorName, "The directory must not exist", null, null, contextList);
        }
    }
}
