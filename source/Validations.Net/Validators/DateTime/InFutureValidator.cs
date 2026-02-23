using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.DateTime;

public sealed class InFutureValidator : IValidator
{
    public static readonly InFutureValidator Instance = new();
    public string Name => IsInFuture.ValidatorName;
    public string DefaultFailureMessage => IsInFuture.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.DateTime dt)
            return dt.ValidateIsInFuture(blackboard, DefaultFailureMessage, memberName);
        if (value is DateTimeOffset dto)
            return dto.ValidateIsInFuture(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not a DateTime or DateTimeOffset", memberName, blackboard,
            [("value", value)]);
    }
}
