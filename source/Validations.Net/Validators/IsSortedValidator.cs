using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public readonly record struct IsSortedParams(bool Descending = false);

public sealed class IsSortedValidator : IValidator
{
    public IsSortedParams Params { get; }

    public IsSortedValidator(bool descending = false)
    {
        Params = new IsSortedParams(descending);
    }

    public string Name => IsSorted.ValidatorName;
    public string DefaultFailureMessage => IsSorted.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.Collections.IEnumerable enumerable && enumerable.CheckIsSorted(Params.Descending))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("descending", Params.Descending)]);
    }
}
