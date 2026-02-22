using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value has the specified length.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateIsLengthAttribute(int length) : ValidationAttribute(IsLength.ValidatorName)
{
    private readonly int _length = length;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
        {
            if (s is not null && s.Length == _length)
                return ValidationResult.CreateFromValidationSuccess();
            var message = Message ?? IsLength.DefaultValidationFailureMessage;
            return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
                new List<(string key, object? value)> { ("value", value), ("expectedLength", _length), ("actualLength", s?.Length ?? -1) });
        }

        if (value is System.Collections.ICollection c)
        {
            if (c is not null && c.Count == _length)
                return ValidationResult.CreateFromValidationSuccess();
            var message = Message ?? IsLength.DefaultValidationFailureMessage;
            return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
                new List<(string key, object? value)> { ("value", value), ("expectedLength", _length), ("actualLength", c?.Count ?? -1) });
        }

        var msg = Message ?? IsLength.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, msg, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("expectedLength", _length) });
    }
}
