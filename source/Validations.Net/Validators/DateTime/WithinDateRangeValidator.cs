using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.DateTime;

public readonly record struct WithinDateRangeParams(System.DateTime Min, System.DateTime Max);

public sealed class WithinDateRangeValidator : IValidator
{
    public WithinDateRangeParams Params { get; }

    public WithinDateRangeValidator(System.DateTime min, System.DateTime max)
    {
        Params = new WithinDateRangeParams(min, max);
    }

    public string Name => IsWithinDateRange.ValidatorName;
    public string DefaultFailureMessage => IsWithinDateRange.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            System.DateTime dt when dt.CheckIsWithinDateRange(Params.Min, Params.Max) => ValidationResult.CreateFromValidationSuccess(),
            System.DateTime dt => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
                [("value", (object)dt), ("min", (object)Params.Min), ("max", (object)Params.Max)]),
            DateTimeOffset dto when dto.CheckIsWithinDateRange(new DateTimeOffset(Params.Min), new DateTimeOffset(Params.Max)) => ValidationResult.CreateFromValidationSuccess(),
            DateTimeOffset dto => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
                [("value", (object)dto), ("min", (object)Params.Min), ("max", (object)Params.Max)]),
            _ => ValidationResult.CreateFromValidationFailure(Name, "Value is not a DateTime or DateTimeOffset", memberName, blackboard,
                [("value", value)])
        };
    }
}
