using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateDoesNotStartWithAttribute(string prefix)
    : ValidatorAttribute(new DoesNotStartWithValidator(prefix));
