using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class EvenValidator : IValidator
{
    public static readonly EvenValidator Instance = new();
    public string Name => IsEven.ValidatorName;
    public string DefaultFailureMessage => IsEven.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            null => ValidationResult.CreateFromValidationSuccess(),
            int v => v.ValidateIsEven(blackboard, DefaultFailureMessage, memberName),
            long v => v.ValidateIsEven(blackboard, DefaultFailureMessage, memberName),
            short v => v.ValidateIsEven(blackboard, DefaultFailureMessage, memberName),
            byte v => v.ValidateIsEven(blackboard, DefaultFailureMessage, memberName),
            uint v => v.ValidateIsEven(blackboard, DefaultFailureMessage, memberName),
            ulong v => v.ValidateIsEven(blackboard, DefaultFailureMessage, memberName),
            ushort v => v.ValidateIsEven(blackboard, DefaultFailureMessage, memberName),
            sbyte v => v.ValidateIsEven(blackboard, DefaultFailureMessage, memberName),
            nint v => v.ValidateIsEven(blackboard, DefaultFailureMessage, memberName),
            nuint v => v.ValidateIsEven(blackboard, DefaultFailureMessage, memberName),
            Int128 v => v.ValidateIsEven(blackboard, DefaultFailureMessage, memberName),
            UInt128 v => v.ValidateIsEven(blackboard, DefaultFailureMessage, memberName),
            BigInteger v => v.ValidateIsEven(blackboard, DefaultFailureMessage, memberName),
            double v => v.ValidateIsEven(blackboard, DefaultFailureMessage, memberName),
            float v => v.ValidateIsEven(blackboard, DefaultFailureMessage, memberName),
            Half v => v.ValidateIsEven(blackboard, DefaultFailureMessage, memberName),
            decimal v => v.ValidateIsEven(blackboard, DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
    }
}
