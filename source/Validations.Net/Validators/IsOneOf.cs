using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsOneOf
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsOneOf<T>(this T? value, params T[] options)
    {
        foreach (var option in options)
        {
            if (value?.Equals(option) ?? false)
            {
                return true;
            }
        }

        return false;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsOneOf<T>(this T? value, ICollection<T> options)
    {
        foreach (var option in options)
        {
            if (value?.Equals(option) ?? false)
            {
                return true;
            }
        }

        return false;
    }
    
    public static T ValidateIsOneOf<T>(this T? value, string propertyName, Blackboard? blackboard = null, params T[] options)
    {
        if (!value.CheckIsOneOf(options))
        {
            throw new ValidationException("IsOneOf", propertyName, $"{propertyName} must be one of the specified values.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "options", options }
            });
        }

        return value!;
    }
    
    public static T ValidateIsOneOf<T>(this T? value, ICollection<T> options, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsOneOf(options))
        {
            throw new ValidationException("IsOneOf", propertyName, $"{propertyName} must be one of the specified values.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "options", options }
            });
        }

        return value!;
    }
}
