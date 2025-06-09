using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsEmpty
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEmpty<T>(this ICollection<T>? value)
    {
        return value is not null && value.Count == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEmpty(this string? value)
    {
        return value is not null && value.Length == 0;
    }

    public static ICollection<T> ValidateIsEmpty<T>(this ICollection<T>? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsEmpty())
        {
            throw new ValidationException("IsEmpty", propertyName, $"{propertyName} must be empty.", blackboard, new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }

    public static string ValidateIsEmpty(this string? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsEmpty())
        {
            throw new ValidationException("IsEmpty", propertyName, $"{propertyName} must be empty.", blackboard, new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
}
