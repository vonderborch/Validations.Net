using System.Numerics;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsEqualsApproximatelyAttribute<T>(T compareTo, T tolerance) : ValidationAttribute("IsEqualsApproximately")
    where T : IComparable<T>, IFloatingPoint<T>
{
    public T CompareTo { get; } = compareTo;
    
    public T Tolerance { get; } = tolerance;

    public override bool Check(object? value)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        return typedValue.CheckIsEqualsApproximately(CompareTo, Tolerance);
    }

    public override void Validate(object? value, string propertyName)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        typedValue.ValidateIsEqualsApproximately(CompareTo, Tolerance, propertyName);
    }
}
