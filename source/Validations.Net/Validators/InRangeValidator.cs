using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public readonly record struct InRangeParams<T>(
    T Min, T Max,
    bool MinInclusive = true, bool MaxInclusive = true) where T : IComparable<T>;

public sealed class InRangeValidator : IValidator
{
    public object Min { get; }
    public object Max { get; }
    public bool MinInclusive { get; }
    public bool MaxInclusive { get; }

    public InRangeValidator(object min, object max, bool minInclusive = true, bool maxInclusive = true)
    {
        Min = min;
        Max = max;
        MinInclusive = minInclusive;
        MaxInclusive = maxInclusive;
    }

    public string Name => IsInRange.ValidatorName;
    public string DefaultFailureMessage => IsInRange.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is IComparable comparable && comparable.CheckIsInRange(Min, Max, MinInclusive, MaxInclusive))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("min", Min), ("max", Max)]);
    }
}

public sealed class InRangeValidator<T> : IValidator where T : IComparable<T>
{
    public InRangeParams<T> Params { get; }

    public InRangeValidator(T min, T max, bool minInclusive = true, bool maxInclusive = true)
    {
        Params = new InRangeParams<T>(min, max, minInclusive, maxInclusive);
    }

    public string Name => IsInRange.ValidatorName;
    public string DefaultFailureMessage => IsInRange.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is IComparable comparable && comparable.CheckIsInRange(Params.Min, Params.Max, Params.MinInclusive, Params.MaxInclusive))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("min", Params.Min), ("max", Params.Max)]);
    }
}
