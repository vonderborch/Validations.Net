using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public readonly record struct DoesNotStartWithParams(string Prefix, StringComparison Comparison = StringComparison.Ordinal);

public sealed class DoesNotStartWithValidator : IValidator
{
    public DoesNotStartWithParams Params { get; }

    public DoesNotStartWithValidator(string prefix, StringComparison comparison = StringComparison.Ordinal)
    {
        Params = new DoesNotStartWithParams(prefix, comparison);
    }

    public string Name => DoesNotStartWith.ValidatorName;
    public string DefaultFailureMessage => DoesNotStartWith.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is not string s)
            return ValidationResult.CreateFromValidationSuccess();
        if (s.CheckDoesNotStartWith(Params.Prefix, Params.Comparison))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("prefix", Params.Prefix)]);
    }
}
