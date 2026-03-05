using System.Numerics;
using Validations.Net.OLD.Validators;

namespace Validations.Net.OLD.Validator.Extensions;

public static class NumericMemberRuleExtensions
{
    public static MemberRule<TParent, T, TMember> IsEven<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : INumber<TMember>
        => rule.Apply(EvenValidator.Instance, severity, message);

    public static MemberRule<TParent, T, TMember> IsOdd<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : INumber<TMember>
        => rule.Apply(OddValidator.Instance, severity, message);

    public static MemberRule<TParent, T, TMember> IsPositive<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : INumber<TMember>
        => rule.Apply(PositiveValidator.Instance, severity, message);

    public static MemberRule<TParent, T, TMember> IsNegative<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : INumber<TMember>
        => rule.Apply(NegativeValidator.Instance, severity, message);

    public static MemberRule<TParent, T, TMember> IsZero<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : INumber<TMember>
        => rule.Apply(ZeroValidator.Instance, severity, message);

    public static MemberRule<TParent, T, TMember> NonZero<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : INumber<TMember>
        => rule.Apply(NonZeroValidator.Instance, severity, message);

    public static MemberRule<TParent, T, TMember> IsNaN<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : INumber<TMember>
        => rule.Apply(NaNValidator.Instance, severity, message);

    public static MemberRule<TParent, T, TMember> IsFinite<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : INumber<TMember>
        => rule.Apply(FiniteValidator.Instance, severity, message);

    public static MemberRule<TParent, T, TMember> IsDivisibleBy<TParent, T, TMember>(
        this MemberRule<TParent, T, TMember> rule,
        long divisor,
        ValidationSeverity severity = ValidationSeverity.Error,
        string? message = null)
        where TMember : INumber<TMember>
        => rule.Apply(new IsDivisibleByValidator(divisor), severity, message);
}
