using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateMaxLengthAttribute(int maxLength)
    : ValidatorAttribute(new MaxLengthValidator(maxLength));
