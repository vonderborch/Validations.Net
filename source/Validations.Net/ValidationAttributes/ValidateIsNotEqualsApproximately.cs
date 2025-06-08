using System.Numerics;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsNotEqualsApproximately<T>(T compareTo, T tolerance) : ValidationAttribute("IsNotEqualsApproximately")
    where T : IComparable<T>, IFloatingPoint<T>
{
    public T CompareTo { get; } = compareTo;

    public T Tolerance { get; } = tolerance;

    public override bool Check(object? value)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        return typedValue.CheckIsNotEqualsApproximately(CompareTo, Tolerance);
    }

    public override void Validate(object? value, string propertyName)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        typedValue.ValidateIsNotEqualsApproximately(CompareTo, Tolerance, propertyName);
    }
}
