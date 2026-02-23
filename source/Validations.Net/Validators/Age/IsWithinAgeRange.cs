using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Age;

/// <summary>
/// The IsWithinAgeRange class provides methods for validation to ensure that
/// an age or date of birth falls within a specified age range.
/// </summary>
public static class IsWithinAgeRange
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsWithinAgeRange";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Age must be within the specified range";

    /// <summary>
    /// Calculates age in years from date of birth to now (UTC).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int GetAgeFromDateOfBirth(System.DateTime dateOfBirth)
    {
        var today = System.DateTime.UtcNow;
        int age = today.Year - dateOfBirth.Year;
        if (dateOfBirth > today.AddYears(-age))
            age--;
        return age;
    }

    /// <summary>
    /// Checks if the given age is within the specified range.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsWithinAgeRange(this int age, int minAge, int maxAge)
    {
        return age >= minAge && age <= maxAge;
    }

    /// <summary>
    /// Checks if the given date of birth yields an age within the specified range.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsWithinAgeRange(this System.DateTime dateOfBirth, int minAge, int maxAge)
    {
        int age = GetAgeFromDateOfBirth(dateOfBirth);
        return age >= minAge && age <= maxAge;
    }

    /// <summary>
    /// Checks if the given date of birth yields an age within the specified range.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsWithinAgeRange(this DateTimeOffset dateOfBirth, int minAge, int maxAge)
    {
        int age = GetAgeFromDateOfBirth(dateOfBirth.UtcDateTime);
        return age >= minAge && age <= maxAge;
    }

    /// <summary>
    /// Validates whether the given age is within the specified range.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsWithinAgeRange(this int age, int minAge, int maxAge, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(age))] string? parameterName = null)
    {
        if (!age.CheckIsWithinAgeRange(minAge, maxAge))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", (object)age), ("minAge", (object)minAge), ("maxAge", (object)maxAge)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given date of birth yields an age within the specified range.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsWithinAgeRange(this System.DateTime dateOfBirth, int minAge, int maxAge, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(dateOfBirth))] string? parameterName = null)
    {
        if (!dateOfBirth.CheckIsWithinAgeRange(minAge, maxAge))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", (object)dateOfBirth), ("minAge", (object)minAge), ("maxAge", (object)maxAge)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given date of birth yields an age within the specified range.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsWithinAgeRange(this DateTimeOffset dateOfBirth, int minAge, int maxAge, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(dateOfBirth))] string? parameterName = null)
    {
        if (!dateOfBirth.CheckIsWithinAgeRange(minAge, maxAge))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", (object)dateOfBirth), ("minAge", (object)minAge), ("maxAge", (object)maxAge)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given age is within the specified range, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsWithinAgeRange(this int age, int minAge, int maxAge, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(age))] string? parameterName = null)
    {
        var validationResult = age.ValidateIsWithinAgeRange(minAge, maxAge, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return age;
    }

    /// <summary>
    /// Ensures the given date of birth yields an age within the specified range, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static System.DateTime EnsureIsWithinAgeRange(this System.DateTime dateOfBirth, int minAge, int maxAge, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(dateOfBirth))] string? parameterName = null)
    {
        var validationResult = dateOfBirth.ValidateIsWithinAgeRange(minAge, maxAge, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return dateOfBirth;
    }

    /// <summary>
    /// Ensures the given date of birth yields an age within the specified range, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTimeOffset EnsureIsWithinAgeRange(this DateTimeOffset dateOfBirth, int minAge, int maxAge, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(dateOfBirth))] string? parameterName = null)
    {
        var validationResult = dateOfBirth.ValidateIsWithinAgeRange(minAge, maxAge, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return dateOfBirth;
    }
}

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

/// <summary>
/// Validates that the decorated member's value (age or date of birth) falls within the specified age range.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsWithinAgeRangeAttribute(int minAge, int maxAge) : ValidationAttribute(IsWithinAgeRange.ValidatorName)
{
    private readonly int _minAge = minAge;
    private readonly int _maxAge = maxAge;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is int age)
            return age.CheckIsWithinAgeRange(_minAge, _maxAge) ? ValidationResult.CreateFromValidationSuccess() : Fail(age, memberName, blackboard);
        if (value is System.DateTime dt)
            return dt.CheckIsWithinAgeRange(_minAge, _maxAge) ? ValidationResult.CreateFromValidationSuccess() : Fail(dt, memberName, blackboard);
        if (value is DateTimeOffset dto)
            return dto.CheckIsWithinAgeRange(_minAge, _maxAge) ? ValidationResult.CreateFromValidationSuccess() : Fail(dto, memberName, blackboard);

        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = Message ?? IsWithinAgeRange.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            [("value", value), ("minAge", (object)_minAge), ("maxAge", (object)_maxAge)]);
    }
}
