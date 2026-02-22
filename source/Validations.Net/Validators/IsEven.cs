using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using System.Numerics;

namespace Validations.Net.Validators;

/// <summary>
/// The IsEven class provides methods for validation to ensure that
/// an integer value is even. Includes functionality to check, enforce,
/// and validate instances where an even value is required.
/// </summary>
public static class IsEven
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsEven";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must be even";

    /// <summary>
    /// Checks if the given value is even.
    /// </summary>
    /// <typeparam name="T">The type of the value being checked.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>
    /// True if the value is even; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEven<T>(this T value) where T : INumber<T>
    {
        return T.IsEvenInteger(value);
    }

    /// <summary>
    /// Validates whether the given value is even.
    /// </summary>
    /// <typeparam name="T">The type of the value being validated.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">An optional blackboard for additional validation context information.</param>
    /// <param name="validationFailureMessage">A custom message to use if validation fails. Defaults to the default failure message.</param>
    /// <param name="parameterName">The name of the parameter being validated, automatically captured by the compiler.</param>
    /// <returns>
    /// A <see cref="ValidationResult"/> indicating whether the validation was successful or failed.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsEven<T>(this T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : INumber<T>
    {
        if (!value.CheckIsEven())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", value)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is even, throwing an exception if validation fails.
    /// </summary>
    /// <typeparam name="T">The type of the value being checked.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">An optional blackboard providing additional context for the validation.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated, automatically captured by the compiler.</param>
    /// <returns>
    /// The original value if validation is successful.
    /// </returns>
    /// <exception cref="ValidationException">
    /// Thrown when the validation fails and the value is not even.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureIsEven<T>(this T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : INumber<T>
    {
        var validationResult = value.ValidateIsEven(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}
