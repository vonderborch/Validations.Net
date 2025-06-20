using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for validating that values are greater than a specified value.
/// </summary>
public static class IsGreaterThan
{
    /// <summary>
    ///     Checks if a value is greater than another value.
    /// </summary>
    /// <typeparam name="T">The type of the values to compare.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="compareTo">The value to compare against.</param>
    /// <returns>True if the value is greater than the compareTo value; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsGreaterThan<T>(this T value, T compareTo) where T : IComparable<T>
    {
        return value.CompareTo(compareTo) > 0;
    }

    /// <summary>
    ///     Ensures that a value is greater than another value, throwing a <see cref="ValidationException" /> if it isn't.
    /// </summary>
    /// <typeparam name="T">The type of the values to compare.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="compareTo">The value to compare against.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not greater than the compareTo value.</exception>
    public static T EnsureIsGreaterThan<T>(this T value, T compareTo, string propertyName,
        Blackboard? blackboard = null) where T : IComparable<T>
    {
        ValidationResult result = value.ValidateIsGreaterThan(compareTo, propertyName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Validates that a value is greater than another value.
    ///     If the value is not greater than the compareTo value, returns a <see cref="ValidationResult" /> containing
    ///     validation failure details.
    /// </summary>
    /// <typeparam name="T">The type of the values to compare.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="compareTo">The value to compare against.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsGreaterThan<T>(this T value, T compareTo, string variableName,
        Blackboard? blackboard = null) where T : IComparable<T>
    {
        if (!value.CheckIsGreaterThan(compareTo))
        {
            ValidationResult result = new(
                new ValidationException("IsGreaterThan", variableName,
                    $"{variableName} must be greater than {compareTo}.",
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
