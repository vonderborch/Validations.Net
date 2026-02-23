using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class IsGreaterThanValidator : IValidator
{
    public object? Comparand { get; }

    public IsGreaterThanValidator(object? comparand)
    {
        Comparand = comparand;
    }

    public string Name => IsGreaterThan.ValidatorName;
    public string DefaultFailureMessage => IsGreaterThan.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is IComparable comparable && comparable.CheckIsGreaterThan(Comparand))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("comparand", Comparand)]);
    }
}
