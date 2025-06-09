using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsNotEqualsWithin
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEqualsWithin<T>(this T? value, T? compareTo, T tolerance) where T : INumber<T>
    {
        if (value == null && compareTo == null)
        {
            return false; // Both are null, considered not equal
        }

        if (value == null || compareTo == null)
        {
            return true; // One is null, the other is not, considered equal
        }

        return T.Abs(value - compareTo) > tolerance;
    }
    
    public static T ValidateIsNotEqualsWithin<T>(this T? value, T? compareTo, T tolerance, string propertyName, Blackboard? blackboard = null) where T : INumber<T>
    {
        if (!value.CheckIsNotEqualsWithin(compareTo, tolerance))
        {
            throw new ValidationException("IsNotEqualsWithin", propertyName, $"{propertyName} must not be approximately equal to {compareTo} within a tolerance of {tolerance}.", blackboard, new Dictionary<string, object?> { { "value", value }, { "compareTo", compareTo }, { "tolerance", tolerance } });
        }

        return value!;
    }
}
