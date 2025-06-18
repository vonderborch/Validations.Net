using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides extension methods for validating that values are not null or empty.
/// </summary>
public static class IsNotNullOrEmpty
{
    /// <summary>
    /// Checks if a collection is not null and not empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <returns>True if the collection is not null and not empty; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrEmpty<T>(this ICollection<T>? value)
    {
        return value is not null && value.Count > 0;
    }

    /// <summary>
    /// Checks if a string is not null and not empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not null and not empty; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrEmpty(this string? value)
    {
        return !string.IsNullOrEmpty(value);
    }
    
    /// <summary>
    /// Validates that a collection is not null and not empty, throwing a <see cref="ValidationException"/> if it is.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original collection if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the collection is null or empty.</exception>
    public static ICollection<T> ValidateIsNotNullOrEmpty<T>(this ICollection<T>? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotNullOrEmpty())
        {
            throw new ValidationException("IsNotNullOrEmpty", propertyName, $"{propertyName} must not be null or empty.", blackboard, new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
    
    /// <summary>
    /// Validates that a string is not null and not empty, throwing a <see cref="ValidationException"/> if it is.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string is null or empty.</exception>
    public static string ValidateIsNotNullOrEmpty(this string? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotNullOrEmpty())
        {
            throw new ValidationException("IsNotNullOrEmpty", propertyName, $"{propertyName} must not be null or empty.", blackboard, new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
}
