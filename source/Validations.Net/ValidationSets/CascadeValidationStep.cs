using SimpleBlackboard.Net;

namespace Validations.Net.ValidationSets;

/// <summary>
/// Wraps a sequence of validation steps and stops on the first error-severity failure.
/// Warning-severity failures are collected without stopping.
/// </summary>
internal sealed class CascadeValidationStep<T>(
    (IValidationStep<T> Step, ValidationSeverity Severity)[] steps)
    : IValidationStep<T>, IAggregateValidationStep<T>
{
    public ValidationResult Execute(T value, IBlackboard? blackboard = null)
        => ExecuteAggregate(value, blackboard);

    public ValidationResult ExecuteAggregate(T value, IBlackboard? blackboard = null)
    {
        var builder = ValidationResult.CreateBuilder();
        foreach (var (step, severity) in steps)
        {
            ValidationResult result;
            if (step is IAggregateValidationStep<T> aggregate)
            {
                result = aggregate.ExecuteAggregate(value, blackboard);
                if (!result.IsValid)
                {
                    builder.AddFailures("", result);
                    builder.AddWarnings("", result);
                    if (severity == ValidationSeverity.Error)
                        break;
                }
            }
            else
            {
                result = step.Execute(value, blackboard);
                if (!result.IsValid)
                {
                    var path = result.ValidationException?.ParameterName ?? string.Empty;
                    if (severity == ValidationSeverity.Warning)
                        builder.AddWarning(path, result);
                    else
                    {
                        builder.AddFailure(path, result);
                        break;
                    }
                }
            }
        }
        return builder.Build();
    }
}
