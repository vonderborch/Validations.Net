using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public readonly record struct NotMatchParams(string Pattern);

public sealed class NotMatchValidator : IValidator
{
    public NotMatchParams Params { get; }

    public NotMatchValidator(string pattern)
    {
        Params = new NotMatchParams(pattern);
    }

    public string Name => IsNotMatch.ValidatorName;
    public string DefaultFailureMessage => IsNotMatch.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is not string s)
            return ValidationResult.CreateFromValidationSuccess();
        if (s.CheckIsNotMatch(Params.Pattern))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("pattern", Params.Pattern)]);
    }
}
