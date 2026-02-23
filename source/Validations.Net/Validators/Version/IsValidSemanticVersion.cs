using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Version;

/// <summary>
/// The IsValidSemanticVersion class provides methods for validation to ensure that
/// a string represents a valid semantic version (major.minor.patch with optional pre-release and build metadata).
/// </summary>
public static class IsValidSemanticVersion
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsValidSemanticVersion";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Must be a valid semantic version";

    /// <summary>
    /// Checks if the given string is a valid semantic version.
    /// Format: major.minor.patch with optional -pre.release and +build.metadata
    /// </summary>
    public static bool CheckIsValidSemanticVersion(this string? value)
    {
        if (value is null || value.Length == 0)
            return false;

        int i = 0;
        if (!TryParseNonNegativeInt(value, ref i, out _) || i >= value.Length || value[i++] != '.')
            return false;
        if (!TryParseNonNegativeInt(value, ref i, out _) || i >= value.Length || value[i++] != '.')
            return false;
        if (!TryParseNonNegativeInt(value, ref i, out _))
            return false;

        if (i < value.Length && value[i] == '-')
        {
            i++;
            if (i >= value.Length)
                return false;
            if (!TryParsePreReleaseOrBuild(value, ref i, true))
                return false;
        }

        if (i < value.Length && value[i] == '+')
        {
            i++;
            if (i >= value.Length)
                return false;
            if (!TryParsePreReleaseOrBuild(value, ref i, false))
                return false;
        }

        return i == value.Length;
    }

    private static bool TryParseNonNegativeInt(string s, ref int i, out int result)
    {
        result = 0;
        if (i >= s.Length || !char.IsDigit(s[i]))
            return false;

        // Leading zeros are forbidden per SemVer spec (e.g. "01" is invalid, "0" is valid)
        if (s[i] == '0')
        {
            if (i + 1 < s.Length && char.IsDigit(s[i + 1]))
                return false;
            i++;
            return true;
        }

        while (i < s.Length && char.IsDigit(s[i]))
        {
            result = result * 10 + (s[i] - '0');
            i++;
        }
        return true;
    }

    private static bool TryParsePreReleaseOrBuild(string s, ref int i, bool isPreRelease)
    {
        while (i < s.Length)
        {
            char c = s[i];
            if (c == '+' && isPreRelease)
                return true;
            if (c == '.' && i + 1 < s.Length && (char.IsLetterOrDigit(s[i + 1]) || s[i + 1] == '-'))
            {
                i++;
                continue;
            }
            if (char.IsLetterOrDigit(c) || c == '-')
            {
                i++;
                continue;
            }
            return false;
        }
        return true;
    }

    /// <summary>
    /// Validates whether the given string is a valid semantic version.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidSemanticVersion(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsValidSemanticVersion())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given string is a valid semantic version, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidSemanticVersion(this string? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsValidSemanticVersion(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public sealed class ValidSemanticVersionValidator : IValidator
{
    public static readonly ValidSemanticVersionValidator Instance = new();
    public string Name => IsValidSemanticVersion.ValidatorName;
    public string DefaultFailureMessage => IsValidSemanticVersion.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
            return s.ValidateIsValidSemanticVersion(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not a string", memberName, blackboard,
            [("value", value)]);
    }
}

/// <summary>
/// Validates that the decorated member's value is a valid semantic version string.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsValidSemanticVersionAttribute() : ValidationAttribute(IsValidSemanticVersion.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
            return s.CheckIsValidSemanticVersion() ? ValidationResult.CreateFromValidationSuccess() : Fail(value, memberName, blackboard);
        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = Message ?? IsValidSemanticVersion.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
