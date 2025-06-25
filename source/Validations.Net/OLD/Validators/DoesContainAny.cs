using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for checking and validating that a string or collection contains any specified items,
///     substrings, or characters.
/// </summary>
public static class DoesContainAny
{
    /// <summary>
    ///     Checks if the string contains any of the specified substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="subStrings">The substrings to check for.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <returns>True if any substring is contained; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny(this string? value, ICollection<string> subStrings,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        return subStrings.Any(x => value.Contains(x, comparison));
    }

    /// <summary>
    ///     Checks if the string contains any of the specified characters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="characters">The characters to check for.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <returns>True if any character is contained; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny(this string? value, ICollection<char> characters,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        return characters.Any(x => value.Contains(x, comparison));
    }

    /// <summary>
    ///     Checks if the collection contains any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="items">The items to check for.</param>
    /// <returns>True if any item is contained; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny<T>(this ICollection<T>? collection, ICollection<T> items)
    {
        if (collection is null)
        {
            return false;
        }

        return items.Any(collection.Contains);
    }

    /// <summary>
    ///     Checks if the collection contains any items matching the specified predicates.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="predicates">The predicates to check for.</param>
    /// <returns>True if any predicate matches any item; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny<T>(this ICollection<T>? collection, ICollection<Func<T, bool>> predicates)
    {
        if (collection is null)
        {
            return false;
        }

        return predicates.Any(collection.Any);
    }

    /// <summary>
    ///     Ensures that the string contains any of the specified substrings, throwing a <see cref="ValidationException" /> if
    ///     it does not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="subStrings">The substrings to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated string.</returns>
    /// <exception cref="ValidationException">Thrown if none of the substrings are contained.</exception>
    public static string EnsureDoesContainAny(this string? value, ICollection<string> subStrings,
        string parameterName,
        StringComparison comparison = StringComparison.Ordinal, IBlackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateDoesContainAny(subStrings, parameterName, comparison, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }

    /// <summary>
    ///     Ensures that the string contains any of the specified characters, throwing a <see cref="ValidationException" /> if
    ///     it does not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="characters">The characters to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated string.</returns>
    /// <exception cref="ValidationException">Thrown if none of the characters are contained.</exception>
    public static string EnsureDoesContainAny(this string? value, ICollection<char> characters, string parameterName,
        StringComparison comparison = StringComparison.Ordinal, IBlackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateDoesContainAny(characters, parameterName, comparison, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }

    /// <summary>
    ///     Ensures that the collection contains any of the specified items, throwing a <see cref="ValidationException" /> if
    ///     it does not.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated collection.</returns>
    /// <exception cref="ValidationException">Thrown if none of the items are contained.</exception>
    public static ICollection<T> EnsureDoesContainAny<T>(this ICollection<T>? collection, ICollection<T> items,
        string parameterName,
        IBlackboard? blackboard = null)
    {
        ValidationResult result = collection.ValidateDoesContainAny(items, parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return collection!;
    }

    /// <summary>
    ///     Ensures that the collection contains any items matching the specified predicates, throwing a
    ///     <see cref="ValidationException" /> if it does not.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicates">The predicates to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated collection.</returns>
    /// <exception cref="ValidationException">Thrown if none of the predicates match any item.</exception>
    public static ICollection<T> EnsureDoesContainAny<T>(this ICollection<T>? collection,
        ICollection<Func<T, bool>> predicates, string parameterName,
        IBlackboard? blackboard = null)
    {
        ValidationResult result = collection.ValidateDoesContainAny(predicates, parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return collection!;
    }

    /// <summary>
    ///     Validates that the string contains any of the specified substrings.
    ///     If the string does not contain any of the substrings, returns a <see cref="ValidationResult" /> containing
    ///     validation failure details.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="subStrings">The substrings to check for.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateDoesContainAny(this string? value, ICollection<string> subStrings,
        string variableName,
        StringComparison comparison = StringComparison.Ordinal, IBlackboard? blackboard = null)
    {
        if (!value.CheckDoesContainAny(subStrings, comparison))
        {
            ValidationResult result = new(
                new ValidationException("DoesContainAny", variableName,
                    $"{variableName} must contain any of the substrings.",
                    blackboard, new Dictionary<string, object?>
                    {
                        { "value", value },
                        { "subStrings", subStrings }
                    }));
            return result;
        }

        return new ValidationResult();
    }

    /// <summary>
    ///     Validates that the string contains any of the specified characters.
    ///     If the string does not contain any of the characters, returns a <see cref="ValidationResult" /> containing
    ///     validation failure details.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="characters">The characters to check for.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateDoesContainAny(this string? value, ICollection<char> characters,
        string variableName,
        StringComparison comparison = StringComparison.Ordinal, IBlackboard? blackboard = null)
    {
        if (!value.CheckDoesContainAny(characters, comparison))
        {
            ValidationResult result = new(
                new ValidationException("DoesContainAny", variableName,
                    $"{variableName} must contain any of the characters.",
                    blackboard, new Dictionary<string, object?>
                    {
                        { "value", value },
                        { "characters", characters }
                    }));
            return result;
        }

        return new ValidationResult();
    }

    /// <summary>
    ///     Validates that the collection contains any of the specified items.
    ///     If the collection does not contain any of the items, returns a <see cref="ValidationResult" /> containing
    ///     validation failure details.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items to check for.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateDoesContainAny<T>(this ICollection<T>? collection, ICollection<T> items,
        string variableName,
        IBlackboard? blackboard = null)
    {
        if (!collection.CheckDoesContainAny(items))
        {
            ValidationResult result = new(
                new ValidationException("DoesContainAny", variableName,
                    $"{variableName} must contain any of the items.",
                    blackboard, new Dictionary<string, object?>
                    {
                        { "collection", collection },
                        { "items", items }
                    }));
            return result;
        }

        return new ValidationResult();
    }

    /// <summary>
    ///     Validates that the collection contains any items matching the specified predicates.
    ///     If the collection does not contain any items matching the predicates, returns a <see cref="ValidationResult" />
    ///     containing validation failure details.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicates">The predicates to check for.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateDoesContainAny<T>(this ICollection<T>? collection,
        ICollection<Func<T, bool>> predicates, string variableName,
        IBlackboard? blackboard = null)
    {
        if (!collection.CheckDoesContainAny(predicates))
        {
            ValidationResult result = new(
                new ValidationException("DoesContainAny", variableName,
                    $"{variableName} must contain at least one item matching any of the predicates.",
                    blackboard, new Dictionary<string, object?>
                    {
                        { "collection", collection },
                        { "predicates", predicates }
                    }));
            return result;
        }

        return new ValidationResult();
    }
}
