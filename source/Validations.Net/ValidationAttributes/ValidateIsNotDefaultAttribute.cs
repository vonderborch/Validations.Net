using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsNotDefaultAttribute() : ValidatorAttribute(NotDefaultValidator.Instance);
