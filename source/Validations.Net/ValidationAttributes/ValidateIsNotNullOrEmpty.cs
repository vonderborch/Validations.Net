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
    
    public override void Validate(object? value, string propertyName)
    {
        switch (value)
        {
            case null:
                value.ValidateIsNotNull(propertyName);
                return;
            case string str:
                str.ValidateIsNotNullOrEmpty(propertyName);
                return;
            case T[] array:
                 array.ValidateIsNotNullOrEmpty(propertyName);
                 return;
            case ICollection<T> collection:
                collection!.ValidateIsNotNullOrEmpty(propertyName);
                return;
            default:
                throw ValidationException.CreateFromTypeMisMatch<object>(ValidatorName, propertyName, value);
        }
    }
}
