using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class PositiveValidator : IValidator
{
    public static readonly PositiveValidator Instance = new();
    public string Name => IsPositive.ValidatorName;
    public string DefaultFailureMessage => IsPositive.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            null => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)]),
            int v => v.ValidateIsPositive(blackboard, DefaultFailureMessage, memberName),
            long v => v.ValidateIsPositive(blackboard, DefaultFailureMessage, memberName),
            short v => v.ValidateIsPositive(blackboard, DefaultFailureMessage, memberName),
            byte v => v.ValidateIsPositive(blackboard, DefaultFailureMessage, memberName),
            uint v => v.ValidateIsPositive(blackboard, DefaultFailureMessage, memberName),
            ulong v => v.ValidateIsPositive(blackboard, DefaultFailureMessage, memberName),
            ushort v => v.ValidateIsPositive(blackboard, DefaultFailureMessage, memberName),
            sbyte v => v.ValidateIsPositive(blackboard, DefaultFailureMessage, memberName),
            nint v => v.ValidateIsPositive(blackboard, DefaultFailureMessage, memberName),
            nuint v => v.ValidateIsPositive(blackboard, DefaultFailureMessage, memberName),
            Int128 v => v.ValidateIsPositive(blackboard, DefaultFailureMessage, memberName),
            UInt128 v => v.ValidateIsPositive(blackboard, DefaultFailureMessage, memberName),
            BigInteger v => v.ValidateIsPositive(blackboard, DefaultFailureMessage, memberName),
            double v => v.ValidateIsPositive(blackboard, DefaultFailureMessage, memberName),
            float v => v.ValidateIsPositive(blackboard, DefaultFailureMessage, memberName),
            Half v => v.ValidateIsPositive(blackboard, DefaultFailureMessage, memberName),
            decimal v => v.ValidateIsPositive(blackboard, DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
    }
}
