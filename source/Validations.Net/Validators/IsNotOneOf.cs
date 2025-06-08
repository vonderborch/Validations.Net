using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsNotOneOf
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotOneOf<T>(this T? value, params T?[] options)
    {
        foreach (var option in options)
        {
            if (value?.Equals(option) ?? false)
            {
                return false;
            }
        }

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotOneOf<T>(this T? value, ICollection<T> options)
    {
        foreach (var option in options)
        {
            if (value?.Equals(option) ?? false)
            {
                return false;
            }
        }

        return true;
    }
    
    public static T ValidateIsNotOneOf<T>(this T? value, string propertyName, Blackboard? blackboard = null, params T?[] options)
    {
        if (!value.CheckIsNotOneOf(options))
        {
            throw new ValidationException("IsNotOneOf", propertyName, $"{propertyName} must not be one of the specified values.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "options", options }
            });
        }

        return value!;
    }
    
    public static T ValidateIsNotOneOf<T>(this T? value, ICollection<T> options, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotOneOf(options))
        {
            throw new ValidationException("IsNotOneOf", propertyName, $"{propertyName} must not be one of the specified values.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "options", options }
            });
        }

        return value!;
    }
}
