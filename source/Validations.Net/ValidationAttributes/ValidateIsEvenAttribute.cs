using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsEvenAttribute() : ValidatorAttribute(EvenValidator.Instance);
