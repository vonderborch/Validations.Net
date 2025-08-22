using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides methods for validating if a value is not within a specified range.
/// </summary>
public static class IsNotInRange
{
    /// <summary>
    ///     The name of the validator.
    /// </summary>
    public const string ValidatorName = "IsNotInRange";

    /// <summary>
    ///     Checks if a value is not within the specified range.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="minimum">The minimum value of the range.</param>
    /// <param name="maximum">The maximum value of the range.</param>
    /// <param name="minimumInclusive">Whether the minimum value is inclusive (default: true).</param>
    /// <param name="maximumInclusive">Whether the maximum value is inclusive (default: true).</param>
    /// <returns>True if the value is not within the range; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotInRange<T>(this T value, T minimum, T maximum, bool minimumInclusive = true,
        bool maximumInclusive = true) where T : IComparable<T>
    {
        return !value.CheckIsInRange(minimum, maximum, minimumInclusive, maximumInclusive);
    }

    /// <summary>
    ///     Ensures that a value is not within the specified range.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="minimum">The minimum value of the range.</param>
    /// <param name="maximum">The maximum value of the range.</param>
    /// <param name="minimumInclusive">Whether the minimum value is inclusive (default: true).</param>
    /// <param name="maximumInclusive">Whether the maximum value is inclusive (default: true).</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is within the specified range.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void EnsureIsNotInRange<T>(this T value, T minimum, T maximum, bool minimumInclusive = true,
        bool maximumInclusive = true, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        where T : IComparable<T>
    {
        var validationResult =
            value.ValidateIsNotInRange(minimum, maximum, minimumInclusive, maximumInclusive, null, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }
    }

    /// <summary>
    ///     Validates if a value is not within the specified range.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="minimum">The minimum value of the range.</param>
    /// <param name="maximum">The maximum value of the range.</param>
    /// <param name="minimumInclusive">Whether the minimum value is inclusive (default: true).</param>
    /// <param name="maximumInclusive">Whether the maximum value is inclusive (default: true).</param>
    /// <param name="blackboard">The blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the value is not within the specified range.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotInRange<T>(this T value, T minimum, T maximum,
        bool minimumInclusive = true, bool maximumInclusive = true, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        if (CheckIsNotInRange(value, minimum, maximum, minimumInclusive, maximumInclusive))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var minOperator = minimumInclusive ? ">=" : ">";
        var maxOperator = maximumInclusive ? "<=" : "<";
        var message = $"Value must not be {minOperator} {minimum} and {maxOperator} {maximum}. Actual value: {value}";
        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            message,
            parameterName,
            blackboard,
            [
                ("value", value), ("minimum", minimum), ("maximum", maximum), ("minimumInclusive", minimumInclusive),
                ("maximumInclusive", maximumInclusive)
            ]
        );
    }
}
