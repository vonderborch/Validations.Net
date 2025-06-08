using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.OLDValidators.Strings;

/// <summary>
///     Provides extension methods for validating whether string values are null or white space.
/// </summary>
public static class NullOrWhiteSpaceValidations
{
    /// <summary>
    ///     Checks if the provided string is not null, empty, or consists only of white-space characters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>true if the string is not null, empty, or white space; otherwise, false.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrWhiteSpace(this string? value)
    {
        return !value.CheckIsNullOrWhiteSpace();
    }

    /// <summary>
    ///     Checks if the provided string is null, empty, or consists only of white-space characters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>true if the string is null, empty, or consists only of white-space characters; otherwise, false.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    ///     Validates that the provided string is not null, empty, or consists only of white-space characters.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Optional. Additional context that will be passed to the exception.</param>
    /// <returns>The original string value if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string is null, empty, or white space.</exception>
    [DebuggerStepThrough]
    public static string ValidateIsNotNullOrWhiteSpace(this string? value, string variableName,
        object? blackboard = null)
    {
        if (value.CheckIsNullOrWhiteSpace())
        {
            throw new ValidationException(variableName, $"{variableName} must not be null, empty, or white space.",
                "IsNotWhiteSpace", blackboard, new Dictionary<string, object?>
                {
                    { "value", value }
                });
        }

        return value;
    }

    /// <summary>
    ///     Validates that the provided string is null, empty, or consists only of white-space characters.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Optional. Additional context that will be passed to the exception.</param>
    /// <returns>The original string value if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not null, empty, or white space.</exception>
    [DebuggerStepThrough]
    public static string ValidateIsNullOrWhiteSpace(this string? value, string variableName, object? blackboard = null)
    {
        if (!value.CheckIsNullOrWhiteSpace())
        {
            throw new ValidationException(variableName, $"{variableName} must be null, empty, or white space.",
                "ValidateIsWhiteSpace", blackboard, new Dictionary<string, object?>
                {
                    { "value", value }
                });
        }

        return value;
    }
}
