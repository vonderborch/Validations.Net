using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class EmptyValidator : IValidator
{
    public static readonly EmptyValidator Instance = new();
    public string Name => IsEmpty.ValidatorName;
    public string DefaultFailureMessage => IsEmpty.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            string s => s.ValidateIsEmpty(blackboard, DefaultFailureMessage, memberName),
            System.Collections.IEnumerable e when value is not string => e.Cast<object?>().ValidateIsEmpty(blackboard, DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
    }
}
