using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class NotNullOrWhiteSpaceValidator : IValidator
{
    public static readonly NotNullOrWhiteSpaceValidator Instance = new();
    public string Name => IsNotNullOrWhiteSpace.ValidatorName;
    public string DefaultFailureMessage => IsNotNullOrWhiteSpace.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
        => value is string s ? s.ValidateIsNotNullOrWhiteSpace(blackboard, DefaultFailureMessage, memberName)
            : ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)]);
}
