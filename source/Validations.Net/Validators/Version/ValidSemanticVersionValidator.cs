using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Version;

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
