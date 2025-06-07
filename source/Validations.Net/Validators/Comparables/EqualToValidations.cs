using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators.Comparables;

/// <summary>
/// Provides extension methods for validating equality conditions.
/// </summary>
public static class EqualToValidations
{
    /// <summary>
    /// Checks if a value is equal to another value.
    /// </summary>
    /// <typeparam name="T">The type that implements <see cref="IComparable{T}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="other">The value to compare against.</param>
    /// <returns><c>true</c> if the values are equal; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEqualTo<T>(this T value, T other) where T : IComparable<T>
    {
        return value.Equals(other);
    }
    
    /// <summary>
    /// Checks if a value is not equal to another value.
    /// </summary>
    /// <typeparam name="T">The type that implements <see cref="IComparable{T}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="other">The value to compare against.</param>
    /// <returns><c>true</c> if the values are not equal; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEqualTo<T>(this T value, T other) where T : IComparable<T>
    {
        return !value.Equals(other);
    }
    
    /// <summary>
    /// Validates that a value is equal to another value, throwing an exception if the validation fails.
    /// </summary>
    /// <typeparam name="T">The type that implements <see cref="IComparable{T}"/>.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="other">The value to compare against.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Optional context object that will be included in the exception if validation fails.</param>
    /// <returns>The original value if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not equal to the other value.</exception>
    [DebuggerStepThrough]
    public static T ValidateIsEqualTo<T>(this T value, T other, string variableName, object? blackboard = null) where T : IComparable<T>
    {
        if (!value.CheckIsEqualTo(other))
        {
            throw new ValidationException(variableName, $"{variableName} must be equal to {other}.", "IsEqualTo", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "other", other }
            });
        }

        return value;
    }
    
    /// <summary>
    /// Validates that a value is not equal to another value, throwing an exception if the validation fails.
    /// </summary>
    /// <typeparam name="T">The type that implements <see cref="IComparable{T}"/>.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="other">The value to compare against.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Optional context object that will be included in the exception if validation fails.</param>
    /// <returns>The original value if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the value is equal to the other value.</exception>
    [DebuggerStepThrough]
    public static T ValidateIsNotEqualTo<T>(this T value, T other, string variableName, object? blackboard = null) where T : IComparable<T>
    {
        if (!value.CheckIsNotEqualTo(other))
        {
            throw new ValidationException(variableName, $"{variableName} must not be equal to {other}.", "IsNotEqualTo", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "other", other }
            });
        }

        return value;
    }
}
