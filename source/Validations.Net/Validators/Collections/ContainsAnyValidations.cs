using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators.Collections;

/// <summary>
/// Provides extension methods for validating if a collection contains any of the specified items.
/// </summary>
public static class ContainsAnyValidations
{
    /// <summary>
    /// Checks if the collection contains any of the items from the specified collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="items">The items to look for in the collection.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <returns>True if the collection contains any of the specified items; otherwise, false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or items is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAny<T>(this IEnumerable<T> collection, IEnumerable<T> items, IEqualityComparer<T>? comparer = null)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return items.Any(item => collection.Contains(item, comparer));
        }
        return items.Any(item => collection.Contains(item));
    }
    
    /// <summary>
    /// Validates that the collection contains at least one of the specified items, throwing an exception if it does not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items to look for in the collection.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <param name="blackboard">Additional data to include in the exception.</param>
    /// <returns>The original collection to enable method chaining.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain any of the specified items.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateContainsAny<T>(this IEnumerable<T> collection, IEnumerable<T> items, string variableName, IEqualityComparer<T>? comparer = null, object? blackboard = null)
    {
        if (!collection.CheckContainsAny(items, comparer))
        {
            throw new ValidationException(variableName, $"{variableName} must contain at least one of the specified items.", "ContainsAny", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "collection", collection },
                { "items", items }
            });
        }

        return collection;
    }
    
    /// <summary>
    /// Checks if the collection contains any of the items from the specified collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="items">The items to look for in the collection.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <returns>True if the collection contains any of the specified items; otherwise, false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or items is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAny<T>(this ICollection<T> collection, IEnumerable<T> items, IEqualityComparer<T>? comparer = null)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return items.Any(item => collection.Contains(item, comparer));
        }
        return items.Any(item => collection.Contains(item));
    }
    
    /// <summary>
    /// Validates that the collection contains at least one of the specified items, throwing an exception if it does not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items to look for in the collection.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <param name="blackboard">Additional data to include in the exception.</param>
    /// <returns>The original collection to enable method chaining.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain any of the specified items.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateContainsAny<T>(this ICollection<T> collection, IEnumerable<T> items, string variableName, IEqualityComparer<T>? comparer = null, object? blackboard = null)
    {
        if (!collection.CheckContainsAny(items, comparer))
        {
            throw new ValidationException(variableName, $"{variableName} must contain at least one of the specified items.", "ContainsAny", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "collection", collection },
                { "items", items }
            });
        }

        return collection;
    }
    
    /// <summary>
    /// Checks if the collection contains any of the items from the specified collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="items">The items to look for in the collection.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <returns>True if the collection contains any of the specified items; otherwise, false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or items is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAny<T>(this IEnumerable<T> collection, ICollection<T> items, IEqualityComparer<T>? comparer = null)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return items.Any(item => collection.Contains(item, comparer));
        }
        return items.Any(item => collection.Contains(item));
    }
    
    /// <summary>
    /// Validates that the collection contains at least one of the specified items, throwing an exception if it does not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items to look for in the collection.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <param name="blackboard">Additional data to include in the exception.</param>
    /// <returns>The original collection to enable method chaining.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain any of the specified items.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateContainsAny<T>(this IEnumerable<T> collection, ICollection<T> items, string variableName, IEqualityComparer<T>? comparer = null, object? blackboard = null)
    {
        if (!collection.CheckContainsAny(items, comparer))
        {
            throw new ValidationException(variableName, $"{variableName} must contain at least one of the specified items.", "ContainsAny", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "collection", collection },
                { "items", items }
            });
        }

        return collection;
    }
    
    /// <summary>
    /// Checks if the collection contains any of the items from the specified collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="items">The items to look for in the collection.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <returns>True if the collection contains any of the specified items; otherwise, false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or items is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAny<T>(this ICollection<T> collection, ICollection<T> items, IEqualityComparer<T>? comparer = null)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return items.Any(item => collection.Contains(item, comparer));
        }
        return items.Any(item => collection.Contains(item));
    }
    
    /// <summary>
    /// Validates that the collection contains at least one of the specified items, throwing an exception if it does not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items to look for in the collection.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <param name="blackboard">Additional data to include in the exception.</param>
    /// <returns>The original collection to enable method chaining.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain any of the specified items.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateContainsAny<T>(this ICollection<T> collection, ICollection<T> items, string variableName, IEqualityComparer<T>? comparer = null, object? blackboard = null)
    {
        if (!collection.CheckContainsAny(items, comparer))
        {
            throw new ValidationException(variableName, $"{variableName} must contain at least one of the specified items.", "ContainsAny", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "collection", collection },
                { "items", items }
            });
        }

        return collection;
    }
    
    /// <summary>
    /// Checks if the collection contains any of the specified items provided as parameters.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <param name="items">The items to look for in the collection.</param>
    /// <returns>True if the collection contains any of the specified items; otherwise, false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or items is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAny<T>(this IEnumerable<T> collection, IEqualityComparer<T>? comparer = null, params T[] items)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return items.Any(item => collection.Contains(item, comparer));
        }
        return items.Any(item => collection.Contains(item));
    }
    
    /// <summary>
    /// Validates that the collection contains at least one of the specified items provided as parameters, throwing an exception if it does not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <param name="items">The items to look for in the collection.</param>
    /// <returns>The original collection to enable method chaining.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain any of the specified items.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateContainsAny<T>(this IEnumerable<T> collection, string variableName, IEqualityComparer<T>? comparer = null, params T[] items)
    {
        if (!collection.CheckContainsAny(comparer, items))
        {
            throw new ValidationException(variableName, $"{variableName} must contain at least one of the specified items.", "ContainsAny", blackboard: null, exceptionContext: new Dictionary<string, object?>
            {
                { "collection", collection },
                { "items", items }
            });
        }

        return collection;
    }
    
    /// <summary>
    /// Checks if the collection contains any of the specified items provided as parameters.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <param name="items">The items to look for in the collection.</param>
    /// <returns>True if the collection contains any of the specified items; otherwise, false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or items is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAny<T>(this ICollection<T> collection, IEqualityComparer<T>? comparer = null, params T[] items)
    {
        collection.ValidateIsNotNull(nameof(collection));
        items.ValidateIsNotNull(nameof(items));

        if (comparer != null)
        {
            return items.Any(item => collection.Contains(item, comparer));
        }
        return items.Any(item => collection.Contains(item));
    }
    
    /// <summary>
    /// Validates that the collection contains at least one of the specified items provided as parameters, throwing an exception if it does not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="comparer">The equality comparer to use for comparing items, or null to use the default comparer.</param>
    /// <param name="items">The items to look for in the collection.</param>
    /// <returns>The original collection to enable method chaining.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain any of the specified items.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateContainsAny<T>(this ICollection<T> collection, string variableName, IEqualityComparer<T>? comparer = null, params T[] items)
    {
        if (!collection.CheckContainsAny(comparer, items))
        {
            throw new ValidationException(variableName, $"{variableName} must contain at least one of the specified items.", "ContainsAny", blackboard: null, exceptionContext: new Dictionary<string, object?>
            {
                { "collection", collection },
                { "items", items }
            });
        }

        return collection;
    }
    
    /// <summary>
    /// Checks if the collection contains any item that satisfies the specified predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns>True if the collection contains any item that satisfies the predicate; otherwise, false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or predicate is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAny<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
    {
        collection.ValidateIsNotNull(nameof(collection));
        predicate.ValidateIsNotNull(nameof(predicate));

        return collection.Any(predicate);
    }
    
    /// <summary>
    /// Validates that the collection contains at least one item that satisfies the specified predicate, throwing an exception if it does not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Additional data to include in the exception.</param>
    /// <returns>The original collection to enable method chaining.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain any item that satisfies the predicate.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateContainsAny<T>(this IEnumerable<T> collection, Func<T, bool> predicate, string variableName, object? blackboard = null)
    {
        if (!collection.CheckContainsAny(predicate))
        {
            throw new ValidationException(variableName, $"{variableName} must contain at least one item that matches the predicate.", "ContainsAny", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "collection", collection },
                { "predicate", predicate }
            });
        }

        return collection;
    }
    
    /// <summary>
    /// Checks if the collection contains any item that satisfies the specified predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns>True if the collection contains any item that satisfies the predicate; otherwise, false.</returns>
    /// <exception cref="ValidationException">Thrown when either collection or predicate is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckContainsAny<T>(this ICollection<T> collection, Func<T, bool> predicate)
    {
        collection.ValidateIsNotNull(nameof(collection));
        predicate.ValidateIsNotNull(nameof(predicate));

        return collection.Any(predicate);
    }
    
    /// <summary>
    /// Validates that the collection contains at least one item that satisfies the specified predicate, throwing an exception if it does not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="variableName">The name of the variable being validated, used in the exception message.</param>
    /// <param name="blackboard">Additional data to include in the exception.</param>
    /// <returns>The original collection to enable method chaining.</returns>
    /// <exception cref="ValidationException">Thrown when the collection does not contain any item that satisfies the predicate.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateContainsAny<T>(this ICollection<T> collection, Func<T, bool> predicate, string variableName, object? blackboard = null)
    {
        if (!collection.CheckContainsAny(predicate))
        {
            throw new ValidationException(variableName, $"{variableName} must contain at least one item that matches the predicate.", "ContainsAny", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "collection", collection },
                { "predicate", predicate }
            });
        }

        return collection;
    }
}
