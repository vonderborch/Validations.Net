using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsNotNullOrEmptyAttribute() : ValidatorAttribute(NotNullOrEmptyValidator.Instance);
