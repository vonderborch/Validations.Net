using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public sealed class ValidateIsValidUriAttribute(UriKind uriKind = UriKind.Absolute)
    : ValidatorAttribute(new IsValidUriValidator(uriKind));
