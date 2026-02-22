using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is NaN.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsNaNAttribute() : ValidationAttribute(IsNaN.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is null) return ValidationResult.CreateFromValidationSuccess();
        try
        {
            if (value is double d && double.IsNaN(d))
                return ValidationResult.CreateFromValidationSuccess();
            if (value is float f && float.IsNaN(f))
                return ValidationResult.CreateFromValidationSuccess();
        }
        catch { }

        var message = Message ?? IsNaN.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
