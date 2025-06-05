using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string is not null or empty.
/// </summary>
public static class StringNotNullOrEmptyValidation
{
    /// <summary>
    /// Determines whether the specified string is not null or empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns><c>true</c> if the string is not null or empty; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotNullOrEmpty(string? value)
    {
        return !string.IsNullOrEmpty(value);
    }
    
    /// <summary>
    /// Extension method that determines whether the specified string is not null or empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns><c>true</c> if the string is not null or empty; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrEmpty(this string? value)
    {
        return !string.IsNullOrEmpty(value);
    }
    
    /// <summary>
    /// Validates that the specified string is not null or empty. Throws a <see cref="ValidationException"/> if the value is null or empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it is not null or empty.</returns>
    /// <exception cref="ValidationException">Thrown when the string is null or empty.</exception>
    [DebuggerStepThrough]
    public static string IsNotNullOrEmptyValidation(string? value, string variableName, object? blackboard = null)
    {
        if (!value.CheckIsNotNullOrEmpty())
        {
            throw new ValidationException(variableName, $"{variableName} can not be null or empty.", "IsNotNullOrEmpty", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
    
    /// <summary>
    /// Extension method that validates the specified string is not null or empty. Throws a <see cref="ValidationException"/> if the value is null or empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it is not null or empty.</returns>
    /// <exception cref="ValidationException">Thrown when the string is null or empty.</exception>
    [DebuggerStepThrough]
    public static string ValidateIsNotNullOrEmpty(this string? value, string variableName, object? blackboard = null)
    {
        IsNotNullOrEmptyValidation(value, variableName, blackboard);
        return value!;
    }
}
