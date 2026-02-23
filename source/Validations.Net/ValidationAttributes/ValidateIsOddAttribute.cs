using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsOddAttribute() : ValidatorAttribute(OddValidator.Instance);
