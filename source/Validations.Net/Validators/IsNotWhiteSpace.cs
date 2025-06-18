using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for validating that strings contain at least one non-whitespace character.
/// </summary>
public static class IsNotWhiteSpace
{
    /// <summary>
    ///     Checks if a string is not null, not empty, and contains at least one non-whitespace character.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>
    ///     True if the string is not null, not empty, and contains at least one non-whitespace character; otherwise,
    ///     false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotWhiteSpace(this string? value)
    {
        return !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    ///     Validates that a string contains at least one non-whitespace character, throwing a
    ///     <see cref="ValidationException" /> if it doesn't.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string is null, empty, or consists only of whitespace characters.</exception>
    public static string ValidateIsNotWhiteSpace(this string? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotWhiteSpace())
        {
            throw new ValidationException("IsNotWhiteSpace", propertyName, $"{propertyName} must not be whitespace.",
                blackboard, new Dictionary<string, object?>
                {
                    { "value", value }
                });
        }

        return value!;
    }
}
