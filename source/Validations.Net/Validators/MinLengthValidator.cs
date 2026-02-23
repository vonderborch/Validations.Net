using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public readonly record struct MinLengthParams(int MinLength);

public sealed class MinLengthValidator : IValidator
{
    public MinLengthParams Params { get; }

    public MinLengthValidator(int minLength)
    {
        Params = new MinLengthParams(minLength);
    }

    public string Name => MinLength.ValidatorName;
    public string DefaultFailureMessage => MinLength.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s && s.CheckMinLength(Params.MinLength))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("minLength", Params.MinLength), ("actualLength", (value as string)?.Length)]);
    }
}
