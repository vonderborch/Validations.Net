using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class NotSameAsValidator : IValidator
{
    public object? Other { get; }

    public NotSameAsValidator(object? other)
    {
        Other = other;
    }

    public string Name => IsNotSameAs.ValidatorName;
    public string DefaultFailureMessage => IsNotSameAs.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value.CheckIsNotSameAs(Other))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("other", Other)]);
    }
}
