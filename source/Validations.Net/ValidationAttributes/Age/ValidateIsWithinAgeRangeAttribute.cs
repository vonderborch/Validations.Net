using SimpleBlackboard.Net;
using Validations.Net.Validators.Age;

namespace Validations.Net.ValidationAttributes.Age;

/// <summary>
/// Validates that the decorated member's value (age or date of birth) falls within the specified age range.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsWithinAgeRangeAttribute(int minAge, int maxAge) : ValidationAttribute(IsWithinAgeRange.ValidatorName)
{
    private readonly int _minAge = minAge;
    private readonly int _maxAge = maxAge;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is int age)
            return age.CheckIsWithinAgeRange(_minAge, _maxAge) ? ValidationResult.CreateFromValidationSuccess() : Fail(age, memberName, blackboard);
        if (value is System.DateTime dt)
            return dt.CheckIsWithinAgeRange(_minAge, _maxAge) ? ValidationResult.CreateFromValidationSuccess() : Fail(dt, memberName, blackboard);
        if (value is DateTimeOffset dto)
            return dto.CheckIsWithinAgeRange(_minAge, _maxAge) ? ValidationResult.CreateFromValidationSuccess() : Fail(dto, memberName, blackboard);

        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = Message ?? IsWithinAgeRange.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            [("value", value), ("minAge", (object)_minAge), ("maxAge", (object)_maxAge)]);
    }
}
