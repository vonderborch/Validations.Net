using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsNotNullOrEmpty
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrEmpty<T>(this ICollection<T>? value)
    {
        return value is not null && value.Count > 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrEmpty(this string? value)
    {
        return !string.IsNullOrEmpty(value);
    }
    
    public static ICollection<T> ValidateIsNotNullOrEmpty<T>(this ICollection<T>? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotNullOrEmpty())
        {
            throw new ValidationException("IsNotNullOrEmpty", propertyName, $"{propertyName} must not be null or empty.", blackboard, new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
    
    public static string ValidateIsNotNullOrEmpty(this string? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotNullOrEmpty())
        {
            throw new ValidationException("IsNotNullOrEmpty", propertyName, $"{propertyName} must not be null or empty.", blackboard, new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
}
