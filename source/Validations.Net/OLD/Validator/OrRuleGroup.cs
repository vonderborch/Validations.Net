using SimpleBlackboard.Net;
using Validations.Net.OLD.ValidationSets;

namespace Validations.Net.OLD.Validator;

/// <summary>
/// An internal validation step that groups rules with OR semantics.
/// The group passes if ANY contained rule passes. If ALL fail, all failures are aggregated.
/// </summary>
internal sealed class OrRuleGroup<T> : IValidationStep<T>, IAggregateValidationStep<T>
{
    private readonly (IValidationStep<T> Step, ValidationSeverity Severity)[] _steps;

    internal OrRuleGroup((IValidationStep<T> Step, ValidationSeverity Severity)[] steps)
    {
        this._steps = steps;
    }

    public ValidationResult Execute(T value, IBlackboard? blackboard = null)
        => ExecuteAggregate(value, blackboard);

    public ValidationResult ExecuteAggregate(T value, IBlackboard? blackboard = null)
    {
        if (this._steps.Length == 0)
            return ValidationResult.CreateFromValidationSuccess();

        var failures = new List<(ValidationResult Result, ValidationSeverity Severity)>();

        for (int i = 0; i < this._steps.Length; i++)
        {
            var (step, severity) = this._steps[i];
            var result = step.Execute(value, blackboard);
            if (result.IsValid)
                return ValidationResult.CreateFromValidationSuccess();
            failures.Add((result, severity));
        }

        var builder = ValidationResult.CreateBuilder();
        foreach (var (result, severity) in failures)
        {
            var path = result.ValidationException?.ParameterName ?? string.Empty;
            if (severity == ValidationSeverity.Warning)
                builder.AddWarning(path, result);
            else
                builder.AddFailure(path, result);
        }
        return builder.Build();
    }
}
