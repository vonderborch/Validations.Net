using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsLengthAttribute : ValidationAttribute
{
    public ValidateIsLengthAttribute(int length) : base("IsLength")
    {
        length.ValidateIsGreaterThanOrEquals(0, nameof(length));
        Length = length;
    }
    
    public int Length { get; }
    
    public override bool Check(object? value)
    {
        return value switch
        {
            string str => str.CheckIsLength(Length),
            ICollection<object?> collection => collection.CheckIsLength(Length),
            _ => throw new ValidationException("IsLength", nameof(value), "Value must be a string, collection, or array.", null, new Dictionary<string, object?>
            {
                { "value", value },
                { "length", Length }
            })
        };
    }
    
    public override void Validate(object? value, string propertyName)
    {
        switch (value)
        {
            case string str:
                str.ValidateIsLength(Length, propertyName);
                break;
            case ICollection<object?> collection:
                collection.ValidateIsLength(Length, propertyName);
                break;
            default:
                throw new ValidationException("IsLength", propertyName, $"{propertyName} must be a string, collection, or array.", null, new Dictionary<string, object?>
                {
                    { "value", value },
                    { "length", Length }
                });
        }
    }
}
