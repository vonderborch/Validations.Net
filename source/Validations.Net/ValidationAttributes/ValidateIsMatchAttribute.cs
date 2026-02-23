using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsMatchAttribute(string pattern)
    : ValidatorAttribute(new IsMatchValidator(pattern));
