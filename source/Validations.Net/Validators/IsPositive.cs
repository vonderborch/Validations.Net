using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a numeric value is positive.
/// </summary>
public static class IsPositive
{
    private const string ValidatorName = "IsPositive";

    #region Check Methods

    /// <summary>
    ///     Checks if a value is positive.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is positive; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsPositive<T>(this T value) where T : IComparable<T>
    {
        return value.CompareTo(default!) > 0;
    }

    /// <summary>
    ///     Checks if a nullable value is positive.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is positive; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsPositive<T>(this T? value) where T : struct, IComparable<T>
    {
        return value.HasValue && value.Value.CompareTo(default) > 0;
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates if a value is positive.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the value is positive.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsPositive<T>(this T value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? fieldName = null) where T : IComparable<T>
    {
        if (CheckIsPositive(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must be positive. Actual value: {value}",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a nullable value is positive.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the value is positive.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsPositive<T>(this T? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? fieldName = null) where T : struct, IComparable<T>
    {
        if (CheckIsPositive(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must be positive. Actual value: {value}",
            fieldName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that a value is positive.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>The original value if it is positive.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not positive.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureIsPositive<T>(this T value) where T : IComparable<T>
    {
        if (!CheckIsPositive(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value)
            };
            throw ValidationException.Create(ValidatorName, $"The value must be positive. Actual value: {value}", null,
                null, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a nullable value is positive.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>The original value if it is positive.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not positive.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureIsPositive<T>(this T? value) where T : struct, IComparable<T>
    {
        if (!CheckIsPositive(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value)
            };
            throw ValidationException.Create(ValidatorName, $"The value must be positive. Actual value: {value}", null,
                null, contextList);
        }

        return value;
    }

    #endregion
}
