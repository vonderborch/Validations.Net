using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsNotOneOfAttribute<T> : ValidationAttribute
{
    private readonly ICollection<T> _options;

    public ValidateIsNotOneOfAttribute(params T[] options) : base("IsNotOneOf")
    {
        string paramName = nameof(options);
        options.ValidateIsNotNull(paramName).ValidateIsNotEmpty(paramName);
        _options = options;
    }

    public ValidateIsNotOneOfAttribute(ICollection<T> options) : base("IsNotOneOf")
    {
        string paramName = nameof(options);
        options.ValidateIsNotNull(paramName).ValidateIsNotEmpty(paramName);
        _options = options;
    }

    public override bool Check(object? value)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        return typedValue.CheckIsNotOneOf(_options);
    }

    public override void Validate(object? value, string propertyName)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        typedValue.ValidateIsNotOneOf(_options, propertyName);
    }
}
