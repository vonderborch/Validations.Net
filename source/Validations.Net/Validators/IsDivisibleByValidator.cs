using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public readonly record struct IsDivisibleByParams(long Divisor);

public sealed class IsDivisibleByValidator : IValidator
{
    public IsDivisibleByParams Params { get; }

    public IsDivisibleByValidator(long divisor)
    {
        Params = new IsDivisibleByParams(divisor);
    }

    public string Name => IsDivisibleBy.ValidatorName;
    public string DefaultFailureMessage => IsDivisibleBy.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is null || Params.Divisor == 0)
            return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
                [("value", value), ("divisor", Params.Divisor)]);
        if (value is long l && l.CheckIsDivisibleBy(Params.Divisor))
            return ValidationResult.CreateFromValidationSuccess();
        if (value is int i && ((long)i).CheckIsDivisibleBy(Params.Divisor))
            return ValidationResult.CreateFromValidationSuccess();
        try
        {
            long converted = Convert.ToInt64(value);
            if (converted.CheckIsDivisibleBy(Params.Divisor))
                return ValidationResult.CreateFromValidationSuccess();
        }
        catch { /* fall through to failure */ }
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("divisor", Params.Divisor)]);
    }
}
