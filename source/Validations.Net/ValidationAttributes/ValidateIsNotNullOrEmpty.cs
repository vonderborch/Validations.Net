using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsNotNullOrEmpty<T>() : ValidationAttribute("IsNotNullOrEmpty")
{
    public override bool Check(object? value)
    {
        switch (value)
        {
            case null:
                return false;
            case string str:
                return !str.CheckIsNotNullOrEmpty();
            case T[] array:
                return array.CheckIsNotNullOrEmpty();
            case ICollection<T> collection:
                return collection!.CheckIsNotNullOrEmpty();
            default:
                throw ValidationException.CreateFromTypeMisMatch<object>(ValidatorName, nameof(value), value);
        }
    }
    
    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        switch (value)
        {
            case null:
                value.ValidateIsNotNull(propertyName, blackboard);
                return;
            case string str:
                str.ValidateIsNotNullOrEmpty(propertyName, blackboard);
                return;
            case T[] array:
                 array.ValidateIsNotNullOrEmpty(propertyName, blackboard);
                 return;
            case ICollection<T> collection:
                collection!.ValidateIsNotNullOrEmpty(propertyName, blackboard);
                return;
            default:
                throw ValidationException.CreateFromTypeMisMatch<object>(ValidatorName, propertyName, value, blackboard);
        }
    }
}
