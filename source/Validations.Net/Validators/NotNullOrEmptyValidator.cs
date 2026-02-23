using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class NotNullOrEmptyValidator : IValidator
{
    public static readonly NotNullOrEmptyValidator Instance = new();
    public string Name => IsNotNullOrEmpty.ValidatorName;
    public string DefaultFailureMessage => IsNotNullOrEmpty.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            string s => s.ValidateIsNotNullOrEmpty(blackboard, DefaultFailureMessage, memberName),
            System.Collections.IEnumerable e when value is not string => e.Cast<object?>().ValidateIsNotNullOrEmpty(blackboard, DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
    }
}
