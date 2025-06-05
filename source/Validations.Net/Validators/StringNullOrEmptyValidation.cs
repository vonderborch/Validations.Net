using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string is null or empty.
/// </summary>
public static class StringNullOrEmptyValidation
{
    /// <summary>
    /// Determines whether the specified string is null or empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns><c>true</c> if the string is null or empty; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNullOrEmpty(string? value)
    {
        return string.IsNullOrEmpty(value);
    }
    
    /// <summary>
    /// Extension method that determines whether the specified string is null or empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns><c>true</c> if the string is null or empty; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrEmpty(this string? value)
    {
        return string.IsNullOrEmpty(value);
    }
    
    /// <summary>
    /// Validates that the specified string is null or empty. Throws a <see cref="ValidationException"/> if the value is not null or empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it is null or empty.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not null or empty.</exception>
    [DebuggerStepThrough]
    public static string IsNullOrEmptyValidation(string? value, string variableName, object? blackboard = null)
    {
        if (!value.CheckIsNullOrEmpty())
        {
            throw new ValidationException(variableName, $"{variableName} must be null or empty.", "IsNullOrEmpty", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
    
    /// <summary>
    /// Extension method that validates the specified string is null or empty. Throws a <see cref="ValidationException"/> if the value is not null or empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it is null or empty.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not null or empty.</exception>
    [DebuggerStepThrough]
    public static string ValidateIsNullOrEmpty(this string? value, string variableName, object? blackboard = null)
    {
        IsNullOrEmptyValidation(value, variableName, blackboard);
        return value!;
    }
}
