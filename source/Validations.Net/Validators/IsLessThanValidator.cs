using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class IsLessThanValidator : IValidator
{
    public object? Comparand { get; }

    public IsLessThanValidator(object? comparand)
    {
        Comparand = comparand;
    }

    public string Name => IsLessThan.ValidatorName;
    public string DefaultFailureMessage => IsLessThan.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is IComparable comparable && comparable.CheckIsLessThan(Comparand))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("comparand", Comparand)]);
    }
}
