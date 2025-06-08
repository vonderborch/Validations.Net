using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsWhiteSpace() : ValidationAttribute("IsWhiteSpace")
{
    public override bool Check(object? value)
    {
        return value switch
        {
            string str => str.CheckIsWhiteSpace(),
            _ => throw ValidationException.CreateFromTypeMisMatch<object>("IsWhiteSpace", nameof(value), value)
        };
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        switch (value)
        {
            case string str:
                str.ValidateIsWhiteSpace(propertyName, blackboard);
                break;
            default:
                throw ValidationException.CreateFromTypeMisMatch<object>("IsWhiteSpace", propertyName, value,
                    blackboard);
        }
    }
}
