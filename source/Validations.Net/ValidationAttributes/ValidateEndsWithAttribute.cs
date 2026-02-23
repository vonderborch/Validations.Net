using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateEndsWithAttribute(string suffix)
    : ValidatorAttribute(new EndsWithValidator(suffix));
