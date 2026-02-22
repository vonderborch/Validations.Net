using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is the same object reference as the expected value.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateIsSameAsAttribute(object? other) : ValidationAttribute(IsSameAs.ValidatorName)
{
    private readonly object? _other = other;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value.CheckIsSameAs(_other))
            return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsSameAs.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(
            Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("other", _other) });
    }
}
