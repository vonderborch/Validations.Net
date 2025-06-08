using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.OLDValidators.Strings;

/// <summary>
///     Provides validation methods to check if a string is or is not white space only.
/// </summary>
public static class WhiteSpaceOnlyValidations
{
    /// <summary>
    ///     Extension method that determines whether the specified string contains non-white space characters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns><c>true</c> if the string is not white space only; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotWhiteSpaceOnly(this string? value)
    {
        return !CheckIsWhiteSpaceOnly(value);
    }

    /// <summary>
    ///     Extension method that determines whether the specified string is only white space.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns><c>true</c> if the string is white space; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsWhiteSpaceOnly(this string? value)
    {
        return !string.IsNullOrEmpty(value) && string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    ///     Extension method that validates the specified string is not white space. Throws a
    ///     <see cref="ValidationException" /> if the value is white space.
    /// </summary>
    /// <param name="value"> The string to check.</param>
    /// <param name="variableName"> The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard"> Optional contextual data to include with the validation exception.</param>
    /// <returns> The original string if it is not white space.</returns>
    /// <exception cref="ValidationException"> Thrown when the string is white space.</exception>
    [DebuggerStepThrough]
    public static string ValidateIsNotWhiteSpaceOnly(this string? value, string variableName, object? blackboard = null)
    {
        if (!value.CheckIsNotWhiteSpaceOnly())
        {
            throw new ValidationException(variableName, $"{variableName} must not be white space.", "IsNotWhiteSpace",
                blackboard, new Dictionary<string, object?>
                {
                    { "value", value }
                });
        }

        return value!;
    }

    /// <summary>
    ///     Extension method that validates the specified string is white space. Throws a <see cref="ValidationException" /> if
    ///     the value is not white space.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original string if it is white space.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not white space.</exception>
    [DebuggerStepThrough]
    public static string ValidateIsWhiteSpaceOnly(this string? value, string variableName, object? blackboard = null)
    {
        if (!value.CheckIsWhiteSpaceOnly())
        {
            throw new ValidationException(variableName, $"{variableName} must be white space.", "ValidateIsWhiteSpace",
                blackboard, new Dictionary<string, object?>
                {
                    { "value", value }
                });
        }

        return value!;
    }
}
