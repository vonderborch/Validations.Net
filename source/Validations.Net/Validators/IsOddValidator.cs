using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class OddValidator : IValidator
{
    public static readonly OddValidator Instance = new();
    public string Name => IsOdd.ValidatorName;
    public string DefaultFailureMessage => IsOdd.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            null => ValidationResult.CreateFromValidationSuccess(),
            int v => v.ValidateIsOdd(blackboard, DefaultFailureMessage, memberName),
            long v => v.ValidateIsOdd(blackboard, DefaultFailureMessage, memberName),
            short v => v.ValidateIsOdd(blackboard, DefaultFailureMessage, memberName),
            byte v => v.ValidateIsOdd(blackboard, DefaultFailureMessage, memberName),
            uint v => v.ValidateIsOdd(blackboard, DefaultFailureMessage, memberName),
            ulong v => v.ValidateIsOdd(blackboard, DefaultFailureMessage, memberName),
            ushort v => v.ValidateIsOdd(blackboard, DefaultFailureMessage, memberName),
            sbyte v => v.ValidateIsOdd(blackboard, DefaultFailureMessage, memberName),
            nint v => v.ValidateIsOdd(blackboard, DefaultFailureMessage, memberName),
            nuint v => v.ValidateIsOdd(blackboard, DefaultFailureMessage, memberName),
            Int128 v => v.ValidateIsOdd(blackboard, DefaultFailureMessage, memberName),
            UInt128 v => v.ValidateIsOdd(blackboard, DefaultFailureMessage, memberName),
            BigInteger v => v.ValidateIsOdd(blackboard, DefaultFailureMessage, memberName),
            double v => v.ValidateIsOdd(blackboard, DefaultFailureMessage, memberName),
            float v => v.ValidateIsOdd(blackboard, DefaultFailureMessage, memberName),
            Half v => v.ValidateIsOdd(blackboard, DefaultFailureMessage, memberName),
            decimal v => v.ValidateIsOdd(blackboard, DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
    }
}
