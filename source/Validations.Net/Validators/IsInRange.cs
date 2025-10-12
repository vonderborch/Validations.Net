using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides methods for validating if a value is within a specified range.
/// </summary>
public static class IsInRange
{
    /// <summary>
    ///     The name of the validator.
    /// </summary>
    public const string ValidatorName = "IsInRange";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must be within the specified range";

    /// <summary>
    ///     Checks if a value is within the specified range.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="minimum">The minimum value of the range.</param>
    /// <param name="maximum">The maximum value of the range.</param>
    /// <param name="minimumInclusive">Whether the minimum value is inclusive (default: true).</param>
    /// <param name="maximumInclusive">Whether the maximum value is inclusive (default: true).</param>
    /// <returns>True if the value is within the range; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsInRange<T>(this T value, T minimum, T maximum, bool minimumInclusive = true,
        bool maximumInclusive = true) where T : IComparable<T>
    {
        var minComparison = value.CompareTo(minimum);
        var maxComparison = value.CompareTo(maximum);

        var minValid = minimumInclusive ? minComparison >= 0 : minComparison > 0;
        var maxValid = maximumInclusive ? maxComparison <= 0 : maxComparison < 0;

        return minValid && maxValid;
    }

    /// <summary>
    ///     Ensures that a value is within the specified range.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="minimum">The minimum value of the range.</param>
    /// <param name="maximum">The maximum value of the range.</param>
    /// <param name="minimumInclusive">Whether the minimum value is inclusive (default: true).</param>
    /// <param name="maximumInclusive">Whether the maximum value is inclusive (default: true).</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not within the specified range.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void EnsureIsInRange<T>(this T value, T minimum, T maximum, bool minimumInclusive = true,
        bool maximumInclusive = true, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IComparable<T>
    {
        var validationResult =
            value.ValidateIsInRange(minimum, maximum, minimumInclusive, maximumInclusive, null, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }
    }

    /// <summary>
    ///     Validates if a value is within the specified range.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="minimum">The minimum value of the range.</param>
    /// <param name="maximum">The maximum value of the range.</param>
    /// <param name="minimumInclusive">Whether the minimum value is inclusive (default: true).</param>
    /// <param name="maximumInclusive">Whether the maximum value is inclusive (default: true).</param>
    /// <param name="blackboard">The blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the value is within the specified range.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsInRange<T>(this T value, T minimum, T maximum,
        bool minimumInclusive = true, bool maximumInclusive = true, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        if (CheckIsInRange(value, minimum, maximum, minimumInclusive, maximumInclusive))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var minOperator = minimumInclusive ? ">=" : ">";
        var maxOperator = maximumInclusive ? "<=" : "<";
        var message = $"Value must be {minOperator} {minimum} and {maxOperator} {maximum}. Actual value: {value}";
        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            validationFailureMessage,
            parameterName,
            blackboard,
            [
                ("value", value), ("minimum", minimum), ("maximum", maximum), ("minimumInclusive", minimumInclusive),
                ("maximumInclusive", maximumInclusive)
            ]
        );
    }
}
