using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsNullOrEmptyAttribute() : ValidatorAttribute(NullOrEmptyValidator.Instance);
