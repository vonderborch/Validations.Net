using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value (string) does not contain the specified substring.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateDoesNotContainAttribute(string substring) : ValidationAttribute(DoesNotContain.ValidatorName)
{
    private readonly string _substring = substring;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string str && str.CheckDoesNotContain(_substring))
            return ValidationResult.CreateFromValidationSuccess();
        var message = Message ?? DoesNotContain.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("substring", _substring) });
    }
}
