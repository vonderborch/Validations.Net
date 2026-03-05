namespace Validations.Net.OLD.Validator;

/// <summary>
/// An OR scope within a <see cref="Validator{T}"/>. The group passes if ANY rule inside passes.
/// If ALL rules fail, all failures are reported. Call <see cref="End"/> to close the scope.
/// </summary>
public sealed class OrScope<T> : ValidatorScope<OrScope<T>, T>
{
    private readonly Validator<T> _parent;

    internal OrScope(Validator<T> parent)
    {
        this._parent = parent;
    }

    /// <summary>
    /// Closes this OR scope. All collected rules are packaged into an <see cref="OrRuleGroup{T}"/>
    /// and added to the parent validator.
    /// </summary>
    public Validator<T> End()
    {
        this._parent.AddStep(new OrRuleGroup<T>(this.Steps.ToArray()), ValidationSeverity.Error);
        return this._parent;
    }
}
