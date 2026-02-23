using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class IsGreaterThanOrEqualsValidator : IValidator
{
    public object? Comparand { get; }

    public IsGreaterThanOrEqualsValidator(object? comparand)
    {
        Comparand = comparand;
    }

    public string Name => IsGreaterThanOrEquals.ValidatorName;
    public string DefaultFailureMessage => IsGreaterThanOrEquals.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is IComparable comparable && comparable.CheckIsGreaterThanOrEquals(Comparand))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("comparand", Comparand)]);
    }
}
