using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsNotCountAttribute(int count)
    : ValidatorAttribute(new NotCountValidator(count));
