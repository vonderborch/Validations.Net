using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsNotOneOfAttribute(params object[] values)
    : ValidatorAttribute(new IsNotOneOfValidator(values));
