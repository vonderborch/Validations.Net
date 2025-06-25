using System.Collections;

namespace Validations.Net.Validators.Helpers;

/// <summary>
/// Provides extension methods for <see cref="ICollection"/> to facilitate common operations.
/// </summary>
public static class CollectionExtension
{
    /// <summary>
    /// Determines whether the specified collection contains a specific value.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to search.</param>
    /// <param name="value">The value to locate in the collection.</param>
    /// <returns>True if the value is found in the collection; otherwise, false.</returns>
    public static bool Contains<T>(this ICollection collection, T value)
    {
        if (collection is ICollection<T> typedCollection)
        {
            return typedCollection.Contains(value);
        }

        foreach (var item in collection)
        {
            if (item is T itemValue && EqualityComparer<T>.Default.Equals(itemValue, value))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Determines whether any element in the specified collection satisfies a condition.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to search.</param>
    /// <param name="predicate">A function that defines the condition to check against each element.</param>
    /// <returns>True if any element satisfies the condition; otherwise, false.</returns>
    public static bool Contains<T>(this ICollection collection, Func<T, bool> predicate)
    {
        if (collection is ICollection<T> typedCollection)
        {
            return typedCollection.Any(predicate);
        }

        foreach (var item in collection)
        {
            if (item is T itemValue && predicate(itemValue))
            {
                return true;
            }
        }

        return false;
    }
}
