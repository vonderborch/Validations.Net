using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string is white space.
/// </summary>
public static class StringWhiteSpaceValidation
{
    /// <summary>
    /// Determines whether the specified string is white space.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns><c>true</c> if the string is white space; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsWhiteSpace(string? value)
    {
        return value is not null && string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    /// Extension method that determines whether the specified string is white space.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns><c>true</c> if the string is white space; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsWhiteSpace(this string? value)
    {
        return IsWhiteSpace(value);
    }

    /// <summary>
    /// Validates that the specified string is white space. Throws a <see cref="ValidationException"/> if the value is not white space.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it is white space.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not white space.</exception>
    [DebuggerStepThrough]
    public static string IsWhiteSpaceValidation(string? value, string variableName, object? blackboard = null)
    {
        if (!value.CheckIsWhiteSpace())
        {
            throw new ValidationException(variableName, $"{variableName} must be white space.", "IsWhiteSpace", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }

    /// <summary>
    /// Extension method that validates the specified string is white space. Throws a <see cref="ValidationException"/> if the value is not white space.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it is white space.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not white space.</exception>
    [DebuggerStepThrough]
    public static string ValidateIsWhiteSpace(this string? value, string variableName, object? blackboard = null)
    {
        IsWhiteSpaceValidation(value, variableName, blackboard);
        return value!;
    }
}
