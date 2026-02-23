using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsGreaterThanAttribute(object comparand)
    : ValidatorAttribute(new IsGreaterThanValidator(comparand));
