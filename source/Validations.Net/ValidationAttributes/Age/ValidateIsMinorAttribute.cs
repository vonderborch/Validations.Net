using SimpleBlackboard.Net;
using Validations.Net.Validators.Age;

namespace Validations.Net.ValidationAttributes.Age;

/// <summary>
/// Validates that the decorated member's value indicates minority.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsMinorAttribute(int adultAge = 18) : ValidationAttribute(IsMinor.ValidatorName)
{
    private readonly int _adultAge = adultAge;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is int age)
            return age.CheckIsMinor(_adultAge) ? ValidationResult.CreateFromValidationSuccess() : Fail(age, memberName, blackboard);
        if (value is System.DateTime dt)
            return dt.CheckIsMinor(_adultAge) ? ValidationResult.CreateFromValidationSuccess() : Fail(dt, memberName, blackboard);
        if (value is DateTimeOffset dto)
            return dto.CheckIsMinor(_adultAge) ? ValidationResult.CreateFromValidationSuccess() : Fail(dto, memberName, blackboard);

        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = Message ?? IsMinor.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            [("value", value), ("adultAge", (object)_adultAge)]);
    }
}
