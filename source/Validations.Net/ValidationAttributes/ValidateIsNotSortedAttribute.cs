using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsNotSortedAttribute(bool descending = false)
    : ValidatorAttribute(new NotSortedValidator(descending));
