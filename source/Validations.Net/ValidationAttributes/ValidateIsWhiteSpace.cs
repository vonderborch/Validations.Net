using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsWhiteSpace() : ValidationAttribute("IsWhiteSpace")
{
    public override bool Check(object? value)
    {
        return value switch
        {
            string str => str.CheckIsWhiteSpace(),
            _ => throw new ValidationException("IsWhiteSpace", nameof(value), "Value must be a string.", null, new Dictionary<string, object?>
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
                str.ValidateIsWhiteSpace(propertyName);
                break;
            default:
                throw new ValidationException("IsWhiteSpace", propertyName, $"{propertyName} must be a string.", null, new Dictionary<string, object?>
                {
                    { "value", value }
                });
        }
    }
}
