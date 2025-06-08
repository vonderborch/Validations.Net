using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.OLDValidators.Collections;

/// <summary>
///     Provides extension methods for validating whether collections contain all specified items or match all specified
///     conditions.
/// </summary>
public static class ContainsAllValidations
{
    /// <summary>
    ///     Checks if a collection contains all items from another collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collections.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="items">The items that should be contained in the collection.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <returns>True if the collection contains all the specified items; otherwise, false.</returns>
    /// <exception cref="ValidationException">Thrown when the collection or items parameter is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAll<T>(this IEnumerable<T> collection, IEnumerable<T> items,
        IEqualityComparer<T>? comparer = null)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return items.All(item => collection.Contains(item, comparer));
        }

        return items.All(collection.Contains);
    }

    /// <summary>
    ///     Checks if a collection contains all items from another collection. Optimized version for ICollection input.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collections.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="items">The items that should be contained in the collection.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <returns>True if the collection contains all the specified items; otherwise, false.</returns>
    /// <exception cref="ValidationException">Thrown when the collection or items parameter is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAll<T>(this ICollection<T> collection, IEnumerable<T> items,
        IEqualityComparer<T>? comparer = null)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return items.All(item => collection.Contains(item, comparer));
        }

        return items.All(collection.Contains);
    }

    /// <summary>
    ///     Checks if a collection contains all items from another collection. Optimized version for ICollection items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collections.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="items">The items that should be contained in the collection.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <returns>True if the collection contains all the specified items; otherwise, false.</returns>
    /// <exception cref="ValidationException">Thrown when the collection or items parameter is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAll<T>(this IEnumerable<T> collection, ICollection<T> items,
        IEqualityComparer<T>? comparer = null)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return items.All(item => collection.Contains(item, comparer));
        }

        return items.All(collection.Contains);
    }

    /// <summary>
    ///     Checks if a collection contains all items from another collection. Optimized version for ICollection inputs.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collections.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="items">The items that should be contained in the collection.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <returns>True if the collection contains all the specified items; otherwise, false.</returns>
    /// <exception cref="ValidationException">Thrown when the collection or items parameter is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAll<T>(this ICollection<T> collection, ICollection<T> items,
        IEqualityComparer<T>? comparer = null)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return items.All(item => collection.Contains(item, comparer));
        }

        return items.All(collection.Contains);
    }

    /// <summary>
    ///     Checks if a collection contains all the specified items provided as parameters.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <param name="items">The items that should be contained in the collection.</param>
    /// <returns>True if the collection contains all the specified items; otherwise, false.</returns>
    /// <exception cref="ValidationException">Thrown when the collection or items parameter is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAll<T>(this IEnumerable<T> collection, IEqualityComparer<T>? comparer = null,
        params T[] items)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return items.All(item => collection.Contains(item, comparer));
        }

        return items.All(collection.Contains);
    }

    /// <summary>
    ///     Checks if a collection contains all the specified items provided as parameters. Optimized version for ICollection
    ///     input.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <param name="items">The items that should be contained in the collection.</param>
    /// <returns>True if the collection contains all the specified items; otherwise, false.</returns>
    /// <exception cref="ValidationException">Thrown when the collection or items parameter is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAll<T>(this ICollection<T> collection, IEqualityComparer<T>? comparer = null,
        params T[] items)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return items.All(item => collection.Contains(item, comparer));
        }

        return items.All(collection.Contains);
    }

    /// <summary>
    ///     Checks if all items in a collection satisfy the specified predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="predicate">The predicate that all items must satisfy.</param>
    /// <returns>True if all items in the collection satisfy the predicate; otherwise, false.</returns>
    /// <exception cref="ValidationException">Thrown when the collection or predicate parameter is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAll<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
    {
        collection.ValidateIsNotNull(nameof(collection));
        predicate.ValidateIsNotNull(nameof(predicate));

        return collection.All(predicate);
    }

    /// <summary>
    ///     Checks if all items in a collection satisfy the specified predicate. Optimized version for ICollection input.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="predicate">The predicate that all items must satisfy.</param>
    /// <returns>True if all items in the collection satisfy the predicate; otherwise, false.</returns>
    /// <exception cref="ValidationException">Thrown when the collection or predicate parameter is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAll<T>(this ICollection<T> collection, Func<T, bool> predicate)
    {
        collection.ValidateIsNotNull(nameof(collection));
        predicate.ValidateIsNotNull(nameof(predicate));

        return collection.All(predicate);
    }

    /// <summary>
    ///     Validates that a collection contains all items from another collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collections.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items that should be contained in the collection.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <param name="blackboard">Optional context object that will be passed to the ValidationException if thrown.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain all the specified items.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateContainsAll<T>(this IEnumerable<T> collection, IEnumerable<T> items,
        string variableName, IEqualityComparer<T>? comparer = null, object? blackboard = null)
    {
        if (!collection.CheckContainsAll(items, comparer))
        {
            throw new ValidationException(variableName,
                $"{variableName} must contain all items in the specified collection.", "ContainsAll", blackboard,
                new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "items", items }
                });
        }

        return collection;
    }

    /// <summary>
    ///     Validates that a collection contains all items from another collection. Optimized version for ICollection input.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collections.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items that should be contained in the collection.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <param name="blackboard">Optional context object that will be passed to the ValidationException if thrown.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain all the specified items.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateContainsAll<T>(this ICollection<T> collection, IEnumerable<T> items,
        string variableName, IEqualityComparer<T>? comparer = null, object? blackboard = null)
    {
        if (!collection.CheckContainsAll(items, comparer))
        {
            throw new ValidationException(variableName,
                $"{variableName} must contain all items in the specified collection.", "ContainsAll", blackboard,
                new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "items", items }
                });
        }

        return collection;
    }

    /// <summary>
    ///     Validates that a collection contains all items from another collection. Optimized version for ICollection items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collections.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items that should be contained in the collection.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <param name="blackboard">Optional context object that will be passed to the ValidationException if thrown.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain all the specified items.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateContainsAll<T>(this IEnumerable<T> collection, ICollection<T> items,
        string variableName, IEqualityComparer<T>? comparer = null, object? blackboard = null)
    {
        if (!collection.CheckContainsAll(items, comparer))
        {
            throw new ValidationException(variableName,
                $"{variableName} must contain all items in the specified collection.", "ContainsAll", blackboard,
                new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "items", items }
                });
        }

        return collection;
    }

    /// <summary>
    ///     Validates that a collection contains all items from another collection. Optimized version for ICollection inputs.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collections.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items that should be contained in the collection.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <param name="blackboard">Optional context object that will be passed to the ValidationException if thrown.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain all the specified items.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateContainsAll<T>(this ICollection<T> collection, ICollection<T> items,
        string variableName, IEqualityComparer<T>? comparer = null, object? blackboard = null)
    {
        if (!collection.CheckContainsAll(items, comparer))
        {
            throw new ValidationException(variableName,
                $"{variableName} must contain all items in the specified collection.", "ContainsAll", blackboard,
                new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "items", items }
                });
        }

        return collection;
    }

    /// <summary>
    ///     Validates that a collection contains all the specified items provided as parameters.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <param name="blackboard">Optional context object that will be passed to the ValidationException if thrown.</param>
    /// <param name="items">The items that should be contained in the collection.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain all the specified items.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateContainsAll<T>(this IEnumerable<T> collection, string variableName,
        IEqualityComparer<T>? comparer = null, object? blackboard = null, params T[] items)
    {
        if (!collection.CheckContainsAll(comparer, items))
        {
            throw new ValidationException(variableName, $"{variableName} must contain all specified items.",
                "ContainsAll", blackboard, new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "items", items }
                });
        }

        return collection;
    }

    /// <summary>
    ///     Validates that a collection contains all the specified items provided as parameters. Optimized version for
    ///     ICollection input.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <param name="blackboard">Optional context object that will be passed to the ValidationException if thrown.</param>
    /// <param name="items">The items that should be contained in the collection.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain all the specified items.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateContainsAll<T>(this ICollection<T> collection, string variableName,
        IEqualityComparer<T>? comparer = null, object? blackboard = null, params T[] items)
    {
        if (!collection.CheckContainsAll(comparer, items))
        {
            throw new ValidationException(variableName, $"{variableName} must contain all specified items.",
                "ContainsAll", blackboard, new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "items", items }
                });
        }

        return collection;
    }

    /// <summary>
    ///     Validates that all items in a collection satisfy the specified predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicate">The predicate that all items must satisfy.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Optional context object that will be passed to the ValidationException if thrown.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when not all items in the collection satisfy the predicate.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateContainsAll<T>(this IEnumerable<T> collection, Func<T, bool> predicate,
        string variableName, object? blackboard = null)
    {
        if (!collection.CheckContainsAll(predicate))
        {
            throw new ValidationException(variableName,
                $"{variableName} must contain all items that match the specified predicate.", "ContainsAll", blackboard,
                new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "predicate", predicate }
                });
        }

        return collection;
    }

    /// <summary>
    ///     Validates that all items in a collection satisfy the specified predicate. Optimized version for ICollection input.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicate">The predicate that all items must satisfy.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Optional context object that will be passed to the ValidationException if thrown.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when not all items in the collection satisfy the predicate.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateContainsAll<T>(this ICollection<T> collection, Func<T, bool> predicate,
        string variableName, object? blackboard = null)
    {
        if (!collection.CheckContainsAll(predicate))
        {
            throw new ValidationException(variableName,
                $"{variableName} must contain all items that match the specified predicate.", "ContainsAll", blackboard,
                new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "predicate", predicate }
                });
        }

        return collection;
    }
}
