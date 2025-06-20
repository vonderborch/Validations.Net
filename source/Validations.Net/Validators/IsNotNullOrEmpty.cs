using System.Collections;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for validating that collections and strings are not null or empty.
/// </summary>
public static class IsNotNullOrEmpty
{
    /// <summary>
    ///     Checks if a collection is not null or empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <returns>True if the collection is not null and has at least one element; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrEmpty<T>(this T? value) where T : ICollection
    {
        return value is not null && value.Count > 0;
    }

    /// <summary>
    ///     Checks if a string is not null or empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not null and has a length greater than 0; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrEmpty(this string? value)
    {
        return !string.IsNullOrEmpty(value);
    }

    /// <summary>
    ///     Ensures that a collection is not null or empty, throwing a <see cref="ValidationException" /> if it is.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original collection if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the collection is null or empty.</exception>
    public static T EnsureIsNotNullOrEmpty<T>(this T? value, string propertyName,
        Blackboard? blackboard = null) where T : ICollection
    {
        ValidationResult result = value.ValidateIsNotNullOrEmpty(propertyName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }

    /// <summary>
    ///     Ensures that a string is not null or empty, throwing a <see cref="ValidationException" /> if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string is null or empty.</exception>
    public static string EnsureIsNotNullOrEmpty(this string? value, string propertyName, Blackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateIsNotNullOrEmpty(propertyName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }

    /// <summary>
    ///     Validates that a collection is not null or empty.
    ///     If the collection is null or empty, returns a <see cref="ValidationResult" /> containing validation failure
    ///     details.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsNotNullOrEmpty<T>(this T? value, string variableName,
        Blackboard? blackboard = null) where T : ICollection
    {
        if (!value.CheckIsNotNullOrEmpty())
        {
            ValidationResult result = new(
                new ValidationException("IsNotNullOrEmpty", variableName,
                    $"{variableName} must not be null or empty.",
                    blackboard, new Dictionary<string, object?>
                    {
                        { "value", value }
                    }));
            return result;
        }

        return new ValidationResult();
    }

    /// <summary>
    ///     Validates that a string is not null or empty.
    ///     If the string is null or empty, returns a <see cref="ValidationResult" /> containing validation failure details.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsNotNullOrEmpty(this string? value, string variableName,
        Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotNullOrEmpty())
        {
            ValidationResult result = new(
                new ValidationException("IsNotNullOrEmpty", variableName,
                    $"{variableName} must not be null or empty.",
                    blackboard, new Dictionary<string, object?>
                    {
                        { "value", value }
                    }));
            return result;
        }

        return new ValidationResult();
    }
}
