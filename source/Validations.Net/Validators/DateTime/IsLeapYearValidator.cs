using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.DateTime;

public sealed class IsLeapYearValidator : IValidator
{
    public static readonly IsLeapYearValidator Instance = new();
    public string Name => IsLeapYear.ValidatorName;
    public string DefaultFailureMessage => IsLeapYear.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is int year)
            return year.ValidateIsLeapYear(blackboard, DefaultFailureMessage, memberName);
        if (value is System.DateTime dt)
            return dt.ValidateIsLeapYear(blackboard, DefaultFailureMessage, memberName);
        if (value is DateTimeOffset dto)
            return dto.ValidateIsLeapYear(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not an int, DateTime, or DateTimeOffset", memberName, blackboard,
            [("value", value)]);
    }
}
