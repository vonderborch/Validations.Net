using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsNotEquals
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEquals<T>(this T? value, T? compareTo)
    {
        if (value == null && compareTo == null)
        {
            return false; // Both are null, considered not equal
        }

        if (value == null || compareTo == null)
        {
            return true; // One is null, the other is not, considered equal
        }

        return !value.Equals(compareTo);
    }
    
    public static T ValidateIsNotEquals<T>(this T? value, T? compareTo, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotEquals(compareTo))
        {
            throw new ValidationException("IsNotEquals", propertyName, $"{propertyName} must not be equal to {compareTo}.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "compareTo", compareTo }
            });
        }

        return value!;
    }
}
