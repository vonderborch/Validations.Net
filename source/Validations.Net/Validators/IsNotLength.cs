using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsNotLength
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotLength<T>(this T?[]? value, int length)
    {
        if (value is null)
        {
            return true;
        }

        return value.Length != length;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotLength<T>(this ICollection<T?>? value, int length)
    {
        if (value is null)
        {
            return true;
        }

        return value.Count != length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotLength(this string? value, int length)
    {
        if (value is null)
        {
            return true;
        }

        return value.Length != length;
    }
    
    public static T?[] ValidateIsNotLength<T>(this T?[]? value, int length, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotLength(length))
        {
            throw new ValidationException("IsNotLength", propertyName, $"{propertyName} must not have a length of {length}.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "length", length }
            });
        }

        return value!;
    }
    
    public static ICollection<T?> ValidateIsNotLength<T>(this ICollection<T?>? value, int length, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotLength(length))
        {
            throw new ValidationException("IsNotLength", propertyName, $"{propertyName} must not have a length of {length}.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "length", length }
            });
        }

        return value!;
    }
    
    public static string ValidateIsNotLength(this string? value, int length, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotLength(length))
        {
            throw new ValidationException("IsNotLength", propertyName, $"{propertyName} must not have a length of {length}.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "length", length }
            });
        }

        return value!;
    }
}
