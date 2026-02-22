using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is less than the comparand.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateIsLessThanAttribute(object comparand) : ValidationAttribute(IsLessThan.ValidatorName)
{
    private readonly object _comparand = comparand;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is IComparable comparable && comparable.CheckIsLessThan(_comparand))
            return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsLessThan.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(
            Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("comparand", _comparand) });
    }
}
