using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class CreditCardValidator : IValidator
{
    public static readonly CreditCardValidator Instance = new();
    public string Name => IsCreditCard.ValidatorName;
    public string DefaultFailureMessage => IsCreditCard.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
        => value is string s ? s.ValidateIsCreditCard(blackboard, DefaultFailureMessage, memberName)
            : ValidationResult.CreateFromValidationSuccess();
}
