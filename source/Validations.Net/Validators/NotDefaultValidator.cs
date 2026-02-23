using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class NotDefaultValidator : IValidator
{
    public static readonly NotDefaultValidator Instance = new();
    public string Name => IsNotDefault.ValidatorName;
    public string DefaultFailureMessage => IsNotDefault.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value.CheckIsNotDefault())
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value)]);
    }
}
