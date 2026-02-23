using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net.ValidationSets;
using Validations.Net.Validators;

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
/// Rules are added via <see cref="For{TMember}"/>, <see cref="When"/>, <see cref="Or"/>, and <see cref="And"/> scopes.
/// Internally delegates execution to an immutable <see cref="ValidationSet{T}"/> that is lazily built and cached.
/// </summary>
public sealed class Validator<T>
{
    private readonly List<(IValidationStep<T> Step, ValidationSeverity Severity)> _steps = new();
    private ValidationSet<T>? _cachedSet;

    internal void AddStep(IValidationStep<T> step, ValidationSeverity severity)
    {
        _steps.Add((step, severity));
        _cachedSet = null;
    }

    /// <summary>
    /// Begins a rule chain for a member selected by <paramref name="selector"/>.
    /// When <paramref name="when"/> is provided, the rules only execute if the condition is true.
    /// When <paramref name="unless"/> is provided, the rules are skipped if the condition is true.
    /// Call <c>.End()</c> on the returned <see cref="MemberRule{TParent,T,TMember}"/> to return to this validator.
    /// </summary>
    public MemberRule<Validator<T>, T, TMember> For<TMember>(
        Func<T, TMember> selector,
        Func<T, bool>? when = null,
        Func<T, bool>? unless = null,
        [CallerArgumentExpression(nameof(selector))] string? selectorExpression = null)
    {
        var memberPath = ValidationSetBuilder.ExtractMemberPath(selectorExpression);
        var addStep = ConditionalAddStep(when, unless);
        return new MemberRule<Validator<T>, T, TMember>(this, addStep, selector, memberPath);
    }

    /// <summary>
    /// Begins a rule chain that validates each element in a collection.
    /// When <paramref name="when"/> is provided, the rules only execute if the condition is true.
    /// When <paramref name="unless"/> is provided, the rules are skipped if the condition is true.
    /// </summary>
    public MemberRule<Validator<T>, T, TMember> ForEach<TItem, TMember>(
        Func<T, IEnumerable<TItem>> collectionSelector,
        Func<TItem, TMember> memberSelector,
        Func<T, bool>? when = null,
        Func<T, bool>? unless = null,
        [CallerArgumentExpression(nameof(collectionSelector))] string? collectionExpression = null,
        [CallerArgumentExpression(nameof(memberSelector))] string? memberExpression = null)
    {
        var collectionPath = ValidationSetBuilder.ExtractMemberPath(collectionExpression);
        var memberPath = ValidationSetBuilder.ExtractMemberPath(memberExpression);
        var addStep = ConditionalAddStep(when, unless);

        Func<T, IEnumerable<(TMember Member, string Path)>> extractor = root =>
            collectionSelector(root).Select((item, index) =>
                (memberSelector(item), $"{collectionPath}[{index}].{memberPath}"));

        return new MemberRule<Validator<T>, T, TMember>(this, addStep, extractor, $"{collectionPath}[].{memberPath}");
    }

    /// <summary>
    /// Begins a rule chain that validates each element in a collection directly.
    /// When <paramref name="when"/> is provided, the rules only execute if the condition is true.
    /// When <paramref name="unless"/> is provided, the rules are skipped if the condition is true.
    /// </summary>
    public MemberRule<Validator<T>, T, TItem> ForEach<TItem>(
        Func<T, IEnumerable<TItem>> collectionSelector,
        Func<T, bool>? when = null,
        Func<T, bool>? unless = null,
        [CallerArgumentExpression(nameof(collectionSelector))] string? collectionExpression = null)
    {
        var collectionPath = ValidationSetBuilder.ExtractMemberPath(collectionExpression);
        var addStep = ConditionalAddStep(when, unless);

        Func<T, IEnumerable<(TItem Member, string Path)>> extractor = root =>
            collectionSelector(root).Select((item, index) =>
                (item, $"{collectionPath}[{index}]"));

        return new MemberRule<Validator<T>, T, TItem>(this, addStep, extractor, $"{collectionPath}[]");
    }

    /// <summary>
    /// Opens a conditional scope. Rules added inside the scope only execute when <paramref name="condition"/> is true.
    /// Call <c>.End()</c> on the returned <see cref="WhenScope{T}"/> to close the scope.
    /// </summary>
    public WhenScope<T> When(Func<T, bool> condition) => new(this, condition);

    /// <summary>
    /// Opens a conditional scope that is the inverse of <see cref="When"/>.
    /// Rules added inside the scope only execute when <paramref name="condition"/> is false.
    /// </summary>
    public WhenScope<T> Unless(Func<T, bool> condition) => new(this, x => !condition(x));

    /// <summary>
    /// Opens an OR scope. The group passes if ANY rule inside passes.
    /// Call <c>.End()</c> on the returned <see cref="OrScope{T}"/> to close the scope.
    /// </summary>
    public OrScope<T> Or() => new(this);

    /// <summary>
    /// Opens an AND scope. The group passes only if ALL rules inside pass (explicit grouping).
    /// Call <c>.End()</c> on the returned <see cref="AndScope{T}"/> to close the scope.
    /// </summary>
    public AndScope<T> And() => new(this);

    /// <summary>
    /// Adds a root-level custom predicate that receives the full object being validated.
    /// Use for cross-member validation (e.g., "start date must be before end date").
    /// </summary>
    public Validator<T> Must(Func<T, bool> predicate, string failureMessage,
        ValidationSeverity severity = ValidationSeverity.Error)
    {
        AddStep(new DelegateValidationStep<T>((value, bb) =>
        {
            if (predicate(value))
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure("Must", failureMessage, null, bb,
                [("value", value)]);
        }), severity);
        return this;
    }

    /// <summary>
    /// Adds a root-level async predicate that receives the full object being validated.
    /// Use for cross-member async validation (e.g., database checks spanning multiple members).
    /// The resulting step requires async execution (<c>ValidateAsync</c> / <c>CheckAsync</c> / <c>EnsureAsync</c>).
    /// </summary>
    public Validator<T> MustAsync(Func<T, CancellationToken, Task<bool>> predicate,
        string failureMessage, ValidationSeverity severity = ValidationSeverity.Error)
    {
        AddStep(new AsyncDelegateValidationStep<T>(async (value, bb, ct) =>
        {
            if (await predicate(value, ct).ConfigureAwait(false))
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure("MustAsync", failureMessage, null, bb,
                [("value", value)]);
        }), severity);
        return this;
    }

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
    /// Builds an immutable <see cref="ValidationSet{T}"/> from the current rules.
    /// </summary>
    public ValidationSet<T> Build() => GetSet();

    private ValidationSet<T> GetSet() => _cachedSet ??= new ValidationSet<T>(_steps.ToArray());

    internal Action<IValidationStep<T>, ValidationSeverity> ConditionalAddStep(
        Func<T, bool>? when, Func<T, bool>? unless = null)
    {
        Func<T, bool>? condition = (when, unless) switch
        {
            (not null, not null) => x => when(x) && !unless(x),
            (not null, null) => when,
            (null, not null) => x => !unless(x),
            _ => null
        };
        if (condition is null) return AddStep;
        return (step, severity) => AddStep(new ConditionalValidationStep<T>(condition, step), severity);
    }
}
