using System.Collections;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
///     Provides extension methods for validating that collections and strings do not have a specific length.
/// </summary>
public static class IsNotLength
{
    /// <summary>
    ///     Checks if a collection does not have a specific length.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="length">The length to check against.</param>
    /// <returns>True if the collection does not have the specified length; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotLength<T>(this T? value, int length) where T : ICollection
    {
        return value is null || value.Count != length;
    }

    /// <summary>
    ///     Checks if a string does not have a specific length.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="length">The length to check against.</param>
    /// <returns>True if the string does not have the specified length; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotLength(this string? value, int length)
    {
        return value is not null && value.Length != length;
    }

    /// <summary>
    ///     Ensures that a collection does not have a specific length, throwing a <see cref="ValidationException" /> if it
    ///     does.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="length">The length to check against.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original collection if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the collection has the specified length.</exception>
    public static T EnsureIsNotLength<T>(this T? value, int length, string propertyName,
        IBlackboard? blackboard = null) where T : ICollection
    {
        ValidationResult result = value.ValidateIsNotLength(length, propertyName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }

    /// <summary>
    ///     Ensures that a string does not have a specific length, throwing a <see cref="ValidationException" /> if it does.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="length">The length to check against.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string has the specified length.</exception>
    public static string? EnsureIsNotLength(this string? value, int length, string propertyName,
        IBlackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateIsNotLength(length, propertyName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Validates that a collection does not have a specific length.
    ///     If the collection has the specified length, returns a <see cref="ValidationResult" /> containing validation failure
    ///     details.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="length">The length to check against.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsNotLength<T>(this T? value, int length, string variableName,
        IBlackboard? blackboard = null) where T : ICollection
    {
        if (!value.CheckIsNotLength(length))
        {
            ValidationResult result = new(
                new ValidationException("IsNotLength", variableName,
                    $"{variableName} must not have length {length}.",
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
    ///     Validates that a string does not have a specific length.
    ///     If the string has the specified length, returns a <see cref="ValidationResult" /> containing validation failure
    ///     details.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="length">The length to check against.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateIsNotLength(this string? value, int length, string variableName,
        IBlackboard? blackboard = null)
    {
        if (!value.CheckIsNotLength(length))
        {
            ValidationResult result = new(
                new ValidationException("IsNotLength", variableName,
                    $"{variableName} must not have length {length}.",
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
}
