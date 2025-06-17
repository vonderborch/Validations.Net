using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class DoesNotContain
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain(this string? value, string subString, StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }
        return !value.Contains(subString, comparison);
    }

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain(this string? value, char character, StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }
        return !value.Contains(character, comparison);
    }

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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain<T>(this ICollection<T>? collection, T item)
    {
        if (collection is null)
        {
            return false;
        }

        return !collection.Contains(item);
    }

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain<T>(this ICollection<T>? collection, Func<T, bool> predicate)
    {
        if (collection is null)
        {
            return false;
        }

        return !collection.Any(predicate);
    }

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
