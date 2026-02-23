using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public readonly record struct IsLengthParams(int Length);

public sealed class IsLengthValidator : IValidator
{
    public IsLengthParams Params { get; }

    public IsLengthValidator(int length)
    {
        Params = new IsLengthParams(length);
    }

    public string Name => IsLength.ValidatorName;
    public string DefaultFailureMessage => IsLength.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            string s when s.CheckIsLength(Params.Length) => ValidationResult.CreateFromValidationSuccess(),
            string s => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
                [("value", value), ("expectedLength", Params.Length), ("actualLength", s.Length)]),
            System.Collections.ICollection c when c.CheckIsLength(Params.Length) => ValidationResult.CreateFromValidationSuccess(),
            System.Collections.ICollection c => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
                [("value", value), ("expectedLength", Params.Length), ("actualLength", c.Count)]),
            _ => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
                [("value", value), ("expectedLength", Params.Length)])
        };
    }
}
