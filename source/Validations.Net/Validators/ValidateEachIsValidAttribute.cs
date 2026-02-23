using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Structural attribute that tells the validation runner to iterate over the collection
/// and recursively validate each element using its own type's ValidationAttributes.
/// This attribute does not perform validation itself.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateEachIsValidAttribute() : ValidationAttribute("ValidateEachIsValid")
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return ValidationResult.CreateFromValidationSuccess();
    }
}
