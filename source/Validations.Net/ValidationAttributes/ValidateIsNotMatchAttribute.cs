using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsNotMatchAttribute(string pattern)
    : ValidatorAttribute(new NotMatchValidator(pattern));
