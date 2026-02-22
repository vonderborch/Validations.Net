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
        if (value is System.Collections.ICollection c)
        {
            if (c is null || c.Count != _count)
                return ValidationResult.CreateFromValidationSuccess();
            var message = Message ?? IsNotCount.DefaultValidationFailureMessage;
            return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
                new List<(string key, object? value)> { ("value", value), ("expectedCount", _count), ("actualCount", c.Count) });
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
                        return ValidationResult.CreateFromValidationSuccess();
                }

                if (actual != _count)
                    return ValidationResult.CreateFromValidationSuccess();
            }
            finally
            {
                if (enumerator is IDisposable d) d.Dispose();
            }

            var msg = Message ?? IsNotCount.DefaultValidationFailureMessage;
            return ValidationResult.CreateFromValidationFailure(Name, msg, memberName, blackboard,
                new List<(string key, object? value)> { ("value", value), ("expectedCount", _count), ("actualCount", actual) });
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}
