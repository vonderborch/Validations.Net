using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators.Identifiers;

/// <summary>
/// The IsValidGuid class provides methods for validation to ensure that
/// a string represents a valid GUID.
/// </summary>
public static class IsValidGuid
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsValidGuid";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Must be a valid GUID";

    /// <summary>
    /// Checks if the given string is a valid GUID.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidGuid(this string? value)
    {
        return Guid.TryParse(value, out _);
    }

    /// <summary>
    /// Validates whether the given string is a valid GUID.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidGuid(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsValidGuid())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given string is a valid GUID, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidGuid(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsValidGuid(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class ValidGuidValidator : IValidator
{
    public static readonly ValidGuidValidator Instance = new();
    public string Name => IsValidGuid.ValidatorName;
    public string DefaultFailureMessage => IsValidGuid.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
            return s.ValidateIsValidGuid(blackboard, this.DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(this.Name, "Value is not a string", memberName, blackboard,
            [("value", value)]);
    }
}

/// <summary>
/// Validates that the decorated member's value is a valid GUID string.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsValidGuidAttribute() : ValidationAttribute(IsValidGuid.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
            return s.CheckIsValidGuid() ? ValidationResult.CreateFromValidationSuccess() : Fail(value, memberName, blackboard);
        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = this.Message ?? IsValidGuid.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(this.Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
