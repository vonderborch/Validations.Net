using SimpleBlackboard.Net;

namespace Validations.Net.ValidationSets;

/// <summary>
/// A validation step backed by a delegate that can return aggregate failures.
/// </summary>
internal sealed class AggregateDelegateValidationStep<T>(Func<T, IBlackboard?, AggregateValidationResult> executeAggregate)
    : IValidationStep<T>, IAggregateValidationStep<T>
{
    public ValidationResult Execute(T value, IBlackboard? blackboard = null)
    {
        var aggregateResult = ExecuteAggregate(value, blackboard);
        if (aggregateResult.IsValid)
            return ValidationResult.CreateFromValidationSuccess();

        if (aggregateResult.Failures.Count == 0)
            return ValidationResult.CreateFromValidationFailure("ValidationSet", "Validation failed", null, blackboard, new List<(string key, object? value)>());

        return aggregateResult.Failures[0].Result;
    }

    public AggregateValidationResult ExecuteAggregate(T value, IBlackboard? blackboard = null) =>
        executeAggregate(value, blackboard);
}
