using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is empty.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsEmptyAttribute() : ValidationAttribute(IsEmpty.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        bool isEmpty = value switch
        {
            string s => s.CheckIsEmpty(),
            System.Collections.ICollection c => c.CheckIsEmpty(),
            System.Collections.IEnumerable e => e.CheckIsEmpty(),
            _ => false
        };

        if (isEmpty) return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsEmpty.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
