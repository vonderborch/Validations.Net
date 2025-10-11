using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
///     Provides extension methods for validating that values are equal to a specified value within a tolerance.
/// </summary>
public static class IsEqualsWithTolerance
{
    /// <summary>
    ///     Checks if a value is equal to another value within a specified tolerance.
    /// </summary>
    /// <typeparam name="T">The type of the values to compare.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="compareTo">The value to compare against.</param>
    /// <param name="tolerance">The tolerance for the comparison.</param>
    /// <returns>True if the values are equal within the tolerance; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEqualsWithTolerance<T>(this T value, T compareTo, T tolerance) where T : INumber<T>
    {
        T difference = T.Abs(value - compareTo);
        return difference <= tolerance;
    }

    /// <summary>
    ///     Ensures that a value is equal to another value within a specified tolerance, throwing a
    ///     <see cref="ValidationException" /> if it isn't.
    /// </summary>
    /// <typeparam name="T">The type of the values to compare.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="compareTo">The value to compare against.</param>
    /// <param name="tolerance">The tolerance for the comparison.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the values are not equal within the tolerance.</exception>
    public static T EnsureIsEqualsWithTolerance<T>(this T value, T compareTo, T tolerance, string propertyName,
        IBlackboard? blackboard = null) where T : INumber<T>
    {
        ValidationResult result = value.ValidateIsEqualsWithTolerance(compareTo, tolerance, propertyName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Validates that a value is equal to another value within a specified tolerance.
    ///     If the values are not equal within the tolerance, returns a <see cref="ValidationResult" /> containing validation
    ///     failure details.
    /// </summary>
    /// <typeparam name="T">The type of the values to compare.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="compareTo">The value to compare against.</param>
    /// <param name="tolerance">The tolerance for the comparison.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsEqualsWithTolerance<T>(this T value, T compareTo, T tolerance,
        string variableName,
        IBlackboard? blackboard = null) where T : INumber<T>
    {
        if (!value.CheckIsEqualsWithTolerance(compareTo, tolerance))
        {
            ValidationResult result = new(
                new ValidationException("IsEqualsWithTolerance", variableName,
                    $"{variableName} must be equal to {compareTo} within tolerance {tolerance}.",
                    blackboard, new Dictionary<string, object?>
                    {
                        { "value", value },
                        { "compareTo", compareTo },
                        { "tolerance", tolerance }
                    }));
            return result;
        }

        return new ValidationResult();
    }
}
