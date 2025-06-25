using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class CollectionDoesContain
{
    /// <summary>
    ///     Checks if the collection contains the specified item.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="item">The item to check for.</param>
    /// <returns>True if the item is contained; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContain<T>(this ICollection<T>? collection, T item)
    {
        if (collection is null)
        {
            return false;
        }

        return collection.Contains(item);
    }

    /// <summary>
    ///     Checks if the collection contains any item matching the specified predicate.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="predicate">The predicate to match items against.</param>
    /// <returns>True if any item matches the predicate; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContain<T>(this ICollection<T>? collection, Func<T, bool> predicate)
    {
        if (collection is null)
        {
            return false;
        }

        return collection.Any(predicate);
    }

    /// <summary>
    ///     Ensures that the collection contains the specified item.
    ///     Throws a ValidationException if it does not.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="item">The item to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated collection.</returns>
    /// <exception cref="ValidationException">Thrown if the item is not contained.</exception>
    public static ICollection<T> EnsureDoesContain<T>(this ICollection<T>? collection, T item, string parameterName,
        IBlackboard? blackboard = null)
    {
        ValidationResult result = collection.ValidateDoesContain(item, parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return collection!;
    }

    /// <summary>
    ///     Ensures that the collection contains any item matching the specified predicate.
    ///     Throws a ValidationException if it does not.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicate">The predicate to match items against.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated collection.</returns>
    /// <exception cref="ValidationException">Thrown if no item matches the predicate.</exception>
    public static ICollection<T> EnsureDoesContain<T>(this ICollection<T>? collection, Func<T, bool> predicate,
        string parameterName, IBlackboard? blackboard = null)
    {
        ValidationResult result = collection.ValidateDoesContain(predicate, parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return collection!;
    }

    /// <summary>
    ///     Validates that the collection contains the specified item.
    ///     Returns a ValidationResult indicating success or failure.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="item">The item to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the validation passed or failed.</returns>
    public static ValidationResult ValidateDoesContain<T>(this ICollection<T>? collection, T item, string parameterName,
        IBlackboard? blackboard = null)
    {
        if (collection.CheckDoesContain(item))
        {
            return new ValidationResult();
        }

        return new ValidationResult(new ValidationException(
            "DoesContain",
            parameterName,
            $"{parameterName} must contain item '{item}'.",
            blackboard,
            new Dictionary<string, object?>
            {
                { "collection", collection },
                { "item", item }
            }));
    }

    /// <summary>
    ///     Validates that the collection contains any item matching the specified predicate.
    ///     Returns a ValidationResult indicating success or failure.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicate">The predicate to match items against.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the validation passed or failed.</returns>
    public static ValidationResult ValidateDoesContain<T>(this ICollection<T>? collection, Func<T, bool> predicate,
        string parameterName, IBlackboard? blackboard = null)
    {
        if (collection.CheckDoesContain(predicate))
        {
            return new ValidationResult();
        }

        return new ValidationResult(new ValidationException(
            "DoesContain",
            parameterName,
            $"{parameterName} collection must contain at least one item matching the predicate.",
            blackboard,
            new Dictionary<string, object?>
            {
                { "collection", collection },
                { "predicate", predicate }
            }));
    }
}
