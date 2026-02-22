using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value does not have the specified count.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateIsNotCountAttribute(int count) : ValidationAttribute(IsNotCount.ValidatorName)
{
    private readonly int _count = count;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        bool isNotCount = value switch
        {
            System.Collections.ICollection c => c.CheckIsNotCount(_count),
            System.Collections.IEnumerable e => e.CheckIsNotCount(_count),
            _ => true
        };

        if (isNotCount) return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsNotCount.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("expectedCount", _count) });
    }
}
