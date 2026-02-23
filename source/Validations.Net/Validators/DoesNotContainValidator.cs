using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public readonly record struct DoesNotContainParams(string Substring, StringComparison Comparison = StringComparison.Ordinal);

public sealed class DoesNotContainValidator : IValidator
{
    public DoesNotContainParams Params { get; }

    public DoesNotContainValidator(string substring, StringComparison comparison = StringComparison.Ordinal)
    {
        Params = new DoesNotContainParams(substring, comparison);
    }

    public string Name => DoesNotContain.ValidatorName;
    public string DefaultFailureMessage => DoesNotContain.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s && s.CheckDoesNotContain(Params.Substring, Params.Comparison))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("substring", Params.Substring)]);
    }
}
