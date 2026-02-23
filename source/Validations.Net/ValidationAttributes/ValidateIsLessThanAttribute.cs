using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsLessThanAttribute(object comparand)
    : ValidatorAttribute(new IsLessThanValidator(comparand));
