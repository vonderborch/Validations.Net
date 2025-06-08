using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.OLDValidators.Strings;

/// <summary>
///     Provides extension methods for validating if a string contains specific characters or substrings.
/// </summary>
public static class ContainsValidations
{
    /// <summary>
    ///     Checks if the string contains a specified substring using the specified comparison type.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <param name="comparisonType">The string comparison type to use. Defaults to StringComparison.Ordinal.</param>
    /// <returns>True if the string contains the substring; otherwise, false.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContains(this string value, string substring,
        StringComparison comparisonType = StringComparison.Ordinal)
    {
        value.ValidateIsNotNull(nameof(value));
        return value.Contains(substring, comparisonType);
    }

    /// <summary>
    ///     Checks if the string contains a specified character using the specified comparison type.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="character">The character to look for.</param>
    /// <param name="comparisonType">The string comparison type to use. Defaults to StringComparison.Ordinal.</param>
    /// <returns>True if the string contains the character; otherwise, false.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContains(this string value, char character,
        StringComparison comparisonType = StringComparison.Ordinal)
    {
        value.ValidateIsNotNull(nameof(value));
        return value.Contains(character, comparisonType);
    }

    /// <summary>
    ///     Validates that the string contains a specified substring. Throws a ValidationException if the validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="substring">The substring that should be present in the value.</param>
    /// <param name="comparisonType">The string comparison type to use. Defaults to StringComparison.Ordinal.</param>
    /// <param name="variableName">
    ///     The name of the variable being validated, used in the exception message. Defaults to
    ///     "value".
    /// </param>
    /// <param name="blackboard">Optional contextual information that will be added to the exception if validation fails.</param>
    /// <returns>The original string if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain the specified substring.</exception>
    [DebuggerStepThrough]
    public static string ValidateContains(this string value, string substring,
        StringComparison comparisonType = StringComparison.Ordinal, string variableName = "value",
        object? blackboard = null)
    {
        if (!value.CheckContains(substring, comparisonType))
        {
            throw new ValidationException(variableName, $"{variableName} must contain '{substring}'.", "Contains",
                blackboard, new Dictionary<string, object?>
                {
                    { "value", value },
                    { "substring", substring }
                });
        }

        return value;
    }

    /// <summary>
    ///     Validates that the string contains a specified character. Throws a ValidationException if the validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="character">The character that should be present in the value.</param>
    /// <param name="comparisonType">The string comparison type to use. Defaults to StringComparison.Ordinal.</param>
    /// <param name="variableName">
    ///     The name of the variable being validated, used in the exception message. Defaults to
    ///     "value".
    /// </param>
    /// <param name="blackboard">Optional contextual information that will be added to the exception if validation fails.</param>
    /// <returns>The original string if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not contain the specified character.</exception>
    [DebuggerStepThrough]
    public static string ValidateContains(this string value, char character,
        StringComparison comparisonType = StringComparison.Ordinal, string variableName = "value",
        object? blackboard = null)
    {
        if (!value.CheckContains(character, comparisonType))
        {
            throw new ValidationException(variableName, $"{variableName} must contain '{character}'.", "Contains",
                blackboard, new Dictionary<string, object?>
                {
                    { "value", value },
                    { "character", character }
                });
        }

        return value;
    }
}
