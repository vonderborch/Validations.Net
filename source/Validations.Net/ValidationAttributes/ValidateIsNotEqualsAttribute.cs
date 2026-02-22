using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value does not equal the expected value.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateIsNotEqualsAttribute(object? expected) : ValidationAttribute(IsNotEquals.ValidatorName)
{
    private readonly object? _expected = expected;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value.CheckIsNotEquals(_expected))
            return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsNotEquals.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(
            Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("expected", _expected) });
    }
}
