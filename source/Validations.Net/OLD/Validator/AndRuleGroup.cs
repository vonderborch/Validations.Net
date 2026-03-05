using SimpleBlackboard.Net;
using Validations.Net.OLD.ValidationSets;

namespace Validations.Net.OLD.Validator;

/// <summary>
/// An internal validation step that groups rules with AND semantics.
/// All rules must pass. Failures from all failing rules are aggregated.
/// </summary>
internal sealed class AndRuleGroup<T> : IValidationStep<T>, IAggregateValidationStep<T>
{
    private readonly (IValidationStep<T> Step, ValidationSeverity Severity)[] _steps;

    internal AndRuleGroup((IValidationStep<T> Step, ValidationSeverity Severity)[] steps)
    {
        this._steps = steps;
    }

    public ValidationResult Execute(T value, IBlackboard? blackboard = null)
        => ExecuteAggregate(value, blackboard);

    public ValidationResult ExecuteAggregate(T value, IBlackboard? blackboard = null)
    {
        if (this._steps.Length == 0)
            return ValidationResult.CreateFromValidationSuccess();

        var builder = ValidationResult.CreateBuilder();
        for (int i = 0; i < this._steps.Length; i++)
        {
            var (step, severity) = this._steps[i];
            var result = step.Execute(value, blackboard);
            if (!result.IsValid)
            {
                var path = result.ValidationException?.ParameterName ?? string.Empty;
                if (severity == ValidationSeverity.Warning)
                    builder.AddWarning(path, result);
                else
                    builder.AddFailure(path, result);
            }
        }
        return builder.Build();
    }
}
