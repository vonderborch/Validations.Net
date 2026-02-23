using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateMinLengthAttribute(int minLength)
    : ValidatorAttribute(new MinLengthValidator(minLength));
