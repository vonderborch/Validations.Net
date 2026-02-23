using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net.ValidationSets;

namespace Validations.Net;

/// <summary>
/// Base class for defining class-based validators with the fluent API.
/// Subclasses configure rules in their constructor. <c>.End()</c> is optional
/// in constructors since the return value is discarded (except when using <see cref="MemberRule{TParent,T,TMember}.Cascade"/>).
/// </summary>
/// <example>
/// <code>
/// public class OrderValidator : AbstractValidator&lt;Order&gt;
/// {
///     public OrderValidator()
///     {
///         For(o => o.CustomerId).NotNull().NotEmpty();
///         For(o => o.Quantity).InRange(1, 1000);
///         For(o => o.ShippingAddress, when: o => o.DeliveryMethod == "ship").NotNullOrEmpty();
///         Must(o => o.TotalCost > 0, "TotalCost must be positive");
///     }
/// }
/// </code>
/// </example>
public abstract class AbstractValidator<T>
{
    private readonly Validator<T> _validator = Validator.Create<T>();

    /// <summary>
    /// Begins a rule chain for a member.
    /// When <paramref name="when"/> is provided, the rules only execute if the condition is true.
    /// When <paramref name="unless"/> is provided, the rules are skipped if the condition is true.
    /// </summary>
    protected MemberRule<Validator<T>, T, TMember> For<TMember>(
        Func<T, TMember> selector,
        Func<T, bool>? when = null,
        Func<T, bool>? unless = null,
        [CallerArgumentExpression(nameof(selector))] string? selectorExpression = null)
        => _validator.For(selector, when, unless, selectorExpression);

    /// <summary>
    /// Begins a rule chain that validates each element in a collection.
    /// When <paramref name="when"/> is provided, the rules only execute if the condition is true.
    /// When <paramref name="unless"/> is provided, the rules are skipped if the condition is true.
    /// </summary>
    protected MemberRule<Validator<T>, T, TMember> ForEach<TItem, TMember>(
        Func<T, IEnumerable<TItem>> collectionSelector,
        Func<TItem, TMember> memberSelector,
        Func<T, bool>? when = null,
        Func<T, bool>? unless = null,
        [CallerArgumentExpression(nameof(collectionSelector))] string? collectionExpression = null,
        [CallerArgumentExpression(nameof(memberSelector))] string? memberExpression = null)
        => _validator.ForEach(collectionSelector, memberSelector, when, unless, collectionExpression, memberExpression);

    /// <summary>
    /// Begins a rule chain that validates each element in a collection directly.
    /// When <paramref name="when"/> is provided, the rules only execute if the condition is true.
    /// When <paramref name="unless"/> is provided, the rules are skipped if the condition is true.
    /// </summary>
    protected MemberRule<Validator<T>, T, TItem> ForEach<TItem>(
        Func<T, IEnumerable<TItem>> collectionSelector,
        Func<T, bool>? when = null,
        Func<T, bool>? unless = null,
        [CallerArgumentExpression(nameof(collectionSelector))] string? collectionExpression = null)
        => _validator.ForEach(collectionSelector, when, unless, collectionExpression);

    /// <summary>
    /// Opens a conditional scope.
    /// </summary>
    protected WhenScope<T> When(Func<T, bool> condition) => _validator.When(condition);

    /// <summary>
    /// Opens an inverse conditional scope. Rules execute when the condition is false.
    /// </summary>
    protected WhenScope<T> Unless(Func<T, bool> condition) => _validator.Unless(condition);

    /// <summary>
    /// Opens an OR scope.
    /// </summary>
    protected OrScope<T> Or() => _validator.Or();

    /// <summary>
    /// Opens an AND scope.
    /// </summary>
    protected AndScope<T> And() => _validator.And();

    /// <summary>
    /// Adds a root-level custom predicate that receives the full object being validated.
    /// Use for cross-member validation.
    /// </summary>
    protected Validator<T> Must(Func<T, bool> predicate, string failureMessage,
        ValidationSeverity severity = ValidationSeverity.Error)
        => _validator.Must(predicate, failureMessage, severity);

    /// <summary>
    /// Adds a root-level async predicate that receives the full object being validated.
    /// Use for cross-member async validation (e.g., database checks spanning multiple members).
    /// </summary>
    protected Validator<T> MustAsync(Func<T, CancellationToken, Task<bool>> predicate,
        string failureMessage, ValidationSeverity severity = ValidationSeverity.Error)
        => _validator.MustAsync(predicate, failureMessage, severity);

    /// <summary>
    /// Executes all rules and returns a composite <see cref="ValidationResult"/>.
    /// </summary>
    public ValidationResult Validate(T value, IBlackboard? blackboard = null)
        => _validator.Validate(value, blackboard);

    /// <summary>
    /// Returns true if all error-severity rules pass.
    /// </summary>
    public bool Check(T value, IBlackboard? blackboard = null)
        => _validator.Check(value, blackboard);

    /// <summary>
    /// Throws <see cref="ValidationException"/> if any error-severity rule fails. Returns the value otherwise.
    /// </summary>
    public T Ensure(T value, IBlackboard? blackboard = null,
        string validationFailureMessage = "Validation failed",
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        => _validator.Ensure(value, blackboard, validationFailureMessage, parameterName);

    /// <summary>
    /// Asynchronously executes all rules and returns a composite <see cref="ValidationResult"/>.
    /// </summary>
    public Task<ValidationResult> ValidateAsync(T value, IBlackboard? blackboard = null,
        CancellationToken cancellationToken = default)
        => _validator.ValidateAsync(value, blackboard, cancellationToken);

    /// <summary>
    /// Asynchronously returns true if all error-severity rules pass.
    /// </summary>
    public Task<bool> CheckAsync(T value, IBlackboard? blackboard = null,
        CancellationToken cancellationToken = default)
        => _validator.CheckAsync(value, blackboard, cancellationToken);

    /// <summary>
    /// Asynchronously throws <see cref="ValidationException"/> if any error-severity rule fails.
    /// </summary>
    public Task<T> EnsureAsync(T value, IBlackboard? blackboard = null,
        string validationFailureMessage = "Validation failed",
        CancellationToken cancellationToken = default)
        => _validator.EnsureAsync(value, blackboard, validationFailureMessage, cancellationToken);

    /// <summary>
    /// Builds an immutable <see cref="ValidationSet{T}"/> from the configured rules.
    /// </summary>
    public ValidationSet<T> Build() => _validator.Build();
}
