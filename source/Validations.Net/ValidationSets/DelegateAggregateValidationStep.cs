using SimpleBlackboard.Net;

namespace Validations.Net.ValidationSets;

/// <summary>
/// A validation step backed by a delegate that can emit multiple failures.
/// Used by ForEach and UseValidator where the step produces a composite result.
/// </summary>
internal sealed class DelegateAggregateValidationStep<T>(Func<T, IBlackboard?, ValidationResult> validate)
    : IValidationStep<T>, IAggregateValidationStep<T>
{
    public ValidationResult Execute(T value, IBlackboard? blackboard = null) => validate(value, blackboard);
    public ValidationResult ExecuteAggregate(T value, IBlackboard? blackboard = null) => validate(value, blackboard);
}
