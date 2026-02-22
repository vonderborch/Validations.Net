using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is odd.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsOddAttribute() : ValidationAttribute(IsOdd.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is null) return ValidationResult.CreateFromValidationSuccess();
        try
        {
            dynamic dyn = value;
            var two = Convert.ChangeType(2, value.GetType());
            bool isOdd = dyn % (dynamic)two != (dynamic)Convert.ChangeType(0, value.GetType());
            if (isOdd) return ValidationResult.CreateFromValidationSuccess();
        }
        catch { }

        var message = Message ?? IsOdd.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
