using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net.OLD.Validation;
using Validations.Net.OLD.Validators;

// ReSharper disable ParameterHidesMember

namespace Validations.Net.OLD.ValidationSets;

/// <summary>
/// Utilities shared by the builder and its extension methods.
/// </summary>
public static class ValidationSetBuilder
{
    /// <summary>
    /// Extracts a member path from a CallerArgumentExpression selector string.
    /// e.g. "x => x.Address.City" → "Address.City"
    /// </summary>
    public static string? ExtractMemberPath(string? selectorExpression)
    {
        if (string.IsNullOrWhiteSpace(selectorExpression))
            return null;

        var arrowIndex = selectorExpression.IndexOf("=>", StringComparison.Ordinal);
        if (arrowIndex < 0)
            return null;

        var memberExpression = selectorExpression[(arrowIndex + 2)..]
            .Trim()
            .Replace("!", string.Empty, StringComparison.Ordinal)
            .Replace("?.", ".", StringComparison.Ordinal);

        var rootDotIndex = memberExpression.IndexOf('.', StringComparison.Ordinal);
        if (rootDotIndex >= 0 && rootDotIndex + 1 < memberExpression.Length)
            memberExpression = memberExpression[(rootDotIndex + 1)..].Trim();

        return string.IsNullOrWhiteSpace(memberExpression) ? null : memberExpression;
    }
}

/// <summary>
/// Fluent builder for constructing immutable ValidationSet instances.
/// </summary>
public sealed class ValidationSetBuilder<T>
{
    private readonly List<(IValidationStep<T> Step, ValidationSeverity Severity)> _steps = new();
    private Func<T, bool>? _currentCondition;

    /// <summary>
    /// Adds a custom validation step that receives a blackboard.
    /// </summary>
    public ValidationSetBuilder<T> Add(Func<T, IBlackboard?, ValidationResult> step,
        ValidationSeverity severity = ValidationSeverity.Error)
    {
        AddStep(new DelegateValidationStep<T>(step), severity);
        return this;
    }

    /// <summary>
    /// Adds a custom validation step (no blackboard needed).
    /// </summary>
    public ValidationSetBuilder<T> Add(Func<T, ValidationResult> step,
        ValidationSeverity severity = ValidationSeverity.Error)
    {
        AddStep(new DelegateValidationStep<T>((value, _) => step(value)), severity);
        return this;
    }

    /// <summary>
    /// Adds a custom validation step.
    /// </summary>
    public ValidationSetBuilder<T> Add(IValidationStep<T> step,
        ValidationSeverity severity = ValidationSeverity.Error)
    {
        AddStep(step, severity);
        return this;
    }

    /// <summary>
    /// Adds a step that fails when the check returns false.
    /// </summary>
    public ValidationSetBuilder<T> AddCheck(Func<T, bool> check, string failureMessage,
        ValidationSeverity severity = ValidationSeverity.Error,
        [CallerArgumentExpression(nameof(check))] string? checkExpression = null)
    {
        Add((value, bb) =>
        {
            if (check(value))
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure(
                "Check", failureMessage, checkExpression, bb,
                [("value", value)]);
        }, severity);
        return this;
    }

    /// <summary>
    /// Adds a step that extracts a member and validates it with the given IValidator.
    /// </summary>
    public ValidationSetBuilder<T> AddValidation<TMember>(Func<T, TMember> selector, IValidator validator,
        ValidationSeverity severity = ValidationSeverity.Error,
        [CallerArgumentExpression(nameof(selector))] string? selectorExpression = null)
    {
        var memberPath = ValidationSetBuilder.ExtractMemberPath(selectorExpression);
        Add((value, bb) =>
        {
            var member = selector(value);
            return validator.Validate(member, memberPath, bb);
        }, severity);
        return this;
    }

    /// <summary>
    /// Adds an async custom validation step that receives a blackboard and cancellation token.
    /// The resulting step requires async execution (<c>ExecuteAsync</c> / <c>CheckAsync</c> / <c>EnsureAsync</c>).
    /// </summary>
    public ValidationSetBuilder<T> AddAsync(
        Func<T, IBlackboard?, CancellationToken, Task<ValidationResult>> step,
        ValidationSeverity severity = ValidationSeverity.Error)
    {
        AddStep(new AsyncDelegateValidationStep<T>(step), severity);
        return this;
    }

    /// <summary>
    /// Adds an async step that fails when the async check returns false.
    /// The resulting step requires async execution (<c>ExecuteAsync</c> / <c>CheckAsync</c> / <c>EnsureAsync</c>).
    /// </summary>
    public ValidationSetBuilder<T> AddCheckAsync(
        Func<T, CancellationToken, Task<bool>> check, string failureMessage,
        ValidationSeverity severity = ValidationSeverity.Error,
        [CallerArgumentExpression(nameof(check))] string? checkExpression = null)
    {
        AddAsync(async (value, bb, ct) =>
        {
            if (await check(value, ct).ConfigureAwait(false))
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure(
                "CheckAsync", failureMessage, checkExpression, bb,
                [("value", value)]);
        }, severity);
        return this;
    }

    /// <summary>
    /// Adds an async step that extracts a member and validates it with the given <see cref="IAsyncValidator"/>.
    /// The resulting step requires async execution (<c>ExecuteAsync</c> / <c>CheckAsync</c> / <c>EnsureAsync</c>).
    /// </summary>
    public ValidationSetBuilder<T> AddValidationAsync<TMember>(Func<T, TMember> selector,
        IAsyncValidator asyncValidator, string name,
        ValidationSeverity severity = ValidationSeverity.Error,
        [CallerArgumentExpression(nameof(selector))] string? selectorExpression = null)
    {
        var memberPath = ValidationSetBuilder.ExtractMemberPath(selectorExpression);
        AddAsync(async (value, bb, ct) =>
        {
            var member = selector(value);
            return await asyncValidator.ValidateAsync(member, memberPath, bb, ct)
                .ConfigureAwait(false);
        }, severity);
        return this;
    }

    /// <summary>
    /// Sets a condition for subsequent steps. Steps added after When() are only
    /// executed if the predicate returns true. Call EndWhen() or When() again to reset.
    /// </summary>
    public ValidationSetBuilder<T> When(Func<T, bool> predicate)
    {
        this._currentCondition = predicate;
        return this;
    }

    /// <summary>
    /// Clears the current condition so subsequent steps run unconditionally.
    /// </summary>
    public ValidationSetBuilder<T> EndWhen()
    {
        this._currentCondition = null;
        return this;
    }

    /// <summary>
    /// Includes all steps from another <see cref="ValidationSet{T}"/> as a composite step.
    /// The included set's individual step severities are respected.
    /// </summary>
    public ValidationSetBuilder<T> AddSet(ValidationSet<T> set)
    {
        AddAggregate((value, bb) => set.Execute(value, bb));
        return this;
    }

    /// <summary>
    /// Adds steps from the type's ValidationAttributes (attribute-based validation).
    /// Individual attribute severities are respected.
    /// </summary>
    public ValidationSetBuilder<T> AddFromType()
    {
        AddAggregate((value, bb) =>
        {
            if (value is null)
            {
                var builder = ValidationResult.CreateBuilder();
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
        return new ValidationSet<T>(this._steps.ToArray());
    }

    private void AddStep(IValidationStep<T> step, ValidationSeverity severity)
    {
        if (this._currentCondition is not null)
            this._steps.Add((new ConditionalValidationStep<T>(this._currentCondition, step), severity));
        else
            this._steps.Add((step, severity));
    }

    private void AddAggregate(Func<T, IBlackboard?, ValidationResult> step)
    {
        var aggregateStep = new AggregateDelegateValidationStep<T>(step);
        if (this._currentCondition is not null)
        {
            var capturedCondition = this._currentCondition;
            this._steps.Add((new AggregateDelegateValidationStep<T>((value, blackboard) =>
                !capturedCondition(value)
                    ? ValidationResult.CreateFromValidationSuccess()
                    : aggregateStep.ExecuteAggregate(value, blackboard)), ValidationSeverity.Error));
            return;
        }

        this._steps.Add((aggregateStep, ValidationSeverity.Error));
    }
}
