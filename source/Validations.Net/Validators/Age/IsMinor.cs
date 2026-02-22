using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Age;

/// <summary>
/// The IsMinor class provides methods for validation to ensure that
/// an age or date of birth indicates minority (under adult age).
/// </summary>
public static class IsMinor
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsMinor";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Must be a minor";

    /// <summary>
    /// Checks if the given age indicates minority.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsMinor(this int age, int adultAge = 18)
    {
        return age < adultAge;
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
    /// Checks if the given date of birth indicates minority based on age calculated from UTC now.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsMinor(this System.DateTime dateOfBirth, int adultAge = 18)
    {
        int age = GetAgeFromDateOfBirth(dateOfBirth);
        return age < adultAge;
    }

    /// <summary>
    /// Checks if the given date of birth indicates minority based on age calculated from UTC now.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsMinor(this DateTimeOffset dateOfBirth, int adultAge = 18)
    {
        int age = GetAgeFromDateOfBirth(dateOfBirth.UtcDateTime);
        return age < adultAge;
    }

    /// <summary>
    /// Validates whether the given age indicates minority.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsMinor(this int age, int adultAge = 18, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(age))] string? parameterName = null)
    {
        if (!age.CheckIsMinor(adultAge))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", (object)age), ("adultAge", (object)adultAge)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given date of birth indicates minority.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsMinor(this System.DateTime dateOfBirth, int adultAge = 18, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(dateOfBirth))] string? parameterName = null)
    {
        if (!dateOfBirth.CheckIsMinor(adultAge))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", (object)dateOfBirth), ("adultAge", (object)adultAge)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates whether the given date of birth indicates minority.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsMinor(this DateTimeOffset dateOfBirth, int adultAge = 18, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(dateOfBirth))] string? parameterName = null)
    {
        if (!dateOfBirth.CheckIsMinor(adultAge))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", (object)dateOfBirth), ("adultAge", (object)adultAge)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given age indicates minority, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsMinor(this int age, int adultAge = 18, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(age))] string? parameterName = null)
    {
        var validationResult = age.ValidateIsMinor(adultAge, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return age;
    }

    /// <summary>
    /// Ensures the given date of birth indicates minority, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static System.DateTime EnsureIsMinor(this System.DateTime dateOfBirth, int adultAge = 18, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(dateOfBirth))] string? parameterName = null)
    {
        var validationResult = dateOfBirth.ValidateIsMinor(adultAge, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return dateOfBirth;
    }

    /// <summary>
    /// Ensures the given date of birth indicates minority, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTimeOffset EnsureIsMinor(this DateTimeOffset dateOfBirth, int adultAge = 18, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(dateOfBirth))] string? parameterName = null)
    {
        var validationResult = dateOfBirth.ValidateIsMinor(adultAge, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return dateOfBirth;
    }
}
