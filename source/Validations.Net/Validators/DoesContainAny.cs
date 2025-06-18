using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class DoesContainAny
{
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

    public static string ValidateDoesContainAny(this string? value, ICollection<string> subStrings, string parameterName,
        StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        if (!value.CheckDoesContainAny(subStrings, comparison))
        {
            throw new ValidationException("DoesContainAny", parameterName, $"{parameterName} must contain any of the substrings.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "subStrings", subStrings }
            });
        }

        return value!;
    }

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

    public static string ValidateDoesContainAny(this string? value, ICollection<char> characters, string parameterName,
        StringComparison comparison = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        if (!value.CheckDoesContainAny(characters, comparison))
        {
            throw new ValidationException("DoesContainAny", parameterName, $"{parameterName} must contain any of the characters.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "characters", characters }
            });
        }

        return value!;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny<T>(this ICollection<T>? collection, ICollection<T> items)
    {
        if (collection is null)
        {
            return false;
        }

        return items.Any(collection.Contains);
    }

    public static ICollection<T> ValidateDoesContainAny<T>(this ICollection<T>? collection, ICollection<T> items, string parameterName,
        Blackboard? blackboard = null)
    {
        if (!collection.CheckDoesContainAny(items))
        {
            throw new ValidationException("DoesContainAny", parameterName, $"{parameterName} must contain any of the items.", blackboard, new Dictionary<string, object?>
            {
                { "collection", collection },
                { "items", items }
            });
        }

        return collection!;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContainAny<T>(this ICollection<T>? collection, ICollection<Func<T, bool>> predicates)
    {
        if (collection is null)
        {
            return false;
        }

        return predicates.Any(collection.Any);
    }

    public static ICollection<T> ValidateDoesContainAny<T>(this ICollection<T>? collection, ICollection<Func<T, bool>> predicates, string parameterName,
        Blackboard? blackboard = null)
    {
        if (!collection.CheckDoesContainAny(predicates))
        {
            throw new ValidationException("DoesContainAny", parameterName, $"{parameterName} must contain at least one item matching any of the predicates.", blackboard, new Dictionary<string, object?>
            {
                { "collection", collection },
                { "predicates", predicates }
            });
        }

        return collection!;
    }
}
