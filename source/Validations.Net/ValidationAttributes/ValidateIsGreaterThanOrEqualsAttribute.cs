using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsGreaterThanOrEqualsAttribute(object comparand)
    : ValidatorAttribute(new IsGreaterThanOrEqualsValidator(comparand));
