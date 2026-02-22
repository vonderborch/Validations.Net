using SimpleBlackboard.Net;
using Validations.Net.Validation;
using Validations.Net.Validators;

namespace Validations.Net.ValidationSets;

/// <summary>
/// Fluent builder for constructing immutable ValidationSet instances.
/// </summary>
public sealed class ValidationSetBuilder<T>
{
    private readonly List<IValidationStep<T>> _steps = new();
    private Func<T, bool>? _currentCondition;

    /// <summary>
    /// Adds a custom validation step.
    /// </summary>
    public ValidationSetBuilder<T> Add(Func<T, IBlackboard?, ValidationResult> step)
    {
        AddStep(new DelegateValidationStep<T>(step));
        return this;
    }

    /// <summary>
    /// Adds a custom validation step.
    /// </summary>
    public ValidationSetBuilder<T> Add(IValidationStep<T> step)
    {
        AddStep(step);
        return this;
    }

    /// <summary>
    /// Sets a condition for subsequent steps. Steps added after When() are only
    /// executed if the predicate returns true. Call EndWhen() or When() again to reset.
    /// </summary>
    public ValidationSetBuilder<T> When(Func<T, bool> predicate)
    {
        _currentCondition = predicate;
        return this;
    }

    /// <summary>
    /// Clears the current condition so subsequent steps run unconditionally.
    /// </summary>
    public ValidationSetBuilder<T> EndWhen()
    {
        _currentCondition = null;
        return this;
    }

    // === Convenience methods for common validators ===

    /// <summary>
    /// Adds a step that validates the value is not null.
    /// </summary>
    public ValidationSetBuilder<T> AddIsNotNull(string? message = null)
    {
        Add((value, bb) =>
        {
            if (value is not null)
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure(
                IsNotNull.ValidatorName, message ?? IsNotNull.DefaultValidationFailureMessage,
                null, bb, new List<(string key, object? value)> { ("value", value) });
        });
        return this;
    }

    /// <summary>
    /// Adds a step that validates a member extracted by selector is not null.
    /// </summary>
    public ValidationSetBuilder<T> AddIsNotNull<TMember>(Func<T, TMember?> selector, string? message = null)
    {
        Add((value, bb) =>
        {
            var member = selector(value);
            if (member is not null)
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure(
                IsNotNull.ValidatorName, message ?? IsNotNull.DefaultValidationFailureMessage,
                null, bb, new List<(string key, object? value)> { ("value", member) });
        });
        return this;
    }

    /// <summary>
    /// Adds a step that validates the value is in the specified range.
    /// </summary>
    public ValidationSetBuilder<T> AddIsInRange<TValue>(Func<T, TValue> selector, TValue min, TValue max,
        bool minInclusive = true, bool maxInclusive = true, string? message = null) where TValue : IComparable<TValue>
    {
        Add((value, bb) =>
        {
            var member = selector(value);
            if (member is not null && member.CheckIsInRange(min, max, minInclusive, maxInclusive))
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure(
                IsInRange.ValidatorName, message ?? IsInRange.DefaultValidationFailureMessage,
                null, bb, new List<(string key, object? value)> { ("value", member), ("min", min), ("max", max) });
        });
        return this;
    }

    /// <summary>
    /// Adds a step that validates a string member is not null or empty.
    /// </summary>
    public ValidationSetBuilder<T> AddIsNotNullOrEmpty(Func<T, string?> selector, string? message = null)
    {
        Add((value, bb) =>
        {
            var str = selector(value);
            if (!string.IsNullOrEmpty(str))
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure(
                IsNotNullOrEmpty.ValidatorName, message ?? IsNotNullOrEmpty.DefaultValidationFailureMessage,
                null, bb, new List<(string key, object? value)> { ("value", str) });
        });
        return this;
    }

    /// <summary>
    /// Adds a step that validates a string member is not null or whitespace.
    /// </summary>
    public ValidationSetBuilder<T> AddIsNotNullOrWhiteSpace(Func<T, string?> selector, string? message = null)
    {
        Add((value, bb) =>
        {
            var str = selector(value);
            if (!string.IsNullOrWhiteSpace(str))
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure(
                IsNotNullOrWhiteSpace.ValidatorName, message ?? IsNotNullOrWhiteSpace.DefaultValidationFailureMessage,
                null, bb, new List<(string key, object? value)> { ("value", str) });
        });
        return this;
    }

    /// <summary>
    /// Adds steps from the type's ValidationAttributes (attribute-based validation).
    /// </summary>
    public ValidationSetBuilder<T> AddFromType()
    {
        AddAggregate((value, bb) =>
        {
            if (value is null)
            {
                var builder = AggregateValidationResult.CreateBuilder();
                builder.AddFailure(
                    "",
                    ValidationResult.CreateFromValidationFailure(
                        "ValidationSet",
                        "Value is null",
                        null,
                        bb,
                        new List<(string key, object? value)> { ("value", null) }));
                return builder.Build();
            }

            return ValidationRunner.Validate(value, bb);
        });
        return this;
    }

    /// <summary>
    /// Builds the immutable ValidationSet.
    /// </summary>
    public ValidationSet<T> Build()
    {
        return new ValidationSet<T>(_steps.ToArray());
    }

    private void AddStep(IValidationStep<T> step)
    {
        if (_currentCondition is not null)
            _steps.Add(new ConditionalValidationStep<T>(_currentCondition, step));
        else
            _steps.Add(step);
    }

    private void AddAggregate(Func<T, IBlackboard?, AggregateValidationResult> step)
    {
        var aggregateStep = new AggregateDelegateValidationStep<T>(step);
        if (_currentCondition is not null)
        {
            var capturedCondition = _currentCondition;
            _steps.Add(new AggregateDelegateValidationStep<T>((value, blackboard) =>
                !capturedCondition(value)
                    ? AggregateValidationResult.Success
                    : aggregateStep.ExecuteAggregate(value, blackboard)));
            return;
        }

        _steps.Add(aggregateStep);
    }
}
