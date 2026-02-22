using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is null or empty.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsNullOrEmptyAttribute() : ValidationAttribute(IsNullOrEmpty.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        bool isNullOrEmpty = value switch
        {
            null => true,
            string s => s.CheckIsNullOrEmpty(),
            System.Collections.ICollection c => c.CheckIsNullOrEmpty(),
            System.Collections.IEnumerable e => e.CheckIsNullOrEmpty(),
            _ => false
        };

        if (isNullOrEmpty) return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsNullOrEmpty.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
