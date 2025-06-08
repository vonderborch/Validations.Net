using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsNotWhiteSpace() : ValidationAttribute("IsNotWhiteSpace")
{
    public override bool Check(object? value)
    {
        return value switch
        {
            string str => str.CheckIsNotWhiteSpace(),
            _ => throw new ValidationException("IsNotWhiteSpace", nameof(value), "Value must be a string.", null, new Dictionary<string, object?>
            {
                { "value", value }
            })
        };
    }
    
    public override void Validate(object? value, string propertyName)
    {
        switch (value)
        {
            case string str:
                str.ValidateIsNotWhiteSpace(propertyName);
                break;
            default:
                throw new ValidationException("IsNotWhiteSpace", propertyName, $"{propertyName} must be a string.", null, new Dictionary<string, object?>
                {
                    { "value", value }
                });
        }
    }
}
