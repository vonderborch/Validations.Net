using SimpleBlackboard.Net;
using Validations.Net.Validators.Age;

namespace Validations.Net.ValidationAttributes.Age;

/// <summary>
/// Validates that the decorated member's value is a valid age (0-150).
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsValidAgeAttribute() : ValidationAttribute(IsValidAge.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is int age)
            return age.CheckIsValidAge() ? ValidationResult.CreateFromValidationSuccess() : Fail(age, memberName, blackboard);

        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = Message ?? IsValidAge.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard, [("value", value)]);
    }
}
