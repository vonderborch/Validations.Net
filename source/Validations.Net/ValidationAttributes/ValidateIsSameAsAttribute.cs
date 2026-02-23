using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsSameAsAttribute(object? other)
    : ValidatorAttribute(new IsSameAsValidator(other));
