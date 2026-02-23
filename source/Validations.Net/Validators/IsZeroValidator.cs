using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class ZeroValidator : IValidator
{
    public static readonly ZeroValidator Instance = new();
    public string Name => IsZero.ValidatorName;
    public string DefaultFailureMessage => IsZero.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            null => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)]),
            int v => v.ValidateIsZero(blackboard, DefaultFailureMessage, memberName),
            long v => v.ValidateIsZero(blackboard, DefaultFailureMessage, memberName),
            short v => v.ValidateIsZero(blackboard, DefaultFailureMessage, memberName),
            byte v => v.ValidateIsZero(blackboard, DefaultFailureMessage, memberName),
            uint v => v.ValidateIsZero(blackboard, DefaultFailureMessage, memberName),
            ulong v => v.ValidateIsZero(blackboard, DefaultFailureMessage, memberName),
            ushort v => v.ValidateIsZero(blackboard, DefaultFailureMessage, memberName),
            sbyte v => v.ValidateIsZero(blackboard, DefaultFailureMessage, memberName),
            nint v => v.ValidateIsZero(blackboard, DefaultFailureMessage, memberName),
            nuint v => v.ValidateIsZero(blackboard, DefaultFailureMessage, memberName),
            Int128 v => v.ValidateIsZero(blackboard, DefaultFailureMessage, memberName),
            UInt128 v => v.ValidateIsZero(blackboard, DefaultFailureMessage, memberName),
            BigInteger v => v.ValidateIsZero(blackboard, DefaultFailureMessage, memberName),
            double v => v.ValidateIsZero(blackboard, DefaultFailureMessage, memberName),
            float v => v.ValidateIsZero(blackboard, DefaultFailureMessage, memberName),
            Half v => v.ValidateIsZero(blackboard, DefaultFailureMessage, memberName),
            decimal v => v.ValidateIsZero(blackboard, DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
    }
}
