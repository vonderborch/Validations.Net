using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using System.Numerics;

namespace Validations.Net.Validators;

/// <summary>
/// The IsDivisibleBy class provides methods for validation to ensure that
/// an integer value is divisible by a given divisor. Includes functionality to check,
/// enforce, and validate instances where divisibility is required.
/// </summary>
public static class IsDivisibleBy
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsDivisibleBy";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Value must be divisible by the divisor";

    /// <summary>
    /// Checks if the given value is divisible by the specified divisor.
    /// </summary>
    /// <typeparam name="T">The type of the value being checked.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="divisor">The divisor to check divisibility against.</param>
    /// <returns>
    /// True if the value is divisible by the divisor (and divisor is not zero); otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsDivisibleBy<T>(this T value, T divisor) where T : INumber<T>
    {
        return divisor != T.Zero && value % divisor == T.Zero;
    }

    /// <summary>
    /// Validates whether the given value is divisible by the specified divisor.
    /// </summary>
    /// <typeparam name="T">The type of the value being validated.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="divisor">The divisor to check divisibility against.</param>
    /// <param name="blackboard">An optional blackboard for additional validation context information.</param>
    /// <param name="validationFailureMessage">A custom message to use if validation fails. Defaults to the default failure message.</param>
    /// <param name="parameterName">The name of the parameter being validated, automatically captured by the compiler.</param>
    /// <returns>
    /// A <see cref="ValidationResult"/> indicating whether the validation was successful or failed.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsDivisibleBy<T>(this T value, T divisor, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : INumber<T>
    {
        if (!value.CheckIsDivisibleBy(divisor))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", value), ("divisor", divisor)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given value is divisible by the specified divisor, throwing an exception if validation fails.
    /// </summary>
    /// <typeparam name="T">The type of the value being checked.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="divisor">The divisor to check divisibility against.</param>
    /// <param name="blackboard">An optional blackboard providing additional context for the validation.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated, automatically captured by the compiler.</param>
    /// <returns>
    /// The original value if validation is successful.
    /// </returns>
    /// <exception cref="ValidationException">
    /// Thrown when the validation fails and the value is not divisible by the divisor.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T EnsureIsDivisibleBy<T>(this T value, T divisor, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : INumber<T>
    {
        var validationResult = value.ValidateIsDivisibleBy(divisor, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return value;
    }
}
