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
            _ => throw new ValidationException("IsEmpty", nameof(value), "Value must be a string, collection, or array.", null, new Dictionary<string, object?>
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
                str.ValidateIsEmpty(propertyName);
                break;
            case ICollection<object?> collection:
                collection.ValidateIsEmpty(propertyName);
                break;
            default:
                throw new ValidationException("IsEmpty", propertyName, $"{propertyName} must be a string, collection, or array.", null, new Dictionary<string, object?>
                {
                    { "value", value }
                });
        }
    }
}
