using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Identifiers;

public readonly record struct ValidIdParams(int MinLength = 1, int MaxLength = 255, string? AllowedPattern = null);

public sealed class ValidIdValidator : IValidator
{
    public ValidIdParams Params { get; }

    public ValidIdValidator(int minLength = 1, int maxLength = 255, string? allowedPattern = null)
    {
        Params = new ValidIdParams(minLength, maxLength, allowedPattern);
    }

    public string Name => IsValidId.ValidatorName;
    public string DefaultFailureMessage => IsValidId.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s && s.CheckIsValidId(Params.MinLength, Params.MaxLength, Params.AllowedPattern))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("minLength", Params.MinLength), ("maxLength", Params.MaxLength)]);
    }
}
