using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value contains only distinct elements.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsDistinctAttribute() : ValidationAttribute(IsDistinct.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.Collections.IEnumerable enumerable)
        {
            var seen = new HashSet<object?>();
            foreach (var item in enumerable)
            {
                if (!seen.Add(item))
                {
                    var msg = Message ?? IsDistinct.DefaultValidationFailureMessage;
                    return ValidationResult.CreateFromValidationFailure(Name, msg, memberName, blackboard,
                        new List<(string key, object? value)> { ("value", value) });
                }
            }
            return ValidationResult.CreateFromValidationSuccess();
        }
        var message = Message ?? IsDistinct.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
