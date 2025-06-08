using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsGreaterThanOrEqualsAtrribute<T>(T compareTo) : ValidationAttribute("IsGreaterThanOrEquals") where T : IComparable<T>
{
    public T CompareTo { get; } = compareTo;

    public override bool Check(object? value)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        return typedValue.CheckIsGreaterThanOrEquals(CompareTo);
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        typedValue.ValidateIsGreaterThanOrEquals(CompareTo, propertyName, blackboard);
    }
}
