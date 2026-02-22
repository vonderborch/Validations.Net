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
        bool isNotLength = value switch
        {
            string s => s.CheckIsNotLength(_length),
            System.Collections.ICollection c => c.CheckIsNotLength(_length),
            _ => true
        };

        if (isNotLength) return ValidationResult.CreateFromValidationSuccess();

        int actualLength = value switch
        {
            string s => s.Length,
            System.Collections.ICollection c => c.Count,
            _ => -1
        };

        var message = Message ?? IsNotLength.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("expectedLength", _length), ("actualLength", actualLength) });
    }
}
