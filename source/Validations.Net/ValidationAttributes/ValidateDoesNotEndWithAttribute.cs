using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value does not end with the specified suffix.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateDoesNotEndWithAttribute(string suffix) : ValidationAttribute(DoesNotEndWith.ValidatorName)
{
    private readonly string _suffix = suffix;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is null)
            return ValidationResult.CreateFromValidationSuccess();
        if (value is string str && str.CheckDoesNotEndWith(_suffix))
            return ValidationResult.CreateFromValidationSuccess();
        if (value is not null && value is not string)
            return ValidationResult.CreateFromValidationFailure(Name, "Value is not a string", memberName, blackboard,
                new List<(string key, object? value)> { ("value", value) });
        var message = Message ?? DoesNotEndWith.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("suffix", _suffix) });
    }
}
