using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.OLDValidators.Collections;

/// <summary>
///     Provides extension methods for validating that collections do not contain all specific items from another
///     collection.
/// </summary>
/// <remarks>
///     This class contains methods to check if a collection does not contain all items from a specified collection or
///     array,
///     or if it does not contain all items that match a specified predicate. Methods are available for both IEnumerable
///     &lt;T&gt; and ICollection&lt;T&gt;.
/// </remarks>
public static class DoesNotContainAllValidations
{
    /// <summary>
    ///     Checks if a collection does not contain all items from another collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collections.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="items">The items to check for.</param>
    /// <param name="comparer">The equality comparer to use for comparing elements, or null to use the default comparer.</param>
    /// <returns>True if the collection does not contain all the specified items, otherwise false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or items is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this IEnumerable<T> collection, IEnumerable<T> items,
        IEqualityComparer<T>? comparer = null)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return !items.All(item => collection.Contains(item, comparer));
        }

        return !items.All(collection.Contains);
    }

    /// <summary>
    ///     Checks if a collection does not contain all items from another collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collections.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="items">The items to check for.</param>
    /// <param name="comparer">The equality comparer to use for comparing elements, or null to use the default comparer.</param>
    /// <returns>True if the collection does not contain all the specified items, otherwise false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or items is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this ICollection<T> collection, IEnumerable<T> items,
        IEqualityComparer<T>? comparer = null)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return !items.All(item => collection.Contains(item, comparer));
        }

        return !items.All(collection.Contains);
    }

    /// <summary>
    ///     Checks if a collection does not contain all items from another collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collections.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="items">The items to check for.</param>
    /// <param name="comparer">The equality comparer to use for comparing elements, or null to use the default comparer.</param>
    /// <returns>True if the collection does not contain all the specified items, otherwise false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or items is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this IEnumerable<T> collection, ICollection<T> items,
        IEqualityComparer<T>? comparer = null)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return !items.All(item => collection.Contains(item, comparer));
        }

        return !items.All(collection.Contains);
    }

    /// <summary>
    ///     Checks if a collection does not contain all items from another collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collections.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="items">The items to check for.</param>
    /// <param name="comparer">The equality comparer to use for comparing elements, or null to use the default comparer.</param>
    /// <returns>True if the collection does not contain all the specified items, otherwise false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or items is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this ICollection<T> collection, ICollection<T> items,
        IEqualityComparer<T>? comparer = null)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return !items.All(item => collection.Contains(item, comparer));
        }

        return !items.All(collection.Contains);
    }

    /// <summary>
    ///     Checks if a collection does not contain all the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="comparer">The equality comparer to use for comparing elements, or null to use the default comparer.</param>
    /// <param name="items">The items to check for.</param>
    /// <returns>True if the collection does not contain all the specified items, otherwise false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or items is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this IEnumerable<T> collection, IEqualityComparer<T>? comparer = null,
        params T[] items)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return !items.All(item => collection.Contains(item, comparer));
        }

        return !items.All(collection.Contains);
    }

    /// <summary>
    ///     Checks if a collection does not contain all the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="comparer">The equality comparer to use for comparing elements, or null to use the default comparer.</param>
    /// <param name="items">The items to check for.</param>
    /// <returns>True if the collection does not contain all the specified items, otherwise false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or items is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this ICollection<T> collection, IEqualityComparer<T>? comparer = null,
        params T[] items)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return !items.All(item => collection.Contains(item, comparer));
        }

        return !items.All(collection.Contains);
    }

    /// <summary>
    ///     Checks if a collection does not contain all items that match a predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="predicate">The predicate function to test each element.</param>
    /// <returns>True if the collection does not contain all items that match the predicate, otherwise false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or predicate is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
    {
        collection.ValidateIsNotNull(nameof(collection));
        predicate.ValidateIsNotNull(nameof(predicate));

        return !collection.All(predicate);
    }

    /// <summary>
    ///     Checks if a collection does not contain all items that match a predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="predicate">The predicate function to test each element.</param>
    /// <returns>True if the collection does not contain all items that match the predicate, otherwise false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or predicate is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this ICollection<T> collection, Func<T, bool> predicate)
    {
        collection.ValidateIsNotNull(nameof(collection));
        predicate.ValidateIsNotNull(nameof(predicate));

        return !collection.All(predicate);
    }

    /// <summary>
    ///     Validates that a collection does not contain all items from another collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collections.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items to check for.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing elements, or null to use the default comparer.</param>
    /// <param name="blackboard">Additional contextual information to include in the ValidationException.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains all specified items.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateDoesNotContainAll<T>(this IEnumerable<T> collection, IEnumerable<T> items,
        string variableName, IEqualityComparer<T>? comparer = null, object? blackboard = null)
    {
        if (!collection.CheckDoesNotContainAll(items, comparer))
        {
            throw new ValidationException(variableName,
                $"{variableName} must not contain all items in the specified collection.", "DoesNotContainAll",
                blackboard, new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "items", items }
                });
        }

        return collection;
    }

    /// <summary>
    ///     Validates that a collection does not contain all items from another collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collections.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items to check for.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing elements, or null to use the default comparer.</param>
    /// <param name="blackboard">Additional contextual information to include in the ValidationException.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains all specified items.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateDoesNotContainAll<T>(this ICollection<T> collection, IEnumerable<T> items,
        string variableName, IEqualityComparer<T>? comparer = null, object? blackboard = null)
    {
        if (!collection.CheckDoesNotContainAll(items, comparer))
        {
            throw new ValidationException(variableName,
                $"{variableName} must not contain all items in the specified collection.", "DoesNotContainAll",
                blackboard, new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "items", items }
                });
        }

        return collection;
    }

    /// <summary>
    ///     Validates that a collection does not contain all items from another collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collections.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items to check for.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing elements, or null to use the default comparer.</param>
    /// <param name="blackboard">Additional contextual information to include in the ValidationException.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains all specified items.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateDoesNotContainAll<T>(this IEnumerable<T> collection, ICollection<T> items,
        string variableName, IEqualityComparer<T>? comparer = null, object? blackboard = null)
    {
        if (!collection.CheckDoesNotContainAll(items, comparer))
        {
            throw new ValidationException(variableName,
                $"{variableName} must not contain all items in the specified collection.", "DoesNotContainAll",
                blackboard, new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "items", items }
                });
        }

        return collection;
    }

    /// <summary>
    ///     Validates that a collection does not contain all items from another collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collections.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items to check for.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing elements, or null to use the default comparer.</param>
    /// <param name="blackboard">Additional contextual information to include in the ValidationException.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains all specified items.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateDoesNotContainAll<T>(this ICollection<T> collection, ICollection<T> items,
        string variableName, IEqualityComparer<T>? comparer = null, object? blackboard = null)
    {
        if (!collection.CheckDoesNotContainAll(items, comparer))
        {
            throw new ValidationException(variableName,
                $"{variableName} must not contain all items in the specified collection.", "DoesNotContainAll",
                blackboard, new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "items", items }
                });
        }

        return collection;
    }

    /// <summary>
    ///     Validates that a collection does not contain all the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing elements, or null to use the default comparer.</param>
    /// <param name="blackboard">Additional contextual information to include in the ValidationException.</param>
    /// <param name="items">The items to check for.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains all specified items.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateDoesNotContainAll<T>(this IEnumerable<T> collection, string variableName,
        IEqualityComparer<T>? comparer = null, object? blackboard = null, params T[] items)
    {
        if (!collection.CheckDoesNotContainAll(comparer, items))
        {
            throw new ValidationException(variableName, $"{variableName} must not contain all specified items.",
                "DoesNotContainAll", blackboard, new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "items", items }
                });
        }

        return collection;
    }

    /// <summary>
    ///     Validates that a collection does not contain all the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing elements, or null to use the default comparer.</param>
    /// <param name="blackboard">Additional contextual information to include in the ValidationException.</param>
    /// <param name="items">The items to check for.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains all specified items.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateDoesNotContainAll<T>(this ICollection<T> collection, string variableName,
        IEqualityComparer<T>? comparer = null, object? blackboard = null, params T[] items)
    {
        if (!collection.CheckDoesNotContainAll(comparer, items))
        {
            throw new ValidationException(variableName, $"{variableName} must not contain all specified items.",
                "DoesNotContainAll", blackboard, new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "items", items }
                });
        }

        return collection;
    }

    /// <summary>
    ///     Validates that a collection does not contain all items that match a predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicate">The predicate function to test each element.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Additional contextual information to include in the ValidationException.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains all items that match the predicate.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateDoesNotContainAll<T>(this IEnumerable<T> collection, Func<T, bool> predicate,
        string variableName, object? blackboard = null)
    {
        if (!collection.CheckDoesNotContainAll(predicate))
        {
            throw new ValidationException(variableName,
                $"{variableName} must not contain any items that match the specified predicate.", "DoesNotContainAll",
                blackboard, new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "predicate", predicate }
                });
        }

        return collection;
    }

    /// <summary>
    ///     Validates that a collection does not contain all items that match a predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicate">The predicate function to test each element.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Additional contextual information to include in the ValidationException.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains all items that match the predicate.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateDoesNotContainAll<T>(this ICollection<T> collection, Func<T, bool> predicate,
        string variableName, object? blackboard = null)
    {
        if (!collection.CheckDoesNotContainAll(predicate))
        {
            throw new ValidationException(variableName,
                $"{variableName} must not contain any items that match the specified predicate.", "DoesNotContainAll",
                blackboard, new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "predicate", predicate }
                });
        }

        return collection;
    }
}
