using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for validating the length of collections and strings.
/// </summary>
public static class IsLength
{
    /// <summary>
    ///     Checks if a collection has a specific length.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="length">The expected length.</param>
    /// <returns>True if the collection has the specified length; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsLength<T>(this ICollection<T>? value, int length)
    {
        return value is not null && value.Count == length;
    }

    /// <summary>
    ///     Checks if a string has a specific length.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="length">The expected length.</param>
    /// <returns>True if the string has the specified length; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsLength(this string? value, int length)
    {
        return value is not null && value.Length == length;
    }

    /// <summary>
    /// Validates that a collection has a specific length.
    /// If the collection does not have the specified length, returns a <see cref="ValidationResult"/> containing validation failure details.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="length">The expected length.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsLength<T>(this ICollection<T>? value, int length, string variableName,
        Blackboard? blackboard = null)
    {
        if (!value.CheckIsLength(length))
        {
            ValidationResult result = new(
                new ValidationException("IsLength", variableName,
                    $"{variableName} must have length {length}.",
                    blackboard, new Dictionary<string, object?>
                    {
                        { "value", value },
                        { "length", length },
                        { "actualLength", value?.Count }
                    }));
            return result;
        }

        return new ValidationResult();
    }

    /// <summary>
    /// Validates that a string has a specific length.
    /// If the string does not have the specified length, returns a <see cref="ValidationResult"/> containing validation failure details.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="length">The expected length.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsLength(this string? value, int length, string variableName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsLength(length))
        {
            ValidationResult result = new(
                new ValidationException("IsLength", variableName,
                    $"{variableName} must have length {length}.",
                    blackboard, new Dictionary<string, object?>
                    {
                        { "value", value },
                        { "length", length },
                        { "actualLength", value?.Length }
                    }));
            return result;
        }

        return new ValidationResult();
    }

    /// <summary>
    ///     Ensures that a collection has a specific length, throwing a <see cref="ValidationException" /> if it doesn't.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="length">The expected length.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original collection if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not have the specified length.</exception>
    public static ICollection<T> EnsureIsLength<T>(this ICollection<T>? value, int length, string propertyName,
        Blackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateIsLength(length, propertyName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }

    /// <summary>
    ///     Ensures that a string has a specific length, throwing a <see cref="ValidationException" /> if it doesn't.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="length">The expected length.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string does not have the specified length.</exception>
    public static string EnsureIsLength(this string? value, int length, string propertyName, Blackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateIsLength(length, propertyName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }
}
