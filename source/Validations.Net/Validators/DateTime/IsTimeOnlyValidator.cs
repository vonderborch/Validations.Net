using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.DateTime;

public sealed class IsTimeOnlyValidator : IValidator
{
    public static readonly IsTimeOnlyValidator Instance = new();
    public string Name => IsTimeOnly.ValidatorName;
    public string DefaultFailureMessage => IsTimeOnly.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.DateTime dt)
            return dt.ValidateIsTimeOnly(blackboard, DefaultFailureMessage, memberName);
        if (value is DateTimeOffset dto)
            return dto.ValidateIsTimeOnly(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not a DateTime or DateTimeOffset", memberName, blackboard,
            [("value", value)]);
    }
}
