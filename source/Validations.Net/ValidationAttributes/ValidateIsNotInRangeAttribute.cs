using System.Numerics;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsNotInRangeAttribute<T> : ValidationAttribute where T : INumber<T>
{
    public T Min { get; }
    public T Max { get; }
    public bool MinIsInclusive { get; }
    public bool MaxIsInclusive { get; }
    
    public ValidateIsNotInRangeAttribute(T min, T max, bool minIsInclusive = true, bool maxIsInclusive = false) : base("IsNotInRange")
    {
        min.ValidateIsLessThanOrEquals(max, nameof(min));
        max.ValidateIsGreaterThanOrEquals(min, nameof(max));
        
        Min = min;
        Max = max;
        MinIsInclusive = minIsInclusive;
        MaxIsInclusive = maxIsInclusive;
    }

    public override bool Check(object? value)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        return typedValue.CheckIsNotInRange(Min, Max, MinIsInclusive, MaxIsInclusive);
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        typedValue.ValidateNotIsInRange(Min, Max, propertyName, MinIsInclusive, MaxIsInclusive, blackboard);
    }
}
