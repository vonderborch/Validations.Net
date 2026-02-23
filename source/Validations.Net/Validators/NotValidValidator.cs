using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class NotValidValidator : IValidator
{
    public static readonly NotValidValidator Instance = new();
    public string Name => IsNotValid.ValidatorName;
    public string DefaultFailureMessage => IsNotValid.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
        => value.ValidateIsNotValid(blackboard, DefaultFailureMessage, memberName);
}
