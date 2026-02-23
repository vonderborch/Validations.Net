using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class NullOrWhiteSpaceValidator : IValidator
{
    public static readonly NullOrWhiteSpaceValidator Instance = new();
    public string Name => IsNullOrWhiteSpace.ValidatorName;
    public string DefaultFailureMessage => IsNullOrWhiteSpace.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
        => value switch
        {
            null => ValidationResult.CreateFromValidationSuccess(),
            string s => s.ValidateIsNullOrWhiteSpace(blackboard, DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
}
