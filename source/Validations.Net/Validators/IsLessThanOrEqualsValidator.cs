using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class IsLessThanOrEqualsValidator : IValidator
{
    public object? Comparand { get; }

    public IsLessThanOrEqualsValidator(object? comparand)
    {
        Comparand = comparand;
    }

    public string Name => IsLessThanOrEquals.ValidatorName;
    public string DefaultFailureMessage => IsLessThanOrEquals.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is IComparable comparable && comparable.CheckIsLessThanOrEquals(Comparand))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("comparand", Comparand)]);
    }
}
