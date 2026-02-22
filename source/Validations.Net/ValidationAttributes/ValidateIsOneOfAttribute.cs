using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is one of the allowed values.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateIsOneOfAttribute(params object[] values) : ValidationAttribute(IsOneOf.ValidatorName)
{
    private readonly object[] _values = values;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value.CheckIsOneOf(_values))
            return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsOneOf.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(
            Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
