using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators.Identifiers;

/// <summary>
/// The IsNotValidGuid class provides methods for validation to ensure that
/// a string does not represent a valid GUID.
/// </summary>
public static class IsNotValidGuid
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotValidGuid";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Must not be a valid GUID";

    /// <summary>
    /// Checks if the given string is not a valid GUID.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidGuid(this string? value)
    {
        return !Guid.TryParse(value, out _);
    }

    /// <summary>
    /// Validates whether the given string is not a valid GUID.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidGuid(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNotValidGuid())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given string is not a valid GUID, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotValidGuid(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNotValidGuid(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class NotValidGuidValidator : IValidator
{
    public static readonly NotValidGuidValidator Instance = new();
    public string Name => IsNotValidGuid.ValidatorName;
    public string DefaultFailureMessage => IsNotValidGuid.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
            return s.ValidateIsNotValidGuid(blackboard, this.DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(this.Name, "Value is not a string", memberName, blackboard,
            [("value", value)]);
    }
}

/// <summary>
/// Validates that the decorated member's value is not a valid GUID string.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsNotValidGuidAttribute() : ValidationAttribute(IsNotValidGuid.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
            return s.CheckIsNotValidGuid() ? ValidationResult.CreateFromValidationSuccess() : Fail(value, memberName, blackboard);
        return ValidationResult.CreateFromValidationSuccess();
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = this.Message ?? IsNotValidGuid.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(this.Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
