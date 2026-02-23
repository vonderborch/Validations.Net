using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public readonly record struct IsMatchParams(string Pattern);

public sealed class IsMatchValidator : IValidator
{
    public IsMatchParams Params { get; }

    public IsMatchValidator(string pattern)
    {
        Params = new IsMatchParams(pattern);
    }

    public string Name => IsMatch.ValidatorName;
    public string DefaultFailureMessage => IsMatch.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s && s.CheckIsMatch(Params.Pattern))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("pattern", Params.Pattern)]);
    }
}
