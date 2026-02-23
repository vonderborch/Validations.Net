using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.FileSystem;

/// <summary>
/// The DoesDirectoryExist class provides methods for validation to ensure that
/// a directory exists at the given path.
/// </summary>
public static class DoesDirectoryExist
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "DoesDirectoryExist";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Directory must exist";

    /// <summary>
    /// Checks if the given path refers to an existing directory.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesDirectoryExist(this string? path)
    {
        return Directory.Exists(path);
    }

    /// <summary>
    /// Validates whether the given path refers to an existing directory.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateDoesDirectoryExist(this string? path, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(path))] string? parameterName = null)
    {
        if (!path.CheckDoesDirectoryExist())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", path)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given path refers to an existing directory, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureDoesDirectoryExist(this string? path, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(path))] string? parameterName = null)
    {
        var validationResult = path.ValidateDoesDirectoryExist(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return path;
    }
}

public sealed class DirectoryExistsValidator : IValidator
{
    public static readonly DirectoryExistsValidator Instance = new();
    public string Name => DoesDirectoryExist.ValidatorName;
    public string DefaultFailureMessage => DoesDirectoryExist.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string path)
            return path.ValidateDoesDirectoryExist(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not a string", memberName, blackboard,
            [("value", value)]);
    }
}

/// <summary>
/// Validates that the decorated member's value is a path to an existing directory.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateDoesDirectoryExistAttribute() : ValidationAttribute(DoesDirectoryExist.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
            return s.CheckDoesDirectoryExist() ? ValidationResult.CreateFromValidationSuccess() : Fail(value, memberName, blackboard);
        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = Message ?? DoesDirectoryExist.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
