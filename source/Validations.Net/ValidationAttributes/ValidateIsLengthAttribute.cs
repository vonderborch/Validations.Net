using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsLengthAttribute : ValidationAttribute
{
    public ValidateIsLengthAttribute(int length) : base("IsLength")
    {
        length.ValidateIsGreaterThanOrEquals(0, nameof(length));
        Length = length;
    }
    
    public int Length { get; }
    
    public override bool Check(object? value)
    {
        return value switch
        {
            string str => str.CheckIsLength(Length),
            ICollection<object?> collection => collection.CheckIsLength(Length),
            _ => throw ValidationException.CreateFromTypeMisMatch<object>("IsLength", nameof(value), value)
        };
    }
    
    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        switch (value)
        {
            case string str:
                str.ValidateIsLength(Length, propertyName, blackboard);
                break;
            case ICollection<object?> collection:
                collection.ValidateIsLength(Length, propertyName, blackboard);
                break;
            default:
                throw ValidationException.CreateFromTypeMisMatch<object>("IsLength", propertyName, value, blackboard);
        }
    }
}
