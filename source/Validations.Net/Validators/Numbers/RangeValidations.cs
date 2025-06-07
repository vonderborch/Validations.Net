using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using Validations.Net.Validators.Comparables;

namespace Validations.Net.Validators.Numbers;

/// <summary>
/// Provides extension methods for validating whether numeric values are within or outside specific ranges.
/// This class supports all types that implement the <see cref="INumber{T}"/> interface.
/// </summary>
public static class RangeValidations
{
    /// <summary>
    /// Checks if a numeric value is within a specified range.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="min">The minimum value of the range.</param>
    /// <param name="max">The maximum value of the range.</param>
    /// <param name="minIsInclusive">Whether the minimum value is inclusive. Default is true.</param>
    /// <param name="maxIsInclusive">Whether the maximum value is inclusive. Default is false.</param>
    /// <returns><c>true</c> if the value is within the specified range; otherwise, <c>false</c>.</returns>
    /// <remarks>This method validates that min is less than or equal to max before performing the range check.</remarks>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsInRange<T>(this T value, T min, T max, bool minIsInclusive = true, bool maxIsInclusive = false) where T : INumber<T>
    {
        min.ValidateIsLessThanOrEqualTo(max, nameof(min));
        max.ValidateIsGreaterThanOrEqualTo(min, nameof(max));
        
        T actualMin = minIsInclusive ? min - T.One : min;
        T actualMax = maxIsInclusive ? max + T.One : max;
        
        return value > actualMin && value < actualMax;
    }
    
    /// <summary>
    /// Checks if a numeric value is outside a specified range.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="min">The minimum value of the range.</param>
    /// <param name="max">The maximum value of the range.</param>
    /// <param name="minIsInclusive">Whether the minimum value is inclusive. Default is true.</param>
    /// <param name="maxIsInclusive">Whether the maximum value is inclusive. Default is false.</param>
    /// <returns><c>true</c> if the value is outside the specified range; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotInRange<T>(this T value, T min, T max, bool minIsInclusive = true, bool maxIsInclusive = false) where T : INumber<T>
    {
        return !value.CheckIsInRange(min, max, minIsInclusive, maxIsInclusive);
    }
    
    /// <summary>
    /// Validates that a numeric value is within a specified range. If the validation fails, a <see cref="ValidationException"/> is thrown.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="min">The minimum value of the range.</param>
    /// <param name="max">The maximum value of the range.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="minIsInclusive">Whether the minimum value is inclusive. Default is true.</param>
    /// <param name="maxIsInclusive">Whether the maximum value is inclusive. Default is false.</param>
    /// <param name="blackboard">Optional. An object that provides additional context for the validation.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not within the specified range.</exception>
    [DebuggerStepThrough]
    public static T ValidateIsInRange<T>(this T value, T min, T max, string variableName, bool minIsInclusive = true, bool maxIsInclusive = false, object? blackboard = null) where T : INumber<T>
    {
        if (!value.CheckIsInRange(min, max, minIsInclusive, maxIsInclusive))
        {
            throw new ValidationException(variableName, $"{variableName} must be in the range [{min}, {max}].", "IsInRange", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "min", min },
                { "max", max },
                { "minIsInclusive", minIsInclusive },
                { "maxIsInclusive", maxIsInclusive }
            });
        }

        return value;
    }
    
    /// <summary>
    /// Validates that a numeric value is outside a specified range. If the validation fails, a <see cref="ValidationException"/> is thrown.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="min">The minimum value of the range.</param>
    /// <param name="max">The maximum value of the range.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="minIsInclusive">Whether the minimum value is inclusive. Default is true.</param>
    /// <param name="maxIsInclusive">Whether the maximum value is inclusive. Default is false.</param>
    /// <param name="blackboard">Optional. An object that provides additional context for the validation.</param>
    /// <returns>The original value if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the value is within the specified range.</exception>
    [DebuggerStepThrough]
    public static T ValidateIsNotInRange<T>(this T value, T min, T max, string variableName, bool minIsInclusive = true, bool maxIsInclusive = false, object? blackboard = null) where T : INumber<T>
    {
        if (!value.CheckIsNotInRange(min, max, minIsInclusive, maxIsInclusive))
        {
            throw new ValidationException(variableName, $"{variableName} must not be in the range [{min}, {max}].", "IsNotInRange", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "min", min },
                { "max", max },
                { "minIsInclusive", minIsInclusive },
                { "maxIsInclusive", maxIsInclusive }
            });
        }

        return value;
    }
}
