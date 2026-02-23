using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateDoesNotEndWithAttribute(string suffix)
    : ValidatorAttribute(new DoesNotEndWithValidator(suffix));
