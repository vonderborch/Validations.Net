using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsLessThanOrEqualsAttribute(object comparand)
    : ValidatorAttribute(new IsLessThanOrEqualsValidator(comparand));
