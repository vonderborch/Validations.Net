using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators;

/// <summary>
/// Class that provides extension methods for validating null values.
/// </summary>
public static class NullValidations
{
    /// <summary>
    /// Extension method that determines whether the specified value is null.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check for null.</param>
    /// <returns><c>true</c> if the value is null; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNull<T>(this T? value)
    {
        return value is null;
    }
    
    /// <summary>
    /// Extension method that determines whether the specified value is not null.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check for null.</param>
    /// <returns><c>true</c> if the value is not null; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNull<T>(this T? value)
    {
        return value is not null;
    }
    

    /// <summary>
    /// Extension method that validates the specified value is null. Throws a <see cref="ValidationException"/> if the value is null.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check for null.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original value if null.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not null.</exception>
    [DebuggerStepThrough]
    public static T? ValidateIsNull<T>(this T? value, string variableName, object? blackboard = null)
    {
        if (!value.CheckIsNull())
        {
            throw new ValidationException(variableName, $"{variableName} must be null.", "Null", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value;
    }

    /// <summary>
    /// Extension method that validates the specified value is not null. Throws a <see cref="ValidationException"/> if the value is null.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check for null.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original value if not null.</returns>
    /// <exception cref="ValidationException">Thrown when the value is null.</exception>
    [DebuggerStepThrough]
    public static T ValidateIsNotNull<T>(this T? value, string variableName, object? blackboard = null)
    {
        if (!value.CheckIsNotNull())
        {
            throw new ValidationException(variableName, $"{variableName} can not be null.", "NotNull", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
}
