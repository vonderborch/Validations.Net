using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsFiniteAttribute() : ValidatorAttribute(FiniteValidator.Instance);
