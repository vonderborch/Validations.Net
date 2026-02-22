using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value matches the specified pattern.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateIsMatchAttribute(string pattern) : ValidationAttribute(IsMatch.ValidatorName)
{
    private readonly string _pattern = pattern;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string str && str.CheckIsMatch(_pattern))
            return ValidationResult.CreateFromValidationSuccess();
        if (value is not null && value is not string)
            return ValidationResult.CreateFromValidationFailure(Name, "Value is not a string", memberName, blackboard,
                new List<(string key, object? value)> { ("value", value) });
        var message = Message ?? IsMatch.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("pattern", _pattern) });
    }
}
