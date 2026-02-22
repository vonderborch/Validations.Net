using SimpleBlackboard.Net;
using Validations.Net.Validators.DateTime;

namespace Validations.Net.ValidationAttributes.DateTime;

/// <summary>
/// Validates that the decorated member's value is a leap year.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsLeapYearAttribute() : ValidationAttribute(IsLeapYear.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is int year)
            return year.CheckIsLeapYear() ? ValidationResult.CreateFromValidationSuccess() : Fail(year, memberName, blackboard);
        if (value is System.DateTime dt)
            return dt.CheckIsLeapYear() ? ValidationResult.CreateFromValidationSuccess() : Fail(dt, memberName, blackboard);
        if (value is DateTimeOffset dto)
            return dto.CheckIsLeapYear() ? ValidationResult.CreateFromValidationSuccess() : Fail(dto, memberName, blackboard);

        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = Message ?? IsLeapYear.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard, [("value", value)]);
    }
}
