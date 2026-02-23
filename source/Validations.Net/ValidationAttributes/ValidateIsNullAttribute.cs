using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsNullAttribute() : ValidatorAttribute(NullValidator.Instance);
