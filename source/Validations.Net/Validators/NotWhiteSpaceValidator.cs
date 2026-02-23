using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class NotWhiteSpaceValidator : IValidator
{
    public static readonly NotWhiteSpaceValidator Instance = new();
    public string Name => IsNotWhiteSpace.ValidatorName;
    public string DefaultFailureMessage => IsNotWhiteSpace.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
        => value switch
        {
            null => ValidationResult.CreateFromValidationSuccess(),
            string s => s.ValidateIsNotWhiteSpace(blackboard, DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationSuccess()
        };
}
