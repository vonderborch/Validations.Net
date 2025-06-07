using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators.Numbers;

/// <summary>
/// Provides validation methods to check if a floating-point value is equal to or not equal to another value within a
/// specified tolerance.
/// </summary>
public static class EqualToValidations
{
    /// <summary>
    /// Checks if a floating-point value is equal to another value within a specified tolerance.
    /// </summary>
    /// <typeparam name="T">The type that implements <see cref="IFloatingPoint{TSelf}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="other">The value to compare against.</param>
    /// <param name="tolerance">The acceptable difference between the values.</param>
    /// <returns><c>true</c> if the absolute difference between values is less than or equal to the tolerance; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEqualTo<T>(this T value, T other, T tolerance) where T : IFloatingPoint<T>
    {
        return T.Abs(value - other) <= tolerance;
    }
    
    /// <summary>
    /// Checks if a floating-point value is not equal to another value within a specified tolerance.
    /// </summary>
    /// <typeparam name="T">The type that implements <see cref="IFloatingPoint{T}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="other">The value to compare against.</param>
    /// <param name="tolerance">The acceptable difference between the values.</param>
    /// <returns><c>true</c> if the absolute difference between values is greater than the tolerance; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEqualTo<T>(this T value, T other, T tolerance) where T : IFloatingPoint<T>
    {
        return T.Abs(value - other) > tolerance;
    }
    
    /// <summary>
    /// Validates that a floating-point value is equal to another value within a specified tolerance, throwing an exception if the validation fails.
    /// </summary>
    /// <typeparam name="T">The type that implements <see cref="IFloatingPoint{T}"/>.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="other">The value to compare against.</param>
    /// <param name="tolerance">The acceptable difference between the values.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Optional context object that will be included in the exception if validation fails.</param>
    /// <returns>The original value if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the absolute difference between values is greater than the tolerance.</exception>
    [DebuggerStepThrough]
    public static T ValidateIsEqualTo<T>(this T value, T other, T tolerance, string variableName, object? blackboard = null) where T : IFloatingPoint<T>
    {
        if (!value.CheckIsEqualTo(other, tolerance))
        {
            throw new ValidationException(variableName, $"{variableName} must be equal to {other} within the tolerance of {tolerance}.", "IsEqualTo", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "other", other },
                { "tolerance", tolerance }
            });
        }

        return value;
    }
    
    /// <summary>
    /// Validates that a floating-point value is not equal to another value within a specified tolerance, throwing an exception if the validation fails.
    /// </summary>
    /// <typeparam name="T">The type that implements <see cref="IFloatingPoint{T}"/>.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="other">The value to compare against.</param>
    /// <param name="tolerance">The acceptable difference between the values.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Optional context object that will be included in the exception if validation fails.</param>
    /// <returns>The original value if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the absolute difference between values is less than or equal to the tolerance.</exception>
    [DebuggerStepThrough]
    public static T ValidateIsNotEqualTo<T>(this T value, T other, T tolerance, string variableName, object? blackboard = null) where T : IFloatingPoint<T>
    {
        if (!value.CheckIsNotEqualTo(other, tolerance))
        {
            throw new ValidationException(variableName, $"{variableName} must not be equal to {other} within the tolerance of {tolerance}.", "IsNotEqualTo", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "other", other },
                { "tolerance", tolerance }
            });
        }

        return value;
    }
}
