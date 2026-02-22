using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is not within the specified range.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateIsNotInRangeAttribute(object min, object max, bool minInclusive = true, bool maxInclusive = true)
    : ValidationAttribute(IsNotInRange.ValidatorName)
{
    private readonly object _min = min;
    private readonly object _max = max;
    private readonly bool _minInclusive = minInclusive;
    private readonly bool _maxInclusive = maxInclusive;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is IComparable comparable)
        {
            int lower = comparable.CompareTo(_min);
            if (lower < 0)
                return ValidationResult.CreateFromValidationSuccess();

            int upper = comparable.CompareTo(_max);
            if (_maxInclusive ? upper > 0 : upper >= 0)
                return ValidationResult.CreateFromValidationSuccess();
        }

        var message = Message ?? IsNotInRange.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(
            Name, message, memberName, blackboard,
            new List<(string key, object? value)>
            {
                ("value", value), ("min", _min), ("max", _max),
                ("minInclusive", _minInclusive), ("maxInclusive", _maxInclusive)
            });
    }
}
