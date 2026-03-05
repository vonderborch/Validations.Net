using SimpleBlackboard.Net;

namespace Validations.Net.OLD.ValidationSets;

/// <summary>
/// A validation step backed by a delegate that can return composite failures.
/// </summary>
internal sealed class AggregateDelegateValidationStep<T>(Func<T, IBlackboard?, ValidationResult> executeAggregate)
    : IValidationStep<T>, IAggregateValidationStep<T>
{
    public ValidationResult Execute(T value, IBlackboard? blackboard = null)
    {
        var compositeResult = ExecuteAggregate(value, blackboard);
        if (compositeResult.IsValid)
            return ValidationResult.CreateFromValidationSuccess();

        if (compositeResult.Failures.Count == 0)
            return ValidationResult.CreateFromValidationFailure("ValidationSet", "Validation failed", null, blackboard, new List<(string key, object? value)>());

        return compositeResult.Failures[0];
    }

    public ValidationResult ExecuteAggregate(T value, IBlackboard? blackboard = null) =>
        executeAggregate(value, blackboard);
}
