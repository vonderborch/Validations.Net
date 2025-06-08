using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsNotLengthAttribute : ValidationAttribute
{
    public int Length { get; }

    public ValidateIsNotLengthAttribute(int length) : base("IsNotLength")
    {
        length.ValidateIsGreaterThanOrEquals(0, nameof(length));
        Length = length;
    }

    public override bool Check(object? value)
    {
        return value switch
        {
            string str => str.CheckIsNotLength(Length),
            ICollection<object?> collection => collection.CheckIsNotLength(Length),
            _ => throw ValidationException.CreateFromTypeMisMatch<object>("IsNotLength", nameof(value), value)
        };
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        switch (value)
        {
            case string str:
                str.ValidateIsNotLength(Length, propertyName, blackboard);
                break;
            case ICollection<object?> collection:
                collection.ValidateIsNotLength(Length, propertyName, blackboard);
                break;
            default:
                throw ValidationException.CreateFromTypeMisMatch<object>("IsNotLength", propertyName, value, blackboard);
        }
    }
}
