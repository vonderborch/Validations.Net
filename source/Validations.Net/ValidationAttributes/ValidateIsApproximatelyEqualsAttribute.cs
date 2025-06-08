using System.Numerics;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsApproximatelyEqualsAttribute<T>(T compareTo, T tolerance) : ValidationAttribute("IsEqualsApproximately")
    where T : IComparable<T>, IFloatingPoint<T>
{
    public T CompareTo { get; } = compareTo;
    
    public T Tolerance { get; } = tolerance;

    public override bool Check(object? value)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        return typedValue.CheckIsEquals(CompareTo, Tolerance);
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        typedValue.ValidateIsEquals(CompareTo, Tolerance, propertyName, blackboard);
    }
}
