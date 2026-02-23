using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class IsNotOneOfValidator : IValidator
{
    public object[] Values { get; }

    public IsNotOneOfValidator(params object[] values)
    {
        Values = values;
    }

    public string Name => IsNotOneOf.ValidatorName;
    public string DefaultFailureMessage => IsNotOneOf.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value.CheckIsNotOneOf(Values))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value)]);
    }
}
