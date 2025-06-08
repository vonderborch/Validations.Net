using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsGreaterThan
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsGreaterThan<T>(this T? value, T? compareTo) where T : IComparable<T>
    {
        if (value == null || compareTo == null)
        {
            if (value != null)
            {
                return true;
            }
            return false; // null is considered less than any non-null value
        }

        return value.CompareTo(compareTo) > 0;
    }
    
    public static T ValidateIsGreaterThan<T>(this T? value, T? compareTo, string propertyName, Blackboard? blackboard = null) where T : IComparable<T>
    {
        if (!value.CheckIsGreaterThan(compareTo))
        {
            throw new ValidationException("IsGreaterThan", propertyName, $"{propertyName} must be greater than {compareTo}.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "compareTo", compareTo }
            });
        }

        return value!;
    }
}
