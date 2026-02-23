using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsLengthAttribute(int length)
    : ValidatorAttribute(new IsLengthValidator(length));
