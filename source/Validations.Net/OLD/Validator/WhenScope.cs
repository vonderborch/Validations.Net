using Validations.Net.OLD.ValidationSets;

namespace Validations.Net.OLD.Validator;

/// <summary>
/// A conditional scope within a <see cref="Validator{T}"/>. Rules added inside this scope
/// are only executed when the condition is true. Call <see cref="End"/> to close the scope,
/// or <see cref="Otherwise"/> to open an else branch.
/// </summary>
public sealed class WhenScope<T> : ValidatorScope<WhenScope<T>, T>
{
    private readonly Validator<T> _parent;
    private readonly Func<T, bool> _condition;

    internal WhenScope(Validator<T> parent, Func<T, bool> condition)
    {
        this._parent = parent;
        this._condition = condition;
    }

    /// <summary>
    /// Closes this conditional scope. All collected rules are wrapped with the condition
    /// and added to the parent validator.
    /// </summary>
    public Validator<T> End()
    {
        FlushToParent();
        return this._parent;
    }

    /// <summary>
    /// Opens an else branch. Rules added in the <see cref="OtherwiseScope{T}"/> execute
    /// when this scope's condition is false. The When-side rules are flushed to the parent immediately.
    /// </summary>
    public OtherwiseScope<T> Otherwise()
    {
        FlushToParent();
        return new OtherwiseScope<T>(this._parent, x => !this._condition(x));
    }

    private void FlushToParent()
    {
        foreach (var (step, severity) in this.Steps)
            this._parent.AddStep(new ConditionalValidationStep<T>(this._condition, step), severity);
    }
}
