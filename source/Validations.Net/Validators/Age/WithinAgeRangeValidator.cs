using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Age;

public readonly record struct WithinAgeRangeParams(int MinAge, int MaxAge);

public sealed class WithinAgeRangeValidator : IValidator
{
    public WithinAgeRangeParams Params { get; }

    public WithinAgeRangeValidator(int minAge, int maxAge)
    {
        Params = new WithinAgeRangeParams(minAge, maxAge);
    }

    public string Name => IsWithinAgeRange.ValidatorName;
    public string DefaultFailureMessage => IsWithinAgeRange.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            int age when age.CheckIsWithinAgeRange(Params.MinAge, Params.MaxAge) => ValidationResult.CreateFromValidationSuccess(),
            int age => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
                [("value", (object)age), ("minAge", (object)Params.MinAge), ("maxAge", (object)Params.MaxAge)]),
            System.DateTime dob when dob.CheckIsWithinAgeRange(Params.MinAge, Params.MaxAge) => ValidationResult.CreateFromValidationSuccess(),
            System.DateTime dob => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
                [("value", (object)dob), ("minAge", (object)Params.MinAge), ("maxAge", (object)Params.MaxAge)]),
            DateTimeOffset dob when dob.CheckIsWithinAgeRange(Params.MinAge, Params.MaxAge) => ValidationResult.CreateFromValidationSuccess(),
            DateTimeOffset dob => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
                [("value", (object)dob), ("minAge", (object)Params.MinAge), ("maxAge", (object)Params.MaxAge)]),
            _ => ValidationResult.CreateFromValidationFailure(Name, "Value is not an int, DateTime, or DateTimeOffset", memberName, blackboard,
                [("value", value)])
        };
    }
}
