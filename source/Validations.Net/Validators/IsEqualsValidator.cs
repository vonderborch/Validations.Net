using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class IsEqualsValidator : IValidator
{
    public object? Expected { get; }

    public IsEqualsValidator(object? expected)
    {
        Expected = expected;
    }

    public string Name => IsEquals.ValidatorName;
    public string DefaultFailureMessage => IsEquals.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value.CheckIsEquals(Expected))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("expected", Expected)]);
    }
}
