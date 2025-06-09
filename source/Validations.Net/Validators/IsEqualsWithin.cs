using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsEqualsWithin
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEqualsWithin<T>(this T? value, T? compareTo, T tolerance) where T : INumber<T>
    {
        if (value == null && compareTo == null)
        {
            return true; // Both are null, considered equal
        }

        if (value == null || compareTo == null)
        {
            return false; // One is null, the other is not, considered not equal
        }

        return T.Abs(value - compareTo) <= tolerance;
    }
    
    public static T ValidateIsEqualsWithin<T>(this T? value, T? compareTo, T tolerance, string propertyName, Blackboard? blackboard = null) where T : INumber<T>
    {
        if (!value.CheckIsEqualsWithin(compareTo, tolerance))
        {
            throw new ValidationException("IsEqualsWithin", propertyName, $"{propertyName} must be approximately equal to {compareTo} within a tolerance of {tolerance}.", blackboard, new Dictionary<string, object?> { { "value", value }, { "compareTo", compareTo }, { "tolerance", tolerance } });
        }

        return value!;
    }
}
