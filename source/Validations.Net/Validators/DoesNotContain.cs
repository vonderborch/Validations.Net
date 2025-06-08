using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class DoesNotContain
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain(this string? value, string? substring, StringComparison comparisonType = StringComparison.Ordinal)
    {
        if (value is null || substring is null)
        {
            return true;
        }

        return !value.Contains(substring, comparisonType);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain<T>(this T?[]? value, T? item)
    {
        if (value is null)
        {
            return true;
        }

        return Array.IndexOf(value, item) < 0;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckDoesNotContain<T>(this ICollection<T?>? value, T? item)
    {
        if (value is null)
        {
            return true;
        }

        return !value.Contains(item);
    }
    
    public static string ValidateDoesNotContain(this string? value, string? substring, string propertyName, StringComparison comparisonType = StringComparison.Ordinal, Blackboard? blackboard = null)
    {
        if (!value.CheckDoesNotContain(substring, comparisonType))
        {
            throw new ValidationException("DoesNotContain", propertyName, $"{propertyName} must not contain '{substring}'.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "substring", substring }
            });
        }

        return value!;
    }
    
    public static T?[] ValidateDoesNotContain<T>(this T?[]? value, T? item, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckDoesNotContain(item))
        {
            throw new ValidationException("DoesNotContain", propertyName, $"{propertyName} must not contain '{item}'.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "item", item }
            });
        }

        return value!;
    }
    
    public static ICollection<T?> ValidateDoesNotContain<T>(this ICollection<T?>? value, T? item, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckDoesNotContain(item))
        {
            throw new ValidationException("DoesNotContain", propertyName, $"{propertyName} must not contain '{item}'.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "item", item }
            });
        }

        return value!;
    }
}
