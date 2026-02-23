using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsNullOrWhiteSpaceAttribute() : ValidatorAttribute(NullOrWhiteSpaceValidator.Instance);
