using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string is not empty.
/// </summary>
public static class StringNotEmptyValidation
{
    /// <summary>
    /// Determines whether the specified string is not empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns><c>true</c> if the string is not empty; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotEmpty(string? value)
    {
        return value is not null && value.Length > 0;
    }

    /// <summary>
    /// Extension method that determines whether the specified string is not empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns><c>true</c> if the string is not empty; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEmpty(this string? value)
    {
        return IsNotEmpty(value);
    }

    /// <summary>
    /// Validates that the specified string is not empty. Throws a <see cref="ValidationException"/> if the value is empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it is not empty.</returns>
    /// <exception cref="ValidationException">Thrown when the string is empty.</exception>
    [DebuggerStepThrough]
    public static string IsNotEmptyValidation(string? value, string variableName, object? blackboard = null)
    {
        if (!value.CheckIsNotEmpty())
        {
            throw new ValidationException(variableName, $"{variableName} must not be empty.", "IsNotEmpty", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }

    /// <summary>
    /// Extension method that validates the specified string is not empty. Throws a <see cref="ValidationException"/> if the value is empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it is not empty.</returns>
    /// <exception cref="ValidationException">Thrown when the string is empty.</exception>
    [DebuggerStepThrough]
    public static string ValidateIsNotEmpty(this string? value, string variableName, object? blackboard = null)
    {
        IsNotEmptyValidation(value, variableName, blackboard);
        return value!;
    }
}
