using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net.Validators.DataFormat;

namespace Validations.Net.Validators.DataFormat;

/// <summary>
/// The IsNotValidJson class provides methods for validation to ensure that
/// a string does not represent valid JSON.
/// </summary>
public static class IsNotValidJson
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotValidJson";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Must not be valid JSON";

    /// <summary>
    /// Checks if the given value is not valid JSON.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidJson(this string? value)
    {
        return !value.CheckIsValidJson();
    }

    /// <summary>
    /// Validates whether the given value is not valid JSON.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidJson(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsNotValidJson())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is not valid JSON, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotValidJson(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsNotValidJson(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class NotValidJsonValidator : IValidator
{
    public static readonly NotValidJsonValidator Instance = new();
    public string Name => IsNotValidJson.ValidatorName;
    public string DefaultFailureMessage => IsNotValidJson.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
            return s.ValidateIsNotValidJson(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not a string", memberName, blackboard,
            [("value", value)]);
    }
}

/// <summary>
/// Validates that the decorated member's value is not valid JSON.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsNotValidJsonAttribute() : ValidationAttribute(IsNotValidJson.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
            return s.CheckIsNotValidJson() ? ValidationResult.CreateFromValidationSuccess() : Fail(s, memberName, blackboard);

        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = Message ?? IsNotValidJson.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard, [("value", value)]);
    }
}
