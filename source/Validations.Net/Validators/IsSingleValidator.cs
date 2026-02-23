using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class SingleValidator : IValidator
{
    public static readonly SingleValidator Instance = new();
    public string Name => IsSingle.ValidatorName;
    public string DefaultFailureMessage => IsSingle.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            string s => s.ValidateIsSingle(blackboard, DefaultFailureMessage, memberName),
            System.Collections.IEnumerable e when value is not string => e.Cast<object?>().ValidateIsSingle(blackboard, DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
    }
}
