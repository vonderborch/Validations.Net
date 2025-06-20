using System.Numerics;
using SimpleBlackboard.Net;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for validating that values are within a specified range.
/// </summary>
public static class IsInRange
{
    /// <summary>
    ///     Checks if a value is within a specified range.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="min">The minimum value of the range (inclusive).</param>
    /// <param name="max">The maximum value of the range (inclusive).</param>
    /// <returns>True if the value is within the range; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsInRange<T>(this T value, T min, T max) where T : IComparable<T>
    {
        return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
    }

    /// <summary>
    /// Validates that a value is within a specified range.
    /// If the value is not within the range, returns a <see cref="ValidationResult"/> containing validation failure details.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="min">The minimum value of the range (inclusive).</param>
    /// <param name="max">The maximum value of the range (inclusive).</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsInRange<T>(this T value, T min, T max, string variableName,
        Blackboard? blackboard = null) where T : IComparable<T>
    {
        if (!value.CheckIsInRange(min, max))
        {
            ValidationResult result = new(
                new ValidationException("IsInRange", variableName,
                    $"{variableName} must be between {min} and {max} (inclusive).",
                    blackboard, new Dictionary<string, object?>
                    {
                        { "value", value },
                        { "min", min },
                        { "max", max }
                    }));
            return result;
        }

        return new ValidationResult();
    }

    /// <summary>
    ///     Ensures that a value is within a specified range, throwing a <see cref="ValidationException" /> if it isn't.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="min">The minimum value of the range (inclusive).</param>
    /// <param name="max">The maximum value of the range (inclusive).</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not within the specified range.</exception>
    public static T EnsureIsInRange<T>(this T value, T min, T max, string propertyName,
        Blackboard? blackboard = null) where T : IComparable<T>
    {
        ValidationResult result = value.ValidateIsInRange(min, max, propertyName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }
}
