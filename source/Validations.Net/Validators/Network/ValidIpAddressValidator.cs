using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Network;

public sealed class ValidIpAddressValidator : IValidator
{
    public static readonly ValidIpAddressValidator Instance = new();
    public string Name => IsValidIpAddress.ValidatorName;
    public string DefaultFailureMessage => IsValidIpAddress.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
            return s.ValidateIsValidIpAddress(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not a string", memberName, blackboard,
            [("value", value)]);
    }
}
