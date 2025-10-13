using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods for checking if a number is not positive infinity.
/// </summary>
public static class IsNotPositiveInfinity
{
    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotPositiveInfinity";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not be positive infinity";

    /// <summary>
    /// Checks if a floating-point value is not positive infinity.
    /// </summary>
    /// <typeparam name="T">The floating-point type that implements IFloatingPoint.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is not positive infinity; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotPositiveInfinity<T>(this T value) where T : IFloatingPoint<T>
    {
        return !T.IsPositiveInfinity(value);
    }

    /// <summary>
    /// Ensures that a floating-point value is not positive infinity.
    /// </summary>
    /// <typeparam name="T">The floating-point type that implements IFloatingPoint.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not positive infinity.</returns>
    /// <exception cref="ValidationException">Thrown when the value is positive infinity.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureIsNotPositiveInfinity<T>(this T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IFloatingPoint<T>
    {
        var validationResult = value.ValidateIsNotPositiveInfinity(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates whether a floating-point value is not positive infinity.
    /// </summary>
    /// <typeparam name="T">The floating-point type that implements IFloatingPoint.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the value is not positive infinity.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotPositiveInfinity<T>(this T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IFloatingPoint<T>
    {
        if (!value.CheckIsNotPositiveInfinity())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}
