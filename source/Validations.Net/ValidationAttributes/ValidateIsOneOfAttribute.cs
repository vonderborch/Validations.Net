using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsOneOfAttribute(params object[] values)
    : ValidatorAttribute(new IsOneOfValidator(values));
