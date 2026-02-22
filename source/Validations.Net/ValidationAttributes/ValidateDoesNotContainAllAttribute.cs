using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value (string) does not contain all of the specified substrings.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateDoesNotContainAllAttribute(params string[] values) : ValidationAttribute(DoesNotContainAll.ValidatorName)
{
    private readonly string[] _values = values;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string str && str.CheckDoesNotContainAll(_values))
            return ValidationResult.CreateFromValidationSuccess();
        var message = Message ?? DoesNotContainAll.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("substrings", _values) });
    }
}
