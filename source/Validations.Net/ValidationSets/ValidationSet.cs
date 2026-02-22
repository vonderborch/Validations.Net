using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.ValidationSets;

/// <summary>
/// An immutable set of validation steps that can be executed against a value of type T.
/// All failures are collected (no short-circuit).
/// </summary>
public sealed class ValidationSet<T>
{
    private readonly (IValidationStep<T> Step, ValidationSeverity Severity)[] _steps;

    internal ValidationSet((IValidationStep<T> Step, ValidationSeverity Severity)[] steps)
    {
        _steps = steps;
    }

    /// <summary>
    /// Executes all steps and returns a composite result with all failures and warnings.
    /// </summary>
    public ValidationResult Execute(T value, IBlackboard? blackboard = null)
    {
        var builder = ValidationResult.CreateBuilder();
        for (int i = 0; i < _steps.Length; i++)
        {
            var (step, severity) = _steps[i];

            if (step is IAggregateValidationStep<T> aggregateStep)
            {
                var compositeResult = aggregateStep.ExecuteAggregate(value, blackboard);
                builder.AddFailures("", compositeResult);
                builder.AddWarnings("", compositeResult);
                continue;
            }

            var result = step.Execute(value, blackboard);
            if (!result.IsValid)
            {
                var path = GetFailurePath(result);
                if (severity == ValidationSeverity.Warning)
                    builder.AddWarning(path, result);
                else
                    builder.AddFailure(path, result);
            }
        }
        return builder.Build();
    }

    /// <summary>
    /// Returns true if all error-severity steps pass. Warning-severity failures are ignored.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Check(T value, IBlackboard? blackboard = null)
    {
        for (int i = 0; i < _steps.Length; i++)
        {
            var (step, severity) = _steps[i];

            if (step is IAggregateValidationStep<T> aggregateStep)
            {
                if (!aggregateStep.ExecuteAggregate(value, blackboard).IsValid)
                    return false;
                continue;
            }

            var result = step.Execute(value, blackboard);
            if (!result.IsValid && severity == ValidationSeverity.Error)
                return false;
        }
        return true;
    }

    /// <summary>
    /// Throws ValidationException if any error-severity step fails.
    /// </summary>
    public T Ensure(T value, IBlackboard? blackboard = null,
        string validationFailureMessage = "Validation failed",
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var compositeResult = Execute(value, blackboard);
        if (!compositeResult.IsValid)
        {
            var failures = compositeResult.Failures;
            if (failures.Count > 0)
            {
                if (failures[0].ValidationException is { } validationEx)
                    throw validationEx;
                if (failures[0].PredicateException is { } predicateEx)
                    throw predicateEx;
            }
            throw ValidationException.Create("ValidationSet", validationFailureMessage, parameterName, blackboard,
                new List<(string key, object? value)> { ("value", value), ("failureCount", failures.Count) });
        }
        return value;
    }

    private static string GetFailurePath(ValidationResult result)
    {
        return result.ValidationException?.ParameterName ?? string.Empty;
    }
}

/// <summary>
/// Static entry point for creating validation sets.
/// </summary>
public static class ValidationSet
{
    /// <summary>
    /// Creates a new builder for a validation set of type T.
    /// </summary>
    public static ValidationSetBuilder<T> For<T>() => new();
}
