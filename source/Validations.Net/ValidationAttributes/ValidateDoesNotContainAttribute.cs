using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateDoesNotContainAttribute(string substring)
    : ValidatorAttribute(new DoesNotContainValidator(substring));
