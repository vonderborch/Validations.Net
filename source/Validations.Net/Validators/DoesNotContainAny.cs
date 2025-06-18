using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class DoesNotContainAny
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny(this string? value, ICollection<string> subStrings,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        return subStrings.Any(x => value.Contains(x, comparison));
    }

    public static string ValidateDoesNotContainAny(this string? value, ICollection<string> subStrings, string propertyName,
        StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        if (!value.CheckDoesNotContainAny(subStrings, comparison))
        {
            throw new ValidationException("DoesNotContainAny", propertyName, $"{propertyName} must not contain any of the substrings.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "subStrings", subStrings }
            });
        }

        return value!;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny(this string? value, ICollection<char> characters,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        return characters.Any(x => value.Contains(x, comparison));
    }

    public static string ValidateDoesNotContainAny(this string? value, ICollection<char> characters, string propertyName,
        StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        if (!value.CheckDoesNotContainAny(characters, comparison))
        {
            throw new ValidationException("DoesNotContainAny", propertyName, $"{propertyName} must not contain any of the characters.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "characters", characters }
            });
        }

        return value!;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this ICollection<T>? collection, ICollection<T> items)
    {
        if (collection is null)
        {
            return false;
        }

        return items.Any(collection.Contains);
    }

    public static ICollection<T> ValidateDoesNotContainAny<T>(this ICollection<T>? collection, ICollection<T> items, string propertyName,
        Blackboard? blackboard = null)
    {
        if (!collection.CheckDoesNotContainAny(items))
        {
            throw new ValidationException("DoesNotContainAny", propertyName, $"{propertyName} must not contain any of the items.", blackboard, new Dictionary<string, object?>
            {
                { "collection", collection },
                { "items", items }
            });
        }

        return collection!;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAny<T>(this ICollection<T>? collection, ICollection<Func<T, bool>> predicates)
    {
        if (collection is null)
        {
            return false;
        }

        return predicates.Any(collection.Any);
    }

    public static ICollection<T> ValidateDoesNotContainAny<T>(this ICollection<T>? collection, ICollection<Func<T, bool>> predicates, string propertyName,
        Blackboard? blackboard = null)
    {
        if (!collection.CheckDoesNotContainAny(predicates))
        {
            throw new ValidationException("DoesNotContainAny", propertyName, $"{propertyName} must not contain any items matching any of the predicates.", blackboard, new Dictionary<string, object?>
            {
                { "collection", collection },
                { "predicates", predicates }
            });
        }

        return collection!;
    }
}
