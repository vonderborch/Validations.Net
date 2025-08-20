using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides methods for validating if a value equals another value.
/// </summary>
public static class IsEquals
{
    /// <summary>
    /// The name of the validator.
    /// </summary>
    public const string ValidatorName = "IsEquals";

    /// <summary>
    /// The validation failure message.
    /// </summary>
    public const string ValidationFailureMessage = "Parameter must equal the expected value";

    /// <summary>
    /// Checks if the value equals the expected value.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="expected">The expected value.</param>
    /// <returns>True if the value equals the expected value, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEquals<T>(this T? value, T? expected) where T : IEquatable<T>
    {
        if (value is null && expected is null) return true;
        if (value is null || expected is null) return false;
        return value.Equals(expected);
    }

    /// <summary>
    /// Checks if the value equals the expected value within the specified tolerance.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="expected">The expected value.</param>
    /// <param name="tolerance">The tolerance for the comparison.</param>
    /// <returns>True if the value equals the expected value within tolerance, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEquals<T>(this T value, T expected, T tolerance) where T : INumber<T>
    {
        var difference = T.Abs(value - expected);
        return difference <= tolerance;
    }

    /// <summary>
    /// Checks if the value equals the expected value using object.Equals.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="expected">The expected value.</param>
    /// <returns>True if the value equals the expected value, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEquals(this object? value, object? expected)
    {
        return Equals(value, expected);
    }

    /// <summary>
    /// Ensures that the value equals the expected value.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="expected">The expected value.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it equals the expected value, otherwise throws a <see cref="ValidationException"/>.</returns>
    /// <exception cref="ValidationException">Thrown when the value does not equal the expected value.</exception>
    public static T? EnsureIsEquals<T>(this T? value, T? expected, string? parameterName = null, IBlackboard? blackboard = null) where T : IEquatable<T>
    {
        ValidationResult result = value.ValidateIsEquals(expected, parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures that the value equals the expected value within the specified tolerance.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="expected">The expected value.</param>
    /// <param name="tolerance">The tolerance for the comparison.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it equals the expected value within tolerance, otherwise throws a <see cref="ValidationException"/>.</returns>
    /// <exception cref="ValidationException">Thrown when the value does not equal the expected value within tolerance.</exception>
    public static T EnsureIsEquals<T>(this T value, T expected, T tolerance, string? parameterName = null, IBlackboard? blackboard = null) where T : INumber<T>
    {
        ValidationResult result = value.ValidateIsEquals(expected, tolerance, parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures that the value equals the expected value using object.Equals.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="expected">The expected value.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it equals the expected value, otherwise throws a <see cref="ValidationException"/>.</returns>
    /// <exception cref="ValidationException">Thrown when the value does not equal the expected value.</exception>
    public static object? EnsureIsEquals(this object? value, object? expected, string? parameterName = null, IBlackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateIsEquals(expected, parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates if the value equals the expected value.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="expected">The expected value.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsEquals<T>(this T? value, T? expected, string? parameterName = null, IBlackboard? blackboard = null) where T : IEquatable<T>
    {
        if (!value.CheckIsEquals(expected))
        {
            ValidationResult result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage, parameterName, blackboard, [("value", value), ("expected", expected)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates if the value equals the expected value within the specified tolerance.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="expected">The expected value.</param>
    /// <param name="tolerance">The tolerance for the comparison.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsEquals<T>(this T value, T expected, T tolerance, string? parameterName = null, IBlackboard? blackboard = null) where T : INumber<T>
    {
        if (!value.CheckIsEquals(expected, tolerance))
        {
            ValidationResult result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage, parameterName, blackboard, [("value", value), ("expected", expected), ("tolerance", tolerance)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates if the value equals the expected value using object.Equals.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="expected">The expected value.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsEquals(this object? value, object? expected, string? parameterName = null, IBlackboard? blackboard = null)
    {
        if (!value.CheckIsEquals(expected))
        {
            ValidationResult result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage, parameterName, blackboard, [("value", value), ("expected", expected)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}
