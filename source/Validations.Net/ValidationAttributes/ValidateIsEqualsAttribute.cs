using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsEqualsAttribute(object? expected)
    : ValidatorAttribute(new IsEqualsValidator(expected));
