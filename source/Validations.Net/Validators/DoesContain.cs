using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for checking and validating that a string or collection contains specified items,
///     substrings, or characters.
/// </summary>
public static class DoesContain
{
    /// <summary>
    ///     Checks if the string contains the specified substring.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="subString">The substring to check for.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <returns>True if the substring is contained; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContain(this string? value, string subString,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        return value.Contains(subString, comparison);
    }

    /// <summary>
    ///     Checks if the string contains the specified substring within a substring range.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="subString">The substring to check for.</param>
    /// <param name="startIndex">The starting index of the substring range.</param>
    /// <param name="count">The number of characters to include in the substring range.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <returns>True if the substring is contained in the range; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContain(this string? value, string subString, int startIndex, int? count = null,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        var stringPart = value.Substring(startIndex, count ?? value.Length - startIndex);
        return stringPart.Contains(subString, comparison);
    }

    /// <summary>
    ///     Checks if the string contains the specified character.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="character">The character to check for.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <returns>True if the character is contained; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContain(this string? value, char character,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        return value.Contains(character, comparison);
    }

    /// <summary>
    ///     Checks if the string contains the specified character within a substring range.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="character">The character to check for.</param>
    /// <param name="startIndex">The starting index of the substring range.</param>
    /// <param name="count">The number of characters to include in the substring range.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <returns>True if the character is contained in the range; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContain(this string? value, char character, int startIndex, int? count = null,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        var subString = value.Substring(startIndex, count ?? value.Length - startIndex);
        return subString.Contains(character, comparison);
    }

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
    ///     Ensures that the string contains the specified substring.
    ///     Throws a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="subString">The substring to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated string.</returns>
    /// <exception cref="ValidationException">Thrown if the substring is not contained.</exception>
    public static string EnsureDoesContain(this string? value, string subString, string parameterName,
        StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateDoesContain(subString, parameterName, comparison, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }

    /// <summary>
    ///     Ensures that the string contains the specified substring within a substring range.
    ///     Throws a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="subString">The substring to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="startIndex">The starting index of the substring range.</param>
    /// <param name="count">The number of characters to include in the substring range.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated string.</returns>
    /// <exception cref="ValidationException">Thrown if the substring is not contained in the range.</exception>
    public static string EnsureDoesContain(this string? value, string subString, string parameterName, int startIndex,
        int? count = null, StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        ValidationResult result =
            value.ValidateDoesContain(subString, parameterName, startIndex, count, comparison, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }

    /// <summary>
    ///     Ensures that the string contains the specified character.
    ///     Throws a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="character">The character to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated string.</returns>
    /// <exception cref="ValidationException">Thrown if the character is not contained.</exception>
    public static string EnsureDoesContain(this string? value, char character, string parameterName,
        StringComparison comparison = StringComparison.Ordinal,
        Blackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateDoesContain(character, parameterName, comparison, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
    }

    /// <summary>
    ///     Ensures that the string contains the specified character within a substring range.
    ///     Throws a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="character">The character to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="startIndex">The starting index of the substring range.</param>
    /// <param name="count">The number of characters to include in the substring range.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>The validated string.</returns>
    /// <exception cref="ValidationException">Thrown if the character is not contained in the range.</exception>
    public static string EnsureDoesContain(this string? value, char character, string parameterName, int startIndex,
        int? count = null,
        StringComparison comparison = StringComparison.Ordinal,
        Blackboard? blackboard = null)
    {
        ValidationResult result =
            value.ValidateDoesContain(character, parameterName, startIndex, count, comparison, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value!;
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
        Blackboard? blackboard = null)
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
        string parameterName, Blackboard? blackboard = null)
    {
        ValidationResult result = collection.ValidateDoesContain(predicate, parameterName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return collection!;
    }

    /// <summary>
    ///     Validates that the string contains the specified substring.
    ///     Returns a ValidationResult indicating success or failure.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="subString">The substring to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the validation passed or failed.</returns>
    public static ValidationResult ValidateDoesContain(this string? value, string subString, string parameterName,
        StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        if (value.CheckDoesContain(subString, comparison))
        {
            return new ValidationResult();
        }

        return new ValidationResult(new ValidationException("DoesContain", parameterName,
            $"{parameterName} must contain {subString}.",
            blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "subString", subString }
            }));
    }

    /// <summary>
    ///     Validates that the string contains the specified substring within a substring range.
    ///     Returns a ValidationResult indicating success or failure.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="subString">The substring to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="startIndex">The starting index of the substring range.</param>
    /// <param name="count">The number of characters to include in the substring range.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the validation passed or failed.</returns>
    public static ValidationResult ValidateDoesContain(this string? value, string subString, string parameterName,
        int startIndex,
        int? count = null, StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        count ??= value?.Length - startIndex;
        if (value.CheckDoesContain(subString, startIndex, count, comparison))
        {
            return new ValidationResult();
        }

        return new ValidationResult(new ValidationException(
            "DoesContain",
            parameterName,
            $"{parameterName} must contain \"{subString}\" (in substring from {startIndex} for {count} characters).",
            blackboard,
            new Dictionary<string, object?>
            {
                { "value", value },
                { "subString", subString },
                { "startIndex", startIndex },
                { "count", count }
            }));
    }

    /// <summary>
    ///     Validates that the string contains the specified character.
    ///     Returns a ValidationResult indicating success or failure.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="character">The character to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the validation passed or failed.</returns>
    public static ValidationResult ValidateDoesContain(this string? value, char character, string parameterName,
        StringComparison comparison = StringComparison.Ordinal,
        Blackboard? blackboard = null)
    {
        if (value.CheckDoesContain(character, comparison))
        {
            return new ValidationResult();
        }

        return new ValidationResult(new ValidationException("DoesContain", parameterName,
            $"{parameterName} must contain '{character}'.",
            blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "character", character }
            }));
    }

    /// <summary>
    ///     Validates that the string contains the specified character within a substring range.
    ///     Returns a ValidationResult indicating success or failure.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="character">The character to check for.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <param name="startIndex">The starting index of the substring range.</param>
    /// <param name="count">The number of characters to include in the substring range.</param>
    /// <param name="comparison">The string comparison type to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the validation passed or failed.</returns>
    public static ValidationResult ValidateDoesContain(this string? value, char character, string parameterName,
        int startIndex,
        int? count = null,
        StringComparison comparison = StringComparison.Ordinal,
        Blackboard? blackboard = null)
    {
        count ??= value?.Length - startIndex;
        if (value.CheckDoesContain(character, startIndex, count, comparison))
        {
            return new ValidationResult();
        }

        return new ValidationResult(new ValidationException(
            "DoesContain",
            parameterName,
            $"{parameterName} must contain '{character}' (in substring from {startIndex} for {count} characters).",
            blackboard,
            new Dictionary<string, object?>
            {
                { "value", value },
                { "character", character },
                { "startIndex", startIndex },
                { "count", count }
            }));
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
        Blackboard? blackboard = null)
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
        string parameterName, Blackboard? blackboard = null)
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
