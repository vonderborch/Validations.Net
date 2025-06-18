using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for validating that strings are not null or whitespace.
/// </summary>
public static class IsNotNullOrWhiteSpace
{
    /// <summary>
    ///     Checks if a string is not null and contains at least one non-whitespace character.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not null and contains at least one non-whitespace character; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrWhiteSpace(this string? value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    ///     Validates that a string is not null and contains at least one non-whitespace character, throwing a
    ///     <see cref="ValidationException" /> if it doesn't.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string is null or consists only of whitespace characters.</exception>
    public static string ValidateIsNotNullOrWhiteSpace(this string? value, string propertyName,
        Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotNullOrWhiteSpace())
        {
            throw new ValidationException("IsNotNullOrWhiteSpace", propertyName,
                $"{propertyName} must not be null or whitespace.", blackboard, new Dictionary<string, object?>
                {
                    { "value", value }
                });
        }

        return value!;
    }
}
