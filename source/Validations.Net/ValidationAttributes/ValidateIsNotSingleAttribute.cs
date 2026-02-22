using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value does not contain exactly one element.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsNotSingleAttribute() : ValidationAttribute(IsNotSingle.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        bool isNotSingle = value switch
        {
            string s => s.CheckIsNotSingle(),
            System.Collections.IEnumerable e => e.CheckIsNotSingle(),
            _ => true
        };

        if (isNotSingle) return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsNotSingle.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
