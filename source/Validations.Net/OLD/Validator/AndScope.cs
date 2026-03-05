namespace Validations.Net.OLD.Validator;

/// <summary>
/// An AND scope within a <see cref="Validator{T}"/>. All rules must pass (same as default behavior,
/// but provides explicit grouping for readability). Call <see cref="End"/> to close the scope.
/// </summary>
public sealed class AndScope<T> : ValidatorScope<AndScope<T>, T>
{
    private readonly Validator<T> _parent;

    internal AndScope(Validator<T> parent)
    {
        this._parent = parent;
    }

    /// <summary>
    /// Closes this AND scope. All collected rules are packaged into an <see cref="AndRuleGroup{T}"/>
    /// and added to the parent validator.
    /// </summary>
    public Validator<T> End()
    {
        this._parent.AddStep(new AndRuleGroup<T>(this.Steps.ToArray()), ValidationSeverity.Error);
        return this._parent;
    }
}
