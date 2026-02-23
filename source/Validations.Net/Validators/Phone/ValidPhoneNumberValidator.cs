using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Phone;

public sealed class ValidPhoneNumberValidator : IValidator
{
    public static readonly ValidPhoneNumberValidator Instance = new();
    public string Name => IsValidPhoneNumber.ValidatorName;
    public string DefaultFailureMessage => IsValidPhoneNumber.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
            return s.ValidateIsValidPhoneNumber(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not a string", memberName, blackboard,
            [("value", value)]);
    }
}
