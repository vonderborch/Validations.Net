using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsNotNullAttribute() : ValidationAttribute("IsNotNull")
{
    public override bool Check(object? value)
    {
        bool result = value.CheckIsNotNull();
        return result;
    }

    public override void Validate(object? value, string propertyName)
    {
        value.ValidateIsNotNull(propertyName);
    }
}
