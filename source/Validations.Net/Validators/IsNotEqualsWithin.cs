using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides extension methods for validating that numeric values are not approximately equal to a specified value within a tolerance.
/// </summary>
public static class IsNotEqualsWithin
{
    /// <summary>
    /// Checks if a numeric value is not approximately equal to another value within a specified tolerance.
    /// </summary>
    /// <typeparam name="T">The type of the numeric values to compare. Must implement <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="compareTo">The value to compare against.</param>
    /// <param name="tolerance">The maximum allowed difference between the values.</param>
    /// <returns>True if the absolute difference between the values is greater than the tolerance; otherwise, false. Two null values are considered equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEqualsWithin<T>(this T? value, T? compareTo, T tolerance) where T : INumber<T>
    {
        if (value == null && compareTo == null)
        {
            return false; // Both are null, considered not equal
        }

        if (value == null || compareTo == null)
        {
            return true; // One is null, the other is not, considered equal
        }

        return T.Abs(value - compareTo) > tolerance;
    }
    
    /// <summary>
    /// Validates that a numeric value is not approximately equal to another value within a specified tolerance, throwing a <see cref="ValidationException"/> if it is.
    /// </summary>
    /// <typeparam name="T">The type of the numeric values to compare. Must implement <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="compareTo">The value to compare against.</param>
    /// <param name="tolerance">The maximum allowed difference between the values.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the absolute difference between the values is less than or equal to the tolerance.</exception>
    public static T ValidateIsNotEqualsWithin<T>(this T? value, T? compareTo, T tolerance, string propertyName, Blackboard? blackboard = null) where T : INumber<T>
    {
        if (!value.CheckIsNotEqualsWithin(compareTo, tolerance))
        {
            throw new ValidationException("IsNotEqualsWithin", propertyName, $"{propertyName} must not be approximately equal to {compareTo} within a tolerance of {tolerance}.", blackboard, new Dictionary<string, object?> { { "value", value }, { "compareTo", compareTo }, { "tolerance", tolerance } });
        }

        return value!;
    }
}
