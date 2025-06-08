using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsGreaterThanOrEquals
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsGreaterThanOrEquals<T>(this T? value, T? compareTo) where T : IComparable<T>
    {
        if (value == null || compareTo == null)
        {
            if (value != null)
            {
                return true;
            }
            return false; // null is considered less than any non-null value
        }

        return value.CompareTo(compareTo) >= 0;
    }

    public static T ValidateIsGreaterThanOrEquals<T>(this T? value, T compareTo, string propertyName, Blackboard? blackboard = null) where T : IComparable<T>
    {
        if (!value.CheckIsGreaterThanOrEquals(compareTo))
        {
            throw new ValidationException("IsGreaterThanOrEquals", propertyName, $"{propertyName} must be greater than or equal to {compareTo}.", blackboard, new Dictionary<string, object?> { { "value", value }, { "compareTo", compareTo } });
        }

        return value!;
    }
}
