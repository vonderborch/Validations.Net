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
