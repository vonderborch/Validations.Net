using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for checking and validating that a string or collection does not contain any specified
///     items, substrings, or characters.
/// </summary>
public static class DoesNotContainAny
{
    /// <summary>
    ///     Checks if the string does not contain any of the specified substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="subStrings">The substrings to check for.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <returns>True if none of the substrings are contained; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny(this string? value, ICollection<string> subStrings,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        return !subStrings.Any(x => value.Contains(x, comparison));
    }

    /// <summary>
    ///     Checks if the string does not contain any of the specified characters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="characters">The characters to check for.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <returns>True if none of the characters are contained; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny(this string? value, ICollection<char> characters,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        return !characters.Any(x => value.Contains(x, comparison));
    }

    /// <summary>
    ///     Checks if the collection does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="items">The items to check for.</param>
    /// <returns>True if none of the items are contained; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this ICollection<T>? collection, ICollection<T> items)
    {
        if (collection is null)
        {
            return false;
        }

        return !items.Any(collection.Contains);
    }

    /// <summary>
    ///     Checks if the collection does not contain any items matching the specified predicates.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="predicates">The predicates to check for.</param>
    /// <returns>True if none of the predicates match any items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this ICollection<T>? collection, ICollection<Func<T, bool>> predicates)
    {
        if (collection is null)
        {
            return false;
        }

        return !predicates.Any(collection.Any);
    }

    /// <summary>
    ///     Validates that the string does not contain any of the specified substrings.
    ///     Throws a ValidationException if it does.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="subStrings">The substrings to check for.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated string.</returns>
    /// <exception cref="ValidationException">Thrown if any substring is contained.</exception>
    public static string ValidateDoesNotContainAny(this string? value, ICollection<string> subStrings,
        string propertyName,
        StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        if (!value.CheckDoesNotContainAny(subStrings, comparison))
        {
            throw new ValidationException("DoesNotContainAny", propertyName,
                $"{propertyName} must not contain any of the substrings.", blackboard, new Dictionary<string, object?>
                {
                    { "value", value },
                    { "subStrings", subStrings }
                });
        }

        return value!;
    }

    /// <summary>
    ///     Validates that the string does not contain any of the specified characters.
    ///     Throws a ValidationException if it does.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="characters">The characters to check for.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated string.</returns>
    /// <exception cref="ValidationException">Thrown if any character is contained.</exception>
    public static string ValidateDoesNotContainAny(this string? value, ICollection<char> characters,
        string propertyName,
        StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        if (!value.CheckDoesNotContainAny(characters, comparison))
        {
            throw new ValidationException("DoesNotContainAny", propertyName,
                $"{propertyName} must not contain any of the characters.", blackboard, new Dictionary<string, object?>
                {
                    { "value", value },
                    { "characters", characters }
                });
        }

        return value!;
    }

    /// <summary>
    ///     Validates that the collection does not contain any of the specified items.
    ///     Throws a ValidationException if it does.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items to check for.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated collection.</returns>
    /// <exception cref="ValidationException">Thrown if any item is contained.</exception>
    public static ICollection<T> ValidateDoesNotContainAny<T>(this ICollection<T>? collection, ICollection<T> items,
        string propertyName,
        Blackboard? blackboard = null)
    {
        if (!collection.CheckDoesNotContainAny(items))
        {
            throw new ValidationException("DoesNotContainAny", propertyName,
                $"{propertyName} must not contain any of the items.", blackboard, new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "items", items }
                });
        }

        return collection!;
    }

    /// <summary>
    ///     Validates that the collection does not contain any items matching the specified predicates.
    ///     Throws a ValidationException if it does.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicates">The predicates to check for.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated collection.</returns>
    /// <exception cref="ValidationException">Thrown if any predicate matches any item.</exception>
    public static ICollection<T> ValidateDoesNotContainAny<T>(this ICollection<T>? collection,
        ICollection<Func<T, bool>> predicates, string propertyName,
        Blackboard? blackboard = null)
    {
        if (!collection.CheckDoesNotContainAny(predicates))
        {
            throw new ValidationException("DoesNotContainAny", propertyName,
                $"{propertyName} must not contain any items matching any of the predicates.", blackboard,
                new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "predicates", predicates }
                });
        }

        return collection!;
    }
}
