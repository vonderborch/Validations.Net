using SimpleBlackboard.Net;

namespace Validations.Net.ValidationSets;

/// <summary>
/// Wraps an inner step with a predicate. The step is skipped (returns success) when the predicate is false.
/// </summary>
internal sealed class ConditionalValidationStep<T>(Func<T, bool> predicate, IValidationStep<T> inner) : IValidationStep<T>
{
    public ValidationResult Execute(T value, IBlackboard? blackboard = null)
    {
        if (!predicate(value))
            return ValidationResult.CreateFromValidationSuccess();
        return inner.Execute(value, blackboard);
    }
}
