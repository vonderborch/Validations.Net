using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public readonly record struct EndsWithParams(string Suffix, StringComparison Comparison = StringComparison.Ordinal);

public sealed class EndsWithValidator : IValidator
{
    public EndsWithParams Params { get; }

    public EndsWithValidator(string suffix, StringComparison comparison = StringComparison.Ordinal)
    {
        Params = new EndsWithParams(suffix, comparison);
    }

    public string Name => EndsWith.ValidatorName;
    public string DefaultFailureMessage => EndsWith.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s && s.CheckEndsWith(Params.Suffix, Params.Comparison))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("suffix", Params.Suffix)]);
    }
}
