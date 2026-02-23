using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsDefaultAttribute() : ValidatorAttribute(DefaultValidator.Instance);
