using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class NonZeroValidator : IValidator
{
    public static readonly NonZeroValidator Instance = new();
    public string Name => IsNonZero.ValidatorName;
    public string DefaultFailureMessage => IsNonZero.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            null => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)]),
            int v => v.ValidateIsNonZero(blackboard, DefaultFailureMessage, memberName),
            long v => v.ValidateIsNonZero(blackboard, DefaultFailureMessage, memberName),
            short v => v.ValidateIsNonZero(blackboard, DefaultFailureMessage, memberName),
            byte v => v.ValidateIsNonZero(blackboard, DefaultFailureMessage, memberName),
            uint v => v.ValidateIsNonZero(blackboard, DefaultFailureMessage, memberName),
            ulong v => v.ValidateIsNonZero(blackboard, DefaultFailureMessage, memberName),
            ushort v => v.ValidateIsNonZero(blackboard, DefaultFailureMessage, memberName),
            sbyte v => v.ValidateIsNonZero(blackboard, DefaultFailureMessage, memberName),
            nint v => v.ValidateIsNonZero(blackboard, DefaultFailureMessage, memberName),
            nuint v => v.ValidateIsNonZero(blackboard, DefaultFailureMessage, memberName),
            Int128 v => v.ValidateIsNonZero(blackboard, DefaultFailureMessage, memberName),
            UInt128 v => v.ValidateIsNonZero(blackboard, DefaultFailureMessage, memberName),
            BigInteger v => v.ValidateIsNonZero(blackboard, DefaultFailureMessage, memberName),
            double v => v.ValidateIsNonZero(blackboard, DefaultFailureMessage, memberName),
            float v => v.ValidateIsNonZero(blackboard, DefaultFailureMessage, memberName),
            Half v => v.ValidateIsNonZero(blackboard, DefaultFailureMessage, memberName),
            decimal v => v.ValidateIsNonZero(blackboard, DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
    }
}
