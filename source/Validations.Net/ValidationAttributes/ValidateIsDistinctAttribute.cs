using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsDistinctAttribute() : ValidatorAttribute(DistinctValidator.Instance);
