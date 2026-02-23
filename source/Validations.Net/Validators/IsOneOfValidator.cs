using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class IsOneOfValidator : IValidator
{
    public object[] Values { get; }

    public IsOneOfValidator(params object[] values)
    {
        Values = values;
    }

    public string Name => IsOneOf.ValidatorName;
    public string DefaultFailureMessage => IsOneOf.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value.CheckIsOneOf(Values))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value)]);
    }
}
