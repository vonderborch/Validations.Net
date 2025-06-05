using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string is empty.
/// </summary>
public static class StringEmptyValidation
{
    /// <summary>
    /// Determines whether the specified string is empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns><c>true</c> if the string is empty; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEmpty(string? value)
    {
        return value is not null && value.Length == 0;
    }
    
    /// <summary>
    /// Extension method that determines whether the specified string is empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns><c>true</c> if the string is empty; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEmpty(this string? value)
    {
        return IsEmpty(value);
    }
    
    /// <summary>
    /// Validates that the specified string is empty. Throws a <see cref="ValidationException"/> if the value is not empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it is empty.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not empty.</exception>
    [DebuggerStepThrough]
    public static string IsEmptyValidation(string? value, string variableName, object? blackboard = null)
    {
        if (!value.CheckIsEmpty())
        {
            throw new ValidationException(variableName, $"{variableName} must be empty.", "IsEmpty", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
    
    /// <summary>
    /// Extension method that validates the specified string is empty. Throws a <see cref="ValidationException"/> if the value is not empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it is empty.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not empty.</exception>
    [DebuggerStepThrough]
    public static string ValidateIsEmpty(this string? value, string variableName, object? blackboard = null)
    {
        IsEmptyValidation(value, variableName, blackboard);
        return value!;
    }
}
