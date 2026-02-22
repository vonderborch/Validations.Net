using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is even.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsEvenAttribute() : ValidationAttribute(IsEven.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is null) return ValidationResult.CreateFromValidationSuccess();
        try
        {
            dynamic dyn = value;
            var two = Convert.ChangeType(2, value.GetType());
            bool isEven = dyn % (dynamic)two == (dynamic)Convert.ChangeType(0, value.GetType());
            if (isEven) return ValidationResult.CreateFromValidationSuccess();
        }
        catch { }

        var message = Message ?? IsEven.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
