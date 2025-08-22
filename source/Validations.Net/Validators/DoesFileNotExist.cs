using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Validates that a file does not exist on the file system.
/// </summary>
public static class DoesFileNotExist
{
    private const string ValidatorName = nameof(DoesFileNotExist);

    /// <summary>
    ///     Checks if the file does not exist on the file system.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <returns>True if the file does not exist; otherwise, false.</returns>
    public static bool CheckDoesFileNotExist(this string? filePath)
    {
        return !filePath.CheckDoesFileExist();
    }

    /// <summary>
    ///     Ensures that the file does not exist on the file system, throwing an exception if validation fails.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the file exists.</exception>
    public static void EnsureDoesFileNotExist(this string? filePath, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckDoesFileNotExist(filePath);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("FilePath", filePath)
            };
            throw ValidationException.Create(ValidatorName, "The file must not exist", null, null, contextList);
        }
    }

    /// <summary>
    ///     Validates that the file does not exist on the file system.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesFileNotExist(this string? filePath, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckDoesFileNotExist(filePath);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("FilePath", filePath)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The file must not exist", fieldName,
                blackboard, contextList);
    }
}
