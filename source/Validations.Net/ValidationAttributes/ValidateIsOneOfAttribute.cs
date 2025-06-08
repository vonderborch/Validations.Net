using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsOneOfAttribute<T> : ValidationAttribute
{
    private readonly ICollection<T> _options;

    public ValidateIsOneOfAttribute(params T[] options) : base("IsOneOf")
    {
        string paramName = nameof(options);
        options.ValidateIsNotNull(paramName).ValidateIsNotEmpty(paramName);
        _options = options;
    }

    public ValidateIsOneOfAttribute(ICollection<T> options) : base("IsOneOf")
    {
        string paramName = nameof(options);
        options.ValidateIsNotNull(paramName).ValidateIsNotEmpty(paramName);
        _options = options;
    }

    public override bool Check(object? value)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        return typedValue.CheckIsOneOf(_options);
    }

    public override void Validate(object? value, string propertyName)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        typedValue.ValidateIsOneOf(_options, propertyName);
    }
}
