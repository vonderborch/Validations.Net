using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateDoesContainAllAttribute(params string[] values)
    : ValidatorAttribute(new DoesContainAllValidator(values));
