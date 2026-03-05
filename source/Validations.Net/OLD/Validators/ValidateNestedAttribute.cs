using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// Structural attribute that tells the validation runner to recursively validate
/// the decorated member's value using its own type's ValidationAttributes.
/// This attribute does not perform validation itself.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateNestedAttribute() : ValidationAttribute("ValidateNested")
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return ValidationResult.CreateFromValidationSuccess();
    }
}
