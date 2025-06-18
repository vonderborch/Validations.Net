using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for validating that collections and strings do not have a specific length.
/// </summary>
public static class IsNotLength
{
    /// <summary>
    ///     Checks if a collection does not have the specified length.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="length">The length to check against.</param>
    /// <returns>True if the collection is null or does not have the specified length; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotLength<T>(this ICollection<T>? value, int length)
    {
        if (value is null)
        {
            return true;
        }

        return value.Count != length;
    }

    /// <summary>
    ///     Checks if a string does not have the specified length.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="length">The length to check against.</param>
    /// <returns>True if the string is null or does not have the specified length; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotLength(this string? value, int length)
    {
        if (value is null)
        {
            return true;
        }

        return value.Length != length;
    }

    /// <summary>
    ///     Validates that a collection does not have the specified length, throwing a <see cref="ValidationException" /> if it
    ///     does.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="length">The length to check against.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original collection if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the collection is not null and has the specified length.</exception>
    public static ICollection<T> ValidateIsNotLength<T>(this ICollection<T>? value, int length, string propertyName,
        Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotLength(length))
        {
            throw new ValidationException("IsNotLength", propertyName,
                $"{propertyName} must not have a length of {length}.", blackboard, new Dictionary<string, object?>
                {
                    { "value", value },
                    { "length", length }
                });
        }

        return value!;
    }

    /// <summary>
    ///     Validates that a string does not have the specified length, throwing a <see cref="ValidationException" /> if it
    ///     does.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="length">The length to check against.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not null and has the specified length.</exception>
    public static string ValidateIsNotLength(this string? value, int length, string propertyName,
        Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotLength(length))
        {
            throw new ValidationException("IsNotLength", propertyName,
                $"{propertyName} must not have a length of {length}.", blackboard, new Dictionary<string, object?>
                {
                    { "value", value },
                    { "length", length }
                });
        }

        return value!;
    }
}
