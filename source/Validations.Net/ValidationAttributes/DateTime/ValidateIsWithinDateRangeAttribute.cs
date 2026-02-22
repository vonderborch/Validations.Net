using SimpleBlackboard.Net;
using Validations.Net.Validators.DateTime;
using IsWithinDateRangeValidator = Validations.Net.Validators.DateTime.IsWithinDateRange;

namespace Validations.Net.ValidationAttributes.DateTime;

/// <summary>
/// Validates that the decorated member's value is within the specified date range.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsWithinDateRangeAttribute(object min, object max) : ValidationAttribute(IsWithinDateRangeValidator.ValidatorName)
{
    private readonly object _min = min;
    private readonly object _max = max;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.DateTime dt && _min is System.DateTime minDt && _max is System.DateTime maxDt)
            return dt.CheckIsWithinDateRange(minDt, maxDt) ? ValidationResult.CreateFromValidationSuccess() : Fail(dt, memberName, blackboard);
        if (value is DateTimeOffset dto && _min is DateTimeOffset minDto && _max is DateTimeOffset maxDto)
            return dto.CheckIsWithinDateRange(minDto, maxDto) ? ValidationResult.CreateFromValidationSuccess() : Fail(dto, memberName, blackboard);

        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = Message ?? IsWithinDateRangeValidator.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            [("value", value), ("min", _min), ("max", _max)]);
    }
}
