using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators.Comparables;

/// <summary>
/// Provides extension methods for validating whether values are less than or equal to other values.
/// This class supports all types that implement the <see cref="INumber{T}"/> interface.
/// </summary>
public static class LessThanOrEqualToValidations
{
    /// <summary>
    /// Checks if a value is less than or equal to another value.
    /// </summary>
    /// <typeparam name="T">The type that implements <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="other">The value to compare against.</param>
    /// <returns><c>true</c> if <paramref name="value"/> is less than or equal to <paramref name="other"/>; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsLessThanOrEqualTo<T>(this T value, T other) where T : IComparable<T>
    {
        return value.CompareTo(other) <= 0;
    }
    
    /// <summary>
    /// Validates that a value is less than or equal to another value. If the validation fails, a <see cref="ValidationException"/> is thrown.
    /// </summary>
    /// <typeparam name="T">The type that implements <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="other">The value to compare against.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Optional. An object that provides additional context for the validation.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when <paramref name="value"/> is not less than or equal to <paramref name="other"/>.</exception>
    [DebuggerStepThrough]
    public static T ValidateIsLessThanOrEqualTo<T>(this T value, T other, string variableName, object? blackboard = null) where T : INumber<T>
    {
        if (!value.CheckIsLessThanOrEqualTo(other))
        {
            throw new ValidationException(variableName, $"{variableName} must be less than or equal to {other}.", "IsLessThanOrEqualTo", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "other", other }
            });
        }

        return value;
    }
}
