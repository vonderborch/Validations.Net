using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a numeric value is odd.
/// </summary>
public static class IsOdd
{
    private const string ValidatorName = "IsOdd";

    #region Check Methods

    /// <summary>
    ///     Checks if an integer is odd.
    /// </summary>
    /// <param name="value">The integer to check.</param>
    /// <returns>True if the integer is odd; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsOdd(this int value)
    {
        return value % 2 != 0;
    }

    /// <summary>
    ///     Checks if a nullable integer is odd.
    /// </summary>
    /// <param name="value">The integer to check.</param>
    /// <returns>True if the integer is odd; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsOdd(this int? value)
    {
        return value.HasValue && value.Value % 2 != 0;
    }

    /// <summary>
    ///     Checks if a long is odd.
    /// </summary>
    /// <param name="value">The long to check.</param>
    /// <returns>True if the long is odd; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsOdd(this long value)
    {
        return value % 2 != 0;
    }

    /// <summary>
    ///     Checks if a nullable long is odd.
    /// </summary>
    /// <param name="value">The long to check.</param>
    /// <returns>True if the long is odd; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsOdd(this long? value)
    {
        return value.HasValue && value.Value % 2 != 0;
    }

    /// <summary>
    ///     Checks if a short is odd.
    /// </summary>
    /// <param name="value">The short to check.</param>
    /// <returns>True if the short is odd; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsOdd(this short value)
    {
        return value % 2 != 0;
    }

    /// <summary>
    ///     Checks if a nullable short is odd.
    /// </summary>
    /// <param name="value">The short to check.</param>
    /// <returns>True if the short is odd; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsOdd(this short? value)
    {
        return value.HasValue && value.Value % 2 != 0;
    }

    /// <summary>
    ///     Checks if a byte is odd.
    /// </summary>
    /// <param name="value">The byte to check.</param>
    /// <returns>True if the byte is odd; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsOdd(this byte value)
    {
        return value % 2 != 0;
    }

    /// <summary>
    ///     Checks if a nullable byte is odd.
    /// </summary>
    /// <param name="value">The byte to check.</param>
    /// <returns>True if the byte is odd; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsOdd(this byte? value)
    {
        return value.HasValue && value.Value % 2 != 0;
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates if an integer is odd.
    /// </summary>
    /// <param name="value">The integer to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the integer is odd.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsOdd(this int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsOdd(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must be odd. Actual value: {value}",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a nullable integer is odd.
    /// </summary>
    /// <param name="value">The integer to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the integer is odd.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsOdd(this int? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsOdd(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must be odd. Actual value: {value}",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a long is odd.
    /// </summary>
    /// <param name="value">The long to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the long is odd.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsOdd(this long value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsOdd(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must be odd. Actual value: {value}",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a nullable long is odd.
    /// </summary>
    /// <param name="value">The long to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the long is odd.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsOdd(this long? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsOdd(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must be odd. Actual value: {value}",
            fieldName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that an integer is odd.
    /// </summary>
    /// <param name="value">The integer to validate.</param>
    /// <returns>The original integer if it is odd.</returns>
    /// <exception cref="ValidationException">Thrown when the integer is not odd.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsOdd(this int value)
    {
        if (!CheckIsOdd(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value)
            };
            throw ValidationException.Create(ValidatorName, $"The value must be odd. Actual value: {value}", null, null,
                contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a nullable integer is odd.
    /// </summary>
    /// <param name="value">The integer to validate.</param>
    /// <returns>The original integer if it is odd.</returns>
    /// <exception cref="ValidationException">Thrown when the integer is not odd.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int? EnsureIsOdd(this int? value)
    {
        if (!CheckIsOdd(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value)
            };
            throw ValidationException.Create(ValidatorName, $"The value must be odd. Actual value: {value}", null, null,
                contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a long is odd.
    /// </summary>
    /// <param name="value">The long to validate.</param>
    /// <returns>The original long if it is odd.</returns>
    /// <exception cref="ValidationException">Thrown when the long is not odd.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long EnsureIsOdd(this long value)
    {
        if (!CheckIsOdd(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value)
            };
            throw ValidationException.Create(ValidatorName, $"The value must be odd. Actual value: {value}", null, null,
                contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a nullable long is odd.
    /// </summary>
    /// <param name="value">The long to validate.</param>
    /// <returns>The original long if it is odd.</returns>
    /// <exception cref="ValidationException">Thrown when the long is not odd.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long? EnsureIsOdd(this long? value)
    {
        if (!CheckIsOdd(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value)
            };
            throw ValidationException.Create(ValidatorName, $"The value must be odd. Actual value: {value}", null, null,
                contextList);
        }

        return value;
    }

    #endregion
}
