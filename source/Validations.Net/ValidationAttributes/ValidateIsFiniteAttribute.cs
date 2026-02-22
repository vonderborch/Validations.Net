using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is finite (not NaN, not infinity).
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsFiniteAttribute() : ValidationAttribute(IsFinite.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is null) return ValidationResult.CreateFromValidationSuccess();
        try
        {
            if (value is double d && double.IsFinite(d))
                return ValidationResult.CreateFromValidationSuccess();
            if (value is float f && float.IsFinite(f))
                return ValidationResult.CreateFromValidationSuccess();
            if (value is decimal)
                return ValidationResult.CreateFromValidationSuccess(); // decimal is always finite
        }
        catch { }

        var message = Message ?? IsFinite.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
