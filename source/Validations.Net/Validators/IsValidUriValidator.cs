using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public readonly record struct IsValidUriParams(UriKind UriKind = UriKind.Absolute);

public sealed class IsValidUriValidator : IValidator
{
    public IsValidUriParams Params { get; }

    public IsValidUriValidator(UriKind uriKind = UriKind.Absolute)
    {
        Params = new IsValidUriParams(uriKind);
    }

    public string Name => IsValidUri.ValidatorName;
    public string DefaultFailureMessage => IsValidUri.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s && s.CheckIsValidUri(Params.UriKind))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value)]);
    }
}
