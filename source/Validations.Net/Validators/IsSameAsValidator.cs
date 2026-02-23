using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class IsSameAsValidator : IValidator
{
    public object? Other { get; }

    public IsSameAsValidator(object? other)
    {
        Other = other;
    }

    public string Name => IsSameAs.ValidatorName;
    public string DefaultFailureMessage => IsSameAs.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value.CheckIsSameAs(Other))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("other", Other)]);
    }
}
