using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string is not null, empty, or whitespace.
/// </summary>
public static class StringNotNullOrWhiteSpaceValidation
{
    /// <summary>
    /// Determines whether the specified string is not null, empty, or whitespace.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns><c>true</c> if the string is not null, empty, or whitespace; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotNullOrWhiteSpace(string? value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }
    
    /// <summary>
    /// Extension method that determines whether the specified string is not null, empty, or whitespace.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns><c>true</c> if the string is not null, empty, or whitespace; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrWhiteSpace(this string? value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }
    
    /// <summary>
    /// Validates that the specified string is not null, empty, or whitespace. Throws a <see cref="ValidationException"/> if the value is null, empty, or whitespace.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it is not null, empty, or whitespace.</returns>
    /// <exception cref="ValidationException">Thrown when the string is null, empty, or whitespace.</exception>
    [DebuggerStepThrough]
    public static string IsNotNullOrWhiteSpaceValidation(string? value, string variableName, object? blackboard = null)
    {
        if (!value.CheckIsNotNullOrWhiteSpace())
        {
            throw new ValidationException(variableName, $"{variableName} can not be null, empty, or whitespace.", "IsNotNullOrWhiteSpace", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
    
    /// <summary>
    /// Extension method that validates the specified string is not null, empty, or whitespace. Throws a <see cref="ValidationException"/> if the value is null, empty, or whitespace.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it is not null, empty, or whitespace.</returns>
    /// <exception cref="ValidationException">Thrown when the string is null, empty, or whitespace.</exception>
    [DebuggerStepThrough]
    public static string ValidateIsNotNullOrWhiteSpace(this string? value, string variableName, object? blackboard = null)
    {
        IsNotNullOrWhiteSpaceValidation(value, variableName, blackboard);
        return value!;
    }
}
