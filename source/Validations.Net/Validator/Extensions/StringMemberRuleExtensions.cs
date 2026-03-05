using Validations.Net.Validators;
using Validations.Net.Validators.DataFormat;
using Validations.Net.Validators.Finance;
using Validations.Net.Validators.Geography;
using Validations.Net.Validators.Identifiers;
using Validations.Net.Validators.Network;
using Validations.Net.Validators.Phone;
using Validations.Net.Validators.Security;
using Validations.Net.Validators.Version;

namespace Validations.Net;

public static class StringMemberRuleExtensions
{
    public static MemberRule<TParent, T, string?> NotEmpty<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(NotEmptyValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> NotNullOrEmpty<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(NotNullOrEmptyValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> NullOrEmpty<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(NullOrEmptyValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> Empty<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(EmptyValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> NotNullOrWhiteSpace<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(NotNullOrWhiteSpaceValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> NullOrWhiteSpace<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error)
        => rule.Apply(NullOrWhiteSpaceValidator.Instance, severity);

    public static MemberRule<TParent, T, string?> WhiteSpace<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(WhiteSpaceValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> NotWhiteSpace<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(NotWhiteSpaceValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> IsCreditCard<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(CreditCardValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> IsValidEmail<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(ValidEmailValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> IsValidJson<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(ValidJsonValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> IsNotValidJson<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(NotValidJsonValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> IsValidXml<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(ValidXmlValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> IsNotValidXml<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(NotValidXmlValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> IsValidBase64<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(ValidBase64Validator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> IsNotValidBase64<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(NotValidBase64Validator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> IsValidPhoneNumber<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(ValidPhoneNumberValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> IsValidGuid<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(ValidGuidValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> IsNotValidGuid<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(NotValidGuidValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> IsValidSemanticVersion<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(ValidSemanticVersionValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> IsValidIban<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(ValidIbanValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> IsValidBicSwift<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(ValidBicSwiftValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> IsValidIpAddress<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(ValidIpAddressValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> IsValidHostname<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(ValidHostnameValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> IsValidLatitude<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(ValidLatitudeValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> IsValidLongitude<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(ValidLongitudeValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> MinLength<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        int minLength,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(new MinLengthValidator(minLength), severity, message);

    public static MemberRule<TParent, T, string?> MaxLength<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        int maxLength,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(new MaxLengthValidator(maxLength), severity, message);

    public static MemberRule<TParent, T, string?> Match<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        string pattern,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(new IsMatchValidator(pattern), severity, message);

    public static MemberRule<TParent, T, string?> NotMatch<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        string pattern,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(new NotMatchValidator(pattern), severity, message);

    public static MemberRule<TParent, T, string?> Contains<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        string substring,
        StringComparison comparison = StringComparison.Ordinal,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(new DoesContainValidator(substring, comparison), severity, message);

    public static MemberRule<TParent, T, string?> DoesNotContain<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        string substring,
        StringComparison comparison = StringComparison.Ordinal,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(new DoesNotContainValidator(substring, comparison), severity, message);

    public static MemberRule<TParent, T, string?> ContainsAll<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity, string? message,
        params string[] substrings)
        => rule.Apply(new DoesContainAllValidator(substrings), severity, message);

    public static MemberRule<TParent, T, string?> ContainsAll<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        params string[] substrings)
        => rule.ContainsAll(ValidationSeverity.Error, null, substrings);

    public static MemberRule<TParent, T, string?> ContainsAny<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity, string? message,
        params string[] substrings)
        => rule.Apply(new DoesContainAnyValidator(substrings), severity, message);

    public static MemberRule<TParent, T, string?> ContainsAny<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        params string[] substrings)
        => rule.ContainsAny(ValidationSeverity.Error, null, substrings);

    public static MemberRule<TParent, T, string?> DoesNotContainAll<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity, string? message,
        params string[] substrings)
        => rule.Apply(new DoesNotContainAllValidator(substrings), severity, message);

    public static MemberRule<TParent, T, string?> DoesNotContainAll<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        params string[] substrings)
        => rule.DoesNotContainAll(ValidationSeverity.Error, null, substrings);

    public static MemberRule<TParent, T, string?> DoesNotContainAny<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity, string? message,
        params string[] substrings)
        => rule.Apply(new DoesNotContainAnyValidator(substrings), severity, message);

    public static MemberRule<TParent, T, string?> DoesNotContainAny<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        params string[] substrings)
        => rule.DoesNotContainAny(ValidationSeverity.Error, null, substrings);

    public static MemberRule<TParent, T, string?> StartsWith<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        string prefix,
        StringComparison comparison = StringComparison.Ordinal,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(new StartsWithValidator(prefix, comparison), severity, message);

    public static MemberRule<TParent, T, string?> EndsWith<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        string suffix,
        StringComparison comparison = StringComparison.Ordinal,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(new EndsWithValidator(suffix, comparison), severity, message);

    public static MemberRule<TParent, T, string?> DoesNotStartWith<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        string prefix,
        StringComparison comparison = StringComparison.Ordinal,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(new DoesNotStartWithValidator(prefix, comparison), severity, message);

    public static MemberRule<TParent, T, string?> DoesNotEndWith<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        string suffix,
        StringComparison comparison = StringComparison.Ordinal,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(new DoesNotEndWithValidator(suffix, comparison), severity, message);

    public static MemberRule<TParent, T, string?> IsValidUri<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        UriKind uriKind = UriKind.Absolute,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(new IsValidUriValidator(uriKind), severity, message);

    public static MemberRule<TParent, T, string?> IsSecurePassword<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        int minLength = 8,
        bool requireUppercase = true,
        bool requireLowercase = true,
        bool requireDigit = true,
        bool requireSpecialChar = true,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(new SecurePasswordValidator(minLength, requireUppercase, requireLowercase, requireDigit, requireSpecialChar), severity, message);

    public static MemberRule<TParent, T, string?> IsValidId<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        int minLength = 1,
        int maxLength = 255,
        string? allowedPattern = null,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(new ValidIdValidator(minLength, maxLength, allowedPattern), severity, message);

    public static MemberRule<TParent, T, string?> IsCompatibleVersion<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        string targetVersion,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(new CompatibleVersionValidator(targetVersion), severity, message);
}
