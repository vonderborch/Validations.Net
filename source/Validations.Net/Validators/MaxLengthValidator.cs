using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public readonly record struct MaxLengthParams(int MaxLength);

public sealed class MaxLengthValidator : IValidator
{
    public MaxLengthParams Params { get; }

    public MaxLengthValidator(int maxLength)
    {
        Params = new MaxLengthParams(maxLength);
    }

    public string Name => MaxLength.ValidatorName;
    public string DefaultFailureMessage => MaxLength.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s && s.CheckMaxLength(Params.MaxLength))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("maxLength", Params.MaxLength), ("actualLength", (value as string)?.Length)]);
    }
}
