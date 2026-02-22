using SimpleBlackboard.Net;

namespace Validations.Net.ValidationSets;

/// <summary>
/// A validation step backed by a delegate.
/// </summary>
internal sealed class DelegateValidationStep<T>(Func<T, IBlackboard?, ValidationResult> validate) : IValidationStep<T>
{
    public ValidationResult Execute(T value, IBlackboard? blackboard = null) => validate(value, blackboard);
}
