using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a number is out of a specified range.
/// </summary>
public static class NumberIsOutOfRangeValidation
{
    /// <summary>
    /// Determines whether the specified value is out of the given range.
    /// </summary>
    /// <typeparam name="T">The type of the value to check, which must implement <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="min">The minimum value of the range.</param>
    /// <param name="max">The maximum value of the range.</param>
    /// <param name="minInclusive">Indicates whether the minimum value is inclusive.</param>
    /// <param name="maxInclusive">Indicates whether the maximum value is inclusive.</param>
    /// <returns><c>true</c> if the value is out of range; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOutOfRange<T>(T value, T min, T max, bool minInclusive = true, bool maxInclusive = true) where T : INumber<T>
    {
        var actualMin = minInclusive ? min : min + T.One;
        var actualMax = maxInclusive ? max : max - T.One;

        return !(value >= actualMin && value <= actualMax);
    }

    /// <summary>
    /// Extension method that checks if the specified value is out of the given range.
    /// </summary>
    /// <typeparam name="T">The type of the value to check, which must implement <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="min">The minimum value of the range.</param>
    /// <param name="max">The maximum value of the range.</param>
    /// <param name="minInclusive">Indicates whether the minimum value is inclusive.</param>
    /// <param name="maxInclusive">Indicates whether the maximum value is inclusive.</param>
    /// <returns><c>true</c> if the value is out of range; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsOutOfRange<T>(this T value, T min, T max, bool minInclusive = true, bool maxInclusive = true) where T : INumber<T>
    {
        return IsOutOfRange(value, min, max, minInclusive, maxInclusive);
    }

    /// <summary>
    /// Validates that the specified value is out of the given range. Throws a <see cref="ValidationException"/> if the value is within the range.
    /// </summary>
    /// <typeparam name="T">The type of the value to check, which must implement <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="min">The minimum value of the range.</param>
    /// <param name="max">The maximum value of the range.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <param name="minInclusive">Indicates whether the minimum value is inclusive.</param>
    /// <param name="maxInclusive">Indicates whether the maximum value is inclusive.</param>
    /// <returns>The original value if it is out of range.</returns>
    /// <exception cref="ValidationException">Thrown when the value is within the range.</exception>
    [DebuggerStepThrough]
    public static T IsOutOfRangeValidation<T>(T value, T min, T max, string variableName, object? blackboard = null, bool minInclusive = true, bool maxInclusive = true) where T : INumber<T>
    {
        if (!value.CheckIsOutOfRange(min, max, minInclusive, maxInclusive))
        {
            throw new ValidationException(variableName, $"{variableName} must be out of range {min} to {max}.", "IsOutOfRange", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "min", min },
                { "max", max },
                { "minInclusive", minInclusive },
                { "maxInclusive", maxInclusive }
            });
        }

        return value;
    }

    /// <summary>
    /// Extension method that validates the specified value is out of the given range. Throws a <see cref="ValidationException"/> if the value is within the range.
    /// </summary>
    /// <typeparam name="T">The type of the value to check, which must implement <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="min">The minimum value of the range.</param>
    /// <param name="max">The maximum value of the range.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <param name="minInclusive">Indicates whether the minimum value is inclusive.</param>
    /// <param name="maxInclusive">Indicates whether the maximum value is inclusive.</param>
    /// <returns>The original value if it is out of range.</returns>
    /// <exception cref="ValidationException">Thrown when the value is within the range.</exception>
    [DebuggerStepThrough]
    public static T ValidateIsOutOfRange<T>(this T value, T min, T max, string variableName, object? blackboard = null, bool minInclusive = true, bool maxInclusive = true) where T : INumber<T>
    {
        return IsOutOfRangeValidation(value, min, max, variableName, blackboard, minInclusive, maxInclusive);
    }
}
