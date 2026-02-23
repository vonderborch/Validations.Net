using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Version;

public readonly record struct CompatibleVersionParams(string TargetVersion);

public sealed class CompatibleVersionValidator : IValidator
{
    public CompatibleVersionParams Params { get; }

    public CompatibleVersionValidator(string targetVersion)
    {
        Params = new CompatibleVersionParams(targetVersion);
    }

    public string Name => IsCompatibleVersion.ValidatorName;
    public string DefaultFailureMessage => IsCompatibleVersion.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s && s.CheckIsCompatibleVersion(Params.TargetVersion))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("target", Params.TargetVersion)]);
    }
}
