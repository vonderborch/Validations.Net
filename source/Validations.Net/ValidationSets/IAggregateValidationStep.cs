using SimpleBlackboard.Net;

namespace Validations.Net.ValidationSets;

/// <summary>
/// Represents a validation step that can emit multiple failures.
/// </summary>
internal interface IAggregateValidationStep<in T>
{
    AggregateValidationResult ExecuteAggregate(T value, IBlackboard? blackboard = null);
}
