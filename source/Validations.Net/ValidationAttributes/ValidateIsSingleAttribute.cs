using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsSingleAttribute() : ValidatorAttribute(SingleValidator.Instance);
