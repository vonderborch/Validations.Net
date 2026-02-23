using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class WhiteSpaceValidator : IValidator
{
    public static readonly WhiteSpaceValidator Instance = new();
    public string Name => IsWhiteSpace.ValidatorName;
    public string DefaultFailureMessage => IsWhiteSpace.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
        => value is string s ? s.ValidateIsWhiteSpace(blackboard, DefaultFailureMessage, memberName)
            : ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)]);
}
