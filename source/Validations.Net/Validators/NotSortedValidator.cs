using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public readonly record struct NotSortedParams(bool Descending = false);

public sealed class NotSortedValidator : IValidator
{
    public NotSortedParams Params { get; }

    public NotSortedValidator(bool descending = false)
    {
        Params = new NotSortedParams(descending);
    }

    public string Name => IsNotSorted.ValidatorName;
    public string DefaultFailureMessage => IsNotSorted.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.Collections.IEnumerable enumerable && enumerable.CheckIsNotSorted(Params.Descending))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("descending", Params.Descending)]);
    }
}
