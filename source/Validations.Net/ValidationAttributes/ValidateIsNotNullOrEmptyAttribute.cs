using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is not null and not empty.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsNotNullOrEmptyAttribute() : ValidationAttribute(IsNotNullOrEmpty.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        bool isNotNullOrEmpty = value switch
        {
            null => false,
            string s => s.CheckIsNotNullOrEmpty(),
            System.Collections.ICollection c => c.CheckIsNotNullOrEmpty(),
            System.Collections.IEnumerable e => e.CheckIsNotNullOrEmpty(),
            _ => false
        };

        if (isNotNullOrEmpty) return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsNotNullOrEmpty.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
