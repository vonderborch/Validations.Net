using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a numeric value is not zero.
/// </summary>
public static class IsNotZero
{
    private const string ValidatorName = "IsNotZero";

    #region Check Methods

    /// <summary>
    ///     Checks if a value is not zero.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is not zero; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotZero<T>(this T value) where T : IEquatable<T>
    {
        return !value.Equals(default!);
    }

    /// <summary>
    ///     Checks if a nullable value is not zero.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is not zero; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotZero<T>(this T? value) where T : struct, IEquatable<T>
    {
        return value.HasValue && !value.Value.Equals(default);
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates if a value is not zero.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the value is not zero.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotZero<T>(this T value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IEquatable<T>
    {
        if (CheckIsNotZero(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must not be zero. Actual value: {value}",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a nullable value is not zero.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the value is not zero.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotZero<T>(this T? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : struct, IEquatable<T>
    {
        if (CheckIsNotZero(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must not be zero. Actual value: {value}",
            parameterName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that a value is not zero.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>The original value if it is not zero.</returns>
    /// <exception cref="ValidationException">Thrown when the value is zero.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureIsNotZero<T>(this T value) where T : IEquatable<T>
    {
        if (!CheckIsNotZero(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value)
            };
            throw ValidationException.Create(ValidatorName, $"The value must not be zero. Actual value: {value}", null,
                null, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a nullable value is not zero.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <returns>The original value if it is not zero.</returns>
    /// <exception cref="ValidationException">Thrown when the value is zero.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureIsNotZero<T>(this T? value) where T : struct, IEquatable<T>
    {
        if (!CheckIsNotZero(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value)
            };
            throw ValidationException.Create(ValidatorName, $"The value must not be zero. Actual value: {value}", null,
                null, contextList);
        }

        return value;
    }

    #endregion
}
