using System.Diagnostics;
using System.Runtime.CompilerServices;
using Validations.Net.Validators.Collections;

namespace Validations.Net.Validators.Strings;

/// <summary>
/// Provides extension methods for validating whether string values are empty (but not null).
/// </summary>
public static class EmptyOnlyValidations
{
    /// <summary>
    /// Checks if the provided string is empty (but not null).
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>true if the string is empty but not null; otherwise, false.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEmptyOnly(this string? value)
    {
        return value is not null && value.Length == 0;
    }
    
    /// <summary>
    /// Checks if the provided string is not empty (either null or has content).
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>true if the string is null or has content; otherwise, false.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEmptyOnly(this string? value)
    {
        return !(value is not null && value.Length == 0);
    }
    
    /// <summary>
    /// Validates that the provided string is empty (but not null).
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Optional. Additional context that will be passed to the exception.</param>
    /// <returns>The original string value if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string is null or has content.</exception>
    [DebuggerStepThrough]
    public static string ValidateIsEmptyOnly(this string? value, string variableName, object? blackboard = null)
    {
        if (!value.CheckIsEmptyOnly())
        {
            throw new ValidationException(variableName, $"{variableName} must be empty.", "IsEmpty", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
    
    /// <summary>
    /// Validates that the provided string is not empty (either null or has content).
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Optional. Additional context that will be passed to the exception.</param>
    /// <returns>The original string value if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string is empty (but not null).</exception>
    [DebuggerStepThrough]
    public static string ValidateIsNotEmptyOnly(this string? value, string variableName, object? blackboard = null)
    {
        if (!value.CheckIsNotEmptyOnly())
        {
            throw new ValidationException(variableName, $"{variableName} must not be empty.", "IsNotEmpty", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
}
