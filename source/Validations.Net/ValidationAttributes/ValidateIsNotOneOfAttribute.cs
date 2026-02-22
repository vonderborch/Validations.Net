using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is not one of the disallowed values.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateIsNotOneOfAttribute(params object[] values) : ValidationAttribute(IsNotOneOf.ValidatorName)
{
    private readonly object[] _values = values;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value.CheckIsNotOneOf(_values))
            return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsNotOneOf.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(
            Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
