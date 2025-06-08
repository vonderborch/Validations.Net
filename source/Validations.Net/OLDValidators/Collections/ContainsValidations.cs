using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.OLDValidators.Collections;

/// <summary>
///     Provides extension methods for validating whether collections contain specific items or match specific conditions.
/// </summary>
public static class ContainsValidations
{
    /// <summary>
    ///     Checks if the collection contains the specified item.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="comparer">Optional custom equality comparer for type T.</param>
    /// <returns>True if the collection contains the item; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown when collection is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContains<T>(this IEnumerable<T> collection, T item, IEqualityComparer<T>? comparer = null)
    {
        collection.ValidateIsNotNull(nameof(collection));
        if (comparer != null)
        {
            return collection.Contains(item, comparer);
        }

        return collection.Contains(item);
    }

    /// <summary>
    ///     Checks if the collection contains the specified item using an optional comparer.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="comparer">Custom equality comparer for type T.</param>
    /// <returns>True if the collection contains the item; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown when collection is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContains<T>(this ICollection<T> collection, T item, IEqualityComparer<T>? comparer)
    {
        collection.ValidateIsNotNull(nameof(collection));
        if (comparer != null)
        {
            return collection.Contains(item, comparer);
        }

        return collection.Contains(item);
    }

    /// <summary>
    ///     Checks if the collection contains any element that satisfies the specified predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="predicate">A function that defines the condition to check for.</param>
    /// <returns>True if any element matches the predicate; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown when collection or predicate is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContains<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
    {
        collection.ValidateIsNotNull(nameof(collection));
        predicate.ValidateIsNotNull(nameof(predicate));

        return collection.Any(predicate);
    }

    /// <summary>
    ///     Checks if the collection contains any element that satisfies the specified predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="predicate">A function that defines the condition to check for.</param>
    /// <returns>True if any element matches the predicate; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown when collection or predicate is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContains<T>(this ICollection<T> collection, Func<T, bool> predicate)
    {
        collection.ValidateIsNotNull(nameof(collection));
        predicate.ValidateIsNotNull(nameof(predicate));

        return collection.Any(predicate);
    }

    /// <summary>
    ///     Validates that the collection contains the specified item.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="item">The item that should be present in the collection.</param>
    /// <param name="variableName">The name of the variable for the error message.</param>
    /// <param name="comparer">Optional custom equality comparer for type T.</param>
    /// <param name="blackboard">Optional context object for validation.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ArgumentNullException">Thrown when collection is null.</exception>
    /// <exception cref="ValidationException">Thrown when the collection doesn't contain the specified item.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateContains<T>(this IEnumerable<T> collection, T item, string variableName,
        IEqualityComparer<T>? comparer, object? blackboard = null)
    {
        if (collection == null)
        {
            throw new ArgumentNullException(nameof(collection));
        }

        if (!collection.CheckContains(item, comparer))
        {
            throw new ValidationException(variableName, $"{variableName} must contain {item}.", "Contains", blackboard,
                new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "item", item }
                });
        }

        return collection;
    }

    /// <summary>
    ///     Validates that the collection contains the specified item.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="item">The item that should be present in the collection.</param>
    /// <param name="variableName">The name of the variable for the error message.</param>
    /// <param name="comparer">Optional custom equality comparer for type T.</param>
    /// <param name="blackboard">Optional context object for validation.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ArgumentNullException">Thrown when collection is null.</exception>
    /// <exception cref="ValidationException">Thrown when the collection doesn't contain the specified item.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateContains<T>(this ICollection<T> collection, T item, string variableName,
        IEqualityComparer<T>? comparer, object? blackboard = null)
    {
        if (!collection.CheckContains(item, comparer))
        {
            throw new ValidationException(variableName, $"{variableName} must contain {item}.", "Contains", blackboard,
                new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "item", item }
                });
        }

        return collection;
    }

    /// <summary>
    ///     Validates that the collection contains at least one element matching the specified predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicate">A function that defines the condition to check for.</param>
    /// <param name="variableName">The name of the variable for the error message.</param>
    /// <param name="blackboard">Optional context object for validation.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ArgumentNullException">Thrown when collection or predicate is null.</exception>
    /// <exception cref="ValidationException">Thrown when no element in the collection matches the predicate.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateContains<T>(this IEnumerable<T> collection, Func<T, bool> predicate,
        string variableName, object? blackboard = null)
    {
        if (!collection.CheckContains(predicate))
        {
            throw new ValidationException(variableName,
                $"{variableName} must contain an item that matches the predicate.", "Contains", blackboard,
                new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "predicate", predicate }
                });
        }

        return collection;
    }

    /// <summary>
    ///     Validates that the collection contains at least one element matching the specified predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicate">A function that defines the condition to check for.</param>
    /// <param name="variableName">The name of the variable for the error message.</param>
    /// <param name="blackboard">Optional context object for validation.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ArgumentNullException">Thrown when collection or predicate is null.</exception>
    /// <exception cref="ValidationException">Thrown when no element in the collection matches the predicate.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateContains<T>(this ICollection<T> collection, Func<T, bool> predicate,
        string variableName, object? blackboard = null)
    {
        if (!collection.CheckContains(predicate))
        {
            throw new ValidationException(variableName,
                $"{variableName} must contain an item that matches the predicate.", "Contains", blackboard,
                new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "predicate", predicate }
                });
        }

        return collection;
    }
}
