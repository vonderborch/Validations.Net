using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateStartsWithAttribute(string prefix)
    : ValidatorAttribute(new StartsWithValidator(prefix));
