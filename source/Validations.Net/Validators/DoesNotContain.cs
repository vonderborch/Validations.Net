using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides extension methods for validating that strings and collections do not contain specified values or elements.
/// </summary>
public static class DoesNotContain
{
    /// <summary>
    /// Checks if a string does not contain a specified substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="subString">The substring to search for.</param>
    /// <param name="comparison">The string comparison option to use.</param>
    /// <returns>True if the string does not contain the substring; otherwise, false. Returns false if the string is null.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain(this string? value, string subString, StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }
        return !value.Contains(subString, comparison);
    }

    /// <summary>
    /// Validates that a string does not contain a specified substring, throwing a <see cref="ValidationException"/> if it does.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="subString">The substring to search for.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="comparison">The string comparison option to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains the specified substring.</exception>
    public static string ValidateDoesNotContain(this string? value, string subString, string propertyName,
        StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        if (!value.CheckDoesNotContain(subString, comparison))
        {
            throw new ValidationException("DoesNotContain", propertyName, $"{propertyName} must not contain {subString}.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "subString", subString }
            });
        }
        return value!;
    }

    /// <summary>
    /// Checks if a substring of a string does not contain a specified substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="subString">The substring to search for.</param>
    /// <param name="startIndex">The starting index of the substring to check.</param>
    /// <param name="count">The number of characters in the substring to check. If null, checks to the end of the string.</param>
    /// <param name="comparison">The string comparison option to use.</param>
    /// <returns>True if the substring does not contain the specified substring; otherwise, false. Returns false if the string is null.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain(this string? value, string subString, int startIndex, int? count = null,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        string stringPart = value.Substring(startIndex, count ?? value.Length - startIndex);
        return !stringPart.Contains(subString, comparison);
    }

    /// <summary>
    /// Validates that a substring of a string does not contain a specified substring, throwing a <see cref="ValidationException"/> if it does.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="subString">The substring to search for.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="startIndex">The starting index of the substring to check.</param>
    /// <param name="count">The number of characters in the substring to check. If null, checks to the end of the string.</param>
    /// <param name="comparison">The string comparison option to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the substring contains the specified substring.</exception>
    public static string ValidateDoesNotContain(this string? value, string subString, string propertyName, int startIndex,
        int? count = null, StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        count ??= value?.Length - startIndex;
        if (!value.CheckDoesNotContain(subString, startIndex, count, comparison))
        {
            throw new ValidationException(
                "DoesNotContain",
                propertyName,
                $"{propertyName} must not contain \"{subString}\" (in substring from {startIndex} for {count} characters).",
                blackboard,
                new Dictionary<string, object?>
                {
                    { "value", value },
                    { "subString", subString },
                    { "startIndex", startIndex },
                    { "count", count }
                });
        }

        return value!;
    }

    /// <summary>
    /// Checks if a string does not contain a specified character.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="character">The character to search for.</param>
    /// <param name="comparison">The string comparison option to use.</param>
    /// <returns>True if the string does not contain the character; otherwise, false. Returns false if the string is null.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain(this string? value, char character, StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }
        return !value.Contains(character, comparison);
    }

    /// <summary>
    /// Validates that a string does not contain a specified character, throwing a <see cref="ValidationException"/> if it does.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="character">The character to search for.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="comparison">The string comparison option to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the string contains the specified character.</exception>
    public static string ValidateDoesNotContain(this string? value, char character, string propertyName,
        StringComparison comparison = StringComparison.Ordinal,
        Blackboard? blackboard = null)
    {
        if (!value.CheckDoesNotContain(character, comparison))
        {
            throw new ValidationException("DoesNotContain", propertyName, $"{propertyName} must not contain '{character}'.",
                blackboard, new Dictionary<string, object?>
                {
                    { "value", value },
                    { "character", character }
                });
        }

        return value!;
    }

    /// <summary>
    /// Checks if a substring of a string does not contain a specified character.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="character">The character to search for.</param>
    /// <param name="startIndex">The starting index of the substring to check.</param>
    /// <param name="count">The number of characters in the substring to check. If null, checks to the end of the string.</param>
    /// <param name="comparison">The string comparison option to use.</param>
    /// <returns>True if the substring does not contain the specified character; otherwise, false. Returns false if the string is null.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain(this string? value, char character, int startIndex, int? count = null,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        string subString = value.Substring(startIndex, count ?? value.Length - startIndex);
        return !subString.Contains(character, comparison);
    }

    /// <summary>
    /// Validates that a substring of a string does not contain a specified character, throwing a <see cref="ValidationException"/> if it does.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="character">The character to search for.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="startIndex">The starting index of the substring to check.</param>
    /// <param name="count">The number of characters in the substring to check. If null, checks to the end of the string.</param>
    /// <param name="comparison">The string comparison option to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the substring contains the specified character.</exception>
    public static string ValidateDoesNotContain(this string? value, char character, string propertyName, int startIndex, int? count = null,
        StringComparison comparison = StringComparison.Ordinal,
        Blackboard? blackboard = null)
    {
        count ??= value?.Length - startIndex;
        if (!value.CheckDoesNotContain(character, startIndex, count, comparison))
        {
            throw new ValidationException(
                "DoesNotContain",
                propertyName,
                $"{propertyName} must not contain '{character}' (in substring from {startIndex} for {count} characters).",
                blackboard,
                new Dictionary<string, object?>
                {
                    { "value", value },
                    { "character", character },
                    { "startIndex", startIndex },
                    { "count", count }
                });
        }

        return value!;
    }
    
    /// <summary>
    /// Checks if a collection does not contain a specified item.
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
    /// Validates that a collection does not contain a specified item, throwing a <see cref="ValidationException"/> if it does.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="item">The item to search for.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original collection if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains the specified item.</exception>
    public static ICollection<T> ValidateDoesNotContain<T>(this ICollection<T>? collection, T item, string propertyName,
        Blackboard? blackboard = null)
    {
        if (!collection.CheckDoesNotContain(item))
        {
            throw new ValidationException(
                "DoesNotContain",
                propertyName,
                $"{propertyName} must not contain item '{item}'.",
                blackboard,
                new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "item", item }
                });
        }

        return collection!;
    }

    /// <summary>
    /// Checks if a collection does not contain any elements matching a predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="predicate">The predicate to match elements against.</param>
    /// <returns>True if the collection does not contain any elements matching the predicate; otherwise, false. Returns false if the collection is null.</returns>
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
    /// Validates that a collection does not contain any elements matching a predicate, throwing a <see cref="ValidationException"/> if it does.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to validate.</param>
    /// <param name="predicate">The predicate to match elements against.</param>
    /// <param name="propertyName">The name of the property being validated, used in the error message.</param>
    /// <param name="blackboard">Optional blackboard for additional context in the validation exception.</param>
    /// <returns>The original collection if validation succeeds.</returns>
    /// <exception cref="ValidationException">Thrown when the collection contains any elements matching the predicate.</exception>
    public static ICollection<T> ValidateDoesNotContain<T>(this ICollection<T>? collection, Func<T, bool> predicate,
        string propertyName, Blackboard? blackboard = null)
    {
        if (!collection.CheckDoesNotContain(predicate))
        {
            throw new ValidationException(
                "DoesNotContain",
                propertyName,
                $"{propertyName} collection must not contain any items matching the predicate.",
                blackboard,
                new Dictionary<string, object?>
                {
                    { "collection", collection },
                    { "predicate", predicate }
                });
        }

        return collection!;
    }
}
