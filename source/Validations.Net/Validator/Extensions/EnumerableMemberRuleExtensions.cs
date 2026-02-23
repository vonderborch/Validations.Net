using System.Collections;
using Validations.Net.Validators;

namespace Validations.Net;

public static class EnumerableMemberRuleExtensions
{
    public static MemberRule<TParent, T, TMember> IsDistinct<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : IEnumerable
        => rule.Apply(DistinctValidator.Instance, severity, message);

    public static MemberRule<TParent, T, TMember> NotDistinct<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : IEnumerable
        => rule.Apply(NotDistinctValidator.Instance, severity, message);

    public static MemberRule<TParent, T, TMember> IsSingle<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : IEnumerable
        => rule.Apply(SingleValidator.Instance, severity, message);

    public static MemberRule<TParent, T, TMember> NotSingle<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : IEnumerable
        => rule.Apply(NotSingleValidator.Instance, severity, message);

    public static MemberRule<TParent, T, TMember> IsSorted<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        bool descending = false,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : IEnumerable
        => rule.Apply(new IsSortedValidator(descending), severity, message);

    public static MemberRule<TParent, T, TMember> NotSorted<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        bool descending = false,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : IEnumerable
        => rule.Apply(new NotSortedValidator(descending), severity, message);

    public static MemberRule<TParent, T, TMember> IsCount<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        int count,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : IEnumerable
        => rule.Apply(new IsCountValidator(count), severity, message);

    public static MemberRule<TParent, T, TMember> NotCount<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        int count,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : IEnumerable
        => rule.Apply(new NotCountValidator(count), severity, message);

    public static MemberRule<TParent, T, TMember> IsLength<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        int length,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : IEnumerable
        => rule.Apply(new IsLengthValidator(length), severity, message);

    public static MemberRule<TParent, T, TMember> NotLength<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        int length,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : IEnumerable
        => rule.Apply(new NotLengthValidator(length), severity, message);
}
