using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Finance;

public sealed class ValidBicSwiftValidator : IValidator
{
    public static readonly ValidBicSwiftValidator Instance = new();
    public string Name => IsValidBicSwift.ValidatorName;
    public string DefaultFailureMessage => IsValidBicSwift.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
            return s.ValidateIsValidBicSwift(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not a string", memberName, blackboard,
            [("value", value)]);
    }
}
