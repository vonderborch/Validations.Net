using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsNotDistinctAttribute() : ValidatorAttribute(NotDistinctValidator.Instance);
