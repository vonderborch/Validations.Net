using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsNotInRangeAttribute(object min, object max, bool minInclusive = true, bool maxInclusive = true)
    : ValidatorAttribute(new NotInRangeValidator(min, max, minInclusive, maxInclusive));
