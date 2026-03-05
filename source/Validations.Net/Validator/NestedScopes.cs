using Validations.Net.ValidationSets;

namespace Validations.Net;

/// <summary>
/// A conditional scope nested inside any <see cref="ValidatorScope{TSelf,T}"/>.
/// Rules added here execute only when the condition is true.
/// Call <see cref="End"/> to return to the parent scope.
/// </summary>
public sealed class NestedWhenScope<TParent, T> : ValidatorScope<NestedWhenScope<TParent, T>, T>
    where TParent : ValidatorScope<TParent, T>
{
    private readonly TParent _parent;
    private readonly Func<T, bool> _condition;

    internal NestedWhenScope(TParent parent, Func<T, bool> condition)
    {
        _parent = parent;
        _condition = condition;
    }

    /// <summary>
    /// Closes this conditional scope. All collected rules are wrapped with the condition
    /// and added to the parent scope.
    /// </summary>
    public TParent End()
    {
        FlushToParent();
        return _parent;
    }

    /// <summary>
    /// Opens an else branch. Rules added in the otherwise scope execute
    /// when this scope's condition is false.
    /// </summary>
    public NestedOtherwiseScope<TParent, T> Otherwise()
    {
        FlushToParent();
        return new NestedOtherwiseScope<TParent, T>(_parent, x => !_condition(x));
    }

    private void FlushToParent()
    {
        foreach (var (step, severity) in Steps)
            _parent.AddStep(new ConditionalValidationStep<T>(_condition, step), severity);
    }
}

/// <summary>
/// The else branch of a <see cref="NestedWhenScope{TParent,T}"/>.
/// Rules added here execute when the original condition is false.
/// </summary>
public sealed class NestedOtherwiseScope<TParent, T> : ValidatorScope<NestedOtherwiseScope<TParent, T>, T>
    where TParent : ValidatorScope<TParent, T>
{
    private readonly TParent _parent;
    private readonly Func<T, bool> _condition;

    internal NestedOtherwiseScope(TParent parent, Func<T, bool> condition)
    {
        _parent = parent;
        _condition = condition;
    }

    /// <summary>
    /// Closes this otherwise scope. All collected rules are wrapped with the negated
    /// condition and added to the parent scope.
    /// </summary>
    public TParent End()
    {
        foreach (var (step, severity) in Steps)
            _parent.AddStep(new ConditionalValidationStep<T>(_condition, step), severity);
        return _parent;
    }
}

/// <summary>
/// An OR scope nested inside any <see cref="ValidatorScope{TSelf,T}"/>.
/// The group passes if ANY rule inside passes.
/// </summary>
public sealed class NestedOrScope<TParent, T> : ValidatorScope<NestedOrScope<TParent, T>, T>
    where TParent : ValidatorScope<TParent, T>
{
    private readonly TParent _parent;

    internal NestedOrScope(TParent parent)
    {
        _parent = parent;
    }

    /// <summary>
    /// Closes this OR scope. All collected rules are packaged into an <see cref="OrRuleGroup{T}"/>
    /// and added to the parent scope.
    /// </summary>
    public TParent End()
    {
        _parent.AddStep(new OrRuleGroup<T>(Steps.ToArray()), ValidationSeverity.Error);
        return _parent;
    }
}

/// <summary>
/// An AND scope nested inside any <see cref="ValidatorScope{TSelf,T}"/>.
/// All rules must pass (explicit grouping for readability).
/// </summary>
public sealed class NestedAndScope<TParent, T> : ValidatorScope<NestedAndScope<TParent, T>, T>
    where TParent : ValidatorScope<TParent, T>
{
    private readonly TParent _parent;

    internal NestedAndScope(TParent parent)
    {
        _parent = parent;
    }

    /// <summary>
    /// Closes this AND scope. All collected rules are packaged into an <see cref="AndRuleGroup{T}"/>
    /// and added to the parent scope.
    /// </summary>
    public TParent End()
    {
        _parent.AddStep(new AndRuleGroup<T>(Steps.ToArray()), ValidationSeverity.Error);
        return _parent;
    }
}
