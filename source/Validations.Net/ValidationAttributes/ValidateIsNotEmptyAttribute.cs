using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is not empty.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsNotEmptyAttribute() : ValidationAttribute(IsNotEmpty.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s) return s.CheckIsNotEmpty() ? ValidationResult.CreateFromValidationSuccess() : Fail(value, memberName, blackboard);
        if (value is System.Collections.ICollection c) return c.Count != 0 ? ValidationResult.CreateFromValidationSuccess() : Fail(value, memberName, blackboard);
        if (value is System.Collections.IEnumerable e)
        {
            var enumerator = e.GetEnumerator();
            try
            {
                return enumerator.MoveNext() ? ValidationResult.CreateFromValidationSuccess() : Fail(value, memberName, blackboard);
            }
            finally
            {
                if (enumerator is IDisposable d) d.Dispose();
            }
        }

        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = Message ?? IsNotEmpty.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
