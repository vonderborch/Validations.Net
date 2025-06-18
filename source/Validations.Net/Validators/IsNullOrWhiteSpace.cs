using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides extension methods for validating that strings are null or whitespace.
/// </summary>
public static class IsNullOrWhiteSpace
{
    /// <summary>
    /// Checks if a string is null or consists only of whitespace characters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is null or consists only of whitespace characters; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }
    
    /// <summary>
    /// Validates that a string is null or consists only of whitespace characters, throwing a <see cref="ValidationException"/> if it isn't.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not null and contains non-whitespace characters.</exception>
    public static string ValidateIsNullOrWhiteSpace(this string? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNullOrWhiteSpace())
        {
            throw new ValidationException("IsNullOrWhiteSpace", propertyName, $"{propertyName} must be null or whitespace.", blackboard, new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
}
