using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.FileSystem;

/// <summary>
/// The DoesDirectoryNotExist class provides methods for validation to ensure that
/// a directory does not exist at the given path.
/// </summary>
public static class DoesDirectoryNotExist
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "DoesDirectoryNotExist";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Directory must not exist";

    /// <summary>
    /// Checks if the given path does not refer to an existing directory.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesDirectoryNotExist(this string? path)
    {
        return !Directory.Exists(path);
    }

    /// <summary>
    /// Validates whether the given path does not refer to an existing directory.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesDirectoryNotExist(this string? path, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(path))] string? parameterName = null)
    {
        if (!path.CheckDoesDirectoryNotExist())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", path)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given path does not refer to an existing directory, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureDoesDirectoryNotExist(this string? path, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(path))] string? parameterName = null)
    {
        var validationResult = path.ValidateDoesDirectoryNotExist(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return path;
    }
}
