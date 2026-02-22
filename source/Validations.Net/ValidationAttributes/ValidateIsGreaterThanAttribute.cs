using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is greater than the comparand.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateIsGreaterThanAttribute(object comparand) : ValidationAttribute(IsGreaterThan.ValidatorName)
{
    private readonly object _comparand = comparand;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is IComparable comparable && comparable.CheckIsGreaterThan(_comparand))
            return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsGreaterThan.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(
            Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("comparand", _comparand) });
    }
}
