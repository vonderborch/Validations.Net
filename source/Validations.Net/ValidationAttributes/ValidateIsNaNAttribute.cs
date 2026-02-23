using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsNaNAttribute() : ValidatorAttribute(NaNValidator.Instance);
