using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators.Identifiers;

/// <summary>
/// The IsValidId class provides methods for validation to ensure that
/// a string represents a valid identifier with configurable format constraints.
/// </summary>
public static class IsValidId
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsValidId";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Must be a valid identifier";

    /// <summary>
    ///     Default regex pattern: alphanumeric, hyphen, and underscore characters only.
    /// </summary>
    public const string DefaultAllowedPattern = @"^[a-zA-Z0-9_-]+$";

    /// <summary>
    /// Checks if the given string is a valid identifier.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="minLength">Minimum length (default 1).</param>
    /// <param name="maxLength">Maximum length (default 255).</param>
    /// <param name="allowedPattern">Regex pattern for allowed characters (default: alphanumeric + hyphen + underscore).</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidId(this string? value, int minLength = 1, int maxLength = 255, string? allowedPattern = null)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        if (value.Length < minLength || value.Length > maxLength)
            return false;

        var pattern = allowedPattern ?? DefaultAllowedPattern;
        return Regex.IsMatch(value, pattern);
    }

    /// <summary>
    /// Validates whether the given string is a valid identifier.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidId(this string? value, int minLength = 1, int maxLength = 255, string? allowedPattern = null,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsValidId(minLength, maxLength, allowedPattern))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("minLength", minLength), ("maxLength", maxLength)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given string is a valid identifier, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidId(this string? value, int minLength = 1, int maxLength = 255, string? allowedPattern = null,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsValidId(minLength, maxLength, allowedPattern, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public readonly record struct ValidIdParams(int MinLength = 1, int MaxLength = 255, string? AllowedPattern = null);

public sealed class ValidIdValidator : IValidator
{
    public ValidIdParams Params { get; }

    public ValidIdValidator(int minLength = 1, int maxLength = 255, string? allowedPattern = null)
    {
        this.Params = new ValidIdParams(minLength, maxLength, allowedPattern);
    }

    public string Name => IsValidId.ValidatorName;
    public string DefaultFailureMessage => IsValidId.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s && s.CheckIsValidId(this.Params.MinLength, this.Params.MaxLength, this.Params.AllowedPattern))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("minLength", this.Params.MinLength), ("maxLength", this.Params.MaxLength)]);
    }
}

/// <summary>
/// Validates that the decorated member's value is a valid identifier string.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsValidIdAttribute(int minLength = 1, int maxLength = 255) : ValidationAttribute(IsValidId.ValidatorName)
{
    private readonly int _minLength = minLength;
    private readonly int _maxLength = maxLength;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s && s.CheckIsValidId(this._minLength, this._maxLength))
            return ValidationResult.CreateFromValidationSuccess();
        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = this.Message ?? IsValidId.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(this.Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("minLength", this._minLength), ("maxLength", this._maxLength) });
    }
}
