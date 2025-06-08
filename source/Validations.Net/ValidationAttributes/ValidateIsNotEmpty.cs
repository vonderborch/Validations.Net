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
            _ => throw new ValidationException("IsNotEmpty", nameof(value), "Value must be a string, collection, or array.", null, new Dictionary<string, object?>
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
                str.ValidateIsNotEmpty(propertyName);
                break;
            case ICollection<object?> collection:
                collection.ValidateIsNotEmpty(propertyName);
                break;
            default:
                throw new ValidationException("IsNotEmpty", propertyName, $"{propertyName} must be a string, collection, or array.", null, new Dictionary<string, object?>
                {
                    { "value", value }
                });
        }
    }
}
