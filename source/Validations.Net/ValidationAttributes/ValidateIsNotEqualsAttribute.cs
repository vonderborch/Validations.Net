using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsNotEqualsAttribute(object? expected)
    : ValidatorAttribute(new NotEqualsValidator(expected));
