using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class NullValidator : IValidator
{
    public static readonly NullValidator Instance = new();
    public string Name => IsNull.ValidatorName;
    public string DefaultFailureMessage => IsNull.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
        => value.ValidateIsNull(blackboard, DefaultFailureMessage, memberName);
}
