using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value equals the expected value.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateIsEqualsAttribute(object? expected) : ValidationAttribute(IsEquals.ValidatorName)
{
    private readonly object? _expected = expected;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        bool equal = Equals(value, _expected);
        if (equal)
            return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsEquals.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(
            Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("expected", _expected) });
    }
}
