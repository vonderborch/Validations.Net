using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.DateTime;

public sealed class IsLocalValidator : IValidator
{
    public static readonly IsLocalValidator Instance = new();
    public string Name => IsLocal.ValidatorName;
    public string DefaultFailureMessage => IsLocal.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.DateTime dt)
            return dt.ValidateIsLocal(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not a DateTime", memberName, blackboard,
            [("value", value)]);
    }
}
