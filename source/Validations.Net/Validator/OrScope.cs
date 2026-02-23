using System.Runtime.CompilerServices;
using Validations.Net.ValidationSets;

namespace Validations.Net;

/// <summary>
/// An OR scope within a <see cref="Validator{T}"/>. The group passes if ANY rule inside passes.
/// If ALL rules fail, all failures are reported. Call <see cref="End"/> to close the scope.
/// </summary>
public sealed class OrScope<T>
{
    private readonly Validator<T> _parent;
    private readonly List<(IValidationStep<T> Step, ValidationSeverity Severity)> _steps = new();

    internal OrScope(Validator<T> parent)
    {
        _parent = parent;
    }

    /// <summary>
    /// Begins a rule chain for a member within this OR scope.
    /// </summary>
    public MemberRule<OrScope<T>, T, TMember> For<TMember>(
        Func<T, TMember> selector,
        Func<T, bool>? when = null,
        Func<T, bool>? unless = null,
        [CallerArgumentExpression(nameof(selector))] string? selectorExpression = null)
    {
        var memberPath = ValidationSetBuilder.ExtractMemberPath(selectorExpression);
        var addStep = ConditionalAddStep(when, unless);
        return new MemberRule<OrScope<T>, T, TMember>(this, addStep, selector, memberPath);
    }

    /// <summary>
    /// Begins a rule chain that validates each element in a collection within this OR scope.
    /// </summary>
    public MemberRule<OrScope<T>, T, TMember> ForEach<TItem, TMember>(
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

        return new MemberRule<OrScope<T>, T, TMember>(this, addStep, extractor, $"{collectionPath}[].{memberPath}");
    }

    /// <summary>
    /// Begins a rule chain that validates each element in a collection directly within this OR scope.
    /// </summary>
    public MemberRule<OrScope<T>, T, TItem> ForEach<TItem>(
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

        return new MemberRule<OrScope<T>, T, TItem>(this, addStep, extractor, $"{collectionPath}[]");
    }

    /// <summary>
    /// Closes this OR scope. All collected rules are packaged into an <see cref="OrRuleGroup{T}"/>
    /// and added to the parent validator.
    /// </summary>
    public Validator<T> End()
    {
        _parent.AddStep(new OrRuleGroup<T>(_steps.ToArray()), ValidationSeverity.Error);
        return _parent;
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
