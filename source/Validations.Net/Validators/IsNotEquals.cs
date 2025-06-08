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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEquals<T>(this T? value, T? compareTo, T tolerance) where T : IFloatingPoint<T>
    {
        if (value == null && compareTo == null)
        {
            return false; // Both are null, considered equal
        }

        if (value == null || compareTo == null)
        {
            return true; // One is null, the other is not, considered not equal
        }

        return T.Abs(value - compareTo) > tolerance;
    }

    public static T ValidateIsNotEquals<T>(this T? value, T? compareTo, T tolerance, string propertyName, Blackboard? blackboard = null) where T : IFloatingPoint<T>
    {
        if (!value.CheckIsNotEquals(compareTo, tolerance))
        {
            throw new ValidationException("IsNotEqualsApproximately", propertyName, $"{propertyName} must not be approximately equal to {compareTo} within a tolerance of {tolerance}.", blackboard, new Dictionary<string, object?> { { "value", value }, { "compareTo", compareTo }, { "tolerance", tolerance } });
        }

        return value!;
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
