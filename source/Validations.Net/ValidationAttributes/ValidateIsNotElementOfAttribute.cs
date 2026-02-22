using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is not an element of the specified disallowed values.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsNotElementOfAttribute(params object?[] disallowedValues) : ValidationAttribute(IsNotElementOf.ValidatorName)
{
    private readonly object?[] _disallowedValues = disallowedValues;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (_disallowedValues.Length == 0)
            return ValidationResult.CreateFromValidationSuccess();

        if (value is string str && _disallowedValues.All(v => v is string))
        {
            var stringValues = _disallowedValues.Cast<string>().ToArray();
            if (str.CheckIsNotElementOf(stringValues))
                return ValidationResult.CreateFromValidationSuccess();
        }
        else
        {
            if (((object?)value).CheckIsNotElementOf(_disallowedValues))
                return ValidationResult.CreateFromValidationSuccess();
        }

        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = Message ?? IsNotElementOf.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("disallowedValues", _disallowedValues) });
    }
}
