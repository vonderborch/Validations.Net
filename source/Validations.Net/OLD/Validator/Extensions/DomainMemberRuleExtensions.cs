using Validations.Net.OLD.Validators.Age;
using Validations.Net.OLD.Validators.FileSystem;
using Validations.Net.OLD.Validators.Streams;

namespace Validations.Net.OLD.Validator.Extensions;

public static class DomainMemberRuleExtensions
{
    public static MemberRule<TParent, T, Stream> CanRead<TParent, T>(
        this MemberRule<TParent, T, Stream> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(CanReadValidator.Instance, severity, message);

    public static MemberRule<TParent, T, Stream> CanWrite<TParent, T>(
        this MemberRule<TParent, T, Stream> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(CanWriteValidator.Instance, severity, message);

    public static MemberRule<TParent, T, Stream> CanSeek<TParent, T>(
        this MemberRule<TParent, T, Stream> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(CanSeekValidator.Instance, severity, message);

    public static MemberRule<TParent, T, int> IsValidAge<TParent, T>(
        this MemberRule<TParent, T, int> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(ValidAgeValidator.Instance, severity, message);

    public static MemberRule<TParent, T, int> IsAdult<TParent, T>(
        this MemberRule<TParent, T, int> rule,
        int adultAge = 18,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(new AdultValidator(adultAge), severity, message);

    public static MemberRule<TParent, T, int> IsMinor<TParent, T>(
        this MemberRule<TParent, T, int> rule,
        int adultAge = 18,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(new MinorValidator(adultAge), severity, message);

    public static MemberRule<TParent, T, int> WithinAgeRange<TParent, T>(
        this MemberRule<TParent, T, int> rule,
        int minAge,
        int maxAge,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(new WithinAgeRangeValidator(minAge, maxAge), severity, message);

    public static MemberRule<TParent, T, string?> FileExists<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(FileExistsValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> FileNotExists<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(FileNotExistsValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> DirectoryExists<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(DirectoryExistsValidator.Instance, severity, message);

    public static MemberRule<TParent, T, string?> DirectoryNotExists<TParent, T>(
        this MemberRule<TParent, T, string?> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(DirectoryNotExistsValidator.Instance, severity, message);
}
