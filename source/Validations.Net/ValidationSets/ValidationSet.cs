using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.ValidationSets;

/// <summary>
/// An immutable set of validation steps that can be executed against a value of type T.
/// All failures are collected (no short-circuit).
/// </summary>
public sealed class ValidationSet<T>
{
    private readonly IValidationStep<T>[] _steps;

    internal ValidationSet(IValidationStep<T>[] steps)
    {
        _steps = steps;
    }

    /// <summary>
    /// Executes all steps and returns an aggregate result with all failures.
    /// </summary>
    public AggregateValidationResult Execute(T value, IBlackboard? blackboard = null)
    {
        var builder = AggregateValidationResult.CreateBuilder();
        for (int i = 0; i < _steps.Length; i++)
        {
            if (_steps[i] is IAggregateValidationStep<T> aggregateStep)
            {
                var aggregateResult = aggregateStep.ExecuteAggregate(value, blackboard);
                if (!aggregateResult.IsValid)
                    builder.AddFailures("", aggregateResult);
                continue;
            }

            var result = _steps[i].Execute(value, blackboard);
            if (!result.IsValid)
                builder.AddFailure("", result);
        }
        return builder.Build();
    }

    /// <summary>
    /// Returns true if all steps pass.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Check(T value, IBlackboard? blackboard = null)
    {
        for (int i = 0; i < _steps.Length; i++)
        {
            if (_steps[i] is IAggregateValidationStep<T> aggregateStep)
            {
                if (!aggregateStep.ExecuteAggregate(value, blackboard).IsValid)
                    return false;
                continue;
            }

            var result = _steps[i].Execute(value, blackboard);
            if (!result.IsValid) return false;
        }
        return true;
    }

    /// <summary>
    /// Throws ValidationException if any step fails.
    /// </summary>
    public T Ensure(T value, IBlackboard? blackboard = null,
        string validationFailureMessage = "Validation failed",
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var aggregateResult = Execute(value, blackboard);
        if (!aggregateResult.IsValid)
        {
            var failures = aggregateResult.Failures;
            if (failures.Count > 0)
            {
                if (failures[0].Result.ValidationException is not null)
                    throw failures[0].Result.ValidationException!;
                if (failures[0].Result.PredicateException is not null)
                    throw failures[0].Result.PredicateException!;
            }
            throw ValidationException.Create("ValidationSet", validationFailureMessage, parameterName, blackboard,
                new List<(string key, object? value)> { ("value", value), ("failureCount", failures.Count) });
        }
        return value;
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
