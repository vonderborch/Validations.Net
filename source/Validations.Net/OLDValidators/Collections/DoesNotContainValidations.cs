using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.OLDValidators.Collections;

/// <summary>
///     Provides extension methods for validating that collections do not contain specific items or items matching specific
///     criteria.
/// </summary>
public static class DoesNotContainValidations
{
    /// <summary>
    ///     Checks if a collection does not contain a specific item.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="item">The item to check for presence in the collection.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default equality comparer.</param>
    /// <returns>True if the collection does not contain the specified item, otherwise false.</returns>
    /// <exception cref="ValidationException">Thrown when collection is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain<T>(this IEnumerable<T> collection, T item,
        IEqualityComparer<T>? comparer = null)
    {
        collection.ValidateIsNotNull(nameof(collection));
        if (comparer != null)
        {
            return !collection.Contains(item, comparer);
        }

        return !collection.Contains(item);
    }

    /// <summary>
    ///     Checks if a collection does not contain a specific item.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="item">The item to check for presence in the collection.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default equality comparer.</param>
    /// <returns>True if the collection does not contain the specified item, otherwise false.</returns>
    /// <exception cref="ValidationException">Thrown when collection is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain<T>(this ICollection<T> collection, T item, IEqualityComparer<T>? comparer)
    {
        collection.ValidateIsNotNull(nameof(collection));
        if (comparer != null)
        {
            return !collection.Contains(item, comparer);
        }

        return !collection.Contains(item);
    }

    /// <summary>
    ///     Checks if a collection does not contain any items that match a predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="predicate">The predicate function to test each element.</param>
    /// <returns>True if the collection does not contain any items that match the predicate, otherwise false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or predicate is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
    {
        collection.ValidateIsNotNull(nameof(collection));
        predicate.ValidateIsNotNull(nameof(predicate));

        return !collection.Any(predicate);
    }

    /// <summary>
    ///     Checks if a collection does not contain any items that match a predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="predicate">The predicate function to test each element.</param>
    /// <returns>True if the collection does not contain any items that match the predicate, otherwise false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or predicate is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain<T>(this ICollection<T> collection, Func<T, bool> predicate)
    {
        collection.ValidateIsNotNull(nameof(collection));
        predicate.ValidateIsNotNull(nameof(predicate));

        return !collection.Any(predicate);
    }

    /// <summary>
    ///     Validates that a collection does not contain a specific item.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="item">The item to check for presence in the collection.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Additional contextual information to include in the ValidationException.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains the specified item.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateDoesNotContain<T>(this IEnumerable<T> collection, T item, string variableName,
        object? blackboard = null)
    {
        if (!collection.CheckDoesNotContain(item))
        {
            throw new ValidationException(variableName, $"{variableName} must not contain {item}.", "DoesNotContain",
                blackboard, new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "item", item }
                });
        }

        return collection;
    }

    /// <summary>
    ///     Validates that a collection does not contain a specific item.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="item">The item to check for presence in the collection.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Additional contextual information to include in the ValidationException.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains the specified item.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateDoesNotContain<T>(this ICollection<T> collection, T item, string variableName,
        object? blackboard = null)
    {
        if (!collection.CheckDoesNotContain(item))
        {
            throw new ValidationException(variableName, $"{variableName} must not contain {item}.", "DoesNotContain",
                blackboard, new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "item", item }
                });
        }

        return collection;
    }

    /// <summary>
    ///     Validates that a collection does not contain any items that match a predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicate">The predicate function to test each element.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Additional contextual information to include in the ValidationException.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains any items that match the predicate.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateDoesNotContain<T>(this IEnumerable<T> collection, Func<T, bool> predicate,
        string variableName, object? blackboard = null)
    {
        if (!collection.CheckDoesNotContain(predicate))
        {
            throw new ValidationException(variableName,
                $"{variableName} must not contain any item that matches the predicate.", "DoesNotContain", blackboard,
                new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "predicate", predicate }
                });
        }

        return collection;
    }

    /// <summary>
    ///     Validates that a collection does not contain any items that match a predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicate">The predicate function to test each element.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Additional contextual information to include in the ValidationException.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains any items that match the predicate.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateDoesNotContain<T>(this ICollection<T> collection, Func<T, bool> predicate,
        string variableName, object? blackboard = null)
    {
        if (!collection.CheckDoesNotContain(predicate))
        {
            throw new ValidationException(variableName,
                $"{variableName} must not contain any item that matches the predicate.", "DoesNotContain", blackboard,
                new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "predicate", predicate }
                });
        }

        return collection;
    }
}
