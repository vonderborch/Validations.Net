using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsNotElementOfAttribute(params object?[] disallowedValues)
    : ValidatorAttribute(new IsNotElementOfValidator(disallowedValues));
