using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is not sorted.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsNotSortedAttribute() : ValidationAttribute(IsNotSorted.ValidatorName)
{
    /// <summary>
    /// When true, validates that it is not descending-sorted; otherwise not ascending-sorted.
    /// </summary>
    public bool Descending { get; set; }

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.Collections.IEnumerable enumerable)
        {
            IComparable? prev = null;
            bool first = true;
            bool isSorted = true;
            foreach (var item in enumerable)
            {
                if (item is not IComparable comparable)
                    break;
                if (!first)
                {
                    int cmp = prev!.CompareTo(comparable);
                    if (Descending ? cmp < 0 : cmp > 0)
                    {
                        isSorted = false;
                        break;
                    }
                }
                prev = comparable;
                first = false;
            }
            if (!isSorted)
                return ValidationResult.CreateFromValidationSuccess();
            var msg = Message ?? IsNotSorted.DefaultValidationFailureMessage;
            return ValidationResult.CreateFromValidationFailure(Name, msg, memberName, blackboard,
                new List<(string key, object? value)> { ("value", value), ("descending", Descending) });
        }
        var message = Message ?? IsNotSorted.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
