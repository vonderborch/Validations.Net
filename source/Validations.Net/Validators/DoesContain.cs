using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class DoesContain
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContain(this string? value, string substring, StringComparison comparisonType = StringComparison.Ordinal)
    {
        if (value is null)
        {
            return false;
        }

        return value.Contains(substring, comparisonType);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContain<T>(this T?[]? value, T? item)
    {
        if (value is null)
        {
            return false;
        }

        return Array.IndexOf(value, item) >= 0;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesContain<T>(this ICollection<T?>? value, T? item)
    {
        if (value is null)
        {
            return false;
        }

        return value.Contains(item);
    }
    
    public static string ValidateDoesContain(this string? value, string substring, string propertyName, StringComparison comparisonType = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        if (!value.CheckDoesContain(substring, comparisonType))
        {
            throw new ValidationException("DoesContain", propertyName, $"{propertyName} must contain '{substring}'.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "substring", substring }
            });
        }

        return value!;
    }
    
    public static T?[] ValidateDoesContain<T>(this T?[]? value, T? item, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckDoesContain(item))
        {
            throw new ValidationException("DoesContain", propertyName, $"{propertyName} must contain '{item}'.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "item", item }
            });
        }

        return value!;
    }
    
    public static ICollection<T?> ValidateDoesContain<T>(this ICollection<T?>? value, T? item, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckDoesContain(item))
        {
            throw new ValidationException("DoesContain", propertyName, $"{propertyName} must contain '{item}'.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "item", item }
            });
        }

        return value!;
    }
}
