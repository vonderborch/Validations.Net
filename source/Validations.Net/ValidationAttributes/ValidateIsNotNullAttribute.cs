using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsNotNullAttribute() : ValidatorAttribute(NotNullValidator.Instance);
