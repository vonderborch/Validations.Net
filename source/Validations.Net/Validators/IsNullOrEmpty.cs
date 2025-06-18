using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides extension methods for validating that values are null or empty.
/// </summary>
public static class IsNullOrEmpty
{
    /// <summary>
    /// Checks if a collection is null or empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <returns>True if the collection is null or empty; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrEmpty<T>(this ICollection<T>? value)
    {
        return value is null || value.Count == 0;
    }
    
    /// <summary>
    /// Checks if a string is null or empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is null or empty; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrEmpty(this string? value)
    {
        return value is null || value.Length == 0;
    }
    
    /// <summary>
    /// Validates that a collection is null or empty, throwing a <see cref="ValidationException"/> if it isn't.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original collection if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the collection is not null or empty.</exception>
    public static ICollection<T> ValidateIsNullOrEmpty<T>(this ICollection<T>? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNullOrEmpty())
        {
            throw new ValidationException("IsNullOrEmpty", propertyName, $"{propertyName} must be null or empty.", blackboard, new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
    
    /// <summary>
    /// Validates that a string is null or empty, throwing a <see cref="ValidationException"/> if it isn't.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not null or empty.</exception>
    public static string ValidateIsNullOrEmpty(this string? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNullOrEmpty())
        {
            throw new ValidationException("IsNullOrEmpty", propertyName, $"{propertyName} must be null or empty.", blackboard, new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
}
