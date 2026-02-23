using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateDoesNotContainAnyAttribute(params string[] values)
    : ValidatorAttribute(new DoesNotContainAnyValidator(values));
