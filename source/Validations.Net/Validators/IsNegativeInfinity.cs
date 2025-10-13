using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods for checking if a number is negative infinity.
/// </summary>
public static class IsNegativeInfinity
{
    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNegativeInfinity";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must be negative infinity";

    /// <summary>
    /// Checks if a floating-point value is negative infinity.
    /// </summary>
    /// <typeparam name="T">The floating-point type that implements IFloatingPoint.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is negative infinity; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNegativeInfinity<T>(this T value) where T : IFloatingPoint<T>
    {
        return T.IsNegativeInfinity(value);
    }

    /// <summary>
    /// Ensures that a floating-point value is negative infinity.
    /// </summary>
    /// <typeparam name="T">The floating-point type that implements IFloatingPoint.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is negative infinity.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not negative infinity.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureIsNegativeInfinity<T>(this T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IFloatingPoint<T>
    {
        var validationResult = value.ValidateIsNegativeInfinity(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }

    /// <summary>
    /// Validates whether a floating-point value is negative infinity.
    /// </summary>
    /// <typeparam name="T">The floating-point type that implements IFloatingPoint.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the value is negative infinity.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNegativeInfinity<T>(this T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IFloatingPoint<T>
    {
        if (!value.CheckIsNegativeInfinity())
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}
