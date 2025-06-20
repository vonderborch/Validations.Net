using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for validating that strings are null or whitespace.
/// </summary>
public static class IsNullOrWhiteSpace
{
    /// <summary>
    ///     Checks if a string is null or whitespace.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is null or consists only of whitespace characters; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    /// Validates that a string is null or whitespace.
    /// If the string is not null or whitespace, returns a <see cref="ValidationResult"/> containing validation failure details.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsNullOrWhiteSpace(this string? value, string variableName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNullOrWhiteSpace())
        {
            ValidationResult result = new(
                new ValidationException("IsNullOrWhiteSpace", variableName,
                    $"{variableName} must be null or whitespace.",
                    blackboard, new Dictionary<string, object?>
                    {
                        { "value", value }
                    }));
            return result;
        }

        return new ValidationResult();
    }

    /// <summary>
    ///     Ensures that a string is null or whitespace, throwing a <see cref="ValidationException" /> if it isn't.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not null and not whitespace.</exception>
    public static string? EnsureIsNullOrWhiteSpace(this string? value, string propertyName, Blackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateIsNullOrWhiteSpace(propertyName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }
}
