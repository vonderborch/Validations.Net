using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for validating that collections and strings are empty.
/// </summary>
public static class IsEmpty
{
    /// <summary>
    ///     Checks if a collection is empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <returns>True if the collection is not null and has no elements; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEmpty<T>(this ICollection<T>? value)
    {
        return value is not null && value.Count == 0;
    }

    /// <summary>
    ///     Checks if a string is empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not null and has a length of 0; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEmpty(this string? value)
    {
        return value is not null && value.Length == 0;
    }

    /// <summary>
    /// Validates that a collection is empty.
    /// If the collection is not empty, returns a <see cref="ValidationResult"/> containing validation failure details.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsEmpty<T>(this ICollection<T>? value, string variableName,
        Blackboard? blackboard = null)
    {
        if (!value.CheckIsEmpty())
        {
            ValidationResult result = new(
                new ValidationException("IsEmpty", variableName,
                    $"{variableName} must be empty.",
                    blackboard, new Dictionary<string, object?>
                    {
                        { "value", value }
                    }));
            return result;
        }

        return new ValidationResult();
    }

    /// <summary>
    /// Validates that a string is empty.
    /// If the string is not empty, returns a <see cref="ValidationResult"/> containing validation failure details.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsEmpty(this string? value, string variableName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsEmpty())
        {
            ValidationResult result = new(
                new ValidationException("IsEmpty", variableName,
                    $"{variableName} must be empty.",
                    blackboard, new Dictionary<string, object?>
                    {
                        { "value", value }
                    }));
            return result;
        }

        return new ValidationResult();
    }

    /// <summary>
    ///     Ensures that a collection is empty, throwing a <see cref="ValidationException" /> if it isn't.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original collection if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the collection is null or not empty.</exception>
    public static ICollection<T> EnsureIsEmpty<T>(this ICollection<T>? value, string propertyName,
        Blackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateIsEmpty(propertyName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }

    /// <summary>
    ///     Ensures that a string is empty, throwing a <see cref="ValidationException" /> if it isn't.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string is null or not empty.</exception>
    public static string EnsureIsEmpty(this string? value, string propertyName, Blackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateIsEmpty(propertyName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }
}
