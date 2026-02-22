using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is greater than or equal to the comparand.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateIsGreaterThanOrEqualsAttribute(object comparand) : ValidationAttribute(IsGreaterThanOrEquals.ValidatorName)
{
    private readonly object _comparand = comparand;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is IComparable comparable && comparable.CompareTo(_comparand) >= 0)
            return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsGreaterThanOrEquals.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(
            Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("comparand", _comparand) });
    }
}
