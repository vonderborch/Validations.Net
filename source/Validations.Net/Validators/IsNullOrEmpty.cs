using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsNullOrEmpty
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrEmpty<T>(this T?[]? value)
    {
        return value is null || value.Length == 0;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrEmpty<T>(this ICollection<T?>? value)
    {
        return value is null || value.Count == 0;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrEmpty(this string? value)
    {
        return value is null || value.Length == 0;
    }
    
    public static T?[] ValidateIsNullOrEmpty<T>(this T?[]? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNullOrEmpty())
        {
            throw new ValidationException("IsNullOrEmpty", propertyName, $"{propertyName} must be null or empty.", blackboard, new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
    
    public static ICollection<T?> ValidateIsNullOrEmpty<T>(this ICollection<T?>? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNullOrEmpty())
        {
            throw new ValidationException("IsNullOrEmpty", propertyName, $"{propertyName} must be null or empty.", blackboard, new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
    
    public static string ValidateIsNullOrEmpty(this string? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNullOrEmpty())
        {
            throw new ValidationException("IsNullOrEmpty", propertyName, $"{propertyName} must be null or empty.", blackboard, new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
}
