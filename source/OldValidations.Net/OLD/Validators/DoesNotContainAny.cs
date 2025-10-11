using System.Collections;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net.OLD.Validators.Helpers;

namespace Validations.Net.OLD.Validators;

/// <summary>
///     Provides extension methods for checking and validating that a string or collection does not contain any specified
///     items, substrings, or characters.
/// </summary>
public static class DoesNotContainAny
{
    /// <summary>
    ///     Checks if the string does not contain any of the specified substrings.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="subStrings">The substrings to check for.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <returns>True if no substring is contained; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny(this string? value, ICollection<string> subStrings,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        return !subStrings.Any(x => value.Contains(x, comparison));
    }

    /// <summary>
    ///     Checks if the string does not contain any of the specified characters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="characters">The characters to check for.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <returns>True if no character is contained; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny(this string? value, ICollection<char> characters,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        return !characters.Any(x => value.Contains(x, comparison));
    }

    /// <summary>
    /// Checks if the collection does not contain any of the specified items.
    /// </summary>
    /// <typeparam name="T">The type of the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="items">The collection of items to check for.</param>
    /// <returns>True if none of the specified items are contained in the collection; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this T? collection, T items) where T : ICollection
    {
        if (collection is null)
        {
            return false;
        }

        foreach (var item in items)
        {
            if (collection.Contains(item))
            {
                return false;
            }
        }

        return true;
    }
    /// <summary>
    /// Checks whether the specified value does <b>not</b> contain any of the provided items or matches any of the provided predicates.
    /// </summary>
    /// <param name="collection">
    /// The collection to check for absence of specified items (for generic overloads).
    /// </param>
    /// <param name="predicates">
    /// A collection of predicates; returns <c>true</c> if none of the predicates match any element in the checked collection.
    /// </param>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <returns>
    /// <c>true</c> if none of the specified predicate are satisfied by the items contained in paramref name="collection"/>;
    /// otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this ICollection<T>? collection, ICollection<Func<T, bool>> predicates)
    {
        if (collection is null)
        {
            return false;
        }

        foreach (var predicate in predicates)
        {
            if (collection.Any(predicate))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Ensures that the string does not contain any of the specified substrings, throwing a
    ///     <see cref="ValidationException" /> if it does.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="subStrings">The substrings to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated string.</returns>
    /// <exception cref="ValidationException">Thrown if any of the substrings are contained.</exception>
    public static string EnsureDoesNotContainAny(this string? value, ICollection<string> subStrings,
        string parameterName,
        StringComparison comparison = StringComparison.Ordinal, IBlackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateDoesNotContainAny(subStrings, parameterName, comparison, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }

    /// <summary>
    ///     Ensures that the string does not contain any of the specified characters, throwing a
    ///     <see cref="ValidationException" /> if it does.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="characters">The characters to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated string.</returns>
    /// <exception cref="ValidationException">Thrown if any of the characters are contained.</exception>
    public static string EnsureDoesNotContainAny(this string? value, ICollection<char> characters,
        string parameterName,
        StringComparison comparison = StringComparison.Ordinal, IBlackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateDoesNotContainAny(characters, parameterName, comparison, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }

    /// <summary>
    ///     Ensures that the collection does not contain any of the specified items, throwing a
    ///     <see cref="ValidationException" /> if it does.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated collection.</returns>
    /// <exception cref="ValidationException">Thrown if any of the items are contained.</exception>
    public static T EnsureDoesNotContainAny<T>(this T? collection, T items,
        string parameterName,
        IBlackboard? blackboard = null) where T : ICollection
    {
        ValidationResult result = collection.ValidateDoesNotContainAny(items, parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return collection!;
    }

    /// <summary>
    ///     Ensures that the collection does not contain any items matching the specified predicates, throwing a
    ///     <see cref="ValidationException" /> if it does.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicates">The predicates to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated collection.</returns>
    /// <exception cref="ValidationException">Thrown if any of the predicates match any item.</exception>
    public static ICollection<T> EnsureDoesNotContainAny<T>(this ICollection<T>? collection,
        ICollection<Func<T, bool>> predicates, string parameterName,
        IBlackboard? blackboard = null)
    {
        ValidationResult result = collection.ValidateDoesNotContainAny(predicates, parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return collection!;
    }

    /// <summary>
    ///     Validates that the string does not contain any of the specified substrings.
    ///     If the string contains any of the substrings, returns a <see cref="ValidationResult" /> containing validation
    ///     failure details.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="subStrings">The substrings to check for.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateDoesNotContainAny(this string? value, ICollection<string> subStrings,
        string variableName,
        StringComparison comparison = StringComparison.Ordinal, IBlackboard? blackboard = null)
    {
        if (!value.CheckDoesNotContainAny(subStrings, comparison))
        {
            ValidationResult result = new(
                new ValidationException("DoesNotContainAny", variableName,
                    $"{variableName} must not contain any of the substrings.",
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
    ///     Validates that the string does not contain any of the specified characters.
    ///     If the string contains any of the characters, returns a <see cref="ValidationResult" /> containing validation
    ///     failure details.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="characters">The characters to check for.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateDoesNotContainAny(this string? value, ICollection<char> characters,
        string variableName,
        StringComparison comparison = StringComparison.Ordinal, IBlackboard? blackboard = null)
    {
        if (!value.CheckDoesNotContainAny(characters, comparison))
        {
            ValidationResult result = new(
                new ValidationException("DoesNotContainAny", variableName,
                    $"{variableName} must not contain any of the characters.",
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
    ///     Validates that the collection does not contain any of the specified items.
    ///     If the collection contains any of the items, returns a <see cref="ValidationResult" /> containing validation
    ///     failure details.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="items">The items to check for.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateDoesNotContainAny<T>(this T? collection, T items,
        string variableName,
        IBlackboard? blackboard = null) where T : ICollection
    {
        if (!collection.CheckDoesNotContainAny(items))
        {
            ValidationResult result = new(
                new ValidationException("DoesNotContainAny", variableName,
                    $"{variableName} must not contain any of the items.",
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
    ///     Validates that the collection does not contain any items matching the specified predicates.
    ///     If the collection contains any items matching the predicates, returns a <see cref="ValidationResult" /> containing
    ///     validation failure details.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicates">The predicates to check for.</param>
    /// <param name="variableName">The name of the variable being validated, used for error reporting.</param>
    /// <param name="blackboard">An optional blackboard object for storing contextual validation details.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the success or failure of the validation.</returns>
    public static ValidationResult ValidateDoesNotContainAny<T>(this ICollection<T>? collection,
        ICollection<Func<T, bool>> predicates, string variableName,
        IBlackboard? blackboard = null)
    {
        if (!collection.CheckDoesNotContainAny(predicates))
        {
            ValidationResult result = new(
                new ValidationException("DoesNotContainAny", variableName,
                    $"{variableName} must not contain any items matching any of the predicates.",
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
