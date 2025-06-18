using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class DoesContainAll
{
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

    public static string ValidateDoesContainAll(this string? value, ICollection<string> subStrings, string parameterName,
        StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        if (!value.CheckDoesContainAll(subStrings, comparison))
        {
            throw new ValidationException("DoesContainAll", parameterName, $"{parameterName} must contain all of the substrings.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "subStrings", subStrings }
            });
        }

        return value!;
    }

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

    public static string ValidateDoesContainAll(this string? value, ICollection<char> characters, string parameterName,
        StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        if (!value.CheckDoesContainAll(characters, comparison))
        {
            throw new ValidationException("DoesContainAll", parameterName, $"{parameterName} must contain all of the characters.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "characters", characters }
            });
        }

        return value!;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll<T>(this ICollection<T>? collection, ICollection<T> items)
    {
        if (collection is null)
        {
            return false;
        }

        return items.All(collection.Contains);
    }

    public static ICollection<T> ValidateDoesContainAll<T>(this ICollection<T>? collection, ICollection<T> items, string parameterName,
        Blackboard? blackboard = null)
    {
        if (!collection.CheckDoesContainAll(items))
        {
            throw new ValidationException("DoesContainAll", parameterName, $"{parameterName} must contain all of the items.", blackboard, new Dictionary<string, object?>
            {
                { "collection", collection },
                { "items", items }
            });
        }

        return collection!;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAll<T>(this ICollection<T>? collection, ICollection<Func<T, bool>> predicates)
    {
        if (collection is null)
        {
            return false;
        }

        return predicates.All(collection.All);
    }

    public static ICollection<T> ValidateDoesContainAll<T>(this ICollection<T>? collection, ICollection<Func<T, bool>> predicates, string parameterName,
        Blackboard? blackboard = null)
    {
        if (!collection.CheckDoesContainAll(predicates))
        {
            throw new ValidationException("DoesContainAll", parameterName, $"All items in {parameterName} must match all of the predicates.", blackboard, new Dictionary<string, object?>
            {
                { "collection", collection },
                { "predicates", predicates }
            });
        }

        return collection!;
    }
}
