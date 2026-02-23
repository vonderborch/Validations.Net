using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsElementOfAttribute(params object?[] allowedValues)
    : ValidatorAttribute(new IsElementOfValidator(allowedValues));
