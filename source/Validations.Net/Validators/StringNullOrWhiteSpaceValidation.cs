using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string is null, empty, or whitespace.
/// </summary>
public static class StringIsNullOrWhiteSpaceValidation
{
    /// <summary>
    /// Determines whether the specified string is null, empty, or whitespace.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns><c>true</c> if the string is null, empty, or whitespace; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsIsNullOrWhiteSpace(string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }
    
    /// <summary>
    /// Extension method that determines whether the specified string is null, empty, or whitespace.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns><c>true</c> if the string is null, empty, or whitespace; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsIsNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }
    
    /// <summary>
    /// Validates that the specified string is null, empty, or whitespace. Throws a <see cref="ValidationException"/> if the value is not null, empty, or whitespace.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it is null, empty, or whitespace.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not null, empty, or whitespace.</exception>
    [DebuggerStepThrough]
    public static string IsIsNullOrWhiteSpaceValidation(string? value, string variableName, object? blackboard = null)
    {
        if (!value.CheckIsIsNullOrWhiteSpace())
        {
            throw new ValidationException(variableName, $"{variableName} must be null, empty, or whitespace.", "IsIsNullOrWhiteSpace", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
    
    /// <summary>
    /// Extension method that validates the specified string is null, empty, or whitespace. Throws a <see cref="ValidationException"/> if the value is not null, empty, or whitespace.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it is null, empty, or whitespace.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not null, empty, or whitespace.</exception>
    [DebuggerStepThrough]
    public static string ValidateIsIsNullOrWhiteSpace(this string? value, string variableName, object? blackboard = null)
    {
        IsIsNullOrWhiteSpaceValidation(value, variableName, blackboard);
        return value!;
    }
}
