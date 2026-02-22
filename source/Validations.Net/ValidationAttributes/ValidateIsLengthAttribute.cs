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
        bool isLength = value switch
        {
            string s => s.CheckIsLength(_length),
            System.Collections.ICollection c => c.CheckIsLength(_length),
            _ => false
        };

        if (isLength) return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsLength.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("expectedLength", _length) });
    }
}
