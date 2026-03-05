using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net.OLD.ValidationSets;

namespace Validations.Net.OLD.Validator;

/// <summary>
/// Extension methods on <see cref="MemberRule{TParent,T,TMember}"/> that auto-call <c>.End()</c>
/// and delegate to the parent scope. This makes <c>.End()</c> optional when chaining rules.
/// <para>
/// <c>.End()</c> is still required when using <see cref="MemberRule{TParent,T,TMember}.Cascade"/> mode.
/// </para>
/// </summary>
public static class MemberRuleForwardingExtensions
{
    // ──────────────────────────────────────────────
    //  Methods available on any scope (ValidatorScope<TScope, T>)
    // ──────────────────────────────────────────────

    public static MemberRule<TScope, T, TNewMember> For<TScope, T, TMember, TNewMember>(
        this MemberRule<TScope, T, TMember> rule,
        Func<T, TNewMember> selector,
        Func<T, bool>? when = null,
        Func<T, bool>? unless = null,
        [CallerArgumentExpression(nameof(selector))] string? selectorExpression = null)
        where TScope : ValidatorScope<TScope, T>
        => rule.End().For(selector, when, unless, selectorExpression);

    public static MemberRule<TScope, T, TNewMember> ForEach<TScope, T, TMember, TItem, TNewMember>(
        this MemberRule<TScope, T, TMember> rule,
        Func<T, IEnumerable<TItem>> collectionSelector,
        Func<TItem, TNewMember> memberSelector,
        Func<T, bool>? when = null,
        Func<T, bool>? unless = null,
        [CallerArgumentExpression(nameof(collectionSelector))] string? collectionExpression = null,
        [CallerArgumentExpression(nameof(memberSelector))] string? memberExpression = null)
        where TScope : ValidatorScope<TScope, T>
        => rule.End().ForEach(collectionSelector, memberSelector, when, unless, collectionExpression, memberExpression);

    public static MemberRule<TScope, T, TItem> ForEach<TScope, T, TMember, TItem>(
        this MemberRule<TScope, T, TMember> rule,
        Func<T, IEnumerable<TItem>> collectionSelector,
        Func<T, bool>? when = null,
        Func<T, bool>? unless = null,
        [CallerArgumentExpression(nameof(collectionSelector))] string? collectionExpression = null)
        where TScope : ValidatorScope<TScope, T>
        => rule.End().ForEach(collectionSelector, when, unless, collectionExpression);

    public static TScope Must<TScope, T, TMember>(
        this MemberRule<TScope, T, TMember> rule,
        Func<T, bool> predicate, string failureMessage,
        ValidationSeverity severity = ValidationSeverity.Error)
        where TScope : ValidatorScope<TScope, T>
        => rule.End().Must(predicate, failureMessage, severity);

    public static TScope MustAsync<TScope, T, TMember>(
        this MemberRule<TScope, T, TMember> rule,
        Func<T, CancellationToken, Task<bool>> predicate,
        string failureMessage, ValidationSeverity severity = ValidationSeverity.Error)
        where TScope : ValidatorScope<TScope, T>
        => rule.End().MustAsync(predicate, failureMessage, severity);

    // ──────────────────────────────────────────────
    //  Methods only available on top-level Validator<T>
    // ──────────────────────────────────────────────

    public static WhenScope<T> When<T, TMember>(
        this MemberRule<Validator<T>, T, TMember> rule,
        Func<T, bool> condition)
        => rule.End().When(condition);

    public static WhenScope<T> Unless<T, TMember>(
        this MemberRule<Validator<T>, T, TMember> rule,
        Func<T, bool> condition)
        => rule.End().Unless(condition);

    public static OrScope<T> Or<T, TMember>(
        this MemberRule<Validator<T>, T, TMember> rule)
        => rule.End().Or();

    public static AndScope<T> And<T, TMember>(
        this MemberRule<Validator<T>, T, TMember> rule)
        => rule.End().And();

    public static Validator<T> Include<T, TMember>(
        this MemberRule<Validator<T>, T, TMember> rule,
        Validator<T> other)
        => rule.End().Include(other);

    public static Validator<T> Include<T, TMember>(
        this MemberRule<Validator<T>, T, TMember> rule,
        AbstractValidator<T> other)
        => rule.End().Include(other);

    public static ValidationResult Validate<T, TMember>(
        this MemberRule<Validator<T>, T, TMember> rule,
        T value, IBlackboard? blackboard = null)
        => rule.End().Validate(value, blackboard);

    public static Task<ValidationResult> ValidateAsync<T, TMember>(
        this MemberRule<Validator<T>, T, TMember> rule,
        T value, IBlackboard? blackboard = null, CancellationToken cancellationToken = default)
        => rule.End().ValidateAsync(value, blackboard, cancellationToken);

    public static bool Check<T, TMember>(
        this MemberRule<Validator<T>, T, TMember> rule,
        T value, IBlackboard? blackboard = null)
        => rule.End().Check(value, blackboard);

    public static Task<bool> CheckAsync<T, TMember>(
        this MemberRule<Validator<T>, T, TMember> rule,
        T value, IBlackboard? blackboard = null, CancellationToken cancellationToken = default)
        => rule.End().CheckAsync(value, blackboard, cancellationToken);

    public static T Ensure<T, TMember>(
        this MemberRule<Validator<T>, T, TMember> rule,
        T value, IBlackboard? blackboard = null,
        string validationFailureMessage = "Validation failed",
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        => rule.End().Ensure(value, blackboard, validationFailureMessage, parameterName);

    public static Task<T> EnsureAsync<T, TMember>(
        this MemberRule<Validator<T>, T, TMember> rule,
        T value, IBlackboard? blackboard = null,
        string validationFailureMessage = "Validation failed",
        CancellationToken cancellationToken = default)
        => rule.End().EnsureAsync(value, blackboard, validationFailureMessage, cancellationToken);

    public static ValidationSet<T> Build<T, TMember>(
        this MemberRule<Validator<T>, T, TMember> rule)
        => rule.End().Build();
}
