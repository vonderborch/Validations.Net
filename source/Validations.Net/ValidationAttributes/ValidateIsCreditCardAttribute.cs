using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsCreditCardAttribute() : ValidatorAttribute(CreditCardValidator.Instance);
