using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a numeric value is zero.
/// </summary>
public static class IsZero
{
    private const string ValidatorName = "IsZero";

    #region Check Methods

    /// <summary>
    ///     Checks if a value is zero.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is zero; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsZero<T>(this T value) where T : IEquatable<T>
    {
        return value.Equals(default!);
    }

    /// <summary>
    ///     Checks if a nullable value is zero.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is zero; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsZero<T>(this T? value) where T : struct, IEquatable<T>
    {
        return value.HasValue && value.Value.Equals(default);
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates if a value is zero.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the value is zero.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsZero<T>(this T value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? fieldName = null) where T : IEquatable<T>
    {
        if (CheckIsZero(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must be zero. Actual value: {value}",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a nullable value is zero.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the value is zero.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsZero<T>(this T? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? fieldName = null) where T : struct, IEquatable<T>
    {
        if (CheckIsZero(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must be zero. Actual value: {value}",
            fieldName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that a value is zero.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>The original value if it is zero.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not zero.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureIsZero<T>(this T value) where T : IEquatable<T>
    {
        if (!CheckIsZero(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value)
            };
            throw ValidationException.Create(ValidatorName, $"The value must be zero. Actual value: {value}", null,
                null, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a nullable value is zero.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>The original value if it is zero.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not zero.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureIsZero<T>(this T? value) where T : struct, IEquatable<T>
    {
        if (!CheckIsZero(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value)
            };
            throw ValidationException.Create(ValidatorName, $"The value must be zero. Actual value: {value}", null,
                null, contextList);
        }

        return value;
    }

    #endregion
}
