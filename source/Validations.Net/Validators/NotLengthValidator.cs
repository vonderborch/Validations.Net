using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public readonly record struct NotLengthParams(int Length);

public sealed class NotLengthValidator : IValidator
{
    public NotLengthParams Params { get; }

    public NotLengthValidator(int length)
    {
        Params = new NotLengthParams(length);
    }

    public string Name => IsNotLength.ValidatorName;
    public string DefaultFailureMessage => IsNotLength.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is null)
            return ValidationResult.CreateFromValidationSuccess();
        return value switch
        {
            string s when s.CheckIsNotLength(Params.Length) => ValidationResult.CreateFromValidationSuccess(),
            string s => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
                [("value", value), ("length", Params.Length), ("actualLength", s.Length)]),
            System.Collections.ICollection c when c.CheckIsNotLength(Params.Length) => ValidationResult.CreateFromValidationSuccess(),
            System.Collections.ICollection c => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
                [("value", value), ("length", Params.Length), ("actualLength", c.Count)]),
            _ => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
                [("value", value), ("length", Params.Length)])
        };
    }
}
