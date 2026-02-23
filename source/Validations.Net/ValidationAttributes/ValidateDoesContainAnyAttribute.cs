using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateDoesContainAnyAttribute(params string[] values)
    : ValidatorAttribute(new DoesContainAnyValidator(values));
