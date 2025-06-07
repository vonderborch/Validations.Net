using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators.Collections;

/// <summary>
/// Provides extension methods for validating that collections do not contain any of the specified items.
/// </summary>
public static class DoesNotContainAnyValidations
{
    /// <summary>
    /// Checks if a collection does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="items">The items to check for presence in the collection.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default equality comparer.</param>
    /// <returns>True if the collection does not contain any of the specified items, otherwise false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or items is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this IEnumerable<T> collection, IEnumerable<T> items, IEqualityComparer<T>? comparer = null)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return !items.Any(item => collection.Contains(item, comparer));
        }
        return !items.Any(collection.Contains);
    }
    
    /// <summary>
    /// Validates that a collection does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items to check for presence in the collection.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default equality comparer.</param>
    /// <param name="blackboard">Additional contextual information to include in the ValidationException.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains any of the specified items.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateDoesNotContainAny<T>(this IEnumerable<T> collection, IEnumerable<T> items, string variableName, IEqualityComparer<T>? comparer = null, object? blackboard = null)
    {
        if (!collection.CheckDoesNotContainAny(items, comparer))
        {
            throw new ValidationException(variableName, $"{variableName} must not contain any of the specified items.", "DoesNotContainAny", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "collection", collection },
                { "items", items }
            });
        }

        return collection;
    }
    
    /// <summary>
    /// Checks if a collection does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="items">The items to check for presence in the collection.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default equality comparer.</param>
    /// <returns>True if the collection does not contain any of the specified items, otherwise false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or items is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this ICollection<T> collection, IEnumerable<T> items, IEqualityComparer<T>? comparer = null)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return !items.Any(item => collection.Contains(item, comparer));
        }
        return !items.Any(collection.Contains);
    }
    
    /// <summary>
    /// Validates that a collection does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items to check for presence in the collection.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default equality comparer.</param>
    /// <param name="blackboard">Additional contextual information to include in the ValidationException.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains any of the specified items.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateDoesNotContainAny<T>(this ICollection<T> collection, IEnumerable<T> items, string variableName, IEqualityComparer<T>? comparer = null, object? blackboard = null)
    {
        if (!collection.CheckDoesNotContainAny(items, comparer))
        {
            throw new ValidationException(variableName, $"{variableName} must not contain any of the specified items.", "DoesNotContainAny", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "collection", collection },
                { "items", items }
            });
        }

        return collection;
    }
    
    /// <summary>
    /// Checks if a collection does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="items">The items to check for presence in the collection.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default equality comparer.</param>
    /// <returns>True if the collection does not contain any of the specified items, otherwise false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or items is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this IEnumerable<T> collection, ICollection<T> items, IEqualityComparer<T>? comparer = null)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return !items.Any(item => collection.Contains(item, comparer));
        }
        return !items.Any(collection.Contains);
    }
    
    /// <summary>
    /// Validates that a collection does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items to check for presence in the collection.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default equality comparer.</param>
    /// <param name="blackboard">Additional contextual information to include in the ValidationException.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains any of the specified items.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateDoesNotContainAny<T>(this IEnumerable<T> collection, ICollection<T> items, string variableName, IEqualityComparer<T>? comparer = null, object? blackboard = null)
    {
        if (!collection.CheckDoesNotContainAny(items, comparer))
        {
            throw new ValidationException(variableName, $"{variableName} must not contain any of the specified items.", "DoesNotContainAny", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "collection", collection },
                { "items", items }
            });
        }

        return collection;
    }
    
    /// <summary>
    /// Checks if a collection does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="items">The items to check for presence in the collection.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default equality comparer.</param>
    /// <returns>True if the collection does not contain any of the specified items, otherwise false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or items is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this ICollection<T> collection, ICollection<T> items, IEqualityComparer<T>? comparer = null)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return !items.Any(item => collection.Contains(item, comparer));
        }
        return !items.Any(collection.Contains);
    }
    
    /// <summary>
    /// Validates that a collection does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items to check for presence in the collection.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default equality comparer.</param>
    /// <param name="blackboard">Additional contextual information to include in the ValidationException.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains any of the specified items.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateDoesNotContainAny<T>(this ICollection<T> collection, ICollection<T> items, string variableName, IEqualityComparer<T>? comparer = null, object? blackboard = null)
    {
        if (!collection.CheckDoesNotContainAny(items, comparer))
        {
            throw new ValidationException(variableName, $"{variableName} must not contain any of the specified items.", "DoesNotContainAny", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "collection", collection },
                { "items", items }
            });
        }

        return collection;
    }
    
    /// <summary>
    /// Checks if a collection does not contain any of the specified items using params array.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default equality comparer.</param>
    /// <param name="items">The items to check for presence in the collection as a params array.</param>
    /// <returns>True if the collection does not contain any of the specified items, otherwise false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or items is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this IEnumerable<T> collection, IEqualityComparer<T>? comparer = null, params T[] items)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return !items.Any(item => collection.Contains(item, comparer));
        }
        return !items.Any(collection.Contains);
    }
    
    /// <summary>
    /// Validates that a collection does not contain any of the specified items using params array.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default equality comparer.</param>
    /// <param name="items">The items to check for presence in the collection as a params array.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains any of the specified items.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateDoesNotContainAny<T>(this IEnumerable<T> collection, string variableName, IEqualityComparer<T>? comparer = null, params T[] items)
    {
        if (!collection.CheckDoesNotContainAny(comparer, items))
        {
            throw new ValidationException(variableName, $"{variableName} must not contain any of the specified items.", "DoesNotContainAny", blackboard: null, exceptionContext: new Dictionary<string, object?>
            {
                { "collection", collection },
                { "items", items }
            });
        }

        return collection;
    }
    
    /// <summary>
    /// Checks if a collection does not contain any of the specified items using params array.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default equality comparer.</param>
    /// <param name="items">The items to check for presence in the collection as a params array.</param>
    /// <returns>True if the collection does not contain any of the specified items, otherwise false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or items is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this ICollection<T> collection, IEqualityComparer<T>? comparer = null, params T[] items)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return !items.Any(item => collection.Contains(item, comparer));
        }
        return !items.Any(collection.Contains);
    }
    
    /// <summary>
    /// Validates that a collection does not contain any of the specified items using params array.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default equality comparer.</param>
    /// <param name="items">The items to check for presence in the collection as a params array.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains any of the specified items.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateDoesNotContainAny<T>(this ICollection<T> collection, string variableName, IEqualityComparer<T>? comparer = null, params T[] items)
    {
        if (!collection.CheckDoesNotContainAny(comparer, items))
        {
            throw new ValidationException(variableName, $"{variableName} must not contain any of the specified items.", "DoesNotContainAny", blackboard: null, exceptionContext: new Dictionary<string, object?>
            {
                { "collection", collection },
                { "items", items }
            });
        }

        return collection;
    }
    
    /// <summary>
    /// Checks if a collection does not contain any items that match a predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="predicate">The predicate function to test each element.</param>
    /// <returns>True if the collection does not contain any items that match the predicate, otherwise false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or predicate is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
    {
        collection.ValidateIsNotNull(nameof(collection));
        predicate.ValidateIsNotNull(nameof(predicate));

        return !collection.Any(predicate);
    }

    /// <summary>
    /// Validates that a collection does not contain any items that match a predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicate">The predicate function to test each element.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Additional contextual information to include in the ValidationException.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains any items that match the predicate.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateDoesNotContainAny<T>(this IEnumerable<T> collection, Func<T, bool> predicate,
        string variableName, object? blackboard = null)
    {
        if (!collection.CheckDoesNotContainAny(predicate))
        {
            throw new ValidationException(variableName, $"{variableName} must not contain any items that match the specified predicate.", "DoesNotContainAll", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "collection", collection },
                { "predicate", predicate }
            });
        }

        return collection;
    }
    
    /// <summary>
    /// Checks if a collection does not contain any items that match a predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="predicate">The predicate function to test each element.</param>
    /// <returns>True if the collection does not contain any items that match the predicate, otherwise false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or predicate is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this ICollection<T> collection, Func<T, bool> predicate)
    {
        collection.ValidateIsNotNull(nameof(collection));
        predicate.ValidateIsNotNull(nameof(predicate));

        return !collection.Any(predicate);
    }
    
    /// <summary>
    /// Validates that a collection does not contain any items that match a predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicate">The predicate function to test each element.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Additional contextual information to include in the ValidationException.</param>
    /// <returns>The original collection if validation passes.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains any items that match the predicate.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateDoesNotContainAny<T>(this ICollection<T> collection, Func<T, bool> predicate,
        string variableName, object? blackboard = null)
    {
        if (!collection.CheckDoesNotContainAny(predicate))
        {
            throw new ValidationException(variableName, $"{variableName} must not contain any items that match the specified predicate.", "DoesNotContainAll", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "collection", collection },
                { "predicate", predicate }
            });
        }

        return collection;
    }
}
