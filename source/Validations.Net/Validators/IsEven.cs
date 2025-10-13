using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a numeric value is even.
/// </summary>
public static class IsEven
{
    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsEven";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must be even";

    #region Check Methods

    /// <summary>
    ///     Checks if an integer is even.
    /// </summary>
    /// <param name="value">The integer to check.</param>
    /// <returns>True if the integer is even; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEven(this int value)
    {
        return value % 2 == 0;
    }

    /// <summary>
    ///     Checks if a nullable integer is even.
    /// </summary>
    /// <param name="value">The integer to check.</param>
    /// <returns>True if the integer is even; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEven(this int? value)
    {
        return value.HasValue && value.Value % 2 == 0;
    }

    /// <summary>
    ///     Checks if a long is even.
    /// </summary>
    /// <param name="value">The long to check.</param>
    /// <returns>True if the long is even; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEven(this long value)
    {
        return value % 2 == 0;
    }

    /// <summary>
    ///     Checks if a nullable long is even.
    /// </summary>
    /// <param name="value">The long to check.</param>
    /// <returns>True if the long is even; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEven(this long? value)
    {
        return value.HasValue && value.Value % 2 == 0;
    }

    /// <summary>
    ///     Checks if a short is even.
    /// </summary>
    /// <param name="value">The short to check.</param>
    /// <returns>True if the short is even; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEven(this short value)
    {
        return value % 2 == 0;
    }

    /// <summary>
    ///     Checks if a nullable short is even.
    /// </summary>
    /// <param name="value">The short to check.</param>
    /// <returns>True if the short is even; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEven(this short? value)
    {
        return value.HasValue && value.Value % 2 == 0;
    }

    /// <summary>
    ///     Checks if a byte is even.
    /// </summary>
    /// <param name="value">The byte to check.</param>
    /// <returns>True if the byte is even; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEven(this byte value)
    {
        return value % 2 == 0;
    }

    /// <summary>
    ///     Checks if a nullable byte is even.
    /// </summary>
    /// <param name="value">The byte to check.</param>
    /// <returns>True if the byte is even; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEven(this byte? value)
    {
        return value.HasValue && value.Value % 2 == 0;
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates if an integer is even.
    /// </summary>
    /// <param name="value">The integer to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the integer is even.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsEven(this int value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsEven(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            validationFailureMessage,
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a nullable integer is even.
    /// </summary>
    /// <param name="value">The integer to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the integer is even.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsEven(this int? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsEven(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            validationFailureMessage,
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a long is even.
    /// </summary>
    /// <param name="value">The long to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the long is even.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsEven(this long value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsEven(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            validationFailureMessage,
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a nullable long is even.
    /// </summary>
    /// <param name="value">The long to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the long is even.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsEven(this long? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsEven(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            validationFailureMessage,
            parameterName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that an integer is even.
    /// </summary>
    /// <param name="value">The integer to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the field being validated.</param>
    /// <returns>The original integer if it is even.</returns>
    /// <exception cref="ValidationException">Thrown when the integer is not even.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsEven(this int value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateIsEven(blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a nullable integer is even.
    /// </summary>
    /// <param name="value">The integer to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the field being validated.</param>
    /// <returns>The original integer if it is even.</returns>
    /// <exception cref="ValidationException">Thrown when the integer is not even.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int? EnsureIsEven(this int? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateIsEven(blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a long is even.
    /// </summary>
    /// <param name="value">The long to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the field being validated.</param>
    /// <returns>The original long if it is even.</returns>
    /// <exception cref="ValidationException">Thrown when the long is not even.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long EnsureIsEven(this long value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateIsEven(blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a nullable long is even.
    /// </summary>
    /// <param name="value">The long to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the field being validated.</param>
    /// <returns>The original long if it is even.</returns>
    /// <exception cref="ValidationException">Thrown when the long is not even.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long? EnsureIsEven(this long? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateIsEven(blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    #endregion
}
