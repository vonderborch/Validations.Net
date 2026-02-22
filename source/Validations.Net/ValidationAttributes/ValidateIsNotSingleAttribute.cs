using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value does not contain exactly one element.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsNotSingleAttribute() : ValidationAttribute(IsNotSingle.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
        {
            if (s is null || s.Length != 1)
                return ValidationResult.CreateFromValidationSuccess();
            var message = Message ?? IsNotSingle.DefaultValidationFailureMessage;
            return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
                new List<(string key, object? value)> { ("value", value), ("actualCount", s.Length) });
        }

        if (value is System.Collections.ICollection c)
        {
            if (c is null || c.Count != 1)
                return ValidationResult.CreateFromValidationSuccess();
            var message = Message ?? IsNotSingle.DefaultValidationFailureMessage;
            return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
                new List<(string key, object? value)> { ("value", value), ("actualCount", c.Count) });
        }

        if (value is System.Collections.IEnumerable e)
        {
            var enumerator = e.GetEnumerator();
            try
            {
                if (!enumerator.MoveNext())
                    return ValidationResult.CreateFromValidationSuccess();

                if (!enumerator.MoveNext())
                {
                    var msg = Message ?? IsNotSingle.DefaultValidationFailureMessage;
                    return ValidationResult.CreateFromValidationFailure(Name, msg, memberName, blackboard,
                        new List<(string key, object? value)> { ("value", value), ("actualCount", 1) });
                }

                return ValidationResult.CreateFromValidationSuccess();
            }
            finally
            {
                if (enumerator is IDisposable d) d.Dispose();
            }
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}
