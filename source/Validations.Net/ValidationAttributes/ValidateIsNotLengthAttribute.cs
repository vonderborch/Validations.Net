using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value does not have the specified length.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateIsNotLengthAttribute(int length) : ValidationAttribute(IsNotLength.ValidatorName)
{
    private readonly int _length = length;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
        {
            if (s is null || s.Length != _length)
                return ValidationResult.CreateFromValidationSuccess();
            var message = Message ?? IsNotLength.DefaultValidationFailureMessage;
            return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
                new List<(string key, object? value)> { ("value", value), ("expectedLength", _length), ("actualLength", s.Length) });
        }

        if (value is System.Collections.ICollection c)
        {
            if (c is null || c.Count != _length)
                return ValidationResult.CreateFromValidationSuccess();
            var message = Message ?? IsNotLength.DefaultValidationFailureMessage;
            return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
                new List<(string key, object? value)> { ("value", value), ("expectedLength", _length), ("actualLength", c.Count) });
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}
