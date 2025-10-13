using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a numeric value is divisible by another value.
/// </summary>
public static class IsDivisibleBy
{
    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsDivisibleBy";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must be divisible by the specified value";

    /// <summary>
    /// Checks if the specified numeric value is divisible by the divisor.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <returns>True if the value is divisible by the divisor; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsDivisibleBy<T>(this T value, T divisor) 
        where T : INumber<T>, IModulusOperators<T, T, T>
    {
        return divisor != T.Zero && value % divisor == T.Zero;
    }

    /// <summary>
    /// Checks if the specified nullable numeric value is divisible by the divisor.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The nullable value to check.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <returns>True if the value is divisible by the divisor; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsDivisibleBy<T>(this T? value, T divisor) 
        where T : struct, INumber<T>, IModulusOperators<T, T, T>
    {
        return value.HasValue && CheckIsDivisibleBy(value.Value, divisor);
    }

    /// <summary>
    /// Ensures that the specified numeric value is divisible by the divisor, throwing a ValidationException if it is not.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not divisible by the divisor.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void EnsureIsDivisibleBy<T>(this T value, T divisor, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) 
        where T : INumber<T>, IModulusOperators<T, T, T>
    {
        var result = value.ValidateIsDivisibleBy(divisor, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }
    }

    /// <summary>
    /// Ensures that the specified nullable numeric value is divisible by the divisor, throwing a ValidationException if it is not.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The nullable value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is null or not divisible by the divisor.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void EnsureIsDivisibleBy<T>(this T? value, T divisor, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) 
        where T : struct, INumber<T>, IModulusOperators<T, T, T>
    {
        var result = value.ValidateIsDivisibleBy(divisor, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }
    }

    /// <summary>
    /// Validates if the specified numeric value is divisible by the divisor.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is divisible by the divisor.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsDivisibleBy<T>(this T value, T divisor, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) 
        where T : INumber<T>, IModulusOperators<T, T, T>
    {
        var isValid = CheckIsDivisibleBy(value, divisor);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("divisor", divisor)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage,
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified nullable numeric value is divisible by the divisor.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The nullable value to validate.</param>
    /// <param name="divisor">The divisor to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is divisible by the divisor.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsDivisibleBy<T>(this T? value, T divisor, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) 
        where T : struct, INumber<T>, IModulusOperators<T, T, T>
    {
        var isValid = CheckIsDivisibleBy(value, divisor);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("divisor", divisor)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage,
            parameterName, blackboard, contextList);
    }
}
