using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsEmptyAttribute() : ValidationAttribute("IsEmpty")
{
    public override bool Check(object? value)
    {
        return value switch
        {
            string str => str.CheckIsEmpty(),
            ICollection<object?> collection => collection.CheckIsEmpty(),
            _ => throw ValidationException.CreateFromTypeMisMatch<object>("IsEmpty", nameof(value), value)
        };
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        switch (value)
        {
            case string str:
                str.ValidateIsEmpty(propertyName, blackboard);
                break;
            case ICollection<object?> collection:
                collection.ValidateIsEmpty(propertyName, blackboard);
                break;
            default:
                throw ValidationException.CreateFromTypeMisMatch<object>("IsEmpty", propertyName, value, blackboard);
        }
    }
}
