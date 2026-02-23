using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Age;

public readonly record struct AdultParams(int AdultAge = 18);

public sealed class AdultValidator : IValidator
{
    public AdultParams Params { get; }

    public AdultValidator(int adultAge = 18)
    {
        Params = new AdultParams(adultAge);
    }

    public string Name => IsAdult.ValidatorName;
    public string DefaultFailureMessage => IsAdult.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            int age when age.CheckIsAdult(Params.AdultAge) => ValidationResult.CreateFromValidationSuccess(),
            int age => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
                [("value", (object)age), ("adultAge", (object)Params.AdultAge)]),
            System.DateTime dob when dob.CheckIsAdult(Params.AdultAge) => ValidationResult.CreateFromValidationSuccess(),
            System.DateTime dob => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
                [("value", (object)dob), ("adultAge", (object)Params.AdultAge)]),
            DateTimeOffset dob when dob.CheckIsAdult(Params.AdultAge) => ValidationResult.CreateFromValidationSuccess(),
            DateTimeOffset dob => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
                [("value", (object)dob), ("adultAge", (object)Params.AdultAge)]),
            _ => ValidationResult.CreateFromValidationFailure(Name, "Value is not an int, DateTime, or DateTimeOffset", memberName, blackboard,
                [("value", value)])
        };
    }
}
