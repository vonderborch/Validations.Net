using Validations.Net.Validators;
using Validations.Net.Validators.DateTime;

namespace Validations.Net;

public static class DateTimeMemberRuleExtensions
{
    public static MemberRule<TParent, T, DateTime> InPast<TParent, T>(
        this MemberRule<TParent, T, DateTime> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(InPastValidator.Instance, severity, message);

    public static MemberRule<TParent, T, DateTimeOffset> InPast<TParent, T>(
        this MemberRule<TParent, T, DateTimeOffset> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(InPastValidator.Instance, severity, message);

    public static MemberRule<TParent, T, DateTime> InFuture<TParent, T>(
        this MemberRule<TParent, T, DateTime> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(InFutureValidator.Instance, severity, message);

    public static MemberRule<TParent, T, DateTimeOffset> InFuture<TParent, T>(
        this MemberRule<TParent, T, DateTimeOffset> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(InFutureValidator.Instance, severity, message);

    public static MemberRule<TParent, T, DateTime> IsWeekend<TParent, T>(
        this MemberRule<TParent, T, DateTime> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(IsWeekendValidator.Instance, severity, message);

    public static MemberRule<TParent, T, DateTimeOffset> IsWeekend<TParent, T>(
        this MemberRule<TParent, T, DateTimeOffset> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(IsWeekendValidator.Instance, severity, message);

    public static MemberRule<TParent, T, DateTime> IsBusinessDay<TParent, T>(
        this MemberRule<TParent, T, DateTime> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(IsBusinessDayValidator.Instance, severity, message);

    public static MemberRule<TParent, T, DateTimeOffset> IsBusinessDay<TParent, T>(
        this MemberRule<TParent, T, DateTimeOffset> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(IsBusinessDayValidator.Instance, severity, message);

    public static MemberRule<TParent, T, DateTime> IsDateOnly<TParent, T>(
        this MemberRule<TParent, T, DateTime> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(IsDateOnlyValidator.Instance, severity, message);

    public static MemberRule<TParent, T, DateTimeOffset> IsDateOnly<TParent, T>(
        this MemberRule<TParent, T, DateTimeOffset> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(IsDateOnlyValidator.Instance, severity, message);

    public static MemberRule<TParent, T, DateTime> IsTimeOnly<TParent, T>(
        this MemberRule<TParent, T, DateTime> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(IsTimeOnlyValidator.Instance, severity, message);

    public static MemberRule<TParent, T, DateTimeOffset> IsTimeOnly<TParent, T>(
        this MemberRule<TParent, T, DateTimeOffset> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(IsTimeOnlyValidator.Instance, severity, message);

    public static MemberRule<TParent, T, DateTime> IsLeapYear<TParent, T>(
        this MemberRule<TParent, T, DateTime> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(IsLeapYearValidator.Instance, severity, message);

    public static MemberRule<TParent, T, DateTimeOffset> IsLeapYear<TParent, T>(
        this MemberRule<TParent, T, DateTimeOffset> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(IsLeapYearValidator.Instance, severity, message);

    public static MemberRule<TParent, T, DateTime> IsUtc<TParent, T>(
        this MemberRule<TParent, T, DateTime> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(IsUtcValidator.Instance, severity, message);

    public static MemberRule<TParent, T, DateTime> IsLocal<TParent, T>(
        this MemberRule<TParent, T, DateTime> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(IsLocalValidator.Instance, severity, message);

    public static MemberRule<TParent, T, DateTime> WithinDateRange<TParent, T>(
        this MemberRule<TParent, T, DateTime> rule,
        DateTime min,
        DateTime max,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        => rule.Apply(new WithinDateRangeValidator(min, max), severity, message);
}
