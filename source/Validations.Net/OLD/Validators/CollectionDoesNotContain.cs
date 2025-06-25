using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

public static class CollectionDoesNotContain
{

    /// <summary>
    ///     Checks if a collection does not contain a specified item.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="item">The item to search for.</param>
    /// <returns>True if the collection does not contain the item; otherwise, false. Returns false if the collection is null.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain<T>(this ICollection<T>? collection, T item)
    {
        if (collection is null)
        {
            return false;
        }

        return !collection.Contains(item);
    }

    /// <summary>
    ///     Checks if a collection does not contain any elements matching a predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="predicate">The predicate to match elements against.</param>
    /// <returns>
    ///     True if the collection does not contain any elements matching the predicate; otherwise, false. Returns false
    ///     if the collection is null.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain<T>(this ICollection<T>? collection, Func<T, bool> predicate)
    {
        if (collection is null)
        {
            return false;
        }

        return !collection.Any(predicate);
    }

    /// <summary>
    ///     Ensures that a collection does not contain a specified item, throwing a <see cref="ValidationException" /> if it
    ///     does.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The original collection if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains the specified item.</exception>
    public static ICollection<T> EnsureDoesNotContain<T>(this ICollection<T>? collection, T item, string parameterName,
        IBlackboard? blackboard = null)
    {
        ValidationResult result = collection.ValidateDoesNotContain(item, parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return collection!;
    }

    /// <summary>
    ///     Ensures that a collection does not contain any elements matching a predicate, throwing a
    ///     <see cref="ValidationException" /> if it does.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicate">The predicate to match elements against.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The original collection if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains any elements matching the predicate.</exception>
    public static ICollection<T> EnsureDoesNotContain<T>(this ICollection<T>? collection, Func<T, bool> predicate,
        string parameterName, IBlackboard? blackboard = null)
    {
        ValidationResult result = collection.ValidateDoesNotContain(predicate, parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return collection!;
    }

    /// <summary>
    ///     Validates that a collection does not contain a specified item.
    ///     Returns a ValidationResult indicating success or failure.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the validation passed or failed.</returns>
    public static ValidationResult ValidateDoesNotContain<T>(this ICollection<T>? collection, T item,
        string parameterName,
        IBlackboard? blackboard = null)
    {
        if (collection.CheckDoesNotContain(item))
        {
            return new ValidationResult();
        }

        return new ValidationResult(new ValidationException(
            "DoesNotContain",
            parameterName,
            $"{parameterName} must not contain item '{item}'.",
            blackboard,
            new Dictionary<string, object?>
            {
                { "collection", collection },
                { "item", item }
            }));
    }

    /// <summary>
    ///     Validates that a collection does not contain any elements matching a predicate.
    ///     Returns a ValidationResult indicating success or failure.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicate">The predicate to match elements against.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the validation passed or failed.</returns>
    public static ValidationResult ValidateDoesNotContain<T>(this ICollection<T>? collection, Func<T, bool> predicate,
        string parameterName, IBlackboard? blackboard = null)
    {
        if (collection.CheckDoesNotContain(predicate))
        {
            return new ValidationResult();
        }

        return new ValidationResult(new ValidationException(
            "DoesNotContain",
            parameterName,
            $"{parameterName} collection must not contain any items matching the predicate.",
            blackboard,
            new Dictionary<string, object?>
            {
                { "collection", collection },
                { "predicate", predicate }
            }));
    }
}
