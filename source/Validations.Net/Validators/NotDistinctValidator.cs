using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class NotDistinctValidator : IValidator
{
    public static readonly NotDistinctValidator Instance = new();
    public string Name => IsNotDistinct.ValidatorName;
    public string DefaultFailureMessage => IsNotDistinct.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            null => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)]),
            System.Collections.IEnumerable e when value is not string => e.Cast<object?>().ValidateIsNotDistinct(blackboard, DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
    }
}
