using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for checking and validating that a string or collection contains all specified items,
///     substrings, or characters.
/// </summary>
public static class DoesContainAll
{
    /// <summary>
    ///     Checks if the string contains all of the specified substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="subStrings">The substrings to check for.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <returns>True if all substrings are contained; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll(this string? value, ICollection<string> subStrings,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        return subStrings.All(x => value.Contains(x, comparison));
    }

    /// <summary>
    ///     Checks if the string contains all of the specified characters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="characters">The characters to check for.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <returns>True if all characters are contained; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll(this string? value, ICollection<char> characters,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        return characters.All(x => value.Contains(x, comparison));
    }

    /// <summary>
    ///     Checks if the collection contains all of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="items">The items to check for.</param>
    /// <returns>True if all items are contained; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll<T>(this ICollection<T>? collection, ICollection<T> items)
    {
        if (collection is null)
        {
            return false;
        }

        return items.All(collection.Contains);
    }

    /// <summary>
    ///     Checks if the collection contains all items matching the specified predicates.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="predicates">The predicates to check for.</param>
    /// <returns>True if all predicates match all items; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll<T>(this ICollection<T>? collection, ICollection<Func<T, bool>> predicates)
    {
        if (collection is null)
        {
            return false;
        }

        return predicates.All(collection.All);
    }

    /// <summary>
    ///     Validates that the string contains all of the specified substrings.
    ///     Returns a ValidationResult indicating success or failure.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="subStrings">The substrings to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the validation passed or failed.</returns>
    public static ValidationResult ValidateDoesContainAll(this string? value, ICollection<string> subStrings,
        string parameterName,
        StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        if (value.CheckDoesContainAll(subStrings, comparison))
        {
            return new ValidationResult();
        }

        return new ValidationResult(new ValidationException("DoesContainAll", parameterName,
            $"{parameterName} must contain all of the substrings.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "subStrings", subStrings }
            }));
    }

    /// <summary>
    ///     Validates that the string contains all of the specified characters.
    ///     Returns a ValidationResult indicating success or failure.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="characters">The characters to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the validation passed or failed.</returns>
    public static ValidationResult ValidateDoesContainAll(this string? value, ICollection<char> characters, string parameterName,
        StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        if (value.CheckDoesContainAll(characters, comparison))
        {
            return new ValidationResult();
        }

        return new ValidationResult(new ValidationException("DoesContainAll", parameterName,
            $"{parameterName} must contain all of the characters.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "characters", characters }
            }));
    }

    /// <summary>
    ///     Validates that the collection contains all of the specified items.
    ///     Returns a ValidationResult indicating success or failure.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the validation passed or failed.</returns>
    public static ValidationResult ValidateDoesContainAll<T>(this ICollection<T>? collection, ICollection<T> items,
        string parameterName,
        Blackboard? blackboard = null)
    {
        if (collection.CheckDoesContainAll(items))
        {
            return new ValidationResult();
        }

        return new ValidationResult(new ValidationException("DoesContainAll", parameterName,
            $"{parameterName} must contain all of the items.", blackboard, new Dictionary<string, object?>
            {
                { "collection", collection },
                { "items", items }
            }));
    }

    /// <summary>
    ///     Validates that the collection contains all items matching the specified predicates.
    ///     Returns a ValidationResult indicating success or failure.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicates">The predicates to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the validation passed or failed.</returns>
    public static ValidationResult ValidateDoesContainAll<T>(this ICollection<T>? collection,
        ICollection<Func<T, bool>> predicates, string parameterName,
        Blackboard? blackboard = null)
    {
        if (collection.CheckDoesContainAll(predicates))
        {
            return new ValidationResult();
        }

        return new ValidationResult(new ValidationException("DoesContainAll", parameterName,
            $"All items in {parameterName} must match all of the predicates.", blackboard,
            new Dictionary<string, object?>
            {
                { "collection", collection },
                { "predicates", predicates }
            }));
    }

    /// <summary>
    ///     Ensures that the string contains all of the specified substrings.
    ///     Throws a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="subStrings">The substrings to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated string.</returns>
    /// <exception cref="ValidationException">Thrown if not all substrings are contained.</exception>
    public static string EnsureDoesContainAll(this string? value, ICollection<string> subStrings,
        string parameterName,
        StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        var result = value.ValidateDoesContainAll(subStrings, parameterName, comparison, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }

    /// <summary>
    ///     Ensures that the string contains all of the specified characters.
    ///     Throws a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="characters">The characters to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated string.</returns>
    /// <exception cref="ValidationException">Thrown if not all characters are contained.</exception>
    public static string EnsureDoesContainAll(this string? value, ICollection<char> characters, string parameterName,
        StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        var result = value.ValidateDoesContainAll(characters, parameterName, comparison, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }

    /// <summary>
    ///     Ensures that the collection contains all of the specified items.
    ///     Throws a ValidationException if it does not.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated collection.</returns>
    /// <exception cref="ValidationException">Thrown if not all items are contained.</exception>
    public static ICollection<T> EnsureDoesContainAll<T>(this ICollection<T>? collection, ICollection<T> items,
        string parameterName,
        Blackboard? blackboard = null)
    {
        var result = collection.ValidateDoesContainAll(items, parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return collection!;
    }

    /// <summary>
    ///     Ensures that the collection contains all items matching the specified predicates.
    ///     Throws a ValidationException if it does not.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicates">The predicates to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated collection.</returns>
    /// <exception cref="ValidationException">Thrown if not all predicates match all items.</exception>
    public static ICollection<T> EnsureDoesContainAll<T>(this ICollection<T>? collection,
        ICollection<Func<T, bool>> predicates, string parameterName,
        Blackboard? blackboard = null)
    {
        var result = collection.ValidateDoesContainAll(predicates, parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return collection!;
    }
}
