using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for checking and validating that a string or collection does not contain all specified
///     items, substrings, or characters.
/// </summary>
public static class DoesNotContainAll
{
    /// <summary>
    ///     Checks if the string does not contain all of the specified substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="subStrings">The substrings to check for.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <returns>True if not all substrings are contained; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this string? value, ICollection<string> subStrings,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        return !subStrings.All(x => value.Contains(x, comparison));
    }

    /// <summary>
    ///     Checks if the string does not contain all of the specified characters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="characters">The characters to check for.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <returns>True if not all characters are contained; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this string? value, ICollection<char> characters,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        return !characters.All(x => value.Contains(x, comparison));
    }

    /// <summary>
    ///     Checks if the collection does not contain all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="items">The items to check for.</param>
    /// <returns>True if not all items are contained; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this ICollection<T>? collection, ICollection<T> items)
    {
        if (collection is null)
        {
            return false;
        }

        return !items.All(collection.Contains);
    }

    /// <summary>
    ///     Checks if the collection does not contain all items matching the specified predicates.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="predicates">The predicates to check for.</param>
    /// <returns>True if not all predicates match all items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this ICollection<T>? collection, ICollection<Func<T, bool>> predicates)
    {
        if (collection is null)
        {
            return false;
        }

        return !predicates.All(predicate => collection.All(predicate));
    }

    /// <summary>
    ///     Validates that the string does not contain all of the specified substrings.
    ///     Throws a ValidationException if it does.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="subStrings">The substrings to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated string.</returns>
    /// <exception cref="ValidationException">Thrown if all substrings are contained.</exception>
    public static string ValidateDoesNotContainAll(this string? value, ICollection<string> subStrings,
        string parameterName,
        StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        if (!value.CheckDoesNotContainAll(subStrings, comparison))
        {
            throw new ValidationException("DoesNotContainAll", parameterName,
                $"{parameterName} must not contain any of the substrings.", blackboard, new Dictionary<string, object?>
                {
                    { "value", value },
                    { "subStrings", subStrings }
                });
        }

        return value!;
    }

    /// <summary>
    ///     Validates that the string does not contain all of the specified characters.
    ///     Throws a ValidationException if it does.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="characters">The characters to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated string.</returns>
    /// <exception cref="ValidationException">Thrown if all characters are contained.</exception>
    public static string ValidateDoesNotContainAll(this string? value, ICollection<char> characters,
        string parameterName,
        StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        if (!value.CheckDoesNotContainAll(characters, comparison))
        {
            throw new ValidationException("DoesNotContainAll", parameterName,
                $"{parameterName} must not contain any of the characters.", blackboard, new Dictionary<string, object?>
                {
                    { "value", value },
                    { "characters", characters }
                });
        }

        return value!;
    }

    /// <summary>
    ///     Validates that the collection does not contain all of the specified items.
    ///     Throws a ValidationException if it does.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated collection.</returns>
    /// <exception cref="ValidationException">Thrown if all items are contained.</exception>
    public static ICollection<T> ValidateDoesNotContainAll<T>(this ICollection<T>? collection, ICollection<T> items,
        string parameterName,
        Blackboard? blackboard = null)
    {
        if (!collection.CheckDoesNotContainAll(items))
        {
            throw new ValidationException("DoesNotContainAll", parameterName,
                $"{parameterName} must not contain any of the items.", blackboard, new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "items", items }
                });
        }

        return collection!;
    }

    /// <summary>
    ///     Validates that the collection does not contain all items matching the specified predicates.
    ///     Throws a ValidationException if it does.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicates">The predicates to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated collection.</returns>
    /// <exception cref="ValidationException">Thrown if all predicates match all items.</exception>
    public static ICollection<T> ValidateDoesNotContainAll<T>(this ICollection<T>? collection,
        ICollection<Func<T, bool>> predicates, string parameterName,
        Blackboard? blackboard = null)
    {
        if (!collection.CheckDoesNotContainAll(predicates))
        {
            throw new ValidationException("DoesNotContainAll", parameterName,
                $"No item in {parameterName} may match any of the predicates.", blackboard,
                new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "predicates", predicates }
                });
        }

        return collection!;
    }
}
