using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators.Strings;

/// <summary>
/// Provides extension methods for validating whether string values are null or empty.
/// </summary>
public static class NullOrEmptyValidations
{
    /// <summary>
    /// Checks if the provided string is null or empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>true if the string is null or empty; otherwise, false.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrEmpty(this string? value)
    {
        return string.IsNullOrEmpty(value);
    }
    
    /// <summary>
    /// Checks if the provided string is not null or empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>true if the string is not null or empty; otherwise, false.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrEmpty(this string? value)
    {
        return !string.IsNullOrEmpty(value);
    }
    
    /// <summary>
    /// Validates that the provided string is null or empty.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Optional. Additional context that will be passed to the exception.</param>
    /// <returns>The original string value if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not null or empty.</exception>
    [DebuggerStepThrough]
    public static string ValidateIsNullOrEmpty(this string? value, string variableName, object? blackboard = null)
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
    /// Validates that the provided string is not null or empty.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Optional. Additional context that will be passed to the exception.</param>
    /// <returns>The original string value if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string is null or empty.</exception>
    [DebuggerStepThrough]
    public static string ValidateIsNotNullOrEmpty(this string? value, string variableName, object? blackboard = null)
    {
        if (!value.CheckIsNotNullOrEmpty())
        {
            throw new ValidationException(variableName, $"{variableName} must not be null or empty.", "IsNotNullOrEmpty", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
}
