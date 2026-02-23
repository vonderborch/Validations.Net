using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsNotSingleAttribute() : ValidatorAttribute(NotSingleValidator.Instance);
