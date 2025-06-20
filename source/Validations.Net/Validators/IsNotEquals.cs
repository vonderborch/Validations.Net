using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for validating that values are not equal to a specified value.
/// </summary>
public static class IsNotEquals
{
    /// <summary>
    ///     Checks if a value is not equal to another value.
    /// </summary>
    /// <typeparam name="T">The type of the values to compare.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="compareTo">The value to compare against.</param>
    /// <returns>True if the values are not equal; otherwise, false. Two null values are considered equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEquals<T>(this T? value, T? compareTo)
    {
        if (value == null && compareTo == null)
        {
            return false; // Both are null, considered equal
        }

        if (value == null || compareTo == null)
        {
            return true; // One is null, the other is not, considered not equal
        }

        return !value.Equals(compareTo); // Use Equals method for comparison
    }

    /// <summary>
    ///     Checks if a value is not equal to another value using the default equality comparer.
    /// </summary>
    /// <typeparam name="T">The type of the values to compare.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="compareTo">The value to compare against.</param>
    /// <param name="comparer">The equality comparer to use for comparison.</param>
    /// <returns>
    ///     True if the values are not equal; otherwise, false.
    ///     Returns false if both values are null, and true if only one is null.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEquals<T>(this T? value, T? compareTo, IEqualityComparer<T> comparer)
    {
        if (value == null && compareTo == null)
        {
            return false; // Both are null, considered equal
        }

        if (value == null || compareTo == null)
        {
            return true; // One is null, the other is not, considered not equal
        }

        return !comparer.Equals(value, compareTo); // Use the provided comparer
    }

    /// <summary>
    ///     Ensures that a value is not equal to another value, throwing a <see cref="ValidationException" /> if it is.
    /// </summary>
    /// <typeparam name="T">The type of the values to compare.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="compareTo">The value to compare against.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the values are equal.</exception>
    public static T? EnsureIsNotEquals<T>(this T? value, T? compareTo, string propertyName,
        Blackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateIsNotEquals(compareTo, propertyName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a value is not equal to another value using the provided equality comparer, throwing a
    ///     <see cref="ValidationException" /> if it is.
    /// </summary>
    /// <typeparam name="T">The type of the values to compare.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="compareTo">The value to compare against.</param>
    /// <param name="comparer">The equality comparer to use for comparison.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the values are equal.</exception>
    public static T? EnsureIsNotEquals<T>(this T? value, T? compareTo, IEqualityComparer<T> comparer,
        string propertyName, Blackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateIsNotEquals(compareTo, comparer, propertyName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Validates that a value is not equal to another value.
    ///     If the values are equal, returns a <see cref="ValidationResult" /> containing validation failure details.
    /// </summary>
    /// <typeparam name="T">The type of the values to compare.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="compareTo">The value to compare against.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsNotEquals<T>(this T? value, T? compareTo, string variableName,
        Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotEquals(compareTo))
        {
            ValidationResult result = new(
                new ValidationException("IsNotEquals", variableName,
                    $"{variableName} must not be equal to {compareTo}.",
                    blackboard, new Dictionary<string, object?>
                    {
                        { "value", value },
                        { "compareTo", compareTo }
                    }));
            return result;
        }

        return new ValidationResult();
    }

    /// <summary>
    ///     Validates that a value is not equal to another value using the provided equality comparer.
    ///     If the values are equal, returns a <see cref="ValidationResult" /> containing validation failure details.
    /// </summary>
    /// <typeparam name="T">The type of the values to compare.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="compareTo">The value to compare against.</param>
    /// <param name="comparer">The equality comparer to use for comparison.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsNotEquals<T>(this T? value, T? compareTo, IEqualityComparer<T> comparer,
        string variableName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotEquals(compareTo, comparer))
        {
            ValidationResult result = new(
                new ValidationException("IsNotEquals", variableName,
                    $"{variableName} must not be equal to {compareTo}.",
                    blackboard, new Dictionary<string, object?>
                    {
                        { "value", value },
                        { "compareTo", compareTo }
                    }));
            return result;
        }

        return new ValidationResult();
    }
}
