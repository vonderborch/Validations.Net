using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is sorted.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsSortedAttribute() : ValidationAttribute(IsSorted.ValidatorName)
{
    /// <summary>
    /// When true, validates descending order; otherwise ascending.
    /// </summary>
    public bool Descending { get; set; }

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.Collections.IEnumerable enumerable && enumerable.CheckIsSorted(Descending))
            return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsSorted.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("descending", Descending) });
    }
}
