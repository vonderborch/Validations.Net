using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class DoesNotContainAll
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this string? value, ICollection<string> subStrings,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        return !subStrings.All(x => value.Contains(x, comparison));
    }

    public static string ValidateDoesNotContainAll(this string? value, ICollection<string> subStrings, string parameterName,
        StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        if (!value.CheckDoesNotContainAll(subStrings, comparison))
        {
            throw new ValidationException("DoesNotContainAll", parameterName, $"{parameterName} must not contain any of the substrings.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "subStrings", subStrings }
            });
        }

        return value!;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll(this string? value, ICollection<char> characters,
        StringComparison comparison = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        return !characters.All(x => value.Contains(x, comparison));
    }

    public static string ValidateDoesNotContainAll(this string? value, ICollection<char> characters, string parameterName,
        StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        if (!value.CheckDoesNotContainAll(characters, comparison))
        {
            throw new ValidationException("DoesNotContainAll", parameterName, $"{parameterName} must not contain any of the characters.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "characters", characters }
            });
        }

        return value!;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this ICollection<T>? collection, ICollection<T> items)
    {
        if (collection is null)
        {
            return false;
        }

        return !items.All(collection.Contains);
    }

    public static ICollection<T> ValidateDoesNotContainAll<T>(this ICollection<T>? collection, ICollection<T> items, string parameterName,
        Blackboard? blackboard = null)
    {
        if (!collection.CheckDoesNotContainAll(items))
        {
            throw new ValidationException("DoesNotContainAll", parameterName, $"{parameterName} must not contain any of the items.", blackboard, new Dictionary<string, object?>
            {
                { "collection", collection },
                { "items", items }
            });
        }

        return collection!;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContainAll<T>(this ICollection<T>? collection, ICollection<Func<T, bool>> predicates)
    {
        if (collection is null)
        {
            return false;
        }

        return !predicates.All(predicate => collection.All(predicate));
    }

    public static ICollection<T> ValidateDoesNotContainAll<T>(this ICollection<T>? collection, ICollection<Func<T, bool>> predicates, string parameterName,
        Blackboard? blackboard = null)
    {
        if (!collection.CheckDoesNotContainAll(predicates))
        {
            throw new ValidationException("DoesNotContainAll", parameterName, $"No item in {parameterName} may match any of the predicates.", blackboard, new Dictionary<string, object?>
            {
                { "collection", collection },
                { "predicates", predicates }
            });
        }

        return collection!;
    }
}
