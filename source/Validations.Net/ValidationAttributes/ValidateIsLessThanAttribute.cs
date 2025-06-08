using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsLessThanAttribute<T>(T compareTo) : ValidationAttribute("IsLessThan") where T : IComparable<T>
{
    public T CompareTo { get; } = compareTo;
    
    public override bool Check(object? value)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        return typedValue.CheckIsLessThan(CompareTo);
    }

    public override void Validate(object? value, string propertyName)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        typedValue.ValidateIsLessThan(CompareTo, propertyName);
    }
}
