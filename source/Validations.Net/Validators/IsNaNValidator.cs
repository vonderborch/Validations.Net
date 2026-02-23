using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class NaNValidator : IValidator
{
    public static readonly NaNValidator Instance = new();
    public string Name => IsNaN.ValidatorName;
    public string DefaultFailureMessage => IsNaN.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            null => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)]),
            double v => v.ValidateIsNaN(blackboard, DefaultFailureMessage, memberName),
            float v => v.ValidateIsNaN(blackboard, DefaultFailureMessage, memberName),
            Half v => v.ValidateIsNaN(blackboard, DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
    }
}
