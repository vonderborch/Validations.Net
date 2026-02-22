using SimpleBlackboard.Net;
using Validations.Net.Validators.Version;

namespace Validations.Net.ValidationAttributes.Version;

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
        if (versionStr.CheckIsCompatibleVersion(TargetVersion))
            return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsCompatibleVersion.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            [("value", value), ("target", TargetVersion)]);
    }
}
