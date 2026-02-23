using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsSortedAttribute(bool descending = false)
    : ValidatorAttribute(new IsSortedValidator(descending));
