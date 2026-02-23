using SimpleBlackboard.Net;
using Validations.Net.ValidationSets;
using Validations.Net.Validators;

// ReSharper disable ParameterHidesMember

namespace Validations.Net;

/// <summary>
/// A fluent rule chain for a specific member of <typeparamref name="T"/>.
/// Validator methods (NotNull, InRange, etc.) add rules to the parent scope.
/// Call <see cref="End"/> to return to the parent.
/// </summary>
/// <typeparam name="TParent">The parent scope type (Validator, WhenScope, OrScope, or AndScope).</typeparam>
/// <typeparam name="T">The root type being validated.</typeparam>
/// <typeparam name="TMember">The type of the selected member.</typeparam>
public sealed class MemberRule<TParent, T, TMember>
{
    private readonly TParent _parent;
    private readonly Action<IValidationStep<T>, ValidationSeverity> _parentAddStep;
    private Action<IValidationStep<T>, ValidationSeverity> _addStep;
    private readonly Func<T, TMember> _selector;
    private readonly string? _memberPath;
    private readonly Func<T, IEnumerable<(TMember Member, string Path)>>? _forEachExtractor;
    private List<(IValidationStep<T> Step, ValidationSeverity Severity)>? _cascadeSteps;

    internal MemberRule(TParent parent, Action<IValidationStep<T>, ValidationSeverity> addStep,
        Func<T, TMember> selector, string? memberPath)
    {
        _parent = parent;
        _parentAddStep = addStep;
        _addStep = addStep;
        _selector = selector;
        _memberPath = memberPath;
    }

    internal MemberRule(TParent parent, Action<IValidationStep<T>, ValidationSeverity> addStep,
        Func<T, IEnumerable<(TMember Member, string Path)>> forEachExtractor, string? memberPath)
    {
        _parent = parent;
        _parentAddStep = addStep;
        _addStep = addStep;
        _selector = _ => default!;
        _memberPath = memberPath;
        _forEachExtractor = forEachExtractor;
    }

    internal Func<T, TMember> Selector => _selector;
    internal string? MemberPath => _memberPath;

    /// <summary>
    /// Applies an arbitrary <see cref="IValidator"/> to this member.
    /// </summary>
    /// <param name="validator">The validator to apply.</param>
    /// <param name="severity">The severity of a failure.</param>
    /// <param name="message">Optional custom error message that overrides the validator's default message on failure.</param>
    public MemberRule<TParent, T, TMember> Apply(IValidator validator,
        ValidationSeverity severity = ValidationSeverity.Error, string? message = null)
    {
        if (_forEachExtractor is not null)
            return ApplyForEach(validator, severity, message);

        var selector = _selector;
        var memberPath = _memberPath;
        _addStep(new DelegateValidationStep<T>((value, bb) =>
        {
            var member = selector(value);
            var result = validator.Validate(member, memberPath, bb);
            if (message is not null && !result.IsValid)
                return ValidationResult.CreateFromValidationFailure(
                    validator.Name, message, memberPath, bb, [("value", member)]);
            return result;
        }), severity);
        return this;
    }

    private MemberRule<TParent, T, TMember> ApplyForEach(IValidator validator,
        ValidationSeverity severity, string? message)
    {
        var extractor = _forEachExtractor!;
        _addStep(new DelegateAggregateValidationStep<T>((value, bb) =>
        {
            var builder = ValidationResult.CreateBuilder();
            foreach (var (member, path) in extractor(value))
            {
                var result = validator.Validate(member, path, bb);
                if (message is not null && !result.IsValid)
                    result = ValidationResult.CreateFromValidationFailure(
                        validator.Name, message, path, bb, [("value", member)]);
                if (!result.IsValid)
                    builder.AddFailure(path, result);
            }
            return builder.Build();
        }), severity);
        return this;
    }

    /// <summary>
    /// Adds a custom predicate check for this member.
    /// </summary>
    public MemberRule<TParent, T, TMember> Must(Func<TMember, bool> predicate, string failureMessage,
        ValidationSeverity severity = ValidationSeverity.Error)
    {
        if (_forEachExtractor is not null)
            return MustForEach(predicate, failureMessage, severity);

        var selector = _selector;
        var memberPath = _memberPath;
        _addStep(new DelegateValidationStep<T>((value, bb) =>
        {
            var member = selector(value);
            if (predicate(member))
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure("Must", failureMessage, memberPath, bb,
                [("value", member)]);
        }), severity);
        return this;
    }

    private MemberRule<TParent, T, TMember> MustForEach(Func<TMember, bool> predicate,
        string failureMessage, ValidationSeverity severity)
    {
        var extractor = _forEachExtractor!;
        _addStep(new DelegateAggregateValidationStep<T>((value, bb) =>
        {
            var builder = ValidationResult.CreateBuilder();
            foreach (var (member, path) in extractor(value))
            {
                if (!predicate(member))
                {
                    var result = ValidationResult.CreateFromValidationFailure("Must", failureMessage, path, bb,
                        [("value", member)]);
                    builder.AddFailure(path, result);
                }
            }
            return builder.Build();
        }), severity);
        return this;
    }

    /// <summary>
    /// Composes an <see cref="AbstractValidator{T}"/> into this rule chain.
    /// The inner validator's results are merged with member paths prefixed by this member's path.
    /// </summary>
    public MemberRule<TParent, T, TMember> UseValidator(AbstractValidator<TMember> validator,
        ValidationSeverity severity = ValidationSeverity.Error)
        => UseValidatorCore((v, bb) => validator.Validate(v, bb), severity);

    /// <summary>
    /// Composes a <see cref="Validator{T}"/> into this rule chain.
    /// The inner validator's results are merged with member paths prefixed by this member's path.
    /// </summary>
    public MemberRule<TParent, T, TMember> UseValidator(Validator<TMember> validator,
        ValidationSeverity severity = ValidationSeverity.Error)
        => UseValidatorCore((v, bb) => validator.Validate(v, bb), severity);

    private MemberRule<TParent, T, TMember> UseValidatorCore(
        Func<TMember, IBlackboard?, ValidationResult> validateFunc,
        ValidationSeverity severity)
    {
        if (_forEachExtractor is not null)
            return UseValidatorForEachCore(validateFunc, severity);

        var selector = _selector;
        var memberPath = _memberPath ?? string.Empty;
        _addStep(new DelegateAggregateValidationStep<T>((value, bb) =>
        {
            var member = selector(value);
            var result = validateFunc(member, bb);
            if (result.IsValid) return result;
            var builder = ValidationResult.CreateBuilder();
            builder.AddFailures(memberPath, result);
            builder.AddWarnings(memberPath, result);
            return builder.Build();
        }), severity);
        return this;
    }

    private MemberRule<TParent, T, TMember> UseValidatorForEachCore(
        Func<TMember, IBlackboard?, ValidationResult> validateFunc,
        ValidationSeverity severity)
    {
        var extractor = _forEachExtractor!;
        _addStep(new DelegateAggregateValidationStep<T>((value, bb) =>
        {
            var builder = ValidationResult.CreateBuilder();
            foreach (var (member, path) in extractor(value))
            {
                var result = validateFunc(member, bb);
                if (!result.IsValid)
                {
                    builder.AddFailures(path, result);
                    builder.AddWarnings(path, result);
                }
            }
            return builder.Build();
        }), severity);
        return this;
    }

    // --- Async rule methods ---

    /// <summary>
    /// Applies an <see cref="IAsyncValidator"/> to this member. The resulting step requires
    /// async execution (<c>ValidateAsync</c> / <c>CheckAsync</c> / <c>EnsureAsync</c>).
    /// </summary>
    public MemberRule<TParent, T, TMember> ApplyAsync(IAsyncValidator asyncValidator, string name,
        ValidationSeverity severity = ValidationSeverity.Error, string? message = null)
    {
        if (_forEachExtractor is not null)
            return ApplyAsyncForEach(asyncValidator, name, severity, message);

        var selector = _selector;
        var memberPath = _memberPath;
        _addStep(new AsyncDelegateValidationStep<T>(async (value, bb, ct) =>
        {
            var member = selector(value);
            var result = await asyncValidator.ValidateAsync(member, memberPath, bb, ct)
                .ConfigureAwait(false);
            if (message is not null && !result.IsValid)
                return ValidationResult.CreateFromValidationFailure(
                    name, message, memberPath, bb, [("value", member)]);
            return result;
        }), severity);
        return this;
    }

    private MemberRule<TParent, T, TMember> ApplyAsyncForEach(IAsyncValidator asyncValidator,
        string name, ValidationSeverity severity, string? message)
    {
        var extractor = _forEachExtractor!;
        _addStep(new AsyncDelegateAggregateValidationStep<T>(async (value, bb, ct) =>
        {
            var builder = ValidationResult.CreateBuilder();
            foreach (var (member, path) in extractor(value))
            {
                var result = await asyncValidator.ValidateAsync(member, path, bb, ct)
                    .ConfigureAwait(false);
                if (message is not null && !result.IsValid)
                    result = ValidationResult.CreateFromValidationFailure(
                        name, message, path, bb, [("value", member)]);
                if (!result.IsValid)
                    builder.AddFailure(path, result);
            }
            return builder.Build();
        }), severity);
        return this;
    }

    /// <summary>
    /// Adds an async predicate check for this member. The resulting step requires
    /// async execution (<c>ValidateAsync</c> / <c>CheckAsync</c> / <c>EnsureAsync</c>).
    /// </summary>
    public MemberRule<TParent, T, TMember> MustAsync(
        Func<TMember, CancellationToken, Task<bool>> predicate, string failureMessage,
        ValidationSeverity severity = ValidationSeverity.Error)
    {
        if (_forEachExtractor is not null)
            return MustAsyncForEach(predicate, failureMessage, severity);

        var selector = _selector;
        var memberPath = _memberPath;
        _addStep(new AsyncDelegateValidationStep<T>(async (value, bb, ct) =>
        {
            var member = selector(value);
            if (await predicate(member, ct).ConfigureAwait(false))
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure("MustAsync", failureMessage, memberPath, bb,
                [("value", member)]);
        }), severity);
        return this;
    }

    private MemberRule<TParent, T, TMember> MustAsyncForEach(
        Func<TMember, CancellationToken, Task<bool>> predicate,
        string failureMessage, ValidationSeverity severity)
    {
        var extractor = _forEachExtractor!;
        _addStep(new AsyncDelegateAggregateValidationStep<T>(async (value, bb, ct) =>
        {
            var builder = ValidationResult.CreateBuilder();
            foreach (var (member, path) in extractor(value))
            {
                if (!await predicate(member, ct).ConfigureAwait(false))
                {
                    var result = ValidationResult.CreateFromValidationFailure(
                        "MustAsync", failureMessage, path, bb, [("value", member)]);
                    builder.AddFailure(path, result);
                }
            }
            return builder.Build();
        }), severity);
        return this;
    }

    /// <summary>
    /// Composes an <see cref="AbstractValidator{T}"/> into this rule chain using its async path.
    /// The inner validator's results are merged with member paths prefixed by this member's path.
    /// </summary>
    public MemberRule<TParent, T, TMember> UseValidatorAsync(AbstractValidator<TMember> validator,
        ValidationSeverity severity = ValidationSeverity.Error)
        => UseValidatorAsyncCore((v, bb, ct) => validator.ValidateAsync(v, bb, ct), severity);

    /// <summary>
    /// Composes a <see cref="Validator{T}"/> into this rule chain using its async path.
    /// The inner validator's results are merged with member paths prefixed by this member's path.
    /// </summary>
    public MemberRule<TParent, T, TMember> UseValidatorAsync(Validator<TMember> validator,
        ValidationSeverity severity = ValidationSeverity.Error)
        => UseValidatorAsyncCore((v, bb, ct) => validator.ValidateAsync(v, bb, ct), severity);

    private MemberRule<TParent, T, TMember> UseValidatorAsyncCore(
        Func<TMember, IBlackboard?, CancellationToken, Task<ValidationResult>> validateFunc,
        ValidationSeverity severity)
    {
        if (_forEachExtractor is not null)
            return UseValidatorAsyncForEachCore(validateFunc, severity);

        var selector = _selector;
        var memberPath = _memberPath ?? string.Empty;
        _addStep(new AsyncDelegateAggregateValidationStep<T>(async (value, bb, ct) =>
        {
            var member = selector(value);
            var result = await validateFunc(member, bb, ct).ConfigureAwait(false);
            if (result.IsValid) return result;
            var builder = ValidationResult.CreateBuilder();
            builder.AddFailures(memberPath, result);
            builder.AddWarnings(memberPath, result);
            return builder.Build();
        }), severity);
        return this;
    }

    private MemberRule<TParent, T, TMember> UseValidatorAsyncForEachCore(
        Func<TMember, IBlackboard?, CancellationToken, Task<ValidationResult>> validateFunc,
        ValidationSeverity severity)
    {
        var extractor = _forEachExtractor!;
        _addStep(new AsyncDelegateAggregateValidationStep<T>(async (value, bb, ct) =>
        {
            var builder = ValidationResult.CreateBuilder();
            foreach (var (member, path) in extractor(value))
            {
                var result = await validateFunc(member, bb, ct).ConfigureAwait(false);
                if (!result.IsValid)
                {
                    builder.AddFailures(path, result);
                    builder.AddWarnings(path, result);
                }
            }
            return builder.Build();
        }), severity);
        return this;
    }

    /// <summary>
    /// Enables cascade mode. When any rule in this chain fails with error severity,
    /// subsequent rules for this member are skipped. Requires <see cref="End"/> to be called.
    /// </summary>
    public MemberRule<TParent, T, TMember> Cascade()
    {
        _cascadeSteps = new();
        _addStep = (step, severity) => _cascadeSteps.Add((step, severity));
        return this;
    }

    // --- Universal validators (work on any TMember) ---

    public MemberRule<TParent, T, TMember> NotNull(ValidationSeverity severity = ValidationSeverity.Error, string? message = null)
        => Apply(NotNullValidator.Instance, severity, message);

    public MemberRule<TParent, T, TMember> IsNull(ValidationSeverity severity = ValidationSeverity.Error, string? message = null)
        => Apply(NullValidator.Instance, severity, message);

    public MemberRule<TParent, T, TMember> NotDefault(ValidationSeverity severity = ValidationSeverity.Error, string? message = null)
        => Apply(NotDefaultValidator.Instance, severity, message);

    public MemberRule<TParent, T, TMember> IsDefault(ValidationSeverity severity = ValidationSeverity.Error, string? message = null)
        => Apply(DefaultValidator.Instance, severity, message);

    public MemberRule<TParent, T, TMember> Equals(object? expected, ValidationSeverity severity = ValidationSeverity.Error, string? message = null)
        => Apply(new IsEqualsValidator(expected), severity, message);

    public MemberRule<TParent, T, TMember> NotEquals(object? expected, ValidationSeverity severity = ValidationSeverity.Error, string? message = null)
        => Apply(new NotEqualsValidator(expected), severity, message);

    public MemberRule<TParent, T, TMember> SameAs(object? other, ValidationSeverity severity = ValidationSeverity.Error, string? message = null)
        => Apply(new IsSameAsValidator(other), severity, message);

    public MemberRule<TParent, T, TMember> NotSameAs(object? other, ValidationSeverity severity = ValidationSeverity.Error, string? message = null)
        => Apply(new NotSameAsValidator(other), severity, message);

    public MemberRule<TParent, T, TMember> OneOf(ValidationSeverity severity = ValidationSeverity.Error, string? message = null, params object[] values)
        => Apply(new IsOneOfValidator(values), severity, message);

    public MemberRule<TParent, T, TMember> NotOneOf(ValidationSeverity severity = ValidationSeverity.Error, string? message = null, params object[] values)
        => Apply(new IsNotOneOfValidator(values), severity, message);

    public MemberRule<TParent, T, TMember> ElementOf(ValidationSeverity severity = ValidationSeverity.Error, string? message = null, params object?[] values)
        => Apply(new IsElementOfValidator(values), severity, message);

    public MemberRule<TParent, T, TMember> NotElementOf(ValidationSeverity severity = ValidationSeverity.Error, string? message = null, params object?[] values)
        => Apply(new IsNotElementOfValidator(values), severity, message);

    public MemberRule<TParent, T, TMember> Valid(ValidationSeverity severity = ValidationSeverity.Error, string? message = null)
        => Apply(ValidValidator.Instance, severity, message);

    public MemberRule<TParent, T, TMember> NotValid(ValidationSeverity severity = ValidationSeverity.Error, string? message = null)
        => Apply(NotValidValidator.Instance, severity, message);

    /// <summary>
    /// Closes this member rule chain and returns to the parent scope.
    /// When <see cref="Cascade"/> mode is active, the collected steps are wrapped
    /// in a stop-on-first-failure group and added to the parent scope.
    /// </summary>
    public TParent End()
    {
        if (_cascadeSteps is { Count: > 0 })
            _parentAddStep(new CascadeValidationStep<T>(_cascadeSteps.ToArray()), ValidationSeverity.Error);
        return _parent;
    }
}
