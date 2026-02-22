using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is within the specified range.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateIsInRangeAttribute(object min, object max, bool minInclusive = true, bool maxInclusive = true)
    : ValidationAttribute(IsInRange.ValidatorName)
{
    private readonly object _min = min;
    private readonly object _max = max;
    private readonly bool _minInclusive = minInclusive;
    private readonly bool _maxInclusive = maxInclusive;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is IComparable comparable && comparable.CheckIsInRange(_min, _max, _minInclusive, _maxInclusive))
            return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsInRange.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(
            Name, message, memberName, blackboard,
            new List<(string key, object? value)>
            {
                ("value", value), ("min", _min), ("max", _max),
                ("minInclusive", _minInclusive), ("maxInclusive", _maxInclusive)
            });
    }
}
