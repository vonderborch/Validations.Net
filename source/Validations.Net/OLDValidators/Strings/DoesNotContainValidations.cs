using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.OLDValidators.Strings;

/// <summary>
///     Provides extension methods for validating that strings do not contain specific characters or substrings.
/// </summary>
/// <remarks>
///     This class contains methods for both checking conditions (methods prefixed with 'Check')
///     and validating conditions (methods prefixed with 'Validate'). The validation methods will throw
///     a <see cref="ValidationException" /> if the validation fails.
/// </remarks>
public static class DoesNotContainValidations
{
    /// <summary>
    ///     Checks if the string contains a specified substring using the specified comparison type.
    /// </summary>
    /// <param name="value">The string to check. Cannot be null.</param>
    /// <param name="substring">The substring to look for.</param>
    /// <param name="comparisonType">The string comparison type to use. Defaults to StringComparison.Ordinal.</param>
    /// <returns>True if the string contains the substring; otherwise, false.</returns>
    /// <remarks>
    ///     Note: Method name suggests it checks if a string does NOT contain a substring, but it currently returns true
    ///     if it DOES contain it.
    /// </remarks>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain(this string value, string substring,
        StringComparison comparisonType = StringComparison.Ordinal)
    {
        value.ValidateIsNotNull(nameof(value));
        return !value.Contains(substring, comparisonType);
    }

    /// <summary>
    ///     Checks if the string contains a specified character using the specified comparison type.
    /// </summary>
    /// <param name="value">The string to check. Cannot be null.</param>
    /// <param name="character">The character to look for.</param>
    /// <param name="comparisonType">The string comparison type to use. Defaults to StringComparison.Ordinal.</param>
    /// <returns>True if the string contains the character; otherwise, false.</returns>
    /// <remarks>
    ///     Note: Method name suggests it checks if a string does NOT contain a character, but it currently returns true
    ///     if it DOES contain it.
    /// </remarks>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain(this string value, char character,
        StringComparison comparisonType = StringComparison.Ordinal)
    {
        value.ValidateIsNotNull(nameof(value));
        return !value.Contains(character, comparisonType);
    }

    /// <summary>
    ///     Validates that the string does not contain the specified substring. Throws a ValidationException if the validation
    ///     fails.
    /// </summary>
    /// <param name="value">The string to validate. Cannot be null.</param>
    /// <param name="substring">The substring that should not be present in the value.</param>
    /// <param name="comparisonType">The string comparison type to use. Defaults to StringComparison.Ordinal.</param>
    /// <param name="variableName">
    ///     The name of the variable being validated, used in the exception message. Defaults to
    ///     "value".
    /// </param>
    /// <param name="blackboard">Optional contextual information that will be added to the exception if validation fails.</param>
    /// <returns>The original string if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains the specified substring.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesNotContain(this string value, string substring,
        StringComparison comparisonType = StringComparison.Ordinal, string variableName = "value",
        object? blackboard = null)
    {
        if (!value.CheckDoesNotContain(substring, comparisonType))
        {
            throw new ValidationException(variableName, $"{variableName} must not contain '{substring}'.",
                "DoesNotContain", blackboard, new Dictionary<string, object?>
                {
                    { "value", value },
                    { "substring", substring }
                });
        }

        return value;
    }

    /// <summary>
    ///     Validates that the string does not contain the specified character. Throws a ValidationException if the validation
    ///     fails.
    /// </summary>
    /// <param name="value">The string to validate. Cannot be null.</param>
    /// <param name="character">The character that should not be present in the value.</param>
    /// <param name="comparisonType">The string comparison type to use. Defaults to StringComparison.Ordinal.</param>
    /// <param name="variableName">
    ///     The name of the variable being validated, used in the exception message. Defaults to
    ///     "value".
    /// </param>
    /// <param name="blackboard">Optional contextual information that will be added to the exception if validation fails.</param>
    /// <returns>The original string if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains the specified character.</exception>
    [DebuggerStepThrough]
    public static string ValidateDoesNotContain(this string value, char character,
        StringComparison comparisonType = StringComparison.Ordinal, string variableName = "value",
        object? blackboard = null)
    {
        if (!value.CheckDoesNotContain(character, comparisonType))
        {
            throw new ValidationException(variableName, $"{variableName} must not contain '{character}'.",
                "DoesNotContain", blackboard, new Dictionary<string, object?>
                {
                    { "value", value },
                    { "character", character }
                });
        }

        return value;
    }
}
