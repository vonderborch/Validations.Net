using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsEquals
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEquals<T>(this T? value, T? compareTo)
    {
        if (value == null && compareTo == null)
        {
            return true; // Both are null, considered equal
        }

        if (value == null || compareTo == null)
        {
            return false; // One is null, the other is not, considered not equal
        }

        return value.Equals(compareTo); // Use Equals method for comparison
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEquals<T>(this T? value, T? compareTo, T tolerance) where T : IFloatingPoint<T>
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
    
    public static T ValidateIsEquals<T>(this T? value, T? compareTo, T tolerance, string propertyName, Blackboard? blackboard = null) where T : IFloatingPoint<T>
    {
        if (!value.CheckIsEquals(compareTo, tolerance))
        {
            throw new ValidationException("IsEqualsApproximately", propertyName, $"{propertyName} must be approximately equal to {compareTo} within a tolerance of {tolerance}.", blackboard, new Dictionary<string, object?> { { "value", value }, { "compareTo", compareTo }, { "tolerance", tolerance } });
        }

        return value!;
    }

    public static T ValidateIsEquals<T>(this T? value, T? compareTo, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsEquals(compareTo))
        {
            throw new ValidationException("IsEquals", propertyName, $"{propertyName} must be equal to {compareTo}.", blackboard, new Dictionary<string, object?> { { "value", value }, { "compareTo", compareTo } });
        }

        return value!;
    }
}
