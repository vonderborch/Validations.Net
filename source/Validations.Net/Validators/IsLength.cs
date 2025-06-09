using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsLength
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsLength<T>(this ICollection<T>? value, int length)
    {
        if (value is null)
        {
            return false;
        }

        return value.Count == length;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsLength(this string? value, int length)
    {
        if (value is null)
        {
            return false;
        }

        return value.Length == length;
    }
    
    public static ICollection<T> ValidateIsLength<T>(this ICollection<T>? value, int length, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsLength(length))
        {
            throw new ValidationException("IsLength", propertyName, $"{propertyName} must have a length of {length}.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "length", length }
            });
        }

        return value!;
    }
    
    public static string ValidateIsLength(this string? value, int length, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsLength(length))
        {
            throw new ValidationException("IsLength", propertyName, $"{propertyName} must have a length of {length}.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "length", length }
            });
        }

        return value!;
    }
}
