using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is not the same object reference as the expected value.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateIsNotSameAsAttribute(object? other) : ValidationAttribute(IsNotSameAs.ValidatorName)
{
    private readonly object? _other = other;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (!ReferenceEquals(value, _other))
            return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsNotSameAs.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(
            Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("other", _other) });
    }
}
