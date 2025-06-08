using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsLessThan
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsLessThan<T>(this T? value, T? compareTo) where T : IComparable<T>
    {
        if (value == null || compareTo == null)
        {
            if (value == null)
            {
                return true; // null is considered less than any non-null value
            }
            return false;
        }
        
        return value.CompareTo(compareTo) < 0;
    }
    
    public static T ValidateIsLessThan<T>(this T? value, T? compareTo, string propertyName, Blackboard? blackboard = null) where T : IComparable<T>
    {
        if (!value.CheckIsLessThan(compareTo))
        {
            throw new ValidationException("IsLessThan", propertyName, $"{propertyName} must be less than {compareTo}.", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "compareTo", compareTo }
            });
        }
        
        return value!;
    }
}
