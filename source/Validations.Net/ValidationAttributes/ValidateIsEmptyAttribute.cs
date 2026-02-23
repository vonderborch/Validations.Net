using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsEmptyAttribute() : ValidatorAttribute(EmptyValidator.Instance);
