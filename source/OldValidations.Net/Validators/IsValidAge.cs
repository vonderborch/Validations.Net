using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Validates that a value represents a valid age.
/// </summary>
public static class IsValidAge
{
    private const string ValidatorName = nameof(IsValidAge);
    private const int MinAge = 0;
    private const int MaxAge = 150;

    #region Check Methods

    /// <summary>
    ///     Checks if the specified value is a valid adult age (18+).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is a valid adult age; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidAdultAge(int value)
    {
        return CheckIsValidAge(value, 18, MaxAge);
    }

    /// <summary>
    ///     Checks if the specified value is a valid age (0-150).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is a valid age; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidAge(int value)
    {
        return value >= MinAge && value <= MaxAge;
    }

    /// <summary>
    ///     Checks if the specified value is a valid age within the specified range.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="minAge">The minimum valid age (inclusive).</param>
    /// <param name="maxAge">The maximum valid age (inclusive).</param>
    /// <returns>True if the value is a valid age within the range; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidAge(int value, int minAge, int maxAge)
    {
        return value >= minAge && value <= maxAge;
    }

    /// <summary>
    ///     Checks if the specified value is a valid child age (0-17).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is a valid child age; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidChildAge(int value)
    {
        return CheckIsValidAge(value, 0, 17);
    }

    /// <summary>
    ///     Checks if the specified value is a valid senior age (65+).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is a valid senior age; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidSeniorAge(int value)
    {
        return CheckIsValidAge(value, 65, MaxAge);
    }

    /// <summary>
    ///     Checks if the specified value is a valid working age (18-65).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is a valid working age; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidWorkingAge(int value)
    {
        return CheckIsValidAge(value, 18, 65);
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates that the specified value is a valid adult age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is a valid adult age.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidAdultAge(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidAdultAge(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("minAge", 18),
            ("maxAge", MaxAge)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is not a valid adult age (must be 18 or older).",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is a valid age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is a valid age.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidAge(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidAge(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("minAge", MinAge),
            ("maxAge", MaxAge)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is not a valid age (must be between {MinAge} and {MaxAge}).",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is a valid age within the specified range.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="minAge">The minimum valid age (inclusive).</param>
    /// <param name="maxAge">The maximum valid age (inclusive).</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is a valid age within the range.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidAge(int value, int minAge, int maxAge, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidAge(value, minAge, maxAge))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("minAge", minAge),
            ("maxAge", maxAge)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is not a valid age (must be between {minAge} and {maxAge}).",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is a valid child age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is a valid child age.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidChildAge(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidChildAge(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("minAge", 0),
            ("maxAge", 17)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is not a valid child age (must be between 0 and 17).",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is a valid senior age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is a valid senior age.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidSeniorAge(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidSeniorAge(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("minAge", 65),
            ("maxAge", MaxAge)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is not a valid senior age (must be 65 or older).",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is a valid working age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is a valid working age.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidWorkingAge(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidWorkingAge(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("minAge", 18),
            ("maxAge", 65)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is not a valid working age (must be between 18 and 65).",
            parameterName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that the specified value is a valid adult age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a valid adult age.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsValidAdultAge(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidAdultAge(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the specified value is a valid age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a valid age.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsValidAge(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidAge(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the specified value is a valid age within the specified range.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="minAge">The minimum valid age (inclusive).</param>
    /// <param name="maxAge">The maximum valid age (inclusive).</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a valid age within the range.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsValidAge(int value, int minAge, int maxAge, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidAge(value, minAge, maxAge, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the specified value is a valid child age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a valid child age.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsValidChildAge(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidChildAge(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the specified value is a valid senior age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a valid senior age.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsValidSeniorAge(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidSeniorAge(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the specified value is a valid working age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a valid working age.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsValidWorkingAge(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidWorkingAge(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    #endregion
}
