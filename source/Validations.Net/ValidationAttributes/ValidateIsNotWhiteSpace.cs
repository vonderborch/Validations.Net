using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsNotWhiteSpace() : ValidationAttribute("IsNotWhiteSpace")
{
    public override bool Check(object? value)
    {
        return value switch
        {
            string str => str.CheckIsNotWhiteSpace(),
            _ => throw ValidationException.CreateFromTypeMisMatch<object>("IsNotWhiteSpace", nameof(value), value)
        };
    }
    
    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        switch (value)
        {
            case string str:
                str.ValidateIsNotWhiteSpace(propertyName, blackboard);
                break;
            default:
                throw ValidationException.CreateFromTypeMisMatch<object>("IsNotWhiteSpace", propertyName, value,
                    blackboard);
        }
    }
}
