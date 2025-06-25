using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
///     Provides extension methods for validating that values are not within a specified range.
/// </summary>
public static class IsNotInRange
{
    /// <summary>
    ///     Checks if a value is not within a specified range.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="min">The minimum value of the range.</param>
    /// <param name="max">The maximum value of the range.</param>
    /// <param name="minIsInclusive">True if the minimum of the range is inclusive, False if it is exclusive.</param>
    /// <param name="maxIsInclusive">True if the maximum of the range is inclusive, False if it is exclusive.</param>
    /// <returns>True if the value is not within the range; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotInRange<T>(this T value, T min, T max, bool minIsInclusive = true, bool maxIsInclusive = false) where T : INumber<T>
    {
        T actualMin = minIsInclusive ? min : min + T.One;
        T actualMax = maxIsInclusive ? max : max - T.One;
        return value < actualMin || value > actualMax;
    }

    /// <summary>
    ///     Ensures that a value is not within a specified range, throwing a <see cref="ValidationException" /> if it is.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="min">The minimum value of the range.</param>
    /// <param name="max">The maximum value of the range.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="minIsInclusive">True if the minimum of the range is inclusive, False if it is exclusive.</param>
    /// <param name="maxIsInclusive">True if the maximum of the range is inclusive, False if it is exclusive.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the value is within the specified range.</exception>
    public static T EnsureIsNotInRange<T>(this T value, T min, T max, string propertyName, bool minIsInclusive = true, bool maxIsInclusive = false,
        IBlackboard? blackboard = null) where T : INumber<T>
    {
        ValidationResult result =
            value.ValidateIsNotInRange(min, max, propertyName, minIsInclusive, maxIsInclusive, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Validates that a value is not within a specified range.
    ///     If the value is within the range, returns a <see cref="ValidationResult" /> containing validation failure details.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="min">The minimum value of the range.</param>
    /// <param name="max">The maximum value of the range.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="minIsInclusive">True if the minimum of the range is inclusive, False if it is exclusive.</param>
    /// <param name="maxIsInclusive">True if the maximum of the range is inclusive, False if it is exclusive.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsNotInRange<T>(this T value, T min, T max, string variableName, bool minIsInclusive = true, bool maxIsInclusive = false,
        IBlackboard? blackboard = null) where T : INumber<T>
    {
        if (!value.CheckIsNotInRange(min, max, minIsInclusive, maxIsInclusive))
        {
            ValidationResult result = new(
                new ValidationException("IsNotInRange", variableName,
                    $"{variableName} must not be between {min} and {max}.",
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
}
