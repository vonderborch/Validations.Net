using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsEqualsAttribute<T>(T compareTo) : ValidationAttribute("IsEquals") where T : IComparable<T>
{
    public T CompareTo { get; } = compareTo;

    public override bool Check(object? value)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        return typedValue.CheckIsEquals(CompareTo);
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        typedValue.ValidateIsEquals(CompareTo, propertyName, blackboard);
    }
}
