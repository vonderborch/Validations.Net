using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is not empty.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsNotEmptyAttribute() : ValidationAttribute(IsNotEmpty.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        bool isNotEmpty = value switch
        {
            string s => s.CheckIsNotEmpty(),
            System.Collections.ICollection c => c.CheckIsNotEmpty(),
            System.Collections.IEnumerable e => e.CheckIsNotEmpty(),
            _ => false
        };

        if (isNotEmpty) return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsNotEmpty.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
