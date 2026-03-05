using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators.DateTime;

/// <summary>
/// The IsWithinDateRange class provides methods for validation to ensure that
/// a date/time value falls within a specified date range.
/// </summary>
public static class IsWithinDateRange
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsWithinDateRange";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Must be within the date range";

    /// <summary>
    /// Checks if the given value is within the specified date range.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsWithinDateRange(this System.DateTime value, System.DateTime min, System.DateTime max)
    {
        return value >= min && value <= max;
    }

    /// <summary>
    /// Checks if the given value is within the specified date range.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsWithinDateRange(this DateTimeOffset value, DateTimeOffset min, DateTimeOffset max)
    {
        return value >= min && value <= max;
    }

    /// <summary>
    /// Validates whether the given value is within the specified date range.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsWithinDateRange(this System.DateTime value, System.DateTime min, System.DateTime max, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsWithinDateRange(min, max))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", (object)value), ("min", (object)min), ("max", (object)max)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given value is within the specified date range.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsWithinDateRange(this DateTimeOffset value, DateTimeOffset min, DateTimeOffset max, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckIsWithinDateRange(min, max))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", (object)value), ("min", (object)min), ("max", (object)max)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is within the specified date range, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static System.DateTime EnsureIsWithinDateRange(this System.DateTime value, System.DateTime min, System.DateTime max, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsWithinDateRange(min, max, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures the given value is within the specified date range, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTimeOffset EnsureIsWithinDateRange(this DateTimeOffset value, DateTimeOffset min, DateTimeOffset max, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateIsWithinDateRange(min, max, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}

public readonly record struct WithinDateRangeParams(System.DateTime Min, System.DateTime Max);

public sealed class WithinDateRangeValidator : IValidator
{
    public WithinDateRangeParams Params { get; }

    public WithinDateRangeValidator(System.DateTime min, System.DateTime max)
    {
        this.Params = new WithinDateRangeParams(min, max);
    }

    public string Name => IsWithinDateRange.ValidatorName;
    public string DefaultFailureMessage => IsWithinDateRange.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            System.DateTime dt when dt.CheckIsWithinDateRange(this.Params.Min, this.Params.Max) => ValidationResult.CreateFromValidationSuccess(),
            System.DateTime dt => ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard,
                [("value", (object)dt), ("min", (object)this.Params.Min), ("max", (object)this.Params.Max)]),
            DateTimeOffset dto when dto.CheckIsWithinDateRange(new DateTimeOffset(this.Params.Min), new DateTimeOffset(this.Params.Max)) => ValidationResult.CreateFromValidationSuccess(),
            DateTimeOffset dto => ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard,
                [("value", (object)dto), ("min", (object)this.Params.Min), ("max", (object)this.Params.Max)]),
            _ => ValidationResult.CreateFromValidationFailure(this.Name, "Value is not a DateTime or DateTimeOffset", memberName, blackboard,
                [("value", value)])
        };
    }
}

/// <summary>
/// Validates that the decorated member's value is within the specified date range.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsWithinDateRangeAttribute(object min, object max) : ValidationAttribute(IsWithinDateRange.ValidatorName)
{
    private readonly object _min = min;
    private readonly object _max = max;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.DateTime dt && this._min is System.DateTime minDt && this._max is System.DateTime maxDt)
            return dt.CheckIsWithinDateRange(minDt, maxDt) ? ValidationResult.CreateFromValidationSuccess() : Fail(dt, memberName, blackboard);
        if (value is DateTimeOffset dto && this._min is DateTimeOffset minDto && this._max is DateTimeOffset maxDto)
            return dto.CheckIsWithinDateRange(minDto, maxDto) ? ValidationResult.CreateFromValidationSuccess() : Fail(dto, memberName, blackboard);

        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = this.Message ?? IsWithinDateRange.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(this.Name, message, memberName, blackboard,
            [("value", value), ("min", this._min), ("max", this._max)]);
    }
}
