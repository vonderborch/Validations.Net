using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value has the specified count.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateIsCountAttribute(int count) : ValidationAttribute(IsCount.ValidatorName)
{
    private readonly int _count = count;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        bool isCount = value switch
        {
            System.Collections.IEnumerable e => e.CheckIsCount(_count),
            _ => false
        };

        if (isCount) return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsCount.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("expectedCount", _count) });
    }
}
