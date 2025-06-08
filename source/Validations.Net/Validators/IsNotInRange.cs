using System.Numerics;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsNotInRange
{
    public static bool CheckIsNotInRange<T>(this T? value, T min, T max, bool minIsInclusive = true, bool maxIsInclusive = false) where T : INumber<T>
    {
        if (value is null)
        {
            return true; // Null values are not in range
        }

        min.ValidateIsLessThanOrEquals(max, nameof(min));
        max.ValidateIsGreaterThanOrEquals(min, nameof(max));

        min = minIsInclusive ? min - T.One : min;
        max = maxIsInclusive ? max + T.One : max;

        return value <= min || value >= max;
    }
    
    public static T ValidateNotIsInRange<T>(this T? value, T min, T max, string propertyName, bool minIsInclusive = true, bool maxIsInclusive = false, Blackboard? blackboard = null) where T : INumber<T>
    {
        if (!value.CheckIsNotInRange(min, max, minIsInclusive, maxIsInclusive))
        {
            throw new ValidationException("IsNotInRange", propertyName, $"{propertyName} must not be in the range [{min}, {max}].", blackboard, new Dictionary<string, object?>
            {
                { "value", value },
                { "min", min },
                { "max", max }
            });
        }

        return value!;
    }
}
