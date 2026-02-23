using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsWhiteSpaceAttribute() : ValidatorAttribute(WhiteSpaceValidator.Instance);
