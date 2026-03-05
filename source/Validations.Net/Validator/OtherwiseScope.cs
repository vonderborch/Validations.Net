using Validations.Net.ValidationSets;

namespace Validations.Net;

/// <summary>
/// The else branch of a <see cref="WhenScope{T}"/>. Rules added here execute when
/// the original When/Unless condition is false. Call <see cref="End"/> to close.
/// </summary>
public sealed class OtherwiseScope<T> : ValidatorScope<OtherwiseScope<T>, T>
{
    private readonly Validator<T> _parent;
    private readonly Func<T, bool> _condition;

    internal OtherwiseScope(Validator<T> parent, Func<T, bool> condition)
    {
        _parent = parent;
        _condition = condition;
    }

    /// <summary>
    /// Closes this otherwise scope. All collected rules are wrapped with the negated
    /// condition and added to the parent validator.
    /// </summary>
    public Validator<T> End()
    {
        foreach (var (step, severity) in Steps)
            _parent.AddStep(new ConditionalValidationStep<T>(_condition, step), severity);
        return _parent;
    }
}
