using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsNullOrEmpty<T>() : ValidationAttribute("IsNullOrEmpty")
{
    public override bool Check(object? value)
    {
        switch (value)
        {
            case null:
                return true;
            case T[] array:
                return array.CheckIsNullOrEmpty();
            case ICollection<T> collection:
                return collection!.CheckIsNullOrEmpty();
            case string str:
                return str.CheckIsNullOrEmpty();
            default:
                throw ValidationException.CreateFromTypeMisMatch<T>(ValidatorName, nameof(value), value);
        }
    }

    public override void Validate(object? value, string propertyName)
    {
        switch (value)
        {
            case null:
                return;
            case T[] array:
                array.ValidateIsNullOrEmpty(propertyName);
                return;
            case ICollection<T> collection:
                collection!.ValidateIsNullOrEmpty(propertyName);
                return;
            case string str:
                str.ValidateIsNullOrEmpty(propertyName);
                return;
            default:
                throw ValidationException.CreateFromTypeMisMatch<T>(ValidatorName, propertyName, value);
        }
    }
}
