using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for validating that strings are not whitespace.
/// </summary>
public static class IsNotWhiteSpace
{
    /// <summary>
    ///     Checks if a string is not whitespace.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is null, empty, or contains non-whitespace characters; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotWhiteSpace(this string? value)
    {
        return string.IsNullOrEmpty(value) || !string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    ///     Ensures that a string is not whitespace, throwing a <see cref="ValidationException" /> if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string is whitespace.</exception>
    public static string? EnsureIsNotWhiteSpace(this string? value, string propertyName, IBlackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateIsNotWhiteSpace(propertyName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Validates that a string is not whitespace.
    ///     If the string is whitespace, returns a <see cref="ValidationResult" /> containing validation failure details.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsNotWhiteSpace(this string? value, string variableName,
        IBlackboard? blackboard = null)
    {
        if (!value.CheckIsNotWhiteSpace())
        {
            ValidationResult result = new(
                new ValidationException("IsNotWhiteSpace", variableName,
                    $"{variableName} must not be whitespace.",
                    blackboard, new Dictionary<string, object?>
                    {
                        { "value", value }
                    }));
            return result;
        }

        return new ValidationResult();
    }
}
