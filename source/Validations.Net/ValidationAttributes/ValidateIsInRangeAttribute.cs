using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsInRangeAttribute(object min, object max, bool minInclusive = true, bool maxInclusive = true)
    : ValidatorAttribute(new InRangeValidator(min, max, minInclusive, maxInclusive));
