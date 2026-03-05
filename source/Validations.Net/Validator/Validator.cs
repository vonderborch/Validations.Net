using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net.ValidationSets;

namespace Validations.Net;

/// <summary>
/// Static entry point for creating fluent validators.
/// </summary>
public static class Validator
{
    /// <summary>
    /// Creates a new mutable validator for the specified type.
    /// </summary>
    public static Validator<T> Create<T>() => new();
}

/// <summary>
/// A mutable, fluent validator that collects rules and can directly validate values.
/// Rules are added via <see cref="ValidatorScope{TSelf,T}.For{TMember}"/>,
/// <see cref="When"/>, <see cref="Or"/>, and <see cref="And"/> scopes.
/// Internally delegates execution to an immutable <see cref="ValidationSet{T}"/> that is lazily built and cached.
/// <para>
/// <b>Thread safety:</b> This type is not thread-safe during configuration. Once all rules have been
/// added, <see cref="Validate"/>, <see cref="Check"/>, and <see cref="Ensure"/> may be called concurrently.
/// For a fully immutable, thread-safe object, call <see cref="Build"/> to obtain a <see cref="ValidationSet{T}"/>.
/// </para>
/// </summary>
public sealed class Validator<T> : ValidatorScope<Validator<T>, T>, IValidateWithContext<T>
{
    private ValidationSet<T>? _cachedSet;

    internal override void AddStep(IValidationStep<T> step, ValidationSeverity severity)
    {
        base.AddStep(step, severity);
        _cachedSet = null;
    }

    /// <summary>
    /// Opens a conditional scope. Rules added inside the scope only execute when <paramref name="condition"/> is true.
    /// Call <c>.End()</c> on the returned <see cref="WhenScope{T}"/> to close the scope.
    /// </summary>
    public new WhenScope<T> When(Func<T, bool> condition) => new(this, condition);

    /// <summary>
    /// Opens a conditional scope that is the inverse of <see cref="When"/>.
    /// Rules added inside the scope only execute when <paramref name="condition"/> is false.
    /// </summary>
    public new WhenScope<T> Unless(Func<T, bool> condition) => new(this, x => !condition(x));

    /// <summary>
    /// Opens an OR scope. The group passes if ANY rule inside passes.
    /// Call <c>.End()</c> on the returned <see cref="OrScope{T}"/> to close the scope.
    /// </summary>
    public new OrScope<T> Or() => new(this);

    /// <summary>
    /// Opens an AND scope. The group passes only if ALL rules inside pass (explicit grouping).
    /// Call <c>.End()</c> on the returned <see cref="AndScope{T}"/> to close the scope.
    /// </summary>
    public new AndScope<T> And() => new(this);

    /// <summary>
    /// Executes all rules and returns a composite <see cref="ValidationResult"/>.
    /// </summary>
    public ValidationResult Validate(T value, IBlackboard? blackboard = null) => GetSet().Execute(value, blackboard);

    /// <summary>
    /// Returns true if all error-severity rules pass.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Check(T value, IBlackboard? blackboard = null) => GetSet().Check(value, blackboard);

    /// <summary>
    /// Throws <see cref="ValidationException"/> if any error-severity rule fails. Returns the value otherwise.
    /// </summary>
    public T Ensure(T value, IBlackboard? blackboard = null,
        string validationFailureMessage = "Validation failed",
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        => GetSet().Ensure(value, blackboard, validationFailureMessage, parameterName);

    /// <summary>
    /// Asynchronously executes all rules and returns a composite <see cref="ValidationResult"/>.
    /// </summary>
    public Task<ValidationResult> ValidateAsync(T value, IBlackboard? blackboard = null,
        CancellationToken cancellationToken = default)
        => GetSet().ExecuteAsync(value, blackboard, cancellationToken);

    /// <summary>
    /// Asynchronously returns true if all error-severity rules pass.
    /// </summary>
    public Task<bool> CheckAsync(T value, IBlackboard? blackboard = null,
        CancellationToken cancellationToken = default)
        => GetSet().CheckAsync(value, blackboard, cancellationToken);

    /// <summary>
    /// Asynchronously throws <see cref="ValidationException"/> if any error-severity rule fails.
    /// </summary>
    public Task<T> EnsureAsync(T value, IBlackboard? blackboard = null,
        string validationFailureMessage = "Validation failed",
        CancellationToken cancellationToken = default)
        => GetSet().EnsureAsync(value, blackboard, validationFailureMessage, cancellationToken);

    /// <summary>
    /// Includes all rules from another <see cref="Validator{T}"/> as a composite step.
    /// The included validator is executed as an aggregate and its individual failures/warnings are merged.
    /// </summary>
    public Validator<T> Include(Validator<T> other)
    {
        AddStep(new AggregateDelegateValidationStep<T>(
            (value, bb) => other.Validate(value, bb)), ValidationSeverity.Error);
        return this;
    }

    /// <summary>
    /// Includes all rules from an <see cref="AbstractValidator{T}"/> as a composite step.
    /// The included validator is executed as an aggregate and its individual failures/warnings are merged.
    /// </summary>
    public Validator<T> Include(AbstractValidator<T> other)
    {
        AddStep(new AggregateDelegateValidationStep<T>(
            (value, bb) => other.Validate(value, bb)), ValidationSeverity.Error);
        return this;
    }

    /// <summary>
    /// Builds an immutable <see cref="ValidationSet{T}"/> from the current rules.
    /// </summary>
    public ValidationSet<T> Build() => GetSet();

    private ValidationSet<T> GetSet() => _cachedSet ??= new ValidationSet<T>(Steps.ToArray());

    // Explicit IValidate<T> implementations (clean, no IBlackboard)
    ValidationResult IValidate<T>.Validate(T value) => Validate(value);
    bool IValidate<T>.Check(T value) => Check(value);
    T IValidate<T>.Ensure(T value, string validationFailureMessage) => Ensure(value, validationFailureMessage: validationFailureMessage);
    Task<ValidationResult> IValidate<T>.ValidateAsync(T value, CancellationToken cancellationToken) => ValidateAsync(value, cancellationToken: cancellationToken);
    Task<bool> IValidate<T>.CheckAsync(T value, CancellationToken cancellationToken) => CheckAsync(value, cancellationToken: cancellationToken);
    Task<T> IValidate<T>.EnsureAsync(T value, string validationFailureMessage, CancellationToken cancellationToken) => EnsureAsync(value, validationFailureMessage: validationFailureMessage, cancellationToken: cancellationToken);

    // Explicit IValidateWithContext<T> implementations (Ensure has extra CallerArgumentExpression param)
    T IValidateWithContext<T>.Ensure(T value, IBlackboard? blackboard, string validationFailureMessage) => Ensure(value, blackboard, validationFailureMessage);
    Task<T> IValidateWithContext<T>.EnsureAsync(T value, IBlackboard? blackboard, string validationFailureMessage, CancellationToken cancellationToken) => EnsureAsync(value, blackboard, validationFailureMessage, cancellationToken);
}
