using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides extension methods for validating that numeric values are within a specified range.
/// </summary>
public static class IsInRange
{
    /// <summary>
    /// Checks if a numeric value is within a specified range.
    /// </summary>
    /// <typeparam name="T">The type of the numeric value. Must implement <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="min">The minimum value of the range.</param>
    /// <param name="max">The maximum value of the range.</param>
    /// <param name="minIsInclusive">Whether the minimum value is included in the range.</param>
    /// <param name="maxIsInclusive">Whether the maximum value is included in the range.</param>
    /// <returns>True if the value is within the specified range; otherwise, false. Null values are not considered in range.</returns>
    /// <exception cref="ValidationException">Thrown when min is greater than max.</exception>
    public static bool CheckIsInRange<T>(this T? value, T min, T max, bool minIsInclusive = true, bool maxIsInclusive = false) where T : INumber<T>
    {
        if (value is null)
        {
            return false; // Null values are not in range
        }

        min.ValidateIsLessThanOrEquals(max, nameof(min));
        max.ValidateIsGreaterThanOrEquals(min, nameof(max));

        min = minIsInclusive ? min - T.One : min;
        max = maxIsInclusive ? max + T.One : max;

        return value > min && value < max;
    }
    
    /// <summary>
    /// Validates that a numeric value is within a specified range, throwing a <see cref="ValidationException"/> if it isn't.
    /// </summary>
    /// <typeparam name="T">The type of the numeric value. Must implement <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="min">The minimum value of the range.</param>
    /// <param name="max">The maximum value of the range.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="minIsInclusive">Whether the minimum value is included in the range.</param>
    /// <param name="maxIsInclusive">Whether the maximum value is included in the range.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not within the specified range or when min is greater than max.</exception>
    public static T ValidateIsInRange<T>(this T? value, T min, T max, string propertyName, bool minIsInclusive = true, bool maxIsInclusive = false, Blackboard? blackboard = null) where T : INumber<T>
    {
        if (!value.CheckIsInRange(min, max, minIsInclusive, maxIsInclusive))
        {
            throw new ValidationException("IsInRange", propertyName, $"{propertyName} must be in the range [{min}, {max}].", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "min", min },
                { "max", max }
            });
        }

        return value!;
    }
}
