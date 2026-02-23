using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public readonly record struct StartsWithParams(string Prefix, StringComparison Comparison = StringComparison.Ordinal);

public sealed class StartsWithValidator : IValidator
{
    public StartsWithParams Params { get; }

    public StartsWithValidator(string prefix, StringComparison comparison = StringComparison.Ordinal)
    {
        Params = new StartsWithParams(prefix, comparison);
    }

    public string Name => StartsWith.ValidatorName;
    public string DefaultFailureMessage => StartsWith.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s && s.CheckStartsWith(Params.Prefix, Params.Comparison))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("prefix", Params.Prefix)]);
    }
}
