using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for validating that values are equal to a specified value.
/// </summary>
public static class IsEquals
{
    /// <summary>
    ///     Checks if a value is equal to another value.
    /// </summary>
    /// <typeparam name="T">The type of the values to compare.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="compareTo">The value to compare against.</param>
    /// <returns>True if the values are equal; otherwise, false. Two null values are considered equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEquals<T>(this T? value, T? compareTo)
    {
        if (value == null && compareTo == null)
        {
            return true; // Both are null, considered equal
        }

        if (value == null || compareTo == null)
        {
            return false; // One is null, the other is not, considered not equal
        }

        return value.Equals(compareTo); // Use Equals method for comparison
    }

    /// <summary>
    ///     Checks if a value is equal to another value using the default equality comparer.
    /// </summary>
    /// <typeparam name="T">The type of the values to compare.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="compareTo">The value to compare against.</param>
    /// <param name="comparer">The equality comparer to use for comparison.</param>
    /// <returns>
    ///     True if the values are equal; otherwise, false.
    ///     Returns true if both values are null, and false if only one is null.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEquals<T>(this T? value, T? compareTo, IEqualityComparer<T> comparer)
    {
        if (value == null && compareTo == null)
        {
            return true; // Both are null, considered equal
        }

        if (value == null || compareTo == null)
        {
            return false; // One is null, the other is not, considered not equal
        }

        return comparer.Equals(value, compareTo); // Use the provided comparer
    }

    /// <summary>
    ///     Validates that a value is equal to another value, throwing a <see cref="ValidationException" /> if it isn't.
    /// </summary>
    /// <typeparam name="T">The type of the values to compare.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="compareTo">The value to compare against.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the values are not equal.</exception>
    public static T? ValidateIsEquals<T>(this T? value, T? compareTo, string propertyName,
        Blackboard? blackboard = null)
    {
        if (!value.CheckIsEquals(compareTo))
        {
            throw new ValidationException("IsEquals", propertyName, $"{propertyName} must be equal to {compareTo}.",
                blackboard, new Dictionary<string, object?> { { "value", value }, { "compareTo", compareTo } });
        }

        return value;
    }

    /// <summary>
    ///     Validates that a value is equal to another value using the provided equality comparer, throwing a
    ///     <see cref="ValidationException" /> if it isn't.
    /// </summary>
    /// <typeparam name="T">The type of the values to compare.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="compareTo">The value to compare against.</param>
    /// <param name="comparer">The equality comparer to use for comparison.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the values are not equal.</exception>
    public static T? ValidateIsEquals<T>(this T? value, T? compareTo, IEqualityComparer<T> comparer,
        string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsEquals(compareTo, comparer))
        {
            throw new ValidationException("IsEquals", propertyName, $"{propertyName} must be equal to {compareTo}.",
                blackboard, new Dictionary<string, object?> { { "value", value }, { "compareTo", compareTo } });
        }

        return value;
    }
}
