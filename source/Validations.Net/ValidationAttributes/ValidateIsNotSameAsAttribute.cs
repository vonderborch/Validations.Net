using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsNotSameAsAttribute(object? other)
    : ValidatorAttribute(new NotSameAsValidator(other));
