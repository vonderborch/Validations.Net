using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsCountAttribute(int count)
    : ValidatorAttribute(new IsCountValidator(count));
