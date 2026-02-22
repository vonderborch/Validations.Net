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
        if (value is System.Collections.ICollection c)
        {
            if (c is not null && c.Count == _count)
                return ValidationResult.CreateFromValidationSuccess();
            var message = Message ?? IsCount.DefaultValidationFailureMessage;
            return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
                new List<(string key, object? value)> { ("value", value), ("expectedCount", _count), ("actualCount", c?.Count ?? -1) });
        }

        if (value is System.Collections.IEnumerable e)
        {
            int actual = 0;
            var enumerator = e.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    actual++;
                    if (actual > _count)
                        break;
                }

                if (actual == _count)
                    return ValidationResult.CreateFromValidationSuccess();
            }
            finally
            {
                if (enumerator is IDisposable d) d.Dispose();
            }

            var msg = Message ?? IsCount.DefaultValidationFailureMessage;
            return ValidationResult.CreateFromValidationFailure(Name, msg, memberName, blackboard,
                new List<(string key, object? value)> { ("value", value), ("expectedCount", _count), ("actualCount", actual) });
        }

        var message2 = Message ?? IsCount.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message2, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("expectedCount", _count) });
    }
}
