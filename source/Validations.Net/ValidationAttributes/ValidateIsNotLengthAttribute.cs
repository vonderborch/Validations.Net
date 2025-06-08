using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsNotLengthAttribute : ValidationAttribute
{
    public int Length { get; }

    public ValidateIsNotLengthAttribute(int length) : base("IsNotLength")
    {
        length.ValidateIsGreaterThanOrEquals(0, nameof(length));
        Length = length;
    }

    public override bool Check(object? value)
    {
        return value switch
        {
            string str => str.CheckIsNotLength(Length),
            ICollection<object?> collection => collection.CheckIsNotLength(Length),
            _ => throw new ValidationException("IsNotLength", nameof(value), "Value must be a string, collection, or array.", null, new Dictionary<string, object?>
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
                str.ValidateIsNotLength(Length, propertyName);
                break;
            case ICollection<object?> collection:
                collection.ValidateIsNotLength(Length, propertyName);
                break;
            default:
                throw new ValidationException("IsNotLength", propertyName, $"{propertyName} must be a string, collection, or array.", null, new Dictionary<string, object?>
                {
                    { "value", value }
                });
        }
    }
}
