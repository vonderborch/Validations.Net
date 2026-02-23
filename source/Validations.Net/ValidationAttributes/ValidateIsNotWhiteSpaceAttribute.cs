using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsNotWhiteSpaceAttribute() : ValidatorAttribute(NotWhiteSpaceValidator.Instance);
