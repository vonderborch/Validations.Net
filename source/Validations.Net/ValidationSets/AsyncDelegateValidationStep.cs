using SimpleBlackboard.Net;

namespace Validations.Net.ValidationSets;

/// <summary>
/// A validation step backed by an async delegate. Implements both <see cref="IValidationStep{T}"/>
/// for sync callers (throws <see cref="NotSupportedException"/>) and <see cref="IAsyncValidationStep{T}"/>
/// for async callers.
/// </summary>
internal sealed class AsyncDelegateValidationStep<T>(
    Func<T, IBlackboard?, CancellationToken, Task<ValidationResult>> validateAsync)
    : IValidationStep<T>, IAsyncValidationStep<T>
{
    public ValidationResult Execute(T value, IBlackboard? blackboard = null)
        => throw new NotSupportedException(
            "This validation step requires async execution. Use ValidateAsync, CheckAsync, or EnsureAsync.");

    public Task<ValidationResult> ExecuteAsync(T value, IBlackboard? blackboard = null,
        CancellationToken cancellationToken = default)
        => validateAsync(value, blackboard, cancellationToken);
}

/// <summary>
/// An aggregate validation step backed by an async delegate. Implements both sync interfaces
/// (throws <see cref="NotSupportedException"/>) and <see cref="IAsyncValidationStep{T}"/>
/// for async callers.
/// </summary>
internal sealed class AsyncDelegateAggregateValidationStep<T>(
    Func<T, IBlackboard?, CancellationToken, Task<ValidationResult>> validateAsync)
    : IValidationStep<T>, IAggregateValidationStep<T>, IAsyncValidationStep<T>
{
    public ValidationResult Execute(T value, IBlackboard? blackboard = null)
        => throw new NotSupportedException(
            "This validation step requires async execution. Use ValidateAsync, CheckAsync, or EnsureAsync.");

    public ValidationResult ExecuteAggregate(T value, IBlackboard? blackboard = null)
        => throw new NotSupportedException(
            "This validation step requires async execution. Use ValidateAsync, CheckAsync, or EnsureAsync.");

    public Task<ValidationResult> ExecuteAsync(T value, IBlackboard? blackboard = null,
        CancellationToken cancellationToken = default)
        => validateAsync(value, blackboard, cancellationToken);
}
