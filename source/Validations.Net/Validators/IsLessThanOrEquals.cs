using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsLessThanOrEquals
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsLessThanOrEquals<T>(this T? value, T? compareTo) where T : IComparable<T>
    {
        if (value == null || compareTo == null)
        {
            if (value == null)
            {
                return true; // null is considered less than any non-null value
            }
            return false;
        }
        
        return value.CompareTo(compareTo) <= 0;
    }
    
    public static T ValidateIsLessThanOrEquals<T>(this T value, T compareTo, string propertyName, Blackboard? blackboard = null) where T : IComparable<T>
    {
        if (!value.CheckIsLessThanOrEquals(compareTo))
        {
            throw new ValidationException("ValidateIsLessThanOrEqualsAttribute", propertyName, $"{propertyName} must be less than or equal to {compareTo}.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "compareTo", compareTo }
            });
        }

        return value;
    }
}
