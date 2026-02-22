using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value contains exactly one element.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsSingleAttribute() : ValidationAttribute(IsSingle.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
        {
            if (s is not null && s.Length == 1)
                return ValidationResult.CreateFromValidationSuccess();
            var message = Message ?? IsSingle.DefaultValidationFailureMessage;
            return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
                new List<(string key, object? value)> { ("value", value), ("actualCount", s?.Length ?? -1) });
        }

        if (value is System.Collections.ICollection c)
        {
            if (c is not null && c.Count == 1)
                return ValidationResult.CreateFromValidationSuccess();
            var message = Message ?? IsSingle.DefaultValidationFailureMessage;
            return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
                new List<(string key, object? value)> { ("value", value), ("actualCount", c?.Count ?? -1) });
        }

        if (value is System.Collections.IEnumerable e)
        {
            var enumerator = e.GetEnumerator();
            try
            {
                if (!enumerator.MoveNext())
                {
                    var msg = Message ?? IsSingle.DefaultValidationFailureMessage;
                    return ValidationResult.CreateFromValidationFailure(Name, msg, memberName, blackboard,
                        new List<(string key, object? value)> { ("value", value), ("actualCount", 0) });
                }

                if (!enumerator.MoveNext())
                    return ValidationResult.CreateFromValidationSuccess();

                int count = 2;
                while (enumerator.MoveNext()) count++;

                var message2 = Message ?? IsSingle.DefaultValidationFailureMessage;
                return ValidationResult.CreateFromValidationFailure(Name, message2, memberName, blackboard,
                    new List<(string key, object? value)> { ("value", value), ("actualCount", count) });
            }
            finally
            {
                if (enumerator is IDisposable d) d.Dispose();
            }
        }

        var message3 = Message ?? IsSingle.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message3, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
