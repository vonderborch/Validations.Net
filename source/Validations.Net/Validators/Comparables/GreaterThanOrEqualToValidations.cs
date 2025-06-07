using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators.Comparables;

/// <summary>
/// Provides extension methods for validating that values are greater than or equal to specified values.
/// </summary>
public static class GreaterThanOrEqualToValidations
{
    /// <summary>
    /// Checks if a value is greater than or equal to another value.
    /// </summary>
    /// <typeparam name="T">The type that implements <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="other">The value to compare against.</param>
    /// <returns><c>true</c> if the value is greater than or equal to the other value; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsGreaterThanOrEqualTo<T>(this T value, T other) where T : IComparable<T>
    {
        return value.CompareTo(other) >= 0;
    }
    
    /// <summary>
    /// Validates that a value is greater than or equal to another value, throwing an exception if the validation fails.
    /// </summary>
    /// <typeparam name="T">The type that implements <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="other">The value to compare against.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Optional context object that will be included in the exception if validation fails.</param>
    /// <returns>The original value if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the value is less than the other value.</exception>
    [DebuggerStepThrough]
    public static T ValidateIsGreaterThanOrEqualTo<T>(this T value, T other, string variableName, object? blackboard = null) where T : INumber<T>
    {
        if (!value.CheckIsGreaterThanOrEqualTo(other))
        {
            throw new ValidationException(variableName, $"{variableName} must be greater than or equal to {other}.", "IsGreaterThanOrEqualTo", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "other", other }
            });
        }

        return value;
    }
}
