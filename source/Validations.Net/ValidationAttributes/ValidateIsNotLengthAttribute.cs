using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsNotLengthAttribute(int length)
    : ValidatorAttribute(new NotLengthValidator(length));
