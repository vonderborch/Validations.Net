using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value contains exactly one element.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsSingleAttribute() : ValidationAttribute(IsSingle.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        bool isSingle = value switch
        {
            string s => s.CheckIsSingle(),
            System.Collections.IEnumerable e => e.CheckIsSingle(),
            _ => false
        };

        if (isSingle) return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsSingle.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
