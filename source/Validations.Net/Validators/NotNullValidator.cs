using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class NotNullValidator : IValidator
{
    public static readonly NotNullValidator Instance = new();
    public string Name => IsNotNull.ValidatorName;
    public string DefaultFailureMessage => IsNotNull.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
        => value.ValidateIsNotNull(blackboard, DefaultFailureMessage, memberName);
}
