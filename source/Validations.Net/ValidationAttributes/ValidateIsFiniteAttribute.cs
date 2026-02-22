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

        bool isFinite = value switch
        {
            double d => d.CheckIsFinite(),
            float f => f.CheckIsFinite(),
            decimal => true,
            _ => false
        };

        if (isFinite) return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsFinite.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
