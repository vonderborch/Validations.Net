using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateDoesNotContainAllAttribute(params string[] values)
    : ValidatorAttribute(new DoesNotContainAllValidator(values));
