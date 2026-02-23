using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class ValidValidator : IValidator
{
    public static readonly ValidValidator Instance = new();
    public string Name => IsValid.ValidatorName;
    public string DefaultFailureMessage => IsValid.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
        => value.ValidateIsValid(blackboard, memberName);
}
