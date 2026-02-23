using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public readonly record struct DoesNotEndWithParams(string Suffix, StringComparison Comparison = StringComparison.Ordinal);

public sealed class DoesNotEndWithValidator : IValidator
{
    public DoesNotEndWithParams Params { get; }

    public DoesNotEndWithValidator(string suffix, StringComparison comparison = StringComparison.Ordinal)
    {
        Params = new DoesNotEndWithParams(suffix, comparison);
    }

    public string Name => DoesNotEndWith.ValidatorName;
    public string DefaultFailureMessage => DoesNotEndWith.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is not string s)
            return ValidationResult.CreateFromValidationSuccess();
        if (s.CheckDoesNotEndWith(Params.Suffix, Params.Comparison))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("suffix", Params.Suffix)]);
    }
}
