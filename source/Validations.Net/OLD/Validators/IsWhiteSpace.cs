using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
///     Provides extension methods for validating that strings are whitespace.
/// </summary>
public static class IsWhiteSpace
{
    /// <summary>
    ///     Checks if a string is whitespace.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not null and consists only of whitespace characters; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value);
    }

    /// <summary>
    ///     Ensures that a string is whitespace, throwing a <see cref="ValidationException" /> if it isn't.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not whitespace.</exception>
    public static string EnsureIsWhiteSpace(this string? value, string propertyName, IBlackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateIsWhiteSpace(propertyName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }

    /// <summary>
    ///     Validates that a string is whitespace.
    ///     If the string is not whitespace, returns a <see cref="ValidationResult" /> containing validation failure details.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsWhiteSpace(this string? value, string variableName,
        IBlackboard? blackboard = null)
    {
        if (!value.CheckIsWhiteSpace())
        {
            ValidationResult result = new(
                new ValidationException("IsWhiteSpace", variableName,
                    $"{variableName} must be whitespace.",
                    blackboard, new Dictionary<string, object?>
                    {
                        { "value", value }
                    }));
            return result;
        }

        return new ValidationResult();
    }
}
