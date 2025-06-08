using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsInRange
{
    public static bool CheckIsInRange<T>(this T? value, T min, T max, bool minIsInclusive = true, bool maxIsInclusive = false) where T : INumber<T>
    {
        if (value is null)
        {
            return false; // Null values are not in range
        }

        min.ValidateIsLessThanOrEquals(max, nameof(min));
        max.ValidateIsGreaterThanOrEquals(min, nameof(max));

        min = minIsInclusive ? min - T.One : min;
        max = maxIsInclusive ? max + T.One : max;

        return value > min && value < max;
    }
    
    public static T ValidateIsInRange<T>(this T? value, T min, T max, string propertyName, bool minIsInclusive = true, bool maxIsInclusive = false, Blackboard? blackboard = null) where T : INumber<T>
    {
        if (!value.CheckIsInRange(min, max, minIsInclusive, maxIsInclusive))
        {
            throw new ValidationException("IsInRange", propertyName, $"{propertyName} must be in the range [{min}, {max}].", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "min", min },
                { "max", max }
            });
        }

        return value!;
    }
}
