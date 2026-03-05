using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators.Version;

/// <summary>
/// The IsCompatibleVersion class provides methods for validation to ensure that
/// two semantic version strings are compatible (same major version, second's minor >= first's minor).
/// </summary>
public static class IsCompatibleVersion
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsCompatibleVersion";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Version must be compatible with the target version";

    /// <summary>
    /// Checks if the given value (first version) is compatible with the target version (second).
    /// Compatible means: same major version, and target's minor >= value's minor.
    /// </summary>
    /// <param name="value">The version being validated (e.g. required minimum version).</param>
    /// <param name="target">The target version to compare against (e.g. actual version).</param>
    /// <returns>True if compatible; false otherwise.</returns>
    public static bool CheckIsCompatibleVersion(this string? value, string? target)
    {
        if (value is null || target is null)
            return false;

        if (!TryParseMajorMinor(value, out var valueMajor, out var valueMinor))
            return false;

        if (!TryParseMajorMinor(target, out var targetMajor, out var targetMinor))
            return false;

        return valueMajor == targetMajor && targetMinor >= valueMinor;
    }

    private static bool TryParseMajorMinor(string version, out int major, out int minor)
    {
        major = 0;
        minor = 0;

        var parts = version.Split('.');
        if (parts.Length < 2)
            return false;

        if (!int.TryParse(parts[0], out major) || major < 0)
            return false;

        if (!int.TryParse(parts[1], out minor) || minor < 0)
            return false;

        return true;
    }

    /// <summary>
    /// Validates whether the given value is compatible with the target version.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsCompatibleVersion(this string? value, string? target, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsCompatibleVersion(target))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("target", target)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is compatible with the target version, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsCompatibleVersion(this string? value, string? target, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsCompatibleVersion(target, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public readonly record struct CompatibleVersionParams(string TargetVersion);

public sealed class CompatibleVersionValidator : IValidator
{
    public CompatibleVersionParams Params { get; }

    public CompatibleVersionValidator(string targetVersion)
    {
        this.Params = new CompatibleVersionParams(targetVersion);
    }

    public string Name => IsCompatibleVersion.ValidatorName;
    public string DefaultFailureMessage => IsCompatibleVersion.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s && s.CheckIsCompatibleVersion(this.Params.TargetVersion))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("target", this.Params.TargetVersion)]);
    }
}

/// <summary>
/// Validates that the decorated member's version string is compatible with the specified target version.
/// Compatible means: same major version, and target's minor >= value's minor.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsCompatibleVersionAttribute(string targetVersion) : ValidationAttribute(IsCompatibleVersion.ValidatorName)
{
    public string TargetVersion { get; } = targetVersion;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        var versionStr = value as string;
        if (versionStr.CheckIsCompatibleVersion(this.TargetVersion))
            return ValidationResult.CreateFromValidationSuccess();

        var message = this.Message ?? IsCompatibleVersion.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(this.Name, message, memberName, blackboard,
            [("value", value), ("target", this.TargetVersion)]);
    }
}
