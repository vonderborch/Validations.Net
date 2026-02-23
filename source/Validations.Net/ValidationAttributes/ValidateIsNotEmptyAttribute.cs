using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsNotEmptyAttribute() : ValidatorAttribute(NotEmptyValidator.Instance);
