using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for validating that strings consist only of whitespace characters.
/// </summary>
public static class IsWhiteSpace
{
    /// <summary>
    ///     Checks if a string is not null, not empty, and consists only of whitespace characters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not null, not empty, and consists only of whitespace characters; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsWhiteSpace(this string? value)
    {
        return !string.IsNullOrEmpty(value) && string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    ///     Validates that a string consists only of whitespace characters, throwing a <see cref="ValidationException" /> if it
    ///     doesn't.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string is null, empty, or contains non-whitespace characters.</exception>
    public static string ValidateIsWhiteSpace(this string? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsWhiteSpace())
        {
            throw new ValidationException("ValidateIsWhiteSpace", propertyName, $"{propertyName} must be whitespace.",
                blackboard, new Dictionary<string, object?>
                {
                    { "value", value }
                });
        }

        return value!;
    }
}
