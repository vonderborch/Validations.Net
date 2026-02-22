using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is a valid credit card number (Luhn algorithm).
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsCreditCardAttribute() : ValidationAttribute(IsCreditCard.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is not string str)
            return ValidationResult.CreateFromValidationSuccess();

        if (str.CheckIsCreditCard())
            return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsCreditCard.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
