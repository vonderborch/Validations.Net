using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Validates that a value does NOT represent a valid age.
/// </summary>
public static class IsNotValidAge
{
    private const string ValidatorName = nameof(IsNotValidAge);

    #region Check Methods

    /// <summary>
    ///     Checks if the specified value is NOT a valid adult age (18+).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a valid adult age; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidAdultAge(int value)
    {
        return !IsValidAge.CheckIsValidAdultAge(value);
    }

    /// <summary>
    ///     Checks if the specified value is NOT a valid age (0-150).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a valid age; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidAge(int value)
    {
        return !IsValidAge.CheckIsValidAge(value);
    }

    /// <summary>
    ///     Checks if the specified value is NOT a valid age within the specified range.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="minAge">The minimum valid age (inclusive).</param>
    /// <param name="maxAge">The maximum valid age (inclusive).</param>
    /// <returns>True if the value is NOT a valid age within the range; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidAge(int value, int minAge, int maxAge)
    {
        return !IsValidAge.CheckIsValidAge(value, minAge, maxAge);
    }

    /// <summary>
    ///     Checks if the specified value is NOT a valid child age (0-17).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a valid child age; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidChildAge(int value)
    {
        return !IsValidAge.CheckIsValidChildAge(value);
    }

    /// <summary>
    ///     Checks if the specified value is NOT a valid senior age (65+).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a valid senior age; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidSeniorAge(int value)
    {
        return !IsValidAge.CheckIsValidSeniorAge(value);
    }

    /// <summary>
    ///     Checks if the specified value is NOT a valid working age (18-65).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a valid working age; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidWorkingAge(int value)
    {
        return !IsValidAge.CheckIsValidWorkingAge(value);
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates that the specified value is NOT a valid adult age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT a valid adult age.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidAdultAge(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidAdultAge(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("minAge", 18),
            ("maxAge", 150)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a valid adult age (18 or older).",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is NOT a valid age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT a valid age.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidAge(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidAge(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("minAge", 0),
            ("maxAge", 150)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a valid age (0-150).",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is NOT a valid age within the specified range.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="minAge">The minimum valid age (inclusive).</param>
    /// <param name="maxAge">The maximum valid age (inclusive).</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT a valid age within the range.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidAge(int value, int minAge, int maxAge, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidAge(value, minAge, maxAge))
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
            $"The value '{value}' is a valid age (between {minAge} and {maxAge}).",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is NOT a valid child age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT a valid child age.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidChildAge(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidChildAge(value))
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
            $"The value '{value}' is a valid child age (between 0 and 17).",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is NOT a valid senior age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT a valid senior age.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidSeniorAge(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidSeniorAge(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("minAge", 65),
            ("maxAge", 150)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a valid senior age (65 or older).",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified value is NOT a valid working age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT a valid working age.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidWorkingAge(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidWorkingAge(value))
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
            $"The value '{value}' is a valid working age (between 18 and 65).",
            parameterName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that the specified value is NOT a valid adult age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is a valid adult age.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsNotValidAdultAge(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidAdultAge(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the specified value is NOT a valid age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is a valid age.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsNotValidAge(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidAge(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the specified value is NOT a valid age within the specified range.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="minAge">The minimum valid age (inclusive).</param>
    /// <param name="maxAge">The maximum valid age (inclusive).</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is a valid age within the range.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsNotValidAge(int value, int minAge, int maxAge, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidAge(value, minAge, maxAge, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the specified value is NOT a valid child age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is a valid child age.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsNotValidChildAge(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidChildAge(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the specified value is NOT a valid senior age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is a valid senior age.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsNotValidSeniorAge(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidSeniorAge(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the specified value is NOT a valid working age.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is a valid working age.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsNotValidWorkingAge(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidWorkingAge(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    #endregion
}
