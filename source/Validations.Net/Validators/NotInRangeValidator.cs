using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public readonly record struct NotInRangeParams<T>(
    T Min, T Max,
    bool MinInclusive = true, bool MaxInclusive = true) where T : IComparable<T>;

public sealed class NotInRangeValidator : IValidator
{
    public object Min { get; }
    public object Max { get; }
    public bool MinInclusive { get; }
    public bool MaxInclusive { get; }

    public NotInRangeValidator(object min, object max, bool minInclusive = true, bool maxInclusive = true)
    {
        Min = min;
        Max = max;
        MinInclusive = minInclusive;
        MaxInclusive = maxInclusive;
    }

    public string Name => IsNotInRange.ValidatorName;
    public string DefaultFailureMessage => IsNotInRange.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is IComparable comparable && comparable.CheckIsNotInRange(Min, Max, MinInclusive, MaxInclusive))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("min", Min), ("max", Max)]);
    }
}

public sealed class NotInRangeValidator<T> : IValidator where T : IComparable<T>
{
    public NotInRangeParams<T> Params { get; }

    public NotInRangeValidator(T min, T max, bool minInclusive = true, bool maxInclusive = true)
    {
        Params = new NotInRangeParams<T>(min, max, minInclusive, maxInclusive);
    }

    public string Name => IsNotInRange.ValidatorName;
    public string DefaultFailureMessage => IsNotInRange.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is IComparable comparable && comparable.CheckIsNotInRange(Params.Min, Params.Max, Params.MinInclusive, Params.MaxInclusive))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("min", Params.Min), ("max", Params.Max)]);
    }
}
