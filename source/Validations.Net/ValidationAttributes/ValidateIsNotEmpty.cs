using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsNotEmpty() : ValidationAttribute("IsNotEmpty")
{
    public override bool Check(object? value)
    {
        return value switch
        {
            string str => str.CheckIsNotEmpty(),
            ICollection<object?> collection => collection.CheckIsNotEmpty(),
            _ => throw ValidationException.CreateFromTypeMisMatch<object>("IsNotEmpty", nameof(value), value)
        };
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        switch (value)
        {
            case string str:
                str.ValidateIsNotEmpty(propertyName, blackboard);
                break;
            case ICollection<object?> collection:
                collection.ValidateIsNotEmpty(propertyName, blackboard);
                break;
            default:
                throw ValidationException.CreateFromTypeMisMatch<object>("IsNotEmpty", propertyName, value, blackboard);
        }
    }
}
