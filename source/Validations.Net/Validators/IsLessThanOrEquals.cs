using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides extension methods for validating that values are less than or equal to a specified value.
/// </summary>
public static class IsLessThanOrEquals
{
    /// <summary>
    /// Checks if a value is less than or equal to another value.
    /// </summary>
    /// <typeparam name="T">The type of the values to compare. Must implement <see cref="IComparable{T}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="compareTo">The value to compare against.</param>
    /// <returns>True if the value is less than or equal to the comparison value; otherwise, false. Null values are considered less than any non-null value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsLessThanOrEquals<T>(this T? value, T? compareTo) where T : IComparable<T>
    {
        if (value == null || compareTo == null)
        {
            if (value == null)
            {
                return true; // null is considered less than any non-null value
            }
            return false;
        }
        
        return value.CompareTo(compareTo) <= 0;
    }
    
    /// <summary>
    /// Validates that a value is less than or equal to another value, throwing a <see cref="ValidationException"/> if it isn't.
    /// </summary>
    /// <typeparam name="T">The type of the values to compare. Must implement <see cref="IComparable{T}"/>.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="compareTo">The value to compare against.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not less than or equal to the comparison value.</exception>
    public static T ValidateIsLessThanOrEquals<T>(this T value, T compareTo, string propertyName, Blackboard? blackboard = null) where T : IComparable<T>
    {
        if (!value.CheckIsLessThanOrEquals(compareTo))
        {
            throw new ValidationException("ValidateIsLessThanOrEqualsAttribute", propertyName, $"{propertyName} must be less than or equal to {compareTo}.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "compareTo", compareTo }
            });
        }

        return value;
    }
}
