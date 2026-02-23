using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class NotEqualsValidator : IValidator
{
    public object? Expected { get; }

    public NotEqualsValidator(object? expected)
    {
        Expected = expected;
    }

    public string Name => IsNotEquals.ValidatorName;
    public string DefaultFailureMessage => IsNotEquals.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value.CheckIsNotEquals(Expected))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("expected", Expected)]);
    }
}
