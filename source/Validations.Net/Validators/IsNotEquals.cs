using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides methods for validating if a value does not equal another value.
/// </summary>
public static class IsNotEquals
{
    /// <summary>
    /// The name of the validator.
    /// </summary>
    public const string ValidatorName = "IsNotEquals";

    /// <summary>
    /// The validation failure message.
    /// </summary>
    public const string ValidationFailureMessage = "Parameter must not equal the specified value";

    /// <summary>
    /// Checks if the value does not equal the specified value.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="notExpected">The value that should not be equal.</param>
    /// <returns>True if the value does not equal the specified value, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEquals<T>(this T? value, T? notExpected) where T : IEquatable<T>
    {
        if (value is null && notExpected is null) return false;
        if (value is null || notExpected is null) return true;
        return !value.Equals(notExpected);
    }

    /// <summary>
    /// Checks if the value does not equal the specified value within the specified tolerance.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="notExpected">The value that should not be equal.</param>
    /// <param name="tolerance">The tolerance for the comparison.</param>
    /// <returns>True if the value does not equal the specified value within tolerance, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEquals<T>(this T value, T notExpected, T tolerance) where T : INumber<T>
    {
        var difference = T.Abs(value - notExpected);
        return difference > tolerance;
    }

    /// <summary>
    /// Checks if the value does not equal the specified value using object.Equals.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="notExpected">The value that should not be equal.</param>
    /// <returns>True if the value does not equal the specified value, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEquals(this object? value, object? notExpected)
    {
        return !Equals(value, notExpected);
    }

    /// <summary>
    /// Ensures that the value does not equal the specified value.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="notExpected">The value that should not be equal.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it does not equal the specified value, otherwise throws a <see cref="ValidationException"/>.</returns>
    /// <exception cref="ValidationException">Thrown when the value equals the specified value.</exception>
    public static T? EnsureIsNotEquals<T>(this T? value, T? notExpected, string? parameterName = null, IBlackboard? blackboard = null) where T : IEquatable<T>
    {
        ValidationResult result = value.ValidateIsNotEquals(notExpected, parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures that the value does not equal the specified value within the specified tolerance.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="notExpected">The value that should not be equal.</param>
    /// <param name="tolerance">The tolerance for the comparison.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it does not equal the specified value within tolerance, otherwise throws a <see cref="ValidationException"/>.</returns>
    /// <exception cref="ValidationException">Thrown when the value equals the specified value within tolerance.</exception>
    public static T EnsureIsNotEquals<T>(this T value, T notExpected, T tolerance, string? parameterName = null, IBlackboard? blackboard = null) where T : INumber<T>
    {
        ValidationResult result = value.ValidateIsNotEquals(notExpected, tolerance, parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Ensures that the value does not equal the specified value using object.Equals.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="notExpected">The value that should not be equal.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it does not equal the specified value, otherwise throws a <see cref="ValidationException"/>.</returns>
    /// <exception cref="ValidationException">Thrown when the value equals the specified value.</exception>
    public static object? EnsureIsNotEquals(this object? value, object? notExpected, string? parameterName = null, IBlackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateIsNotEquals(notExpected, parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates if the value does not equal the specified value.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="notExpected">The value that should not be equal.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNotEquals<T>(this T? value, T? notExpected, string? parameterName = null, IBlackboard? blackboard = null) where T : IEquatable<T>
    {
        if (!value.CheckIsNotEquals(notExpected))
        {
            ValidationResult result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage, parameterName, blackboard, [("value", value), ("notExpected", notExpected)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates if the value does not equal the specified value within the specified tolerance.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="notExpected">The value that should not be equal.</param>
    /// <param name="tolerance">The tolerance for the comparison.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNotEquals<T>(this T value, T notExpected, T tolerance, string? parameterName = null, IBlackboard? blackboard = null) where T : INumber<T>
    {
        if (!value.CheckIsNotEquals(notExpected, tolerance))
        {
            ValidationResult result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage, parameterName, blackboard, [("value", value), ("notExpected", notExpected), ("tolerance", tolerance)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates if the value does not equal the specified value using object.Equals.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="notExpected">The value that should not be equal.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNotEquals(this object? value, object? notExpected, string? parameterName = null, IBlackboard? blackboard = null)
    {
        if (!value.CheckIsNotEquals(notExpected))
        {
            ValidationResult result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage, parameterName, blackboard, [("value", value), ("notExpected", notExpected)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}
