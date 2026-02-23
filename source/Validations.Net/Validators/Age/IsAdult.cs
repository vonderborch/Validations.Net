using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Age;

/// <summary>
/// The IsAdult class provides methods for validation to ensure that
/// an age or date of birth indicates adulthood.
/// </summary>
public static class IsAdult
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsAdult";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Must be an adult";

    /// <summary>
    /// Checks if the given age indicates adulthood.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsAdult(this int age, int adultAge = 18)
    {
        return age >= adultAge;
    }

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
    /// Checks if the given date of birth indicates adulthood based on age calculated from UTC now.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsAdult(this System.DateTime dateOfBirth, int adultAge = 18)
    {
        int age = GetAgeFromDateOfBirth(dateOfBirth);
        return age >= adultAge;
    }

    /// <summary>
    /// Checks if the given date of birth indicates adulthood based on age calculated from UTC now.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsAdult(this DateTimeOffset dateOfBirth, int adultAge = 18)
    {
        int age = GetAgeFromDateOfBirth(dateOfBirth.UtcDateTime);
        return age >= adultAge;
    }

    /// <summary>
    /// Validates whether the given age indicates adulthood.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsAdult(this int age, int adultAge = 18, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(age))] string? parameterName = null)
    {
        if (!age.CheckIsAdult(adultAge))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", (object)age), ("adultAge", (object)adultAge)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given date of birth indicates adulthood.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsAdult(this System.DateTime dateOfBirth, int adultAge = 18, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(dateOfBirth))] string? parameterName = null)
    {
        if (!dateOfBirth.CheckIsAdult(adultAge))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", (object)dateOfBirth), ("adultAge", (object)adultAge)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given date of birth indicates adulthood.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsAdult(this DateTimeOffset dateOfBirth, int adultAge = 18, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(dateOfBirth))] string? parameterName = null)
    {
        if (!dateOfBirth.CheckIsAdult(adultAge))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", (object)dateOfBirth), ("adultAge", (object)adultAge)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given age indicates adulthood, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsAdult(this int age, int adultAge = 18, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(age))] string? parameterName = null)
    {
        var validationResult = age.ValidateIsAdult(adultAge, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return age;
    }

    /// <summary>
    /// Ensures the given date of birth indicates adulthood, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static System.DateTime EnsureIsAdult(this System.DateTime dateOfBirth, int adultAge = 18, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(dateOfBirth))] string? parameterName = null)
    {
        var validationResult = dateOfBirth.ValidateIsAdult(adultAge, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return dateOfBirth;
    }

    /// <summary>
    /// Ensures the given date of birth indicates adulthood, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTimeOffset EnsureIsAdult(this DateTimeOffset dateOfBirth, int adultAge = 18, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(dateOfBirth))] string? parameterName = null)
    {
        var validationResult = dateOfBirth.ValidateIsAdult(adultAge, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return dateOfBirth;
    }
}

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

/// <summary>
/// Validates that the decorated member's value indicates adulthood.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsAdultAttribute(int adultAge = 18) : ValidationAttribute(IsAdult.ValidatorName)
{
    private readonly int _adultAge = adultAge;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is int age)
            return age.CheckIsAdult(_adultAge) ? ValidationResult.CreateFromValidationSuccess() : Fail(age, memberName, blackboard);
        if (value is System.DateTime dt)
            return dt.CheckIsAdult(_adultAge) ? ValidationResult.CreateFromValidationSuccess() : Fail(dt, memberName, blackboard);
        if (value is DateTimeOffset dto)
            return dto.CheckIsAdult(_adultAge) ? ValidationResult.CreateFromValidationSuccess() : Fail(dto, memberName, blackboard);

        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = Message ?? IsAdult.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            [("value", value), ("adultAge", (object)_adultAge)]);
    }
}
