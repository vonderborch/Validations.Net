using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class NotSingleValidator : IValidator
{
    public static readonly NotSingleValidator Instance = new();
    public string Name => IsNotSingle.ValidatorName;
    public string DefaultFailureMessage => IsNotSingle.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            string s => s.ValidateIsNotSingle(blackboard, DefaultFailureMessage, memberName),
            System.Collections.IEnumerable e when value is not string => e.Cast<object?>().ValidateIsNotSingle(blackboard, DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationSuccess()
        };
    }
}
