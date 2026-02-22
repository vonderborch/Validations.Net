using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is an element of the specified allowed values.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsElementOfAttribute(params object?[] allowedValues) : ValidationAttribute(IsElementOf.ValidatorName)
{
    private readonly object?[] _allowedValues = allowedValues;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (_allowedValues.Length == 0)
            return Fail(value, memberName, blackboard);

        if (value is string str && _allowedValues.All(v => v is string))
        {
            var stringValues = _allowedValues.Cast<string>().ToArray();
            if (str.CheckIsElementOf(stringValues))
                return ValidationResult.CreateFromValidationSuccess();
        }
        else
        {
            if (((object?)value).CheckIsElementOf(_allowedValues))
                return ValidationResult.CreateFromValidationSuccess();
        }

        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = Message ?? IsElementOf.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("allowedValues", _allowedValues) });
    }
}
