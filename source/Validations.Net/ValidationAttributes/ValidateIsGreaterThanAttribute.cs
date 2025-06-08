using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsGreaterThanAttribute<T>(T compareTo) : ValidationAttribute("IsGreaterThan") where T : IComparable<T>
{
    public T CompareTo { get; } = compareTo;

    public override bool Check(object? value)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        return typedValue.CheckIsGreaterThan(CompareTo);
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        typedValue.ValidateIsGreaterThan(CompareTo, propertyName, blackboard);
    }
}
