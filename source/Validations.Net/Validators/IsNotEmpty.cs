using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsNotEmpty
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEmpty<T>(this ICollection<T>? value)
    {
        return value is not null && value.Count > 0;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEmpty(this string? value)
    {
        return value is not null && value.Length > 0;
    }
    
    public static ICollection<T> ValidateIsNotEmpty<T>(this ICollection<T> value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotEmpty())
        {
            throw new ValidationException("IsNotEmpty", propertyName, $"{propertyName} must not be empty.", blackboard, new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value;
    }
    
    public static string ValidateIsNotEmpty(this string? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotEmpty())
        {
            throw new ValidationException("IsNotEmpty", propertyName, $"{propertyName} must not be empty.", blackboard, new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
}
