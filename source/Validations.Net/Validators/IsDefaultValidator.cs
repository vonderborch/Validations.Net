using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class DefaultValidator : IValidator
{
    public static readonly DefaultValidator Instance = new();
    public string Name => IsDefault.ValidatorName;
    public string DefaultFailureMessage => IsDefault.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
        => value.ValidateIsDefault(blackboard, DefaultFailureMessage, memberName);
}
