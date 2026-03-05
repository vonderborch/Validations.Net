using System.Runtime.CompilerServices;
using Validations.Net.ValidationSets;

namespace Validations.Net;

/// <summary>
/// Base class for validator scopes that share <see cref="For{TMember}"/>, <see cref="ForEach{TItem,TMember}"/>,
/// <see cref="Must"/>, <see cref="MustAsync"/>, and conditional step logic.
/// Uses the Curiously Recurring Template Pattern so fluent methods return the concrete scope type.
/// </summary>
/// <typeparam name="TSelf">The concrete scope type (CRTP).</typeparam>
/// <typeparam name="T">The root type being validated.</typeparam>
public abstract class ValidatorScope<TSelf, T> where TSelf : ValidatorScope<TSelf, T>
{
    private protected readonly List<(IValidationStep<T> Step, ValidationSeverity Severity)> Steps = new();

    internal virtual void AddStep(IValidationStep<T> step, ValidationSeverity severity)
        => Steps.Add((step, severity));

    /// <summary>
    /// Begins a rule chain for a member selected by <paramref name="selector"/>.
    /// When <paramref name="when"/> is provided, the rules only execute if the condition is true.
    /// When <paramref name="unless"/> is provided, the rules are skipped if the condition is true.
    /// Call <c>.End()</c> on the returned <see cref="MemberRule{TParent,T,TMember}"/> to return to this scope.
    /// </summary>
    public MemberRule<TSelf, T, TMember> For<TMember>(
        Func<T, TMember> selector,
        Func<T, bool>? when = null,
        Func<T, bool>? unless = null,
        [CallerArgumentExpression(nameof(selector))] string? selectorExpression = null)
    {
        var memberPath = ValidationSetBuilder.ExtractMemberPath(selectorExpression);
        var addStep = ConditionalAddStep(when, unless);
        return new MemberRule<TSelf, T, TMember>((TSelf)this, addStep, selector, memberPath, Steps);
    }

    /// <summary>
    /// Begins a rule chain that validates each element in a collection.
    /// </summary>
    public MemberRule<TSelf, T, TMember> ForEach<TItem, TMember>(
        Func<T, IEnumerable<TItem>> collectionSelector,
        Func<TItem, TMember> memberSelector,
        Func<T, bool>? when = null,
        Func<T, bool>? unless = null,
        [CallerArgumentExpression(nameof(collectionSelector))] string? collectionExpression = null,
        [CallerArgumentExpression(nameof(memberSelector))] string? memberExpression = null)
    {
        var collectionPath = ValidationSetBuilder.ExtractMemberPath(collectionExpression);
        var memberPath = ValidationSetBuilder.ExtractMemberPath(memberExpression);
        var addStep = ConditionalAddStep(when, unless);

        Func<T, IEnumerable<(TMember Member, string Path)>> extractor = root =>
            collectionSelector(root).Select((item, index) =>
                (memberSelector(item), $"{collectionPath}[{index}].{memberPath}"));

        return new MemberRule<TSelf, T, TMember>((TSelf)this, addStep, extractor, $"{collectionPath}[].{memberPath}", Steps);
    }

    /// <summary>
    /// Begins a rule chain that validates each element in a collection directly.
    /// </summary>
    public MemberRule<TSelf, T, TItem> ForEach<TItem>(
        Func<T, IEnumerable<TItem>> collectionSelector,
        Func<T, bool>? when = null,
        Func<T, bool>? unless = null,
        [CallerArgumentExpression(nameof(collectionSelector))] string? collectionExpression = null)
    {
        var collectionPath = ValidationSetBuilder.ExtractMemberPath(collectionExpression);
        var addStep = ConditionalAddStep(when, unless);

        Func<T, IEnumerable<(TItem Member, string Path)>> extractor = root =>
            collectionSelector(root).Select((item, index) =>
                (item, $"{collectionPath}[{index}]"));

        return new MemberRule<TSelf, T, TItem>((TSelf)this, addStep, extractor, $"{collectionPath}[]", Steps);
    }

    /// <summary>
    /// Adds a root-level custom predicate that receives the full object being validated.
    /// Use for cross-member validation (e.g., "start date must be before end date").
    /// </summary>
    public TSelf Must(Func<T, bool> predicate, string failureMessage,
        ValidationSeverity severity = ValidationSeverity.Error)
    {
        AddStep(new DelegateValidationStep<T>((value, bb) =>
        {
            if (predicate(value))
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure("Must", failureMessage, null, bb,
                [("value", value)]);
        }), severity);
        return (TSelf)this;
    }

    /// <summary>
    /// Adds a root-level async predicate that receives the full object being validated.
    /// Use for cross-member async validation (e.g., database checks spanning multiple members).
    /// The resulting step requires async execution.
    /// </summary>
    public TSelf MustAsync(Func<T, CancellationToken, Task<bool>> predicate,
        string failureMessage, ValidationSeverity severity = ValidationSeverity.Error)
    {
        AddStep(new AsyncDelegateValidationStep<T>(async (value, bb, ct) =>
        {
            if (await predicate(value, ct).ConfigureAwait(false))
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure("MustAsync", failureMessage, null, bb,
                [("value", value)]);
        }), severity);
        return (TSelf)this;
    }

    /// <summary>
    /// Opens a conditional scope. Rules added inside the scope only execute when <paramref name="condition"/> is true.
    /// </summary>
    public NestedWhenScope<TSelf, T> When(Func<T, bool> condition) => new((TSelf)this, condition);

    /// <summary>
    /// Opens a conditional scope. Rules added inside the scope only execute when <paramref name="condition"/> is false.
    /// </summary>
    public NestedWhenScope<TSelf, T> Unless(Func<T, bool> condition) => new((TSelf)this, x => !condition(x));

    /// <summary>
    /// Opens an OR scope. The group passes if ANY rule inside passes.
    /// </summary>
    public NestedOrScope<TSelf, T> Or() => new((TSelf)this);

    /// <summary>
    /// Opens an AND scope. The group passes only if ALL rules inside pass.
    /// </summary>
    public NestedAndScope<TSelf, T> And() => new((TSelf)this);

    internal Action<IValidationStep<T>, ValidationSeverity> ConditionalAddStep(
        Func<T, bool>? when, Func<T, bool>? unless = null)
    {
        Func<T, bool>? condition = (when, unless) switch
        {
            (not null, not null) => x => when(x) && !unless(x),
            (not null, null) => when,
            (null, not null) => x => !unless(x),
            _ => null
        };
        if (condition is null) return AddStep;
        return (step, severity) =>
            AddStep(new ConditionalValidationStep<T>(condition, step), severity);
    }
}
