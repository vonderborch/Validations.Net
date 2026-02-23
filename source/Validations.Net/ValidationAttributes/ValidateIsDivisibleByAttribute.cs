using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsDivisibleByAttribute(long divisor)
    : ValidatorAttribute(new IsDivisibleByValidator(divisor));
