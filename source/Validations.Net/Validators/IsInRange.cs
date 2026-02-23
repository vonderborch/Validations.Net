using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsInRange class provides methods for validation to ensure that
/// a value is within a specified range. Includes functionality to check, enforce,
/// and validate instances where range constraints are required.
/// </summary>
public static class IsInRange
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsInRange";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must be within the specified range";

    /// <summary>
    /// Checks if the given value is within the specified range (non-generic overload for boxed values).
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when min is greater than max.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsInRange(this IComparable? value, object? min, object? max, bool minInclusive = true, bool maxInclusive = true)
    {
        if (value is null) return false;
        if (min is IComparable comparableMin && max is not null && comparableMin.CompareTo(max) > 0)
            throw new ArgumentException($"min ({min}) must not be greater than max ({max})");
        try
        {
            int lower = value.CompareTo(min);
            if (minInclusive ? lower < 0 : lower <= 0)
                return false;
            int upper = value.CompareTo(max);
            return maxInclusive ? upper <= 0 : upper < 0;
        }
        catch (ArgumentException) { return false; }
    }

    /// <summary>
    /// Checks if the given value is within the specified range.
    /// </summary>
    /// <param name="minInclusive">If true, the minimum bound is inclusive; otherwise exclusive.</param>
    /// <param name="maxInclusive">If true, the maximum bound is inclusive; otherwise exclusive.</param>
    /// <exception cref="ArgumentException">Thrown when min is greater than max.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsInRange<T>(this T value, T min, T max, bool minInclusive = true, bool maxInclusive = true) where T : IComparable<T>
    {
        if (value is null) return false;
        if (min is not null && max is not null && min.CompareTo(max) > 0)
            throw new ArgumentException($"min ({min}) must not be greater than max ({max})");

        int lower = value.CompareTo(min);
        if (minInclusive ? lower < 0 : lower <= 0)
            return false;

        int upper = value.CompareTo(max);
        return maxInclusive ? upper <= 0 : upper < 0;
    }

    /// <summary>
    /// Validates whether the given value is within the specified range.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsInRange<T>(this T value, T min, T max, bool minInclusive = true, bool maxInclusive = true,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        if (!value.CheckIsInRange(min, max, minInclusive, maxInclusive))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("min", min), ("max", max), ("minInclusive", minInclusive), ("maxInclusive", maxInclusive)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is within the specified range, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureIsInRange<T>(this T value, T min, T max, bool minInclusive = true, bool maxInclusive = true,
        IBlackboard? blackboard = null, string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        var validationResult = value.ValidateIsInRange(min, max, minInclusive, maxInclusive, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

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

public sealed class ValidateIsInRangeAttribute(object min, object max, bool minInclusive = true, bool maxInclusive = true)
    : ValidatorAttribute(new InRangeValidator(min, max, minInclusive, maxInclusive));
