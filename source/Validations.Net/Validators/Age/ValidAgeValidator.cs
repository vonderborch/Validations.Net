using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Age;

public sealed class ValidAgeValidator : IValidator
{
    public static readonly ValidAgeValidator Instance = new();
    public string Name => IsValidAge.ValidatorName;
    public string DefaultFailureMessage => IsValidAge.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is int age)
            return age.ValidateIsValidAge(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not an int", memberName, blackboard,
            [("value", value)]);
    }
}
