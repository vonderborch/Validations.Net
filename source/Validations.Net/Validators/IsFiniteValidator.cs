using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class FiniteValidator : IValidator
{
    public static readonly FiniteValidator Instance = new();
    public string Name => IsFinite.ValidatorName;
    public string DefaultFailureMessage => IsFinite.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            null => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)]),
            double v => v.ValidateIsFinite(blackboard, DefaultFailureMessage, memberName),
            float v => v.ValidateIsFinite(blackboard, DefaultFailureMessage, memberName),
            Half v => v.ValidateIsFinite(blackboard, DefaultFailureMessage, memberName),
            decimal v => v.ValidateIsFinite(blackboard, DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
    }
}
