using System.Runtime.CompilerServices;
using Validations.Net.ValidationSets;

namespace Validations.Net;

/// <summary>
/// A conditional scope within a <see cref="Validator{T}"/>. Rules added inside this scope
/// are only executed when the condition is true. Call <see cref="End"/> to close the scope,
/// or <see cref="Otherwise"/> to open an else branch.
/// </summary>
public sealed class WhenScope<T>
{
    private readonly Validator<T> _parent;
    private readonly Func<T, bool> _condition;
    private readonly List<(IValidationStep<T> Step, ValidationSeverity Severity)> _steps = new();

    internal WhenScope(Validator<T> parent, Func<T, bool> condition)
    {
        _parent = parent;
        _condition = condition;
    }

    /// <summary>
    /// Begins a rule chain for a member within this conditional scope.
    /// When <paramref name="when"/> is provided, the rules have an additional condition.
    /// </summary>
    public MemberRule<WhenScope<T>, T, TMember> For<TMember>(
        Func<T, TMember> selector,
        Func<T, bool>? when = null,
        Func<T, bool>? unless = null,
        [CallerArgumentExpression(nameof(selector))] string? selectorExpression = null)
    {
        var memberPath = ValidationSetBuilder.ExtractMemberPath(selectorExpression);
        var addStep = ConditionalAddStep(when, unless);
        return new MemberRule<WhenScope<T>, T, TMember>(this, addStep, selector, memberPath);
    }

    /// <summary>
    /// Begins a rule chain that validates each element in a collection within this conditional scope.
    /// </summary>
    public MemberRule<WhenScope<T>, T, TMember> ForEach<TItem, TMember>(
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

        return new MemberRule<WhenScope<T>, T, TMember>(this, addStep, extractor, $"{collectionPath}[].{memberPath}");
    }

    /// <summary>
    /// Begins a rule chain that validates each element in a collection directly within this conditional scope.
    /// </summary>
    public MemberRule<WhenScope<T>, T, TItem> ForEach<TItem>(
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

        return new MemberRule<WhenScope<T>, T, TItem>(this, addStep, extractor, $"{collectionPath}[]");
    }

    /// <summary>
    /// Closes this conditional scope. All collected rules are wrapped with the condition
    /// and added to the parent validator.
    /// </summary>
    public Validator<T> End()
    {
        FlushToParent();
        return _parent;
    }

    /// <summary>
    /// Opens an else branch. Rules added in the <see cref="OtherwiseScope{T}"/> execute
    /// when this scope's condition is false. The When-side rules are flushed to the parent immediately.
    /// </summary>
    public OtherwiseScope<T> Otherwise()
    {
        FlushToParent();
        return new OtherwiseScope<T>(_parent, x => !_condition(x));
    }

    private void FlushToParent()
    {
        foreach (var (step, severity) in _steps)
            _parent.AddStep(new ConditionalValidationStep<T>(_condition, step), severity);
    }

    private void AddStep(IValidationStep<T> step, ValidationSeverity severity)
        => _steps.Add((step, severity));

    private Action<IValidationStep<T>, ValidationSeverity> ConditionalAddStep(
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
            _steps.Add((new ConditionalValidationStep<T>(condition, step), severity));
    }
}
