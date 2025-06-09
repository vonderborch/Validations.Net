using System.Numerics;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValdiateIsNotEqualsWithinAttribute<T>(T compareTo, T tolerance) : ValidationAttribute("IsNotEqualsWithin") where T : INumber<T>
{
    public T CompareTo { get; } = compareTo;

    public T Tolerance { get; } = tolerance;

    public override bool Check(object? value)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        return typedValue.CheckIsNotEqualsWithin(CompareTo, Tolerance);
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        typedValue.ValidateIsNotEqualsWithin(CompareTo, Tolerance, propertyName, blackboard);
    }
}
