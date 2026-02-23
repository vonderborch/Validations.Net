using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateDoesContainAttribute(string substring)
    : ValidatorAttribute(new DoesContainValidator(substring));
