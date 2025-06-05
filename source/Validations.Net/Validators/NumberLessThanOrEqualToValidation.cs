using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a number is less than or equal to another number.
/// </summary>
public static class NumberLessThanOrEqualToValidation
{
    /// <summary>
    /// Determines whether the specified value is less than or equal to the other value.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="other">The value to compare against.</param>
    /// <returns><c>true</c> if the value is less than or equal to the other value; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqualTo<T>(T value, T other) where T : INumber<T>
    {
        return value <= other;
    }
    
    /// <summary>
    /// Extension method that checks if the specified value is less than or equal to the other value.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="other">The value to compare against.</param>
    /// <returns><c>true</c> if the value is less than or equal to the other value; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsLessThanOrEqualTo<T>(this T value, T other) where T : INumber<T>
    {
        return value <= other;
    }

    /// <summary>
    /// Validates that the specified value is less than or equal to the other value. Throws a <see cref="ValidationException"/> if the value is not less or equal.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="other">The value to compare against.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original value if it is less than or equal to the other value.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not less than or equal to the other value.</exception>
    [DebuggerStepThrough]
    public static T IsLessThanOrEqualToValidation<T>(T value, T other, string variableName, object? blackboard = null) where T : INumber<T>
    {
        if (!value.CheckIsLessThanOrEqualTo(other))
        {
            throw new ValidationException(variableName, $"{variableName} must be less than or equal to {other}.", "IsLessThanOrEqualTo", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "other", other }
            });
        }

        return value;
    }
    
    /// <summary>
    /// Extension method that validates the specified value is less than or equal to the other value. Throws a <see cref="ValidationException"/> if the value is not less or equal.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="other">The value to compare against.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original value if it is less than or equal to the other value.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not less than or equal to the other value.</exception>
    [DebuggerStepThrough]
    public static T ValidateIsLessThanOrEqualTo<T>(this T value, T other, string variableName, object? blackboard = null) where T : INumber<T>
    {
        return IsLessThanOrEqualToValidation(value, other, variableName, blackboard);
    }
}
