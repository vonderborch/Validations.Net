using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public readonly record struct DoesContainParams(string Substring, StringComparison Comparison = StringComparison.Ordinal);

public sealed class DoesContainValidator : IValidator
{
    public DoesContainParams Params { get; }

    public DoesContainValidator(string substring, StringComparison comparison = StringComparison.Ordinal)
    {
        Params = new DoesContainParams(substring, comparison);
    }

    public string Name => DoesContain.ValidatorName;
    public string DefaultFailureMessage => DoesContain.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s && s.CheckDoesContain(Params.Substring, Params.Comparison))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("substring", Params.Substring)]);
    }
}
