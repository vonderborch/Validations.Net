using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string is not white space.
/// </summary>
public static class StringNotWhiteSpaceValidation
{
    /// <summary>
    /// Determines whether the specified string is not white space.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns><c>true</c> if the string is not white space; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotWhiteSpace(string? value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    /// Extension method that determines whether the specified string is not white space.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns><c>true</c> if the string is not white space; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotWhiteSpace(this string? value)
    {
        return IsNotWhiteSpace(value);
    }

    /// <summary>
    /// Validates that the specified string is not white space. Throws a <see cref="ValidationException"/> if the value is white space.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it is not white space.</returns>
    /// <exception cref="ValidationException">Thrown when the string is white space.</exception>
    [DebuggerStepThrough]
    public static string IsNotWhiteSpaceValidation(string? value, string variableName, object? blackboard = null)
    {
        if (!value.CheckIsNotWhiteSpace())
        {
            throw new ValidationException(variableName, $"{variableName} must not be white space.", "IsNotWhiteSpace", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }

    /// <summary>
    /// Extension method that validates the specified string is not white space. Throws a <see cref="ValidationException"/> if the value is white space.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it is not white space.</returns>
    /// <exception cref="ValidationException">Thrown when the string is white space.</exception>
    [DebuggerStepThrough]
    public static string ValidateIsNotWhiteSpace(this string? value, string variableName, object? blackboard = null)
    {
        IsNotWhiteSpaceValidation(value, variableName, blackboard);
        return value!;
    }
}
