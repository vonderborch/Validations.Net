using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class NegativeValidator : IValidator
{
    public static readonly NegativeValidator Instance = new();
    public string Name => IsNegative.ValidatorName;
    public string DefaultFailureMessage => IsNegative.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            null => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)]),
            int v => v.ValidateIsNegative(blackboard, DefaultFailureMessage, memberName),
            long v => v.ValidateIsNegative(blackboard, DefaultFailureMessage, memberName),
            short v => v.ValidateIsNegative(blackboard, DefaultFailureMessage, memberName),
            sbyte v => v.ValidateIsNegative(blackboard, DefaultFailureMessage, memberName),
            nint v => v.ValidateIsNegative(blackboard, DefaultFailureMessage, memberName),
            Int128 v => v.ValidateIsNegative(blackboard, DefaultFailureMessage, memberName),
            BigInteger v => v.ValidateIsNegative(blackboard, DefaultFailureMessage, memberName),
            double v => v.ValidateIsNegative(blackboard, DefaultFailureMessage, memberName),
            float v => v.ValidateIsNegative(blackboard, DefaultFailureMessage, memberName),
            Half v => v.ValidateIsNegative(blackboard, DefaultFailureMessage, memberName),
            decimal v => v.ValidateIsNegative(blackboard, DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
    }
}
