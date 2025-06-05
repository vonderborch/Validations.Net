using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a number is equal to another number.
/// </summary>
public static class NumberEqualToValidation
{
    /// <summary>
    /// Determines whether the specified value is equal to the other value.
    /// </summary>
    /// <typeparam name="T">The type of the value to check, which must implement <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="other">The value to compare against.</param>
    /// <returns><c>true</c> if the value is equal to the other value; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo<T>(T value, T other) where T : INumber<T>
    {
        return value == other;
    }

    /// <summary>
    /// Extension method that checks if the specified value is equal to the other value.
    /// </summary>
    /// <typeparam name="T">The type of the value to check, which must implement <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="other">The value to compare against.</param>
    /// <returns><c>true</c> if the value is equal to the other value; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEqualTo<T>(this T value, T other) where T : INumber<T>
    {
        return value == other;
    }

    /// <summary>
    /// Validates that the specified value is equal to the other value. Throws a <see cref="ValidationException"/> if the value is not equal.
    /// </summary>
    /// <typeparam name="T">The type of the value to check, which must implement <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="other">The value to compare against.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original value if it is equal to the other value.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not equal to the other value.</exception>
    [DebuggerStepThrough]
    public static T IsEqualToValidation<T>(T value, T other, string variableName, object? blackboard = null) where T : INumber<T>
    {
        if (!value.CheckIsEqualTo(other))
        {
            throw new ValidationException(variableName, $"{variableName} must be equal to {other}.", "IsEqualTo", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "other", other }
            });
        }

        return value;
    }
    
    /// <summary>
    /// Extension method that validates the specified value is equal to the other value. Throws a <see cref="ValidationException"/> if the value is not equal.
    /// </summary>
    /// <typeparam name="T">The type of the value to check, which must implement <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="other">The value to compare against.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original value if it is equal to the other value.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not equal to the other value.</exception>
    [DebuggerStepThrough]
    public static T ValidateIsEqualTo<T>(this T value, T other, string variableName, object? blackboard = null) where T : INumber<T>
    {
        return IsEqualToValidation(value, other, variableName, blackboard);
    }
}
