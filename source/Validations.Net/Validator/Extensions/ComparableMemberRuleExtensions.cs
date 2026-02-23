using Validations.Net.Validators;

namespace Validations.Net;

public static class ComparableMemberRuleExtensions
{
    public static MemberRule<TParent, T, TMember> InRange<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        TMember min, TMember max, bool minInclusive = true, bool maxInclusive = true,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : IComparable<TMember>
        => rule.Apply(new InRangeValidator<TMember>(min, max, minInclusive, maxInclusive), severity, message);

    public static MemberRule<TParent, T, TMember> NotInRange<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        TMember min, TMember max, bool minInclusive = true, bool maxInclusive = true,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : IComparable<TMember>
        => rule.Apply(new NotInRangeValidator<TMember>(min, max, minInclusive, maxInclusive), severity, message);

    public static MemberRule<TParent, T, TMember> GreaterThan<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        TMember comparand,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : IComparable<TMember>
        => rule.Apply(new IsGreaterThanValidator((object?)comparand), severity, message);

    public static MemberRule<TParent, T, TMember> GreaterThanOrEquals<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        TMember comparand,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : IComparable<TMember>
        => rule.Apply(new IsGreaterThanOrEqualsValidator((object?)comparand), severity, message);

    public static MemberRule<TParent, T, TMember> LessThan<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        TMember comparand,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : IComparable<TMember>
        => rule.Apply(new IsLessThanValidator((object?)comparand), severity, message);

    public static MemberRule<TParent, T, TMember> LessThanOrEquals<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        TMember comparand,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : IComparable<TMember>
        => rule.Apply(new IsLessThanOrEqualsValidator((object?)comparand), severity, message);
}
