using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value (string) contains at least one of the specified substrings.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateDoesContainAnyAttribute(params string[] values) : ValidationAttribute(DoesContainAny.ValidatorName)
{
    private readonly string[] _values = values;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string str && str.CheckDoesContainAny(_values))
            return ValidationResult.CreateFromValidationSuccess();
        var message = Message ?? DoesContainAny.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("substrings", _values) });
    }
}
